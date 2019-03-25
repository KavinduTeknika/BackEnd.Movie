using BackEnd.Movie.AzureBlob;
using BackEnd.Movie.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataWriteRepository
{
    /// <summary>
    /// This class provides the functionality to update the movie data in a persistent data storage.
    /// </summary>
    public class MovieDataWriteRepository : IMovieDataWriteRepository
    {
        private readonly IAzureBlobStorage _blobStorage;
        public MovieDataWriteRepository(IAzureBlobStorage blobStorage) {
            _blobStorage = blobStorage;          
        }
        /// <summary>
        /// Write the movie data to the azure blob storage.
        /// </summary>
        /// <param name="movieEntities">a list of movie entities to be written to the storage</param>
        /// <returns></returns>
        public async Task UpdateMoviesInBlob(IList<MovieEntity> movieEntities)
        {
            var jsonOutput = JsonConvert.SerializeObject(movieEntities, Formatting.Indented);
            await _blobStorage.UploadTextAsync("movies.json", jsonOutput).ConfigureAwait(false);            
        }
    }
}
