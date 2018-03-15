using System;
using System.Collections.Generic;
using System.Linq;
using AppFom.Helpers;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Octane.Xam.VideoPlayer.iOS;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using XamForms.Controls.iOS;

[assembly: ExportRenderer(typeof(Telerik.XamarinForms.Input.RadCalendar), typeof(Telerik.XamarinForms.InputRenderer.iOS.CalendarRenderer))]

namespace AppFom.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            new Telerik.XamarinForms.InputRenderer.iOS.CalendarRenderer();

            global::Xamarin.Forms.Forms.Init();
            FormsVideoPlayer.Init();
            Telerik.XamarinForms.Common.iOS.TelerikForms.Init();
            FormsMaps.Init();
            Calendar.Init();
            ImageCircleRenderer.Init();

            //-----------------------------------------------------------//
            //----------------SCREEN WIDTH & HEIGTH----------------------//
            //-----------------------------------------------------------//           
            Fom.Screen.Width = (int)UIScreen.MainScreen.Bounds.Width;
            Fom.Screen.Height = (int)UIScreen.MainScreen.Bounds.Height;



            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
