using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Models;
using AppFom.Pages;
using Xamarin.Forms;

namespace AppFom.MasterDetail
{
    public class VMMenuPage : INotifyPropertyChanged
    {

        #region Vars & Properties

        private INavigation Navigation;

        #endregion

        #region Implementa NotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {

            if (PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Contructors

        public VMMenuPage(INavigation navigation)
        {
            this.Navigation = navigation;
            StartLoadDefault();
        }

        #endregion

        #region Bindeos


        private Command command_Close;
        public const string Command_ClosePropertyName = "Command_Close";
        public Command Command_Close
        {
            get
            {
                return command_Close ?? (command_Close = new Command(async () => await ExecuteCommandClose()));
            }
        }

        Command commandAccount;
        public const string CommandAccountPropertyName = "CommandAccount";
        public Command CommandAccount
        {
            get
            {
                return commandAccount ?? (commandAccount = new Command(async () => await ExecuteCommandAccount()));
            }
        }

        private ImageSource source_Photo;
        public const string Source_PhotoPropertyName = "Source_Photo";
        public ImageSource Source_Photo
        {
            get { return source_Photo; }
            set { source_Photo = value; }
        }

        private string textName;
        public const string TextNamePropertyName = "TextName";
        public string TextName
        {
            get { return textName; }
            set { textName = value; }
        }

        private string textDesRoll;
        public const string TextDesRollPropertyName = "TextDesRoll";
        public string TextDesRoll
        {
            get { return textDesRoll; }
            set { textDesRoll = value; }
        }

        private string textCounter;
        public const string TextCounterPropertyName = "TextCounter";
        public string TextCounter
        {
            get { return textCounter; }
            set { textCounter = value; }
        }

        #endregion

        #region Methods

        public void StartLoadDefault()
        {
            Debug.WriteLine("Carga perfil menu");

            source_Photo = string.IsNullOrEmpty(Fom.Globals.USERFOM.url_avatar) ? ImageSource.FromResource("AppFom.Images.img_fom_nopic.png") : ImageSource.FromUri(new Uri(Fom.Globals.USERFOM.url_avatar));
            textName = Fom.Globals.USERFOM.nombre;
            textDesRoll = Fom.Globals.USERFOM.descripcion_rol;

            textCounter = "0";

            OnPropertyChanged(Source_PhotoPropertyName);
            OnPropertyChanged(TextNamePropertyName);
            OnPropertyChanged(TextDesRollPropertyName);

            OnPropertyChanged(TextCounterPropertyName);

        }



        async Task ExecuteCommandClose()
        {

            var x = await UserDialogs.Instance.ConfirmAsync("Seguro que desea cerrar sesión ?", "Cancelar", "OK");
            if (x)
            {
                var CachedUser = Fom.Cache.GetCachedObject<User>(CacheKeys.User);
                CachedUser.onSession = false;

                // Almacenamos datos de usuario en cache
                Fom.Cache.SetCachedObject<User>(CacheKeys.User, CachedUser);

                // Vamos al login
                App.Current.MainPage = new NavigationPage(new PageLogin());
            }

        }

        public async Task ExecuteCommandAccount()
        {

            Debug.WriteLine("Ir a Mi Cuenta");

            App.Current.MainPage = new RootPage("Account");//new NavigationPage(new RootPage("Account"));


        }

        public void UpdateMenuPage()
        {
            source_Photo = ImageSource.FromUri(new Uri(Fom.Globals.USERFOM.url_avatar));
            OnPropertyChanged(Source_PhotoPropertyName);
        }

        public void UpdateCounter(string count)
        {
            textCounter = count;
            OnPropertyChanged(TextCounterPropertyName);
        }

        #endregion
    }
}