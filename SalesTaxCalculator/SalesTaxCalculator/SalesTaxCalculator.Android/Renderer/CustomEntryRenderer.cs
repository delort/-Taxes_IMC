using System;
using Android.Content;
using SalesTaxCalculator.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace SalesTaxCalculator.Droid.Renderer
{
    public class CustomEntryRenderer : EntryRenderer
    {
        private bool _isDisposed;

        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
                return;
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Control.SetPadding(1, 1, 1, 1);
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
