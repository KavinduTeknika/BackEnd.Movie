using System.Collections.Generic;

namespace BackEnd.Movie.DataWriteRepository.Models
{
    /// <summary>
    /// This model represents the thrid party API response.
    /// </summary>
     public class MovieDataResponse
     {
        public IList<UpdatedMovieModel> Movies { get; set; }
     }
    
}
