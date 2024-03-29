﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Telephony;
using AppFom.Interfaces;
using Xamarin.Forms;
using Uri = Android.Net.Uri;

[assembly: Xamarin.Forms.Dependency(typeof(AppFom.Droid.CallingServicesDroid))]
namespace AppFom.Droid
{
    public class CallingServicesDroid : ICallingServices
    {
        public CallingServicesDroid() { }

        public async Task<bool> CallingNumber(string number)
        {
            Debug.WriteLine("Llamar desde Droid: " + number);
            var context = Forms.Context;
            if (context == null)
                return false;

            var intent = new Intent(Intent.ActionCall);
            intent.SetData(Uri.Parse("tel:" + number));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        public static bool IsIntentAvailable(Context context, Intent intent)
        {
            var packageManager = context.PackageManager;

            var list = packageManager.QueryIntentServices(intent, 0)
                .Union(packageManager.QueryIntentActivities(intent, 0));

            if (list.Any())
                return true;

            var manager = TelephonyManager.FromContext(context);
            return manager.PhoneType != PhoneType.None;
        }

    }
}

