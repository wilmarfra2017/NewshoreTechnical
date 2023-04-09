using MediatR;
using Moq;
using Newshore.Technical.Domain.Commands.Transports;
using Newshore.Technical.Domain.ResponseModels;

namespace Newshore.Technical.Domain.Commands.JourneyTransports
{
    public class TransportCommandsDomainTest
    {
        
        private Mock<IMediator>? _mediator;
        private const string EXCEPTION_MESSAGE = "Generated Exception for test";
        

        
        [Fact]
        public void Create_Success()
        {
            CreateTransportService command = new();

            _mediator = new();
            _mediator
                .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new TransportResponse());

            TransportResponse? testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;

            Assert.NotNull(testResult);
        }

        [Fact]
        public void Create_Error()
        {
            try
            {
                CreateTransportService command = new();

                _mediator = new();
                _mediator
                    .Setup(med => med.Send(command, It.IsAny<CancellationToken>()))
                    .Throws(new Exception(EXCEPTION_MESSAGE));

                TransportResponse? testResult = _mediator.Object.Send(command, It.IsAny<CancellationToken>()).Result;
            }
            catch (Exception ex)
            {
                Assert.Equal(EXCEPTION_MESSAGE, ex.Message);
            }
        }
        

        
        [Fact]
        public void Delete_Success()
        {
            DeleteTransportService command = new();

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
                DeleteTransportService command = new();

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
            UpdateTransportService command = new();

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
                UpdateTransportService command = new();

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
