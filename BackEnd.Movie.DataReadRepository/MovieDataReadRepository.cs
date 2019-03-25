using BackEnd.Movie.AzureBlob;
using BackEnd.Movie.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataReadRepository
{
    /// <summary>
    /// This class represents the functionalities to read the movie data from azure blob. 
    /// </summary>
    public class MovieDataReadRepository : IMovieDataReadRepository
    {
        private readonly IAzureBlobStorage _blobStorage;
        public MovieDataReadRepository(IAzureBlobStorage blobStorage) {
            _blobStorage = blobStorage;          
        }

        public async Task<IList<MovieEntity>> GetMovies()
        {
            var moviesBlob = await _blobStorage.DownloadAsTextAsync("movies.json").ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IList<MovieEntity>>(moviesBlob ?? "");
        }
    }
}
