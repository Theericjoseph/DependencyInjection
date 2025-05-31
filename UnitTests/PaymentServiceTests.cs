using Microsoft.Extensions.Logging;
using Moq;
using TypesofDI.Models;
using TypesofDI.Processor;
using TypesofDI.Services;

namespace UnitTests
{
    [TestClass]
    public class PaymentServiceTests
    {
        private Mock<ILoggerFactory> _loggerFactoryMock = null!;
        private Mock<ILogger<PaymentService>> _loggerMock = null!;

        [TestInitialize]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<PaymentService>>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerFactoryMock
                 .Setup(f => f.CreateLogger(typeof(PaymentService).FullName!))
                .Returns(_loggerMock.Object);
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-10)]
        public void Pay_InvalidAmount_DoesNotCallProcessor(Int32 badAmount)
        {
            // Arrange
            var processorMock = new Mock<IPaymentProcessor>();
            var detailsMock = new Mock<IPaymentDetails>();
            detailsMock.Setup(d => d.Verify());  // no exception

            var service = new PaymentService(
                processorMock.Object,
                _loggerFactoryMock.Object);

            // Act
            var result = service.Pay(badAmount, detailsMock.Object);

            // Assert
            Assert.IsFalse(result, "Pay should return false for non‐positive amounts.");
            processorMock.Verify(
                p => p.ProcessPayment(
                    It.IsAny<decimal>(),
                    It.IsAny<IPaymentDetails>()),
                Times.Never, "Processor must not be called on invalid amount.");
        }

        [TestMethod]
        public void Pay_InvalidDetails_DoesNotCallProcessor()
        {
            // Arrange
            var processorMock = new Mock<IPaymentProcessor>();
            var detailsMock = new Mock<IPaymentDetails>();
            // Simulate Verify() throwing for null/invalid details
            detailsMock
                .Setup(d => d.Verify())
                .Throws<ArgumentNullException>();

            var service = new PaymentService(
                processorMock.Object,
                _loggerFactoryMock.Object);

            // Act
            var result = service.Pay(100m, detailsMock.Object);

            // Assert
            Assert.IsFalse(result, "Pay should return false when Verify() throws.");
            processorMock.Verify(
                p => p.ProcessPayment(
                    It.IsAny<decimal>(),
                    It.IsAny<IPaymentDetails>()),
                Times.Never, "Processor must not be called on invalid details.");
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void Pay_RelaysProcessorResult(bool processorResult)
        {
            // Arrange
            var processorMock = new Mock<IPaymentProcessor>();
            processorMock
                .Setup(p => p.ProcessPayment(
                    It.IsAny<decimal>(),
                    It.IsAny<IPaymentDetails>()))
                .Returns(processorResult);

            var detailsMock = new Mock<IPaymentDetails>();
            detailsMock.Setup(d => d.Verify());  // valid details

            var service = new PaymentService(
                processorMock.Object,
                _loggerFactoryMock.Object);

            // Act
            var result = service.Pay(50m, detailsMock.Object);

            // Assert
            Assert.AreEqual(
                processorResult,
                result,
                "Pay should return whatever the processor returns when all preconditions pass.");

            processorMock.Verify(
                p => p.ProcessPayment(50m, detailsMock.Object),
                Times.Once,
                "Processor must be called exactly once with the provided amount and details.");
        }
    }
}
