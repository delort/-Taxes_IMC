using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SalesTaxCalculator.PageModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SalesTaxCalculator.Pages;

[MvxTabbedPagePresentation]
public partial class TaxCalculatorPage : MvxContentPage<TaxCalculatorPageModel>
{
    public TaxCalculatorPage()
    {
        InitializeComponent();
        Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        On<iOS>().SetUseSafeArea(false);
    }
}