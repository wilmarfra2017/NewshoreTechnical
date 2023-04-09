using Moq;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Test.Infrastructure
{
    public class JourneyCommandsRepositoryTest
    {
        
        private Mock<IJourneyRepository>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        

        
        [Fact]
        public void Create_Success()
        {
            _mockRepository = new();
            Journey validJourney = new();
            _mockRepository.Setup(rep => rep.Create(validJourney)).Returns(Task.FromResult(validJourney));
            Journey testResult = _mockRepository.Object.Create(validJourney).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                _mockRepository = new();
                Journey validJourney = new();
                _mockRepository.Setup(rep => rep.Create(validJourney)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Create(validJourney);
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
            Journey validJourney = new();
            _mockRepository.Setup(rep => rep.Delete(validJourney)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Delete(validJourney).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Delete_Error()
        {
            try
            {
                _mockRepository = new();
                Journey validJourney = new();
                _mockRepository.Setup(rep => rep.Delete(validJourney)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Delete(validJourney);
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
            Journey validJourney = new();
            _mockRepository.Setup(rep => rep.Update(validJourney)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Update(validJourney).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Update_Error()
        {
            try
            {
                _mockRepository = new();
                Journey validJourney = new();
                _mockRepository.Setup(rep => rep.Update(validJourney)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Update(validJourney);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        
    }
}
