using Newtonsoft.Json;

namespace BackEnd.Movie.Contracts
{
    /// <summary>
    /// This model represents the movie details that are retrieving from the data store.
    /// </summary>
    public class MovieEntity
    {
        /// <summary>
        /// An unique identifier to reference the movie. 
        /// </summary>
        public string MovieReferenceId { get; set; }

        /// <summary>
        /// The title of the movie
        /// </summary>
        public string MovieTitle { get; set; }

        /// <summary>
        /// The movie's released year.
        /// </summary>
        public string ReleasedYear { get; set; }

        /// <summary>
        /// The price of the movie.
        /// </summary>     
        public string Price { get; set; }

        /// <summary>
        /// The release date of the movie.
        /// </summary>
        public string ReleadedDate { get; set; }
    }
}