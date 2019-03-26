using AutoMapper;
using BackEnd.Movie.Contracts;
using BackEnd.Movie.DataReadRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Movie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class MoviesController : ControllerBase
    {
        private readonly IMovieDataReadRepository _movieDataReadRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieDataReadRepository movieDataReadRepository, IMapper mapper, ILogger<MoviesController> logger)
        {
            _movieDataReadRepository = movieDataReadRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET api/movies
        [HttpGet]
        [ProducesResponseType(typeof(Contracts.Movie), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<ActionResult> GetMovies([FromQuery]GetMoviesRequest request)
        {
            try
            {
                var moviesEntities = await _movieDataReadRepository.GetMovies().ConfigureAwait(true);
                if (moviesEntities == null)
                {
                    _logger.LogInformation("GetMovies: No movies were found.");
                    return NotFound($"No movies were found.");
                }
                var movies = _mapper.Map<IList<MovieEntity>, IList<Contracts.Movie>>(moviesEntities);

                if (movies == null)
                {
                    _logger.LogInformation("GetMovies: No movies were found.");
                    return NotFound($"No movies were found.");
                };
                //Select movies were released within from/to released year of the movies.
                var result = movies.Where(x => x.ReleasedYear >= (request?.ReleasedFromYear ?? 0) && x.ReleasedYear <= (request?.ReleasedToYear ?? DateTime.Now.Year)).ToList();
                return Ok(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "GetMovies : GetMovies failed.");
                return new StatusCodeResult(501);
            }
        }
    }
}
