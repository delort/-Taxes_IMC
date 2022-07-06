using System;
using SalesTaxCalculator.iOS.Renderer;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace SalesTaxCalculator.iOS.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        bool _isDisposed;

        public CustomEntryRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;
            Control.BorderStyle = UITextBorderStyle.None;
        }

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            _isDisposed = true;

            base.Dispose(disposing);
            GC.SuppressFinalize(this);
        }
    }
}
