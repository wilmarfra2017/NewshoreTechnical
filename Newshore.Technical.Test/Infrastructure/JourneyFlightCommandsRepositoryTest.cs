using Moq;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Test.Infrastructure
{
    public class JourneyFlightCommandsRepositoryTest
    {
        private Mock<IJourneyFlightRepository>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";

        [Fact]
        public void Create_Success()
        {
            _mockRepository = new();
            JourneyFlight validJourneyFlight = new();
            _mockRepository.Setup(rep => rep.Create(validJourneyFlight)).Returns(Task.FromResult(validJourneyFlight));
            JourneyFlight testResult = _mockRepository.Object.Create(validJourneyFlight).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                _mockRepository = new();
                JourneyFlight validJourneyFlight = new();
                _mockRepository.Setup(rep => rep.Create(validJourneyFlight)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Create(validJourneyFlight);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }

        [Fact]
        public void Delete_Success()
        {
            _mockRepository = new();
            JourneyFlight validJourneyFlight = new();
            _mockRepository.Setup(rep => rep.Delete(validJourneyFlight)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Delete(validJourneyFlight).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Delete_Error()
        {
            try
            {
                _mockRepository = new();
                JourneyFlight validJourneyFlight = new();
                _mockRepository.Setup(rep => rep.Delete(validJourneyFlight)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Delete(validJourneyFlight);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }

        [Fact]
        public void Update_Success()
        {
            _mockRepository = new();
            JourneyFlight validJourneyFlight = new();
            _mockRepository.Setup(rep => rep.Update(validJourneyFlight)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Update(validJourneyFlight).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Update_Error()
        {
            try
            {
                _mockRepository = new();
                JourneyFlight validJourneyFlight = new();
                _mockRepository.Setup(rep => rep.Update(validJourneyFlight)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Update(validJourneyFlight);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
    }
}
