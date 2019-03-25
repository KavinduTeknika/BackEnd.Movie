using AutoMapper;
using BackEnd.Movie.Contracts;
using BackEnd.Movie.DataWriteRepository.Models;

namespace BackEnd.Movie
{
    public class MovieMapperProfile : Profile
    {
        /// <summary>
        /// Creates and configures the mapper profile.
        /// </summary>
        public MovieMapperProfile()
        {
            CreateMap<MovieEntity, Contracts.Movie>();
            CreateMap<UpdatedMovieModel, MovieEntity>();
        }
    }
    }
