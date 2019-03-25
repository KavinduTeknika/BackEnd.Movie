using BackEnd.Movie.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataWriteRepository
{
    public class MovieDataUpdateService : BackgroundService
    {
        private readonly IMovieDataProvider _movieDataProvider;
        private readonly IMovieDataWriteRepository _movieDataWriteRepository;

        public MovieDataUpdateService(IMovieDataProvider movieDataProvider, IMovieDataWriteRepository movieDataWriteRepository)
        {
            _movieDataProvider = movieDataProvider;
            _movieDataWriteRepository = movieDataWriteRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //Retrieve movie details from third party api.
                var movies = await _movieDataProvider.RetrieveMovies().ConfigureAwait(false);

                //TODO : Implement a retry machanism to retrive movie data from the third party data provider.
                //Retry machanism should decided on the maximum number of retry attempts and increase the time interval in between each and 
                // every attemppt exponentially.

                //Update the data storage with the filtered results.
                if (movies != null)
                {
                    var movieEntities = new List<MovieEntity>();
                    foreach (var movie in movies)
                    {
                        movieEntities.Add(new MovieEntity
                        {
                            MovieReferenceId = movie.MovieReferenceId,
                            MovieTitle = movie.MovieTitle,
                            Price = movie.Price,
                            ReleadedDate = movie.ReleadedDate,
                            ReleasedYear = movie.ReleasedYear
                        });
                    }
                    await _movieDataWriteRepository.UpdateMoviesInBlob(movieEntities).ConfigureAwait(false);
                }

                // Set up a waiting time period before the next data fetch from the third party api.                
                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

    }
}
