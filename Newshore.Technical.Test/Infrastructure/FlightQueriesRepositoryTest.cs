using Moq;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;

namespace Newshore.Technical.Test.Infrastructure
{
    public class FlightQueriesRepositoryTest
    {
        
        private Mock<IFlightFinder>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        

        
        [Fact]
        public void GetAllFlights_ExistsFlights()
        {
            _mockRepository = new();

            List<Flight>? flights = new() { new Flight { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetAllFlights_NotExistsFlights()
        {
            _mockRepository = new();

            List<Flight>? flights = null;

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetAllFlights_GenerateException()
        {
            try
            {
                _mockRepository = new();

                _mockRepository.Setup(rep => rep.GetAll()).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Flight>? testResult = _mockRepository.Object.GetAll().Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetFlightById_ExistsAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Flight validFlight = new() { Id = id };

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validFlight));

            Flight? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetFlightById_NotExistAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Flight? validFlight = null;

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validFlight));

            Flight? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetFlightById_GenerateException()
        {
            try
            {
                int id = new Random().Next();
                _mockRepository = new();
                Flight validFlight = new();
                _mockRepository.Setup(rep => rep.GetById(id)).Throws(new Exception(EXCEPTION_MESSAGE));

                Flight? testResult = _mockRepository.Object.GetById(id).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetFlightsByJourney_ExistsFlights()
        {
            _mockRepository = new();

            int journeyId = new Random().Next();

            List<Flight>? flights = new() { new Flight { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetListByJourney(journeyId)).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetListByJourney(journeyId).Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetFlightsByJourney_NotExistsFlights()
        {
            _mockRepository = new();

            int journeyId = new Random().Next();

            List<Flight>? flights = null;

            _mockRepository.Setup(rep => rep.GetListByJourney(journeyId)).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetListByJourney(journeyId).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetFlightsByJourney_GenerateException()
        {
            try
            {
                _mockRepository = new();

                int journeyId = new Random().Next();

                _mockRepository.Setup(rep => rep.GetListByJourney(journeyId)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Flight>? testResult = _mockRepository.Object.GetListByJourney(journeyId).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetFlightsByPlaces_ExistsFlights()
        {
            _mockRepository = new();

            string origin = "origin";
            string destination = "destination";

            List<Flight>? flights = new() { new Flight { Id = new Random().Next(), Origin = origin, Destination = destination } };

            _mockRepository.Setup(rep => rep.GetListByPlaces(origin, destination)).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetListByPlaces(origin, destination).Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetFlightsByPlaces_NotExistsFlights()
        {
            _mockRepository = new();

            string origin = "origin";
            string destination = "destination";

            List<Flight>? flights = null;

            _mockRepository.Setup(rep => rep.GetListByPlaces(origin, destination)).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetListByPlaces(origin, destination).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetFlightsByPlaces_GenerateException()
        {
            try
            {
                _mockRepository = new();

                string origin = "origin";
                string destination = "destination";

                _mockRepository.Setup(rep => rep.GetListByPlaces(origin, destination)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Flight>? testResult = _mockRepository.Object.GetListByPlaces(origin, destination).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetFlightsByTransportId_ExistsFlights()
        {
            _mockRepository = new();
            int transportId = new Random().Next();
            
            List<Flight>? flights = new() { new Flight { TransportId = transportId } };

            _mockRepository.Setup(rep => rep.GetListByTransportId(transportId)).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetListByTransportId(transportId).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetFlightsByTransportId_NotExistFlights()
        {
            _mockRepository = new();
            int transportId = new Random().Next();

            List<Flight>? flights = null;

            _mockRepository.Setup(rep => rep.GetListByTransportId(transportId)).Returns(Task.FromResult(flights));

            List<Flight>? testResult = _mockRepository.Object.GetListByTransportId(transportId).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetFlightsByTransportId_GenerateException()
        {
            try
            {
                _mockRepository = new();
                int transportId = new Random().Next();

                _mockRepository.Setup(rep => rep.GetListByTransportId(transportId)).Throws(new Exception(EXCEPTION_MESSAGE));

               List<Flight>? testResult = _mockRepository.Object.GetListByTransportId(transportId).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetExistsFlight_ExistsAccount()
        {
            _mockRepository = new();
            Flight validFlight = new();

            _mockRepository.Setup(rep => rep.GetExistsFlight(validFlight)).Returns(Task.FromResult(validFlight));

            Flight? testResult = _mockRepository.Object.GetExistsFlight(validFlight).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetExistsFlight_NotExistAccount()
        {
            _mockRepository = new();
            Flight? validFlight = null;

            _mockRepository.Setup(rep => rep.GetExistsFlight(validFlight)).Returns(Task.FromResult(validFlight));

            Flight? testResult = _mockRepository.Object.GetExistsFlight(validFlight).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetExistsFlight_GenerateException()
        {
            try
            {
                _mockRepository = new();
                Flight validFlight = new();

                _mockRepository.Setup(rep => rep.GetExistsFlight(validFlight)).Throws(new Exception(EXCEPTION_MESSAGE));

                Flight? testResult = _mockRepository.Object.GetExistsFlight(validFlight).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        
    }
}
