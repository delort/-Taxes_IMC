using System;
using System.Diagnostics;
using SalesTaxCalculator.Common.Interfaces;

namespace SalesTaxCalculator.Services;

public class Logger: ILogger
{
    public void LogError(Exception ex)
    {
#if DEBUG
        Debug.WriteLine(new string('*', 20));
        Debug.WriteLine(ex.Message);
        Debug.WriteLine(ex.StackTrace);
        Debug.WriteLine(new string('*', 20));
#else
        //todo integrate AppCenter for Release Mode
        //this is not needed in our example and can be added later
#endif
        
    }

    public void LogDebugMessage(string message)
    {
#if DEBUG
        Debug.WriteLine($"DEBUG_LOG: {message} {DateTime.Now.ToShortTimeString()}");
#endif
    }
}