using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesofDI.Models
{
    public class CreditCardDetails : IPaymentDetails
    {
        string CardNumber { get; set; }

        public CreditCardDetails(string cardNumber) {
            CardNumber = cardNumber;
        }

        public void Verify()
        {
            // Simulate verification logic
            if (string.IsNullOrWhiteSpace(CardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty.");
            }
            var stringArray = CardNumber.Split('-');
            if (stringArray.Length != 4)
            {
                throw new ArgumentException("Invalid card number format.");
            }

        }

    }
}
