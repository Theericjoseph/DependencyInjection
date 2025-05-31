
using TypesofDI.Enums;
using TypesofDI.Models;
using static TypesofDI.Configuration.PaymentConfig;


namespace TypesofDI.Processor
{
    public class PayPalProcessor : IPaymentProcessor
    {
        private readonly EnvironmentType _environment;
        private readonly ProcessorConfig _config;

        public PayPalProcessor(EnvironmentType environment, ProcessorConfig config)
        {
            _environment = environment;
            _config = config;
        }
        public bool ProcessPayment(decimal amount, IPaymentDetails details)
        {
            // Logic to process PayPal payment
            Console.WriteLine($"Processing PayPal payment of ${amount}");
            return true; // Simulate successful payment
        }
    }
    
}
