using BackEnd.Movie.DataWriteRepository.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataWriteRepository
{
    /// <summary>
    /// This class provides the functionality to call third party API and filter the responses.
    /// </summary>
    public class MovieDataProvider : IMovieDataProvider
    {
        private readonly ILogger<MovieDataProvider> _logger;

        public MovieDataProvider(ILogger<MovieDataProvider> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Fetch movie details from third party api and filter the result.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<UpdatedMovieModel>> RetrieveMovies()
        {
            var movieDetailesEntities = new List<UpdatedMovieModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    var result = new List<UpdatedMovieModel>();
                    client.BaseAddress = new Uri("http://webjetapitest.azurewebsites.net/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("x-access-token", "sjd1HfkjU83ksdsm3802k");

                    var cinamaworldMovies = await RetrieveMovieDetails(client, "cinemaworld").ConfigureAwait(false);
                    var filmworlddMovies = await RetrieveMovieDetails(client, "filmworld").ConfigureAwait(false);

                    movieDetailesEntities.AddRange(cinamaworldMovies);
                    movieDetailesEntities.AddRange(filmworlddMovies);

                    return movieDetailesEntities;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "MovieDateProvider : Retrive movie details from external services.");
                return movieDetailesEntities;
            }
        }

        /// <summary>
        /// Fetch movie details for the specified movie id from the third party API and filter the result.
        /// </summary>
        /// <param name="client">HttpClient instance</param>
        /// <param name="moveType">Move detaails are availble at two different sources. 1. cinamaworld 2. filmworld</param>
        /// <returns></returns>
        private async Task<IList<UpdatedMovieModel>> RetrieveMovieDetails(HttpClient client, string moveType)
        {
            IList<UpdatedMovieModel> movieDetailesEntities = new List<UpdatedMovieModel>();
            HttpResponseMessage moviesResponse = await client.GetAsync("api/" + moveType + "/movies").ConfigureAwait(false);
            if (moviesResponse.IsSuccessStatusCode)
            {
                var data = moviesResponse.Content.ReadAsStringAsync().Result;
                var moviesDataResponse = JsonConvert.DeserializeObject<MovieDataResponse>(data);
                foreach (var movieEntity in moviesDataResponse.Movies)
                {
                    try
                    {
                        HttpResponseMessage movieDetailsResponse = client.GetAsync("api/" + moveType + "/movie/" + movieEntity?.MovieReferenceId).Result;
                        if (movieDetailsResponse.IsSuccessStatusCode)
                        {
                            var movieData = await movieDetailsResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                            var movieDetails = JsonConvert.DeserializeObject<UpdatedMovieModel>(movieData);
                            movieDetailesEntities.Add(movieDetails);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "MovieDateProvider : Retrive movie details for {id} failed for {movieType}", movieEntity?.MovieReferenceId, moveType);
                    }

                }

            }
            return movieDetailesEntities;
        }
    }
}
