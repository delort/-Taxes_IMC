using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SalesTaxCalculator.Controls
{
    public partial class CustomEntry : ContentView
    {
        public CustomEntry()
        {
            InitializeComponent();
            TitleLbl.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            TitleLbl.SetBinding(Label.TextColorProperty, new Binding(nameof(BackgroundColor), source: this));

            InputCtrl.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this));
            InputCtrl.SetBinding(Entry.TextColorProperty, new Binding(nameof(TextColor), source: this));
            InputCtrl.SetBinding(Entry.MaxLengthProperty, new Binding(nameof(MaxLength), source: this));

            Separator.SetBinding(BoxView.ColorProperty, new Binding(nameof(BackgroundColor), source: this));

            //ErrorLbl.SetBinding(Label.TextProperty, new Binding(nameof(Error), source: this));
        }

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
           propertyName: nameof(Title),
           returnType: typeof(string),
           declaringType: typeof(CustomEntry),
           defaultValue: default);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
           propertyName: nameof(Text),
           returnType: typeof(string),
           declaringType: typeof(CustomEntry),
           defaultValue: default,
           defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
           propertyName: nameof(Error),
           returnType: typeof(string),
           declaringType: typeof(CustomEntry),
           defaultValue: default);

        public string Error
        {
            get => (string)GetValue(ErrorProperty);
            set => SetValue(ErrorProperty, value);
        }

        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(
          propertyName: nameof(BackgroundColor),
          returnType: typeof(Color),
          declaringType: typeof(CustomEntry),
          defaultValue: Color.Black);
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
           propertyName: nameof(TextColor),
           returnType: typeof(Color),
           declaringType: typeof(CustomEntry),
           defaultValue: Color.Black);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
           propertyName: nameof(MaxLength),
           returnType: typeof(int),
           declaringType: typeof(CustomEntry),
           defaultValue: 100);

        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }
    }
}

