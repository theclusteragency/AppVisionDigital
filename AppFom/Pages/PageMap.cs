using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Models;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace AppFom.Pages
{
    public class PageMap : ContentPage
    {
        public PageMap()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                Title = "Mapa";

                var location = await GetCurrentLocation();
                //var services = new OperationServices();

                //// Actualizamos token 
                //var cacheUser = Fom.Cache.GetCachedObject<User>(CacheKeys.User);

                //Fom.Globals.USERFOM.latitud = Convert.ToString(location.Key);
                //Fom.Globals.USERFOM.longitud = Convert.ToString(location.Value);
                //Fom.Globals.USERFOM.token = cacheUser.token;// Lo obtenemos del cache siempre


                //var result = await services.UpdateUser(Fom.Globals.USERFOM);

                //// Pedimos eventos
                //var events = await services.GetOperEvents();

                //if (events.data.Count > 0)
                //Fom.Globals.MISEVENTOS = events.data;

                // Construimos mapa
                var map = new Map(
                    MapSpan.FromCenterAndRadius(
                       new Position(location.Key, location.Value), Distance.FromMiles(10.3)))
                {
                    IsShowingUser = true,
                    HeightRequest = 100,
                    WidthRequest = 960,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                var stack = new StackLayout { Spacing = 0 };

                foreach (var item in Fom.Globals.MISEVENTOS)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(double.Parse(item.latitud, CultureInfo.InvariantCulture), double.Parse(item.longitud, CultureInfo.InvariantCulture)),
                        Label = item.descripcion,
                        Address = item.direccion
                    };

                    pin.Clicked += async (sender, e) =>
                    {

                        await PinSelected(pin, item);
                    };

                    map.Pins.Add(pin);

                }

                stack.Children.Add(map);

                // Slider para zoom 
                var slider = new Slider(1, 18, 1);
                slider.ValueChanged += (sender, e) =>
                {
                    var zoomLevel = e.NewValue; // between 1 and 18
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                    map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
                };
                // stack.Children.Add(slider);

                Content = stack;

            });

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

        async Task PinSelected(Pin pin, Event evento)
        {
            Debug.WriteLine("Navega a evento {0} {1}", evento.id_evento, evento.descripcion);
            await Navigation.PushAsync(new PageEventDetail(evento));
        }


    }
}
