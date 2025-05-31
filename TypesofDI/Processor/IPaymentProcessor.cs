using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypesofDI.Models;

namespace TypesofDI.Processor
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount, IPaymentDetails details);
    }
}
