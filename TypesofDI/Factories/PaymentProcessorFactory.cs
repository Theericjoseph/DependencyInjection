using TypesofDI.Configuration;
using TypesofDI.Enums;
using TypesofDI.Processor;

namespace TypesofDI.Factories
{
    public class PaymentProcessorFactory
    {
        private readonly EnvironmentType _env;
        private readonly PaymentConfig _cfg;

        
        public PaymentProcessorFactory(EnvironmentType env, PaymentConfig cfg)
        {
            _env = env;
            _cfg = cfg;
        }
        public PaymentProcessorFactory(EnvironmentType env)
            : this(env, new PaymentConfig())
        {
        }

        public IPaymentProcessor Create(PaymentMethod method)
            => method switch
            {
                PaymentMethod.CreditCard => new CreditCardProcessor(_env, _cfg.CreditCard),
                PaymentMethod.PayPal => new PayPalProcessor(_env, _cfg.PayPal),
                PaymentMethod.Afterpay => new AfterpayProcessor(_env, _cfg.Afterpay),
                _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
            };
    }
    
}
