using Microsoft.Extensions.Logging;
using TypesofDI.Factories;
using TypesofDI.Services;
using TypesofDI;
using TypesOfDI.UI;
using TypesofDI.Models;

namespace TypesOfDI
{
    public class App
    {
        private readonly PaymentProcessorFactory _factory;
        private readonly ILogger<App> _logger;
        private readonly ILoggerFactory _loggerFactory;

        public App(PaymentProcessorFactory factory,
                   ILoggerFactory loggerfactory)
        {
            _factory = factory;
            _loggerFactory = loggerfactory;
            _logger = _loggerFactory.CreateLogger<App>();
        }

        public void Run()
        {
            _logger.LogInformation("Starting DI Demo…");

            // 1) Select method
            var method = ConsolePrompts.PromptForMethod();
            _logger.LogInformation("User selected {Method}", method);

            // 2) Create processor
            var processor = _factory.Create(method);

            // 3) Read amount
            var amount = ConsolePrompts.PromptForAmount();

            // 4) Read details
            var details = ConsolePrompts.PromptForDetails(method);

            // 5) Inject into service and execute
            var service = new PaymentService(processor, _loggerFactory);
            try
            { 
                service.Pay(amount, (IPaymentDetails)details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the payment.");
            }
        }
    }
}