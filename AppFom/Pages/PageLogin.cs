using System;
using AppFom.Components;
using AppFom.Helpers;
using AppFom.ViewModels;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageLogin : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        #endregion


        public PageLogin()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var BgLayout = new RelativeLayout();

            //var BgImage = new Image { Source = ImageSource.FromResource("AppFom.Images.bg_fom_blelogin.png"), Aspect = Aspect.AspectFill };
            //var BgImage = new Image { Source = ImageSource.FromResource("AppFom.Images.bg_fom_blelogin_red.png"), Aspect = Aspect.AspectFill };
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

            var IcoMapRafaGana = new Image { Source = ImageSource.FromResource("AppFom.Images.img_fom_control.png") };

            if (Fom.Device.IsDroid)
            {
                BgLayout.Children.Add(IcoMapRafaGana,
                                      Constraint.Constant(Fom.Screen.Width / 2 - (439 * .35 / 2)),
                                      Constraint.Constant(Fom.Screen.Height * .07),
                                      Constraint.Constant(439 * .35),
                                      Constraint.Constant(287 * .35)
                             );
            }
            else
            {

                BgLayout.Children.Add(IcoMapRafaGana,
                                      Constraint.Constant(Fom.Screen.Width / 2 - (439 * .35 / 2)),
                                      Constraint.Constant(Fom.Screen.Height * .15),
                                      Constraint.Constant(439 * .35),
                                      Constraint.Constant(287 * .35)
                             );
            }
            // Contruye pantalla
            ScreenBuilder(SlRoot);
            BgLayout.Children.Add(SlRoot,
                             Constraint.Constant(0),
                             Constraint.Constant(Fom.Screen.Height * .02),
                             Constraint.RelativeToParent((Parent) =>
                             {
                                 return Parent.Width;
                             }),
                             Constraint.RelativeToParent((Parent) =>
                             {
                                 return Parent.Height;
                             })
                        );

            this.BindingContext = new VMLogin(this.Navigation);

            Content = BgLayout;
        }


        public void ScreenBuilder(StackLayout root)
        {

            var SlWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 20
            };

            var lblInitSession = new Label
            {
                Text = "Iniciar sesión",
                TextColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };


            var SlWrapUser = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };
            var EntryUser = new CustomEntry
            {
                Placeholder = " Correo electrónica",
                PlaceholderColor = Fom.Colors.UIKitWhite,
                TextColor = Fom.Colors.UIKitWhite
            };
            EntryUser.SetBinding(Entry.TextProperty, "TextUser");
            var SlLineUser = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlWrapUser.Children.Add(EntryUser);
            SlWrapUser.Children.Add(SlLineUser);


            var SlWrapPsw = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };

            var EntryPsw = new CustomEntry
            {
                Placeholder = " Contraseña",
                IsPassword = true,
                PlaceholderColor = Fom.Colors.UIKitWhite,
                TextColor = Fom.Colors.UIKitWhite
            };
            EntryPsw.SetBinding(Entry.TextProperty, "TextPsw");

            var SlLinePsw = new StackLayout
            {
                BackgroundColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1
            };
            SlWrapPsw.Children.Add(EntryPsw);
            SlWrapPsw.Children.Add(SlLinePsw);

            var TgBtnIniSession = new TapGestureRecognizer();
            TgBtnIniSession.SetBinding(TapGestureRecognizer.CommandProperty, "CommandSignIn");
            var ImgBtnIniSession = new Image
            {
                Source = ImageSource.FromResource("AppFom.Images.btn_fom_follow.png"),
                WidthRequest = 671 * .3,
                HeightRequest = 176 * .3
            };

            ImgBtnIniSession.GestureRecognizers.Add(TgBtnIniSession);

            var lblForgetPsw = new Label
            {
                Text = "¿Olvidaste tu contraseña?",
                TextColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Italic,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
            };

            SlWrap.Children.Add(lblInitSession);
            SlWrap.Children.Add(SlWrapUser);
            SlWrap.Children.Add(SlWrapPsw);
            SlWrap.Children.Add(lblForgetPsw);
            SlWrap.Children.Add(ImgBtnIniSession);

            root.Children.Add(SlWrap);
        }

    }
}


