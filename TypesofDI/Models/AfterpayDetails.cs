using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesofDI.Models
{
    public class AfterpayDetails : IPaymentDetails
    {
        public string AppId { get; set; }
        public AfterpayDetails(string appId) {
            AppId = appId;
        }

        public void Verify()
        {
            if (string.IsNullOrWhiteSpace(AppId))
            {
                throw new ArgumentException("Afterpay App ID cannot be null or empty.");
            }
            var stringArray = AppId.Split('-');
            // Simulate verification logic
            if (string.IsNullOrWhiteSpace(AppId))
            {
                throw new ArgumentException("Invalid Afterpay App ID.");
            }

            if (stringArray.Length != 2)
            {
                throw new ArgumentException("Afterpay App ID must be in the format 'xxxx-afterpay'.");
            }

            if (!stringArray[1].Equals("afterpay", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("Afterpay App ID must end with '-afterpay'.");
            }
        }
    }
}
