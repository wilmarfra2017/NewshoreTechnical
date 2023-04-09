using MediatR;
using Moq;
using Newshore.Technical.Domain.Commands.Flights;
using Newshore.Technical.Domain.ResponseModels;

namespace Newshore.Technical.Test.Domain
{
    public class FlightCommandsDomainTest
    {
        
        private Mock<IMediator>? _mediator;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        

        
        [Fact]
        public void Create_Success()
        {
            CreateFlightService command = new();

            _mediator = new();
            _mediator
                .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FlightResponse());

            FlightResponse? testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                CreateFlightService command = new();

                _mediator = new();
                _mediator
                    .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                FlightResponse? testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void Delete_Success()
        {
            DeleteFlightService command = new();

            _mediator = new();
            _mediator
                 .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Unit.Value);

            Unit testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.IsType<Unit>(testResult);
        }

        [Fact]
        public void Delete_Error()
        {
            try
            {
                DeleteFlightService command = new();

                _mediator = new();
                _mediator.Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                _mediator.Object.Send(command, It.IsAny<CancellationToken>());
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void Update_Success()
        {
            UpdateFlightService command = new();

            _mediator = new();
            _mediator
                 .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(Unit.Value);

            Unit testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.IsType<Unit>(testResult);
        }

        [Fact]
        public void Update_Error()
        {
            try
            {
                UpdateFlightService command = new();

                _mediator = new();
                _mediator.Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                _mediator.Object.Send(command, It.IsAny<CancellationToken>());
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        
    }
}
