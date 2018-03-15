using System;
using Akavache;
using AppFom.Helpers;
using AppFom.MasterDetail;
using AppFom.Models;
using AppFom.Pages;
using Xamarin.Forms;

namespace AppFom
{
    public class App : Application
    {
        public static INavigation INavPage { get; set; }

        public App()
        {
            //Inicializamos cache
            BlobCache.ApplicationName = "ResuelveTuDeuda";
            Fom.Cache.Init();

            var cacheUser = Fom.Cache.GetCachedObject<User>(CacheKeys.User);

            if (cacheUser != null && cacheUser.onSession)
            {

                // Solicitamos datos del usuario y pasamso al root
                Fom.Globals.USERFOM = cacheUser;
                // Pasamos al root
                MainPage = new RootPage();//new NavigationPage(new RootPage());
            }
            else
            {

                // The root page of your application           
                MainPage = new NavigationPage(new PageSession());
            }

            //MainPage = new NavigationPage(new PageTest());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
