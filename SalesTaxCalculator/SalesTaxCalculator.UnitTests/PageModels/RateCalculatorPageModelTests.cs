using System;
using FluentAssertions;
using FluentAssertions.Execution;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using SalesTaxCalculator.Common.Models;
using SalesTaxCalculator.Common.Models.Requests;
using SalesTaxCalculator.PageModels;
using SalesTaxCalculator.Resources;

namespace SalesTaxCalculator.UnitTests.PageModels;

public class RateCalculatorPageModelTests : IocSetup
{
    
    [Test]
    public void EnsureModelCreated()
    {
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.Should().NotBeNull();
    }
    
    [Test]
    public void EnsureValidatorsCreated()
    {
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.ZipCode.Validations.Should().HaveCount(2);
        vm.RateModel.Country.Validations.Should().HaveCount(2);
        vm.RateModel.City.Validations.Should().HaveCount(0);
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldDisplayError_WhenNoInternet()
    {
        ConnectivityService.HasInternetConnection().Returns(false);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.InternetConnectionRequiredMessage);
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldDisplayError_WhenValidationFailed()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage, Arg.Any<string>());
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldDisplayError_WhenApiThrowError()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        RateService.GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Throws(new Exception());
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();

        vm.RateModel.ZipCode.Value = "90404";
        vm.RateModel.Country.Value = "US";
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.GeneralErrorMessage);
            Logger.Received(1).LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.Received(1).GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldNotDisplayError_WhenAllArgsCorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.ZipCode.Value = "90404";
        vm.RateModel.Country.Value = "US";
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.DidNotReceive().ShowAlert(Arg.Any<string>());
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.Received(1).GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldDisplayError_WhenZipIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.Country.Value = "US";
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ZipCode: {AppResources.RequiredFieldMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [TestCase("92")]
    [TestCase("9090909090")]
    public void CalculateRatesCommand_ShouldDisplayError_WhenZipIncorrect(string zip)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.ZipCode.Value = zip;
        vm.RateModel.Country.Value = "US";
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ZipCode: {AppResources.ZipCodeInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldDisplayError_WhenCountryIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.ZipCode.Value = "90404";
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"Country: {AppResources.RequiredFieldMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [TestCase("CAA")]
    [TestCase("USS")]
    [TestCase("EU")]
    [TestCase("PL")]
    [TestCase("UA")]
    public void CalculateRatesCommand_ShouldDisplayError_WhenCountryIncorrect(string country)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.ZipCode.Value = "90404";
        vm.RateModel.Country.Value = country;
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"Country: {AppResources.CountryCodeInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateRatesCommand_ShouldNReturnDetailedModel_WhenAllArgsCorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        RateService.GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(new Rate
            {
                City = "Test"
            });

        var vm = Ioc.IoCConstruct<RateCalculatorPageModel>();
        
        vm.RateModel.ZipCode.Value = "90404";
        vm.RateModel.Country.Value = "US";
        vm.CalculateRatesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.DidNotReceive().ShowAlert(Arg.Any<string>());
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.Received(1).GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            vm.RateDetailsModel.Should().NotBeNull();
            vm.RateDetailsModel.City.Should().Be("Test");
        }
    }
}