using System;
using AppFom.Helpers;
using AppFom.ViewModels;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageSupport : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        #endregion


        public PageSupport()
        {
            Title = "Soporte";

            var BgLayout = new RelativeLayout();

            //var BgImage = new Image { Source = ImageSource.FromResource("AppFom.Images.bg_fom_blelogin.png"), Aspect = Aspect.AspectFill };
            var BgImage = new Image { Source = ImageSource.FromResource("AppFom.Images.bg_fom_blelogin_green.png"), Aspect = Aspect.AspectFill };

            BgLayout.Children.Add(BgImage,
                              Constraint.Constant(0),
                              Constraint.Constant(0),
                              Constraint.RelativeToParent((Parent) =>
                              {
                                  return Parent.Width;
                              }),
                              Constraint.RelativeToParent((Parent) =>
                              {
                                  return Parent.Height;
                              })
                         );

            // Contruye pantalla
            ScreenBuilder(SlRoot);
            BgLayout.Children.Add(SlRoot,
                             Constraint.Constant(0),
                             Constraint.Constant(0),
                             Constraint.RelativeToParent((Parent) =>
                             {
                                 return Parent.Width;
                             }),
                             Constraint.RelativeToParent((Parent) =>
                             {
                                 return Parent.Height;
                             })
                        );

            this.BindingContext = new VMSupport(this.Navigation);

            Content = BgLayout;
        }


        public void ScreenBuilder(StackLayout root)
        {

            var SlWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 30,
                Padding = new Thickness(0, 50, 0, 0)
            };


            var SlWrapTel = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            var LblContactTel = new Label
            {
                Text = "Contacto telefónico",
                TextColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            var TgBtnTel = new TapGestureRecognizer();
            TgBtnTel.SetBinding(TapGestureRecognizer.CommandProperty, "CommandCallNumber");
            var ImgTelefono = new Image
            {
                Source = ImageSource.FromResource("AppFom.Images.ico_support_tel.png"),
                WidthRequest = 238 * .3,
                HeightRequest = 233 * .3
            };

            ImgTelefono.GestureRecognizers.Add(TgBtnTel);


            SlWrapTel.Children.Add(LblContactTel);
            SlWrapTel.Children.Add(ImgTelefono);


            var SlWrapMail = new StackLayout
            {
                Orientation = StackOrientation.Vertical
            };

            var LblContactMail = new Label
            {
                Text = "Contacto",
                TextColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,

            };

            var TgBtnMail = new TapGestureRecognizer();
            TgBtnMail.SetBinding(TapGestureRecognizer.CommandProperty, "CommandSendMail");
            var ImgMail = new Image
            {
                Source = ImageSource.FromResource("AppFom.Images.ico_support_mail.png"),
                WidthRequest = 266 * .3,
                HeightRequest = 178 * .3
            };
            ImgMail.GestureRecognizers.Add(TgBtnMail);

            SlWrapMail.Children.Add(LblContactMail);
            SlWrapMail.Children.Add(ImgMail);

            SlWrap.Children.Add(SlWrapTel);
            SlWrap.Children.Add(SlWrapMail);

            root.Children.Add(SlWrap);
        }

    }
}


