using BackEnd.Movie.DataWriteRepository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataWriteRepository
{
    public interface IMovieDataProvider
    {
        Task<IList<UpdatedMovieModel>> RetrieveMovies();
    }
}