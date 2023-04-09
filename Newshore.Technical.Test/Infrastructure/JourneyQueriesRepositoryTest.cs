using Moq;
using Newshore.Technical.Infrastructure.Interfaces;
using Newshore.Technical.Domain.Aggregates.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newshore.Technical.Test.Infrastructure
{
    public class JourneyQueriesRepositoryTest
    {
        
        private Mock<IJourneyFinder>? _mockRepository;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        

        
        [Fact]
        public void GetAllJourneys_ExistsJourneys()
        {
            _mockRepository = new();

            List<Journey>? journeys = new() { new Journey { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(journeys));

            List<Journey>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetAllJourneys_NotExistsJourneys()
        {
            _mockRepository = new();

            List<Journey>? journeys = null;

            _mockRepository.Setup(rep => rep.GetAll()).Returns(Task.FromResult(journeys));

            List<Journey>? testResult = _mockRepository.Object.GetAll().Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetAllJourneys_GenerateException()
        {
            try
            {
                _mockRepository = new();

                _mockRepository.Setup(rep => rep.GetAll()).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Journey>? testResult = _mockRepository.Object.GetAll().Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetJourneyById_ExistsAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Journey validJourney = new() { Id = id };

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validJourney));

            Journey? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void GetJourneyById_NotExistAccount()
        {
            int id = new Random().Next();
            _mockRepository = new();
            Journey? validJourney = null;

            _mockRepository.Setup(rep => rep.GetById(id)).Returns(Task.FromResult(validJourney));

            Journey? testResult = _mockRepository.Object.GetById(id).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetJourneyById_GenerateException()
        {
            try
            {
                int id = new Random().Next();
                _mockRepository = new();
                Journey validJourney = new();
                _mockRepository.Setup(rep => rep.GetById(id)).Throws(new Exception(EXCEPTION_MESSAGE));

                Journey? testResult = _mockRepository.Object.GetById(id).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetJourneysByFlight_ExistsJourneys()
        {
            _mockRepository = new();

            int flightId = new Random().Next();

            List<Journey>? journeys = new() { new Journey { Id = new Random().Next() } };

            _mockRepository.Setup(rep => rep.GetListByFlightId(flightId)).Returns(Task.FromResult(journeys));

            List<Journey>? testResult = _mockRepository.Object.GetListByFlightId(flightId).Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetJourneysByFlight_NotExistsJourneys()
        {
            _mockRepository = new();

            int flightId = new Random().Next();

            List<Journey>? journeys = null;

            _mockRepository.Setup(rep => rep.GetListByFlightId(flightId)).Returns(Task.FromResult(journeys));

            List<Journey>? testResult = _mockRepository.Object.GetListByFlightId(flightId).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetJourneysByFlight_GenerateException()
        {
            try
            {
                _mockRepository = new();

                int flightId = new Random().Next();

                _mockRepository.Setup(rep => rep.GetListByFlightId(flightId)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Journey>? testResult = _mockRepository.Object.GetListByFlightId(flightId).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void GetJourneysByPlaces_ExistsJourneys()
        {
            _mockRepository = new();

            string origin = "origin";
            string destination = "destination";

            List<Journey>? journeys = new() { new Journey { Id = new Random().Next(), Origin = origin, Destination = destination } };

            _mockRepository.Setup(rep => rep.GetListByPlaces(origin, destination)).Returns(Task.FromResult(journeys));

            List<Journey>? testResult = _mockRepository.Object.GetListByPlaces(origin, destination).Result;

            Assert.True(testResult != null && testResult.Any());
        }

        [Fact]
        public void GetJourneysByPlaces_NotExistsJourneys()
        {
            _mockRepository = new();

            string origin = "origin";
            string destination = "destination";

            List<Journey>? journeys = null;

            _mockRepository.Setup(rep => rep.GetListByPlaces(origin, destination)).Returns(Task.FromResult(journeys));

            List<Journey>? testResult = _mockRepository.Object.GetListByPlaces(origin, destination).Result;

            Assert.Null(testResult);
        }

        [Fact]
        public void GetJourneysByPlaces_GenerateException()
        {
            try
            {
                _mockRepository = new();

                string origin = "origin";
                string destination = "destination";

                _mockRepository.Setup(rep => rep.GetListByPlaces(origin, destination)).Throws(new Exception(EXCEPTION_MESSAGE));

                List<Journey>? testResult = _mockRepository.Object.GetListByPlaces(origin, destination).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        
    }
}
