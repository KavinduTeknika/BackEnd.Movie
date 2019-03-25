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
        public int? FromReleasedYear { get; set; }
        public int? ToReleasedYear { get; set; }       
    }
}
