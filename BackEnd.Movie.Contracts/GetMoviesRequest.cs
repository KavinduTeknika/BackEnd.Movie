using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Movie.Contracts
{
    /// <summary>
    /// This model represent the api/movies request with optional parameters.
    /// </summary>
    public class GetMoviesRequest
    {
        public int? ReleasedFromYear { get; set; }
        public int? ReleasedToYear { get; set; }       
    }
}
