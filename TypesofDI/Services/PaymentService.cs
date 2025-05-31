using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesofDI.Models;
using TypesofDI.Processor;

namespace TypesofDI.Services
{
    public class PaymentService
    {
        private readonly IPaymentProcessor _processor;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentProcessor processor, ILoggerFactory loggerFactory)
        {
            _processor = processor;   // injected here
            _logger = loggerFactory.CreateLogger<PaymentService>();
        }

        public bool Pay(decimal amount, IPaymentDetails details)
        {
            bool result = false; // Initialize result to false
            try
            {
                details.Verify(); // Verify payment details, eg This verifies if the card number is of the correct format
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Payment details cannot be null. Please provide valid payment details.");
                return false; // Return false if details are null
            }
            
            if (amount <= 0)
            {
                _logger.LogError("Payment amount must be greater than zero.");
                return result; 
            }
            if (IsPaymentDetailsValid(details) == false) 
            {
                _logger.LogError("Payment details are invalid.");
                return result;
            }
            try
            {
                _logger.LogInformation("Processing payment of ${Amount}", amount);
                result = _processor.ProcessPayment(amount, details); // The processor tryes to process the payment
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging payment details");
                return false; 
            }
            
            return result; // Return the result of the payment processing

        }

        public bool IsPaymentDetailsValid(IPaymentDetails details)
        {
            // Validate payment details, Program would validate the details based on the type of payment processor
            // for example, checking if credit card number is valid, by checking with Visa or Mastercard API, etc.
            // This is a placeholder for actual validation logic.
            return true;
        }

    }
}
