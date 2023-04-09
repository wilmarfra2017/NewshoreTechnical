using Moq;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Test.Infrastructure
{
    public class TransportQueriesRepositoryTest
    {
        
        private Mock<ITransportFinder>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        

        
        [Fact]
        public void GetAllTransports_ExistsTransports()
        {
            _mockRepository = new();

            List<Transport>? transports = new() { new Transport { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(transports));

            List<Transport>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetAllTransports_NotExistsTransports()
        {
            _mockRepository = new();

            List<Transport>? transports = null;

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(transports));

            List<Transport>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetAllTransports_GenerateException()
        {
            try
            {
                _mockRepository = new();

                _mockRepository.Setup(rep => rep.GetAll()).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Transport>? testResult = _mockRepository.Object.GetAll().Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetTransportById_ExistsAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Transport validTransport = new() { Id = id };

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validTransport));

            Transport? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetTransportById_NotExistAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Transport? validTransport = null;

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validTransport));

            Transport? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetTransportById_GenerateException()
        {
            try
            {
                int id = new Random().Next();
                _mockRepository = new();
                Transport validTransport = new();
                _mockRepository.Setup(rep => rep.GetById(id)).Throws(new Exception(EXCEPTION_MESSAGE));

                Transport? testResult = _mockRepository.Object.GetById(id).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetTransportsByFlight_ExistsTransports()
        {
            _mockRepository = new();

            int transportId = new Random().Next();

            List<Transport>? transports = new() { new Transport { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetListByFlightId(transportId)).Returns(Task.FromResult(transports));

            List<Transport>? testResult = _mockRepository.Object.GetListByFlightId(transportId).Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetTransportsByFlight_NotExistsTransports()
        {
            _mockRepository = new();

            int transportId = new Random().Next();

            List<Transport>? transports = null;

            _mockRepository.Setup(rep => rep.GetListByFlightId(transportId)).Returns(Task.FromResult(transports));

            List<Transport>? testResult = _mockRepository.Object.GetListByFlightId(transportId).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetTransportsByFlight_GenerateException()
        {
            try
            {
                _mockRepository = new();

                int transportId = new Random().Next();

                _mockRepository.Setup(rep => rep.GetListByFlightId(transportId)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Transport>? testResult = _mockRepository.Object.GetListByFlightId(transportId).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetExistsTransport_ExistsAccount()
        {
            _mockRepository = new();
            Transport validTransport = new();

            _mockRepository.Setup(rep => rep.GetExistsTransport(validTransport)).Returns(Task.FromResult(validTransport));

            Transport? testResult = _mockRepository.Object.GetExistsTransport(validTransport).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetExistsTransport_NotExistAccount()
        {
            _mockRepository = new();
            Transport? validTransport = null;

            _mockRepository.Setup(rep => rep.GetExistsTransport(validTransport)).Returns(Task.FromResult(validTransport));

            Transport? testResult = _mockRepository.Object.GetExistsTransport(validTransport).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetExistsTransport_GenerateException()
        {
            try
            {
                _mockRepository = new();
                Transport validTransport = new();

                _mockRepository.Setup(rep => rep.GetExistsTransport(validTransport)).Throws(new Exception(EXCEPTION_MESSAGE));

                Transport? testResult = _mockRepository.Object.GetExistsTransport(validTransport).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        
    }
}
