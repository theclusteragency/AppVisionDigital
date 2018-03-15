using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AppFom.Helpers;
using Xamarin;
using ImageCircle.Forms.Plugin.Droid;
using Acr.UserDialogs;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;
using Xamarin.Forms;
using Octane.Xam.VideoPlayer.Android;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.Android.CalendarRenderer))]
namespace AppFom.Droid
{
    [Activity(Label = "Visión Digital", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            FormsVideoPlayer.Init();
            FormsMaps.Init(this, bundle);
            XamForms.Controls.Droid.Calendar.Init();
            ImageCircleRenderer.Init();
            UserDialogs.Init(this);


            //-----------------------------------------------------------//
            //----------------SCREEN WIDTH & HEIGTH----------------------//
            //-----------------------------------------------------------//
            Fom.Screen.Width = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            Fom.Screen.Height = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);

            //-----------------------------------------------------------//
            //---------------SERVICIOS GOOGLE VALIDOS--------------------//
            //-----------------------------------------------------------//
            IsPlayServicesAvailable();

            if (Intent.Extras != null)
            {
                foreach (var key in Intent.Extras.KeySet())
                {
                    var value = Intent.Extras.GetString(key);
                    Log.Debug("Key: {0} Value: {1}", key, value);
                }
            }

            LoadApplication(new App());
        }

        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                    Console.Write(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                else
                {

                    Console.Write("This device is not supported");
                    Finish();
                }
                return false;
            }
            else
            {
                Console.Write("Google Play Services is available.");
                return true;
            }
        }

    }
}
