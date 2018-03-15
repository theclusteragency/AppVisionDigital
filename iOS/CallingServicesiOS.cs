using System;
using System.Threading.Tasks;
using AppFom.Interfaces;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AppFom.iOS.CallingServicesiOS))]
namespace AppFom.iOS
{
    public class CallingServicesiOS : ICallingServices
    {
        public CallingServicesiOS() { }

        public async Task<bool> CallingNumber(string number)
        {
            //Makes a new NSUrl
            var callURL = new NSUrl("tel:" + number);

            if (UIApplication.SharedApplication.CanOpenUrl(callURL))
            {
                //After checking if phone can open NSUrl, it either opens the URL or outputs to the console.

                UIApplication.SharedApplication.OpenUrl(callURL);

            }
            else
            {
                //OUTPUT to console

                Console.WriteLine("Can't make call");
            }

            return true;
        }
    }
}
