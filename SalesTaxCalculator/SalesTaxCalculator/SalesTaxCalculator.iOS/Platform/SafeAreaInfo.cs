using SalesTaxCalculator.Services.Interfaces;
using UIKit;

namespace SalesTaxCalculator.iOS.Platform
{
    public class SafeAreaInfo : ISafeAreaInfo
    {
        public int Top => (int)UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Top;
        public int Bottom => (int)UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Bottom;
        public int Left => (int)UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Left;
        public int Right => (int)UIApplication.SharedApplication.KeyWindow.SafeAreaInsets.Right;
    }
}
