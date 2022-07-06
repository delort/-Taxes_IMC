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

public class TaxCalculatorPageModelTests : IocSetup
{
    [Test]
    public void EnsureModelCreated()
    {
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        vm.TaxModel.Should().NotBeNull();
    }
    
    [Test]
    public void EnsureValidatorsCreated()
    {
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        vm.TaxModel.ToZip.Validations.Should().HaveCount(2);
        vm.TaxModel.ToState.Validations.Should().HaveCount(2);
        vm.TaxModel.ToCountry.Validations.Should().HaveCount(2);
        
        vm.TaxModel.FromCountry.Validations.Should().HaveCount(1);
        vm.TaxModel.FromState.Validations.Should().HaveCount(1);
        vm.TaxModel.Shipping.Validations.Should().HaveCount(1);
        vm.TaxModel.FromZip.Validations.Should().HaveCount(1);
        vm.TaxModel.Amount.Validations.Should().HaveCount(1);
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenNoInternet()
    {
        ConnectivityService.HasInternetConnection().Returns(false);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        vm.CalculateTaxesCommand.Execute();

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
    public void CalculateTaxesCommand_ShouldDisplayError_WhenValidationFailed()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        vm.CalculateTaxesCommand.Execute();

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
    public void CalculateTaxesCommand_ShouldDisplayError_WhenApiThrowError()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        TaxService.CalculateTaxes(Arg.Any<TaxesRequest>())
            .Throws(new Exception());
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();

        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.GeneralErrorMessage);
            Logger.Received(1).LogError(Arg.Any<Exception>());
            TaxService.Received(1).CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldNotDisplayError_WhenAllArgsCorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.DidNotReceive().ShowAlert(Arg.Any<string>());
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.Received(1).CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenZipIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ToZip: {AppResources.RequiredFieldMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [TestCase("92")]
    [TestCase("9090909090")]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenZipIncorrect(string zip)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = zip;
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ToZip: {AppResources.ZipCodeInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenCountryIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ToCountry: {AppResources.RequiredFieldMessage}");
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
    public void CalculateTaxesCommand_ShouldDisplayError_WhenCountryIncorrect(string country)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = country;
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ToCountry: {AppResources.CountryCodeInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenStateIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ToState: {AppResources.RequiredFieldMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [TestCase("CAAD")]
    [TestCase("USS")]
    [TestCase("E")]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenStateIncorrect(string state)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = state;
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"ToState: {AppResources.StateInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenAmountIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.Amount.Value = string.Empty;
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"Amount: {AppResources.NumberInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [TestCase("CAAD")]
    [TestCase("2+2")]
    [TestCase("123 123")]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenAmountIncorrect(string amount)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = amount;
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"Amount: {AppResources.NumberInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenShippingIncorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = string.Empty;
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"Shipping: {AppResources.NumberInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [TestCase("CAAD")]
    [TestCase("2+2")]
    [TestCase("123 123")]
    public void CalculateTaxesCommand_ShouldDisplayError_WhenShippingIncorrect(string shipping)
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = shipping;
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.Received(1).ShowAlert(AppResources.ValidationFailedMessage,$"Shipping: {AppResources.NumberInvalidMessage}");
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.DidNotReceive().CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
    [Test]
    public void CalculateTaxesCommand_ShouldNReturnDetailedModel_WhenAllArgsCorrect()
    {
        ConnectivityService.HasInternetConnection().Returns(true);
        TaxService.CalculateTaxes(Arg.Any<TaxesRequest>())
            .Returns(new Tax
            {
                TaxSource = "Test"
            });

        var vm = Ioc.IoCConstruct<TaxCalculatorPageModel>();
        
        
        vm.TaxModel.ToCountry.Value = "CA";
        vm.TaxModel.ToZip.Value = "M5V 2T6";
        vm.TaxModel.ToState.Value = "ON";
        vm.TaxModel.Amount.Value = "16.95";
        vm.TaxModel.Shipping.Value = "10";
        vm.CalculateTaxesCommand.Execute();

        using (new AssertionScope())
        {
            ConnectivityService.Received(1).HasInternetConnection();
            DialogService.DidNotReceive().ShowAlert(Arg.Any<string>());
            Logger.DidNotReceive().LogError(Arg.Any<Exception>());
            TaxService.Received(1).CalculateTaxes(Arg.Any<TaxesRequest>());
            RateService.DidNotReceive().GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            vm.TaxDetailsModel.Should().NotBeNull();
            vm.TaxDetailsModel.TaxSource.Should().Be("Test");
        }
    }
}