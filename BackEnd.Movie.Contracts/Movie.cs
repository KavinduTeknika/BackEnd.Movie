namespace BackEnd.Movie.Contracts
{   
        /// <summary>
        /// This model represents the api/movies response.
        /// </summary>
        public class Movie
        {
            /// <summary>
            /// The title of the movie.
            /// </summary>
            public string MovieTitle { get; set; }

            /// <summary>
            /// The released year of the movie.
            /// </summary>
            public int ReleasedYear { get; set; }

            /// <summary>
            /// The proce of the movie.
            /// </summary>
            public decimal Price { get; set; }
        }
  
}
