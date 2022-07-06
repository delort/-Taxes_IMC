using System;
namespace SalesTaxCalculator.Services.Interfaces
{
    public interface ISafeAreaInfo
    {
        int Top { get; }
        int Bottom { get; }
        int Left { get; }
        int Right { get; }
    }
}