namespace SalesTaxCalculator.Common.Interfaces;

public interface ILogger
{
    void LogError(Exception ex);
    void LogDebugMessage(string message);
}