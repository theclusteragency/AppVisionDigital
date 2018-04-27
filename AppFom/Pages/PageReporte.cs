using System;
using AppFom.Helpers;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageReporte : ContentPage
    {
        private WebView webViewView;
        private ActivityIndicator activityIndicator;
        private ContentView isWorkingView;
        private StackLayout Sl_Wrap;
        private bool IsLoaded;
        private StackLayout slRoot = new StackLayout() { Padding = new Thickness(20, 0, 20, 20), Spacing = 0 };
        private RelativeLayout rlRoot = new RelativeLayout();


        public PageReporte()
        {
            //NavigationPage.SetHasNavigationBar(this, false);

            // Screen Builder
            ScreenBuilder(slRoot);
            rlRoot.Children.Add(slRoot,
                             Constraint.Constant(0),
                             Constraint.Constant(0),
                                Constraint.Constant(Fom.Screen.Width),
                               Constraint.Constant(Fom.Screen.Height)
                        );




            Content = rlRoot;

        }

        public async void ScreenBuilder(StackLayout layout)
        {
            IsLoaded = false;

            activityIndicator = new ActivityIndicator()
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                IsRunning = true
            };

            isWorkingView = new ContentView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = activityIndicator,
                IsVisible = true
            };


            webViewView = new WebView()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Source = "https://visiondigitaledumex.com/evaluaciones",
                IsVisible = false
            };


            webViewView.Navigating += (sender, e) =>
            {
                if (!IsLoaded)
                {
                    isWorkingView.IsVisible = true;
                    webViewView.IsVisible = false;
                }
            };

            webViewView.Navigated += (sender, e) =>
            {
                isWorkingView.IsVisible = false;
                webViewView.IsVisible = true;
                IsLoaded = true;
            };



            Sl_Wrap = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            Sl_Wrap.Children.Add(webViewView);
            Sl_Wrap.Children.Add(isWorkingView);

            layout.Children.Add(Sl_Wrap);

        }

    }
}



