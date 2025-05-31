using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypesofDI.Configuration
{
    public class PaymentConfig
    {
        public string Environment { get; set; } = "Development";
        public ProcessorConfig CreditCard { get; set; }
        public ProcessorConfig PayPal { get; set; }
        public ProcessorConfig Afterpay { get; set; }

        public class ProcessorConfig
        {
            public string MerchantId { get; set; }    // used by CC
            public string ApiKey { get; set; }    // used by CC
            public string ClientId { get; set; }    // used by PayPal
            public string ClientSecret { get; set; }    // used by PayPal
            public string AccountId { get; set; }    // used by Afterpay
            public string ApiToken { get; set; }    // used by Afterpay
        }
    }
}
