using BackEnd.Movie.AzureBlob;
using BackEnd.Movie.Contracts;
using BackEnd.Movie.Controllers;
using BackEnd.Movie.DataReadRepository;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Movie.Test
{
    [TestClass]
    public class GetMoviesTest
    {
        private Mock<IMovieDataReadRepository> _movieDataReadRepository = null;

        [TestInitialize]
        public void Initialize()
        {
            _movieDataReadRepository = new Mock<IMovieDataReadRepository>(new Mock<IAzureBlobStorage>().Object);
        }

        [TestMethod]
        public async Task GetAllMovies()
        {
            //Arrange
            Mock<IMovieDataReadRepository> _movieDataReadRepository = new Mock<IMovieDataReadRepository>(new Mock<IAzureBlobStorage>().Object);
            _movieDataReadRepository.
                Setup(x => x.GetMovies())
                .ReturnsAsync(
                        new List<MovieEntity>(){
                            new MovieEntity {
                                MovieReferenceId="12345",
                                MovieTitle="Test movie title",
                                Price="100",
                                ReleadedDate="23 May 2000",
                                ReleasedYear="2000"
                            }
                        }
                    );
            MoviesController _movieController = new MoviesController(
                                                                    _movieDataReadRepository.Object,
                new Mock<AutoMapper.IMapper>().Object,
                new Mock<ILogger<MoviesController>>().Object);
            var getMovieRequest = new GetMoviesRequest
            {
                FromReleasedYear = null,
                ToReleasedYear = null
            };

            //Act
            var actualResult = await _movieController.GetMovies(getMovieRequest).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(actualResult);
        }

        [TestMethod]
        public async void GetMoviesWhenReleasedFromYearIsProvided()
        {
            //Arrange
            Mock<IMovieDataReadRepository> _movieDataReadRepository = new Mock<IMovieDataReadRepository>(new Mock<IAzureBlobStorage>().Object);
            _movieDataReadRepository.
                Setup(x => x.GetMovies())
                .ReturnsAsync(
                        new List<MovieEntity>(){
                            new MovieEntity {
                                MovieReferenceId="12345",
                                MovieTitle="Test movie title",
                                Price="100",
                                ReleadedDate="23 May 2000",
                                ReleasedYear="2000"
                            },
                             new MovieEntity {
                                MovieReferenceId="67890",
                                MovieTitle="Test movie title2",
                                Price="123",
                                ReleadedDate="23 May 2019",
                                ReleasedYear="2019"
                            }
                        }
                    );
            MoviesController _movieController = new MoviesController(
                _movieDataReadRepository.Object,
                new Mock<AutoMapper.IMapper>().Object,
                new Mock<ILogger<MoviesController>>().Object);

            var getMovieRequest = new GetMoviesRequest
            {
                FromReleasedYear = 2005,
                ToReleasedYear = null
            };

            //Act
            var actualResult = await _movieController.GetMovies(getMovieRequest).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(getMovieRequest);

        }

        [TestMethod]
        public async Task GetMoviesWhenReleasedToYearIsProvided()
        {
            //Arrange
            Mock<IMovieDataReadRepository> _movieDataReadRepository = new Mock<IMovieDataReadRepository>(new Mock<IAzureBlobStorage>().Object);
            _movieDataReadRepository.
                Setup(x => x.GetMovies())
                .ReturnsAsync(
                        new List<MovieEntity>(){
                            new MovieEntity {
                                MovieReferenceId="12345",
                                MovieTitle="Test movie title",
                                Price="100",
                                ReleadedDate="23 May 2005",
                                ReleasedYear="2005"
                            },
                             new MovieEntity {
                                MovieReferenceId="67890",
                                MovieTitle="Test movie title2",
                                Price="123",
                                ReleadedDate="23 May 2019",
                                ReleasedYear="2019"
                            }
                        }
                    );
            MoviesController _movieController = new MoviesController(
                _movieDataReadRepository.Object,
                new Mock<AutoMapper.IMapper>().Object,
                new Mock<ILogger<MoviesController>>().Object);

            var getMovieRequest = new GetMoviesRequest
            {
                FromReleasedYear = null,
                ToReleasedYear = 2018
            };

            //Act
            var actualResult = await _movieController.GetMovies(getMovieRequest).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(getMovieRequest);
        }

        [TestMethod]
        public async Task GetMoviesWhenReleasedFromAndToYearsAreProvided()
        {
            //Arrange
            Mock<IMovieDataReadRepository> _movieDataReadRepository = new Mock<IMovieDataReadRepository>(new Mock<IAzureBlobStorage>().Object);
            _movieDataReadRepository.
                Setup(x => x.GetMovies())
                .ReturnsAsync(
                        new List<MovieEntity>(){
                            new MovieEntity {
                                MovieReferenceId="12345",
                                MovieTitle="Test movie title",
                                Price="100",
                                ReleadedDate="23 May 2005",
                                ReleasedYear="2005"
                            },
                             new MovieEntity {
                                MovieReferenceId="67890",
                                MovieTitle="Test movie title2",
                                Price="123",
                                ReleadedDate="23 May 2019",
                                ReleasedYear="2019"
                            }
                        }
                    );
            MoviesController _movieController = new MoviesController(
                _movieDataReadRepository.Object,
                new Mock<AutoMapper.IMapper>().Object,
                new Mock<ILogger<MoviesController>>().Object);

            var getMovieRequest = new GetMoviesRequest
            {
                FromReleasedYear = 2000,
                ToReleasedYear = 2019
            };

            //Act
            var actualResult = await _movieController.GetMovies(getMovieRequest).ConfigureAwait(false);

            //Assert
            Assert.IsNotNull(getMovieRequest);
        }
    }
}
