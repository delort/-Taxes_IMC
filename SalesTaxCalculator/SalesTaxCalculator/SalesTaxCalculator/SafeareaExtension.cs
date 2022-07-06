using System;
using MvvmCross;
using SalesTaxCalculator.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SalesTaxCalculator
{
    [ContentProperty(nameof(Margin))]
    public class SafeAreaExtension : IMarkupExtension
    {
        #region Fields
        /// <summary>
        /// The safe area instance to be used
        /// </summary>
        private readonly ISafeAreaInfo _safeAreaInfo;
        #endregion Fields

        #region Constructors
        /// <summary>
        /// Constructor used to create an instance of this class
        /// </summary>
        public SafeAreaExtension()
        {
            if (Mvx.IoCProvider.CanResolve<ISafeAreaInfo>())
            {
                _safeAreaInfo = Mvx.IoCProvider.Resolve<ISafeAreaInfo>();
            }
        }
        #endregion Constructors

        #region Properties
        /// <summary>
        /// Gets or sets the margin of control
        /// </summary>
        public Thickness Margin { get; set; } = new(0);

        /// <summary>
        /// Gets or sets the value if need to add top margin regarding to safe area value
        /// </summary>
        public bool IncludeTop { get; set; }

        /// <summary>
        /// Gets or sets the value if need to add bottom margin regarding to safe area value
        /// </summary>
        public bool IncludeBottom { get; set; }
        #endregion Properties

        #region Public Methods
        /// <summary>
        /// Method used to handle provided value in xaml
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_safeAreaInfo != null)
            {
                return new Thickness(Margin.Left,
                                     IncludeTop ? Margin.Top + _safeAreaInfo.Top : Margin.Top,
                                     Margin.Right,
                                     IncludeBottom ? Margin.Bottom + _safeAreaInfo.Bottom : Margin.Bottom);
            }
            return Margin;
        }
        #endregion Public Methods
    }
}
