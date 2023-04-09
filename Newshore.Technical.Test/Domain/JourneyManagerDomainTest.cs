using Moq;
using Newshore.Technical.Domain.Interfaces;
using Newshore.Technical.Transverse.Dto;

namespace Newshore.Technical.Test.Domain
{
    public class JourneyManagerDomainTest
    {
        
        private Mock<IJourneyDomain>? _mockDomain;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
         

        
        [Fact]
        public async Task GetJourneysByOriginAndDestination_ExistsJourneys()
        {
            _mockDomain = new();
            string origin = "origin";
            string destination = "destination";

            List<JourneyDto>? journeys =  new() { new JourneyDto() { Id =  new Random().Next() } };
                                   
            _mockDomain.Setup(rep => rep.FindItinerariesFromOriginToDestination(origin, destination)).Returns(Task.FromResult<List<JourneyDto>?>(journeys));

            List<JourneyDto>? testResult =  await _mockDomain.Object.FindItinerariesFromOriginToDestination(origin, destination);

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public async Task GetJourneysByOriginAndDestination_NotExistsJourneys()
        {
            _mockDomain = new();
            string origin = "origin";
            string destination = "destination";

            List<JourneyDto>? journeys = null;

            _mockDomain.Setup(rep => rep.FindItinerariesFromOriginToDestination(origin, destination)).Returns(Task.FromResult(journeys));

            List<JourneyDto>? testResult = await _mockDomain.Object.FindItinerariesFromOriginToDestination(origin, destination);

            Assert.Null(testResult);
        }

        [Fact]
        public async Task GetJourneysByOriginAndDestination_GenerateException()
        {
            try
            {
                _mockDomain = new();
                string origin = "origin";
                string destination = "destination";

                _mockDomain.Setup(rep => rep.FindItinerariesFromOriginToDestination(origin, destination)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<JourneyDto>? testResult = await _mockDomain.Object.FindItinerariesFromOriginToDestination(origin, destination);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        


    }
}
