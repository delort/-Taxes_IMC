using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using SalesTaxCalculator.Common.Interfaces;
using SalesTaxCalculator.PageModels;
using SalesTaxCalculator.Services;
using SalesTaxCalculator.Services.ApiClients;
using SalesTaxCalculator.Services.Http;
using SalesTaxCalculator.Services.Services;
using Xamarin.Forms;

namespace SalesTaxCalculator
{
    public class CoreApp : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<MainPageModel>();
            
            var contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            };
            
            var jsonSerializer = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };

            var refitSettings = new RefitSettings(new NewtonsoftJsonContentSerializer(jsonSerializer));
            
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ILogger, Logger>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IApiConfigurator, ApiConfigurator>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IConnectivityService, EssentialsConnectivityService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IRateService, RateService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ITaxService, TaxService>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IHttpClientFactory, HttpClientFactory>();
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IDialogService>(() => new FormsDialogService(Application.Current));

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<ITaxesApi>(() =>
            {
                var apiConfigurator = Mvx.IoCProvider.Resolve<IApiConfigurator>();
                var httpClientManager = Mvx.IoCProvider.Resolve<IHttpClientFactory>();
                return RestService.For<ITaxesApi>(httpClientManager.CreateHttpClient(apiConfigurator.GetApiUrl(), true), refitSettings);
            });

            Mvx.IoCProvider.LazyConstructAndRegisterSingleton<IRatesApi>(() =>
            {
                var apiConfigurator = Mvx.IoCProvider.Resolve<IApiConfigurator>();
                var httpClientManager = Mvx.IoCProvider.Resolve<IHttpClientFactory>();
                return RestService.For<IRatesApi>(httpClientManager.CreateHttpClient(apiConfigurator.GetApiUrl(), true), refitSettings);
            });

        }
    }
}