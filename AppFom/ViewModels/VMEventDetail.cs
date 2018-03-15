using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using AppFom.Models;
using AppFom.Pages;
using Plugin.ExternalMaps;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMEventDetail : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private EventDetail _detail;
        #endregion


        public VMEventDetail(INavigation navigation, EventDetail evento)
        {
            this.Navigation = navigation;
            Services = new OperationServices();
            _detail = evento;

            LoadDefault(evento);

        }


        private Color colorDetailEvent;
        public Color ColorDetailEvent
        {
            get { return colorDetailEvent; }
            set { SetProperty(ref colorDetailEvent, value); }
        }

        private string textTitle;
        public string TextTitle
        {
            get { return textTitle; }
            set { SetProperty(ref textTitle, value); }
        }

        private string textCategory;
        public string TextCategory
        {
            get { return textCategory; }
            set { SetProperty(ref textCategory, value); }
        }

        private string textPlace;
        public string TextPlace
        {
            get { return textPlace; }
            set { SetProperty(ref textPlace, value); }
        }

        Command goActivity;
        public const string GoActivityPropertyName = "GoActivity";
        public Command GoActivity
        {
            get
            {
                return goActivity ??
                    (goActivity = new Command(async () => await ExecuteGoActivity()));
            }
        }

        Command goGalery;
        public const string GoGaleryPropertyName = "GoGalery";
        public Command GoGalery
        {
            get
            {
                return goGalery ??
                    (goGalery = new Command(async () => await ExecuteGoGalery()));
            }
        }

        Command goComments;
        public const string GoCommentsPropertyName = "GoComments";
        public Command GoComments
        {
            get
            {
                return goComments ??
                    (goComments = new Command(async () => await ExecuteGoComments()));
            }
        }

        Command commandStart;
        public const string CommandStartPropertyName = "CommandStart";
        public Command CommandStart
        {
            get
            {
                return commandStart ??
                    (commandStart = new Command(async () => await ExecuteCommandStart()));
            }
        }

        Command commandFinish;
        public const string CommandFinishPropertyName = "CommandFinish";
        public Command CommandFinish
        {
            get
            {
                return commandFinish ??
                    (commandFinish = new Command(async () => await ExecuteCommandFinish()));
            }
        }

        Command commandLocation;
        public const string CommandLocationPropertyName = "CommandLocation";
        public Command CommandLocation
        {
            get
            {
                return commandLocation ??
                    (commandLocation = new Command(async () => await ExecuteCommandLocation()));
            }
        }

        public async Task LoadDefault(EventDetail evento)
        {

            UserDialogs.Instance.ShowLoading();
            // Cargamos las valriables
            Color colorBadge = Color.Gray;
            if (evento.evento.estatus == "Programado")
            {
                colorBadge = Color.Green;
            }
            else if (evento.evento.estatus == "Iniciado")
            {
                colorBadge = Color.Blue;
            }
            else if (evento.evento.estatus == "Finalizado")
            {
                colorBadge = Color.Red;
            }

            colorDetailEvent = colorBadge;
            textTitle = evento.evento.descripcion;//"Entrega de muchos libros";
            textCategory = evento.evento.categoria; //"Cultura";
            TextPlace = evento.evento.direccion; //"Polancio homero";


        }

        async Task ExecuteCommandLocation()
        {
            var x = await UserDialogs.Instance.ConfirmAsync("Mostrar ruta al evento ?", "Cancelar", "OK");
            if (x)
            {
                Debug.WriteLine("Mostramos ruta");
                var latitude = Convert.ToDouble(_detail.evento.latitud);
                var longitude = Convert.ToDouble(_detail.evento.longitud);
                var success = await CrossExternalMaps.Current.NavigateTo(
                    _detail.evento.descripcion,
                  latitude,
                  longitude
                );
            }
        }


        async Task ExecuteCommandFinish()
        {
            Debug.WriteLine("Finish Event");

            try
            {

                var x = await UserDialogs.Instance.ConfirmAsync("Seguro que desea terminar el evento ?", "Cancelar", "OK");
                if (x)
                {
                    UserDialogs.Instance.ShowLoading();

                    var generic = new { id_estatus = 3, id_evento = _detail.evento.id_evento };
                    // Terminar evento
                    await Services.UpdStatusEvent(generic);

                    ColorDetailEvent = Color.Red;

                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error en login : " + ex.Message);
                UserDialogs.Instance.HideLoading();
            }
        }

        async Task ExecuteCommandStart()
        {
            Debug.WriteLine("Start Event");

            try
            {
                var x = await UserDialogs.Instance.ConfirmAsync("Seguro que desea iniciar el evento ?", "Cancelar", "OK");
                if (x)
                {
                    //Iniciar evento
                    UserDialogs.Instance.ShowLoading();

                    var generic = new { id_estatus = 2, id_evento = _detail.evento.id_evento };

                    // Iniciar evento
                    await Services.UpdStatusEvent(generic);

                    ColorDetailEvent = Color.Blue;

                    UserDialogs.Instance.HideLoading();
                }

            }
            catch (Exception ex)
            {

                Debug.WriteLine("Error en login : " + ex.Message);
                UserDialogs.Instance.HideLoading();
            }

        }

        async Task ExecuteGoComments()
        {
            Debug.WriteLine("Ir a comentarios");

            await this.Navigation.PushAsync(new PageComments(_detail.comentarios, _detail.evento.id_evento));

        }

        async Task ExecuteGoGalery()
        {
            Debug.WriteLine("Ir a galeria");

            //var pics = new List<Foto>(){ new Foto { usuario = "Pedro", url_foto = "http://lorempixel.com/100/100/" },
            //new Foto { usuario = "Luis", url_foto = "http://lorempixel.com/100/100/" } };

            await this.Navigation.PushAsync(new PageGalery(_detail.fotos, _detail.evento.id_evento));
            //await this.Navigation.PushAsync(new PageGalery(pics, _detail.evento.id_evento));

        }

        async Task ExecuteGoActivity()
        {
            Debug.WriteLine("Ir a Actividades");

            await this.Navigation.PushAsync(new PageActivities(_detail.actividades));
        }
    }
}
