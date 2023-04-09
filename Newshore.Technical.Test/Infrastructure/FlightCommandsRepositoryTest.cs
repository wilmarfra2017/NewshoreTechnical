using Moq;
using Newshore.Technical.Domain.Aggregates.Entities;
using Newshore.Technical.Domain.Aggregates.Interfaces;

namespace Newshore.Technical.Test.Infrastructure
{
    public class FlightCommandsRepositoryTest
    {
        private Mock<IFlightRepository>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";

        [Fact]
        public void Create_Success()
        {
            _mockRepository = new();
            Flight validFlight = new();
            _mockRepository.Setup(rep => rep.Create(validFlight)).Returns(Task.FromResult(validFlight));
            Flight testResult = _mockRepository.Object.Create(validFlight).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                _mockRepository = new();
                Flight validFlight = new();
                _mockRepository.Setup(rep => rep.Create(validFlight)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Create(validFlight);
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
            Flight validFlight = new();
            _mockRepository.Setup(rep => rep.Delete(validFlight)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Delete(validFlight).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Delete_Error()
        {
            try
            {
                _mockRepository = new();
                Flight validFlight = new();
                _mockRepository.Setup(rep => rep.Delete(validFlight)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Delete(validFlight);
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
            Flight validFlight = new();
            _mockRepository.Setup(rep => rep.Update(validFlight)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Update(validFlight).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Update_Error()
        {
            try
            {
                _mockRepository = new();
                Flight validFlight = new();
                _mockRepository.Setup(rep => rep.Update(validFlight)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Update(validFlight);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
    }
}
