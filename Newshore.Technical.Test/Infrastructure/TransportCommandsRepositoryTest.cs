using Moq;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Test.Infrastructure
{
    public class TransportCommandsRepositoryTest
    {
        private Mock<ITransportRepository>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";

        [Fact]
        public void Create_Success()
        {
            _mockRepository = new();
            Transport validTransport = new();
            _mockRepository.Setup(rep => rep.Create(validTransport)).Returns(Task.FromResult(validTransport));
            Transport testResult = _mockRepository.Object.Create(validTransport).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                _mockRepository = new();
                Transport validTransport = new();
                _mockRepository.Setup(rep => rep.Create(validTransport)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Create(validTransport);
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
            Transport validTransport = new();
            _mockRepository.Setup(rep => rep.Delete(validTransport)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Delete(validTransport).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Delete_Error()
        {
            try
            {
                _mockRepository = new();
                Transport validTransport = new();
                _mockRepository.Setup(rep => rep.Delete(validTransport)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Delete(validTransport);
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
            Transport validTransport = new();
            _mockRepository.Setup(rep => rep.Update(validTransport)).Returns(Task.FromResult(true));
            bool testResult = _mockRepository.Object.Update(validTransport).Result;

            Assert.True(testResult);
        }

        [Fact]
        public void Update_Error()
        {
            try
            {
                _mockRepository = new();
                Transport validTransport = new();
                _mockRepository.Setup(rep => rep.Update(validTransport)).Throws(new Exception(EXCEPTION_MESSAGE));
                _mockRepository.Object.Update(validTransport);
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
    }
}
