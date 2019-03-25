using BackEnd.Movie.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataReadRepository
{
    public interface IMovieDataReadRepository
    {
        Task<IList<MovieEntity>> GetMovies();
    }
}