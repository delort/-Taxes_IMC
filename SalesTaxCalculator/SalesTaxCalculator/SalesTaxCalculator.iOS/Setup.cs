using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using SalesTaxCalculator.iOS.Platform;
using SalesTaxCalculator.Services.Interfaces;

namespace SalesTaxCalculator.iOS;

public class Setup : MvxFormsIosSetup<CoreApp, App>
{
    protected override IMvxIoCProvider InitializeIoC()
    {
        var ioc = base.InitializeIoC();

        ioc.LazyConstructAndRegisterSingleton<ISafeAreaInfo, SafeAreaInfo>();

        return ioc;
    }
}