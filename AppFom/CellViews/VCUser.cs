using System;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AppFom.CellViews
{
    public class VCUser : ViewCell
    {
        public VCUser()
        {
            Tapped += (sender, e) =>
            {
                this.View.BackgroundColor = Color.Transparent;
            };

            var slWrap = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(30, 0, 0, 0)
            };

            var foto = new CircleImage
            {
                BorderColor = Color.LightGray,
                BorderThickness = 1,
                HeightRequest = 60,
                WidthRequest = 60,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,

            };
            foto.SetBinding(Image.SourceProperty, "url_avatar");
            slWrap.Children.Add(foto);

            var slWrapName = new StackLayout
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 5
            };

            var LblUser = new Label()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White
            };
            LblUser.SetBinding(Label.TextProperty, "nombre");

            var LblApe = new Label()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White
            };
            LblApe.SetBinding(Label.TextProperty, "apellido_paterno");

            slWrapName.Children.Add(LblUser);
            slWrapName.Children.Add(LblApe);

            slWrap.Children.Add(LblUser);

            this.View = slWrap;
        }
    }
}