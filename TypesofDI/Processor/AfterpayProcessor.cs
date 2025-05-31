
using TypesofDI.Enums;
using TypesofDI.Models;
using static TypesofDI.Configuration.PaymentConfig;


namespace TypesofDI.Processor
{
    public class AfterpayProcessor : IPaymentProcessor
    {
        private readonly EnvironmentType _env;
        private readonly ProcessorConfig _config;

        public AfterpayProcessor(EnvironmentType environment, ProcessorConfig config)
        {
            _env = environment;
            _config = config;
        }
        public bool ProcessPayment(decimal amount, IPaymentDetails details)
        {
            // Logic to process Afterpay payment 
            Console.WriteLine($"Processing Afterpay payment of ${amount}");
            return true; // Simulate successful payment
        }
    }
}
