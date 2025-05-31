using System;
using System.Collections.Generic;
using TypesofDI.Enums;
using TypesofDI.Models;


namespace TypesOfDI.UI
{
    public static class ConsolePrompts
    {
        public static PaymentMethod PromptForMethod()
        {
            while (true)
            {
                Console.WriteLine("Select payment method (enter the number):");
                foreach (PaymentMethod pm in Enum.GetValues(typeof(PaymentMethod)))
                {
                    if (pm == PaymentMethod.None) continue;
                    Console.WriteLine($"  {(int)pm}. {pm}");
                }

                var input = Console.ReadLine();
                Console.WriteLine($"You entered: '{input}'");

                if (int.TryParse(input, out var choice) &&
                    Enum.IsDefined(typeof(PaymentMethod), choice) &&
                    choice != (int)PaymentMethod.None)
                {
                    return (PaymentMethod)choice;
                }

                Console.WriteLine("Invalid selection—please try again.\n");
            }
        }

        public static decimal PromptForAmount()
        {
            while (true)
            {
                Console.Write("Enter amount to charge: ");
                var input = Console.ReadLine();
                Console.WriteLine($"You entered amount: '{input}'");

                if (decimal.TryParse(input, out var amount) && amount > 0)
                {
                    Console.WriteLine($"Confirmed amount: ${amount}\n");
                    return amount;
                }

                Console.WriteLine("Invalid amount—please enter a positive number.\n");
            }
        }

        public static object PromptForDetails(PaymentMethod method)
        {
            var success = false;
            object details = null;
            while (!success)
            {
                try
                {
                    details = PromptForDetailsInternal(method);
                    success = true;
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}\nPlease try again.");
                }
            }

            return details;
        }

        private static object PromptForDetailsInternal(PaymentMethod method)
        {
            switch (method)
            {
                case PaymentMethod.CreditCard:
                    Console.Write("Enter card number: ");
                    var cardNum = Console.ReadLine()!;
                    Console.WriteLine($"You entered card number: '{cardNum}'\n");
                    return new CreditCardDetails(cardNum);

                case PaymentMethod.PayPal:
                    Console.Write("Enter PayPal user ID or username: ");
                    var ppId = Console.ReadLine()!;
                    Console.WriteLine($"You entered PayPal ID: '{ppId}'\n");
                    return new PayPalDetails(ppId);

                case PaymentMethod.Afterpay:
                    Console.Write("Enter Afterpay user ID: ");
                    var apId = Console.ReadLine()!;
                    Console.WriteLine($"You entered Afterpay ID: '{apId}'\n");
                    return new AfterpayDetails(apId);

                default:
                    throw new InvalidOperationException("Unknown payment method");
            }
        }
    }
}
