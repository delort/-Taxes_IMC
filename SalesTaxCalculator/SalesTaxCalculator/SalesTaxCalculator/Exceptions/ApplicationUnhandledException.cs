using System;

namespace SalesTaxCalculator.Exceptions
{
    public class ApplicationUnhandledException : Exception
    {
        public ApplicationUnhandledException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}