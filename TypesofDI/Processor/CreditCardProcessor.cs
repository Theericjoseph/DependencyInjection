using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypesofDI.Enums;
using TypesofDI.Models;
using static TypesofDI.Configuration.PaymentConfig;


namespace TypesofDI.Processor
{
    public class CreditCardProcessor : IPaymentProcessor
    {
        private readonly EnvironmentType _environment;
        private readonly ProcessorConfig _config;

        public CreditCardProcessor(EnvironmentType environment, ProcessorConfig config)
        {
            _environment = environment;
            _config = config;
        }
        public bool ProcessPayment(decimal amount, IPaymentDetails details)
        {
        
            // Logic to process credit card payment
            Console.WriteLine($"Processing credit card payment of ${amount}");
            return true; // Simulate successful payment
        }
    }
    
}
