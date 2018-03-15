using System;
using AppFom.Helpers;
using AppFom.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageAccount : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        #endregion


        public PageAccount()
        {
            //NavigationPage.SetHasNavigationBar(this, false);
            Title = "Perfil";
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

            this.BindingContext = new VMAccount(this.Navigation);

            Content = BgLayout;
        }


        public void ScreenBuilder(StackLayout root)
        {

            var SlWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 20
            };

            var SlWrapPhoto = new StackLayout
            {

                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(20)
            };


            var TgCamera = new TapGestureRecognizer();
            TgCamera.SetBinding(TapGestureRecognizer.CommandProperty, "CommandCamera");
            var ImgUser = new CircleImage
            {
                BorderColor = Color.LightGray,

                BorderThickness = 1,
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,

            };
            ImgUser.SetBinding(Image.SourceProperty, "ImgSourceUser");
            ImgUser.GestureRecognizers.Add(TgCamera);

            SlWrapPhoto.Children.Add(ImgUser);

            var SlWrapInfo = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(40, 20, 40, 10),
                Spacing = 10
            };

            var LblName = new Label
            {

                FontAttributes = FontAttributes.Bold,
                TextColor = Fom.Colors.UIKitWhite,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
            };
            LblName.SetBinding(Label.TextProperty, "TextName");

            var LblJob = new Label
            {
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };
            LblJob.SetBinding(Label.TextProperty, "TextDesRol");

            var LblJobDes = new Label
            {
                Text = "Especialista en desarrollo de aplicaciones mobiles con  Xamarin Forms",
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
            };
            LblJobDes.SetBinding(Label.TextProperty, "TextDesJob");

            var LblEmail = new Label
            {

                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),

            };
            LblEmail.SetBinding(Label.TextProperty, "TextEmail");


            SlWrapInfo.Children.Add(LblName);
            SlWrapInfo.Children.Add(LblJob);
            SlWrapInfo.Children.Add(LblJobDes);
            SlWrapInfo.Children.Add(LblEmail);

            SlWrap.Children.Add(SlWrapPhoto);
            SlWrap.Children.Add(SlWrapInfo);

            root.Children.Add(SlWrap);
        }

    }
}


