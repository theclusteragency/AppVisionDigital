using System;
using Xamarin.Forms;

namespace AppFom.CellViews
{
    public class VCEvent : ViewCell
    {
        public VCEvent()
        {
            Tapped += (sender, e) =>
            {
                this.View.BackgroundColor = Color.Transparent;
            };

            var slWrap = new StackLayout();
            slWrap.Orientation = StackOrientation.Vertical;

            var slWrapperContent = new StackLayout
            {

                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 10, 0, 10),
            };

            var SlContent = new StackLayout
            {

                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(10, 0, 0, 0)
            };
            SlContent.BindingContextChanged += (sender, i) =>
            {
                base.OnBindingContextChanged();
                dynamic c = BindingContext;
                if (c != null)
                {
                    if (c.estatus == "Programado")
                    {
                        SlContent.BackgroundColor = Color.Green;
                    }
                    else if (c.estatus == "Iniciado")
                    {
                        SlContent.BackgroundColor = Color.Blue;
                    }
                    else if (c.estatus == "Finalizado")
                    {
                        SlContent.BackgroundColor = Color.Red;
                    }
                }
            };

            var SlTextContent = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 5,
                BackgroundColor = Color.White,
                Padding = new Thickness(10, 5, 5, 5)
            };

            var LblEventDesc = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black
            };
            LblEventDesc.SetBinding(Label.TextProperty, "descripcion");
            SlTextContent.Children.Add(LblEventDesc);

            var LblEventCateg = new Label()
            {
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
            };
            LblEventCateg.SetBinding(Label.TextProperty, "categoria");
            SlTextContent.Children.Add(LblEventCateg);

            var slAddress = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 10
            };

            var PinAddress = new Image { Source = ImageSource.FromResource("AppFom.Images.img_fom_pin.png"), Aspect = Aspect.AspectFit };
            slAddress.Children.Add(PinAddress);


            var LblEventAddress = new Label()
            {
                TextColor = Color.Gray,
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
            };
            LblEventAddress.SetBinding(Label.TextProperty, "direccion");
            slAddress.Children.Add(LblEventAddress);

            SlTextContent.Children.Add(slAddress);


            var slInfo = new StackLayout
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 10
            };

            var BtnInfo = new Image
            {
                Source = ImageSource.FromResource("AppFom.Images.img_fom_btnmoreinfo.png"),
                WidthRequest = 261 * .3,
                HeightRequest = 58 * .3
            };
            slInfo.Children.Add(BtnInfo);

            SlTextContent.Children.Add(slInfo);

            SlContent.Children.Add(SlTextContent);

            slWrapperContent.Children.Add(SlContent);
            slWrap.Children.Add(slWrapperContent);

            this.View = slWrap;
        }
    }
}
