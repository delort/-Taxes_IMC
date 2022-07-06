using System;
using MvvmCross.Base;
using MvvmCross.Navigation;
using MvvmCross.Tests;
using NSubstitute;
using NUnit.Framework;
using SalesTaxCalculator.Common.Interfaces;

namespace SalesTaxCalculator.UnitTests;

public class IocSetup : MvxIoCSupportingTest
{
    public IocSetup()
    {
        Setup();
        Builder = new IocBuilder(Ioc);
        RegisterMocks();
        RegisterDispatcher();
    }

    public IocBuilder Builder { get; }

    public IDialogService DialogService { get; protected set; }
    public ILogger Logger { get; protected set; }
    public IMvxMainThreadAsyncDispatcher Dispatcher { get; protected set; }
    public IRateService RateService { get; protected set; }
    public ITaxService TaxService { get; protected set; }
    public IConnectivityService ConnectivityService { get; protected set; }
    public IMvxNavigationService MvxNavigationService { get; protected set; }

    public void ClearCallsAndMocks() => Builder.ClearCallsAndMocks();
    
    [TearDown]
    public void Cleanup()
    {
        ClearCallsAndMocks();
    }

    private void RegisterMocks()
    {
        MvxNavigationService = Builder.Mock<IMvxNavigationService>();
        ConnectivityService = Builder.Mock<IConnectivityService>();
        RateService = Builder.Mock<IRateService>();
        TaxService = Builder.Mock<ITaxService>();
        DialogService = Builder.Mock<IDialogService>();
        Logger = Builder.Mock<ILogger>();
    }

    private void RegisterDispatcher()
    {
        Dispatcher = Substitute.For<IMvxMainThreadAsyncDispatcher>();
        Dispatcher
            .When(x => x.ExecuteOnMainThreadAsync(Arg.Any<Action>()))
            .Do(info => ((Action)info.Args()[0]).Invoke());
        Ioc.RegisterSingleton(Dispatcher);
    }
}
