using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.CellViews;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Models;
using AppFom.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace AppFom.Pages
{
    public class PageEvents : ContentPage
    {
        #region Vars & Properties

        private StackLayout SlRoot = new StackLayout() { Padding = new Thickness(20) };

        #endregion


        public PageEvents()
        {
            try
            {

                UserDialogs.Instance.ShowLoading();

                Device.BeginInvokeOnMainThread(async () =>
                {

                    Title = "Mis Eventos";
                    //NavigationPage.SetHasNavigationBar(this, false);

                    var location = await GetCurrentLocation();
                    var services = new OperationServices();

                    // Actualizamos token 
                    var cacheUser = Fom.Cache.GetCachedObject<User>(CacheKeys.User);

                    Fom.Globals.USERFOM.latitud = Convert.ToString(location.Key);
                    Fom.Globals.USERFOM.longitud = Convert.ToString(location.Value);
                    Fom.Globals.USERFOM.token = cacheUser.token;// Lo obtenemos del cache siempre


                    var result = await services.UpdateUser(Fom.Globals.USERFOM);

                    // Pedimos eventos
                    var events = await services.GetOperEvents();

                    if (events.data.Count > 0)
                    {
                        Fom.Globals.MISEVENTOS = events.data;
                        Fom.VMmenu.UpdateCounter(events.data.Count.ToString());
                    }



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

                    this.BindingContext = new VMCalendarDay(this.Navigation);

                    UserDialogs.Instance.HideLoading();

                    Content = BgLayout;
                });

            }
            catch (Exception ex)
            {

                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }


        public void ScreenBuilder(StackLayout root)
        {

            var slWrap = new StackLayout
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Spacing = 20,
                Padding = new Thickness(0, 0, 0, 0)
            };

            var listEvents = new ListView();
            listEvents.BackgroundColor = Color.Transparent;
            listEvents.ItemTemplate = new DataTemplate(typeof(VCEvent));
            //listEvents.SetBinding(ListView.ItemsSourceProperty, "SelectedEvents");
            listEvents.ItemsSource = Fom.Globals.MISEVENTOS;
            listEvents.SetBinding(ListView.SelectedItemProperty, "SelectedEvents");
            //listEvents.SetBinding(ListView.IsRefreshingProperty, "IsBusy");
            listEvents.RowHeight = 120;
            listEvents.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                ((ListView)sender).SelectedItem = null;
            };
            listEvents.SeparatorColor = Color.Transparent;

            slWrap.Children.Add(listEvents);

            root.Children.Add(slWrap);

        }

        public async Task<KeyValuePair<double, double>> GetCurrentLocation()
        {

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 100;

            var position = await locator.GetLastKnownLocationAsync();

            if (position != null)
            {

                return new KeyValuePair<double, double>(position.Latitude, position.Longitude);
            }
            else
            {

                return new KeyValuePair<double, double>(19.39068, -99.283697);
            }

        }

    }
}


