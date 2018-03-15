using System;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AppFom.CellViews
{
    public class VCComment : ViewCell
    {
        public VCComment()
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
                Spacing = 10
            };

            var SlWrapImage = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 1
            };


            var ImgUser = new CircleImage
            {
                BorderColor = Color.LightGray,
                BorderThickness = 1,
                HeightRequest = 50,
                WidthRequest = 50,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,

            };
            ImgUser.SetBinding(Image.SourceProperty, "url_avatar");

            SlWrapImage.Children.Add(ImgUser);

            var LblUser = new Label()
            {
                //VerticalOptions = LayoutOptions.FillAndExpand,
                //HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
            };
            LblUser.SetBinding(Label.TextProperty, "nombre");
            SlWrapImage.Children.Add(LblUser);

            var lblComment = new Label
            {

                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };
            lblComment.SetBinding(Label.TextProperty, "descripcion");

            var lblMesaage = new Label
            {

                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.White
            };
            lblMesaage.SetBinding(Label.TextProperty, "mensaje");


            slWrap.Children.Add(SlWrapImage);
            slWrap.Children.Add(lblComment);
            slWrap.Children.Add(lblMesaage);

            this.View = slWrap;
        }
    }
}