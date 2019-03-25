using BackEnd.Movie.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataWriteRepository
{
    public interface IMovieDataWriteRepository
    {
        Task UpdateMoviesInBlob(IList<MovieEntity> movieEntities);
    }
}