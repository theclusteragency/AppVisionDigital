using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using AppFom.MasterDetail;
using AppFom.Models;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMLogin : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private User dataUser;

        #endregion


        public VMLogin(INavigation navigation)
        {
            this.Navigation = navigation;
            Services = new OperationServices();
            dataUser = Fom.Cache.GetCachedObject<User>(CacheKeys.User);

            LoadDefault();

        }

        private string textUser;
        public string TextUser
        {
            get { return textUser; }
            set { SetProperty(ref textUser, value); }
        }

        private string textPsw;
        public string TextPsw
        {
            get { return textPsw; }
            set { SetProperty(ref textPsw, value); }
        }


        Command commandSignIn;
        public const string CommandSignInPropertyName = "CommandSignIn";
        public Command CommandSignIn
        {
            get
            {
                return commandSignIn ??
                    (commandSignIn = new Command(async () => await ExecuteCommandSignIn()));
            }
        }


        public void LoadDefault()
        {

            if (dataUser != null && !string.IsNullOrEmpty(dataUser.correo))
            {

                textUser = dataUser.correo;
                //textPsw = dataUser.contrasena;
            }

        }

        async Task ExecuteCommandSignIn()
        {
            Debug.WriteLine("SignIn");

            try
            {

                if (string.IsNullOrEmpty(textUser) || string.IsNullOrEmpty(textPsw))
                {


                    var toast = new ToastConfig("Debes agregar usuario y contraseña validos.");
                    toast.SetDuration(3000);
                    toast.SetBackgroundColor(System.Drawing.Color.FromArgb(245, 167, 22));
                    toast.SetMessageTextColor(System.Drawing.Color.FromArgb(255, 255, 255));
                    UserDialogs.Instance.Toast(toast);
                }
                else
                {

                    UserDialogs.Instance.ShowLoading("");
                    //CHECK LOGIN
                    var obj = new { usuario = textUser, password = textPsw };
                    var result = await Services.CheckLogin(obj);
                    if (result.code == 0)
                    {
                        result.data.id_rol = 2;
                        Fom.Globals.USERFOM = result.data;
                        Fom.Globals.TOKENAPNS = string.IsNullOrEmpty(result.data.token) ? Fom.Globals.TOKENAPNS : result.data.token;
                        result.data.onSession = true;

                        //// Almacenamos datos de usuario en cache
                        var datUsr = new User
                        {

                            nombre = result.data.nombre,
                            apellido_paterno = result.data.apellido_paterno,
                            apellido_materno = result.data.apellido_materno,
                            correo = result.data.correo,
                            contrasena = textPsw,
                            edad = result.data.edad,
                            nick = result.data.nick,
                            url_avatar = result.data.url_avatar,
                            id_rol = 2,
                            //token      = result.data.nombre, // token sera el de la base
                            id_usuario = result.data.id_usuario,
                            descripcion_rol = result.data.descripcion_rol,
                            es_activo = result.data.es_activo,
                            telefono = result.data.telefono,
                            id_estado = result.data.id_estado,
                            estado = result.data.estado,
                            onSession = true

                        };

                        Fom.Cache.SetCachedObject(CacheKeys.User, datUsr);

                        UserDialogs.Instance.HideLoading();
                        await Navigation.PushModalAsync(new RootPage());

                    }
                    else
                    {

                        UserDialogs.Instance.HideLoading();
                        await UserDialogs.Instance.AlertAsync(result.message, "Mensaje", "Aceptar");
                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error en login : " + ex.Message);
                UserDialogs.Instance.HideLoading();
            }

        }
    }
}
