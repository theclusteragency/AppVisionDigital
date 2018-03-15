using System;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AppFom.CellViews
{
    public class VCPhoto : ViewCell
    {
        public VCPhoto()
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

            //var foto = new Image { HeightRequest = 90, WidthRequest = 90 };
            var foto = new CircleImage
            {
                BorderColor = Color.LightGray,
                BorderThickness = 1,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,

            };

            foto.SetBinding(Image.SourceProperty, "url_foto");
            slWrap.Children.Add(foto);

            var LblUser = new Label()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White
            };
            LblUser.SetBinding(Label.TextProperty, "nombre");
            slWrap.Children.Add(LblUser);

            this.View = slWrap;
        }
    }
}