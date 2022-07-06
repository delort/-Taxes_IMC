using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using SalesTaxCalculator.Droid.Platform;
using SalesTaxCalculator.Services.Interfaces;

namespace SalesTaxCalculator.Droid;

public class Setup : MvxFormsAndroidSetup<CoreApp, App>
{
    protected override IMvxIoCProvider InitializeIoC()
    {
        var ioc = base.InitializeIoC();

        ioc.LazyConstructAndRegisterSingleton<ISafeAreaInfo, SafeAreaInfo>();

        return ioc;
    }
}