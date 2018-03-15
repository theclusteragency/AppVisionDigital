using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFom.Helpers;
using AppFom.Pages;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMSession : ViewModelMaster
    {
        public VMSession(INavigation navigation)
        {

            this.Navigation = navigation;
        }


        Command commandGoLogin;
        public const string CommandGoLoginPropertyName = "CommandGoLogin";
        public Command CommandGoLogin
        {
            get
            {
                return commandGoLogin ??
                    (commandGoLogin = new Command(async () => await ExecuteCommandGoLogin()));
            }
        }

        async Task ExecuteCommandGoLogin()
        {
            Debug.WriteLine("Go Loging");
            await Navigation.PushAsync(new PageLogin());
        }



    }
}
