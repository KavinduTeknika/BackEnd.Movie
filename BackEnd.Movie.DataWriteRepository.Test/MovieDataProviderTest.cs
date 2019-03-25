using BackEnd.Movie.DataWriteRepository;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace BackEnd.Movie.DataWriteRepository.Test
{
    [TestClass]
    public class MovieDataProviderTest
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            MovieDataProvider movieDataProvider = new MovieDataProvider(new Mock<ILogger<MovieDataProvider>>().Object);
            var test= await movieDataProvider.RetrieveMovies().ConfigureAwait(false);
            Assert.IsNotNull(test);
        }
    }
}
