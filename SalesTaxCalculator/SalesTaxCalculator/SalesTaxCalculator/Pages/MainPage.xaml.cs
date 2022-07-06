using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using SalesTaxCalculator.PageModels;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace SalesTaxCalculator.Pages
{
    [MvxTabbedPagePresentation(TabbedPosition.Root, Animated = true, NoHistory = true)]
    public partial class MainPage : MvxTabbedPage<MainPageModel>
    {
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}

