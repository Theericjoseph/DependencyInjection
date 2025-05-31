using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesofDI.Models
{
    public class PayPalDetails : IPaymentDetails
    {
        public string UserId { get; set; }
        public PayPalDetails(string userId) {
            UserId = userId;
        }

        public void Verify()
        {
            if (string.IsNullOrWhiteSpace(UserId))
            {
                throw new ArgumentException("PayPal User ID cannot be null or empty.");
            }
            // Simulate verification logic
            var stringArray = UserId.Split('@');
            if (stringArray.Length != 2)
            {
                throw new ArgumentException("Invalid PayPal User ID format.");
            }
            
            if (stringArray[1].Equals("paypal.com", StringComparison.OrdinalIgnoreCase) == false)
            {
                throw new ArgumentException("PayPal User ID must end with '@paypal.com'.");
            }
        }
    }
}
