using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace SalesTaxCalculator.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate  : MvxFormsApplicationDelegate<Setup, CoreApp, App>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            return base.FinishedLaunching(app, options);
        }
    }
}

