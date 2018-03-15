using System;
using Android.App;
using AppFom.Helpers;
using AppFom.Models;
using Firebase.Iid;

namespace AppFom.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FomFirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            System.Diagnostics.Debug.WriteLine("Refreshed token: " + refreshedToken);
            AlmacenaToken(refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }


        async void SendRegistrationToServer(string token)
        {
            System.Diagnostics.Debug.WriteLine("Token :" + token);
            Fom.Globals.TOKENAPNS = token;

            //await SNSUtils.registerWithSNS(SNSUtils.Platform.Android, token);
        }

        /// <summary>
        /// Guarda el token, del dispositivo de forma local, para poder ser usados posteriormente.
        /// </summary>
        /// <param name="token">Token del dispotivo.</param>
        void AlmacenaToken(string token)
        {
            System.Diagnostics.Debug.Write("Token Android:" + token);

            var CachedUser = Fom.Cache.GetCachedObject<User>(CacheKeys.User);
            CachedUser.token = token;

            // Almacenamos datos de usuario en cache
            Fom.Cache.SetCachedObject<User>(CacheKeys.User, CachedUser);
        }
    }
}