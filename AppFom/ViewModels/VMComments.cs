using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMComments : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private Amazon.S3.Transfer.TransferUtility transferUtility;
        private ObservableCollection<Comentario> comments;
        private int _idevento;

        #endregion


        public VMComments(INavigation navigation, List<Comentario> comentarios, int idevento)
        {
            this.Navigation = navigation;
            Services = new OperationServices();
            comments = new ObservableCollection<Comentario>();
            foreach (var item in comentarios)
            {

                comments.Add(item);
            }
            //comments = comentarios;
            _idevento = idevento;
            LoadDefault();

        }

        private ObservableCollection<Comentario> sourceListActivities;
        public ObservableCollection<Comentario> SourceListActivities
        {
            get { return sourceListActivities; }
            set { SetProperty(ref sourceListActivities, value); }
        }

        public const string TextMessagePropertyName = "TextMessage";
        private string textMessage;
        public string TextMessage
        {
            get { return textMessage; }
            set { SetProperty(ref textMessage, value); }
        }

        Command commandComment;
        public const string CommandCommentPropertyName = "CommandComment";
        public Command CommandComment
        {
            get
            {
                return commandComment ??
                    (commandComment = new Command(async () => await ExecuteCommandComment()));
            }
        }


        public void LoadDefault()
        {
            sourceListActivities = comments;
            textMessage = "";
        }

        public async Task ExecuteCommandComment()
        {
            if (string.IsNullOrEmpty(textMessage))
            {

                UserDialogs.Instance.Alert("Debes agregar un comentario", "Mensaje", "Aceptar");
            }
            else
            {

                UserDialogs.Instance.ShowLoading();

                var comment = new
                {

                    comentario = textMessage,
                    id_evento = _idevento,
                    id_usuario = Fom.Globals.USERFOM.id_usuario,
                    fecha_comentario = DateTime.Now.ToString("dd/MM/yyyy")

                };

                var result = Services.AddComment(comment);

                comments.Add(new Comentario()
                {

                    descripcion = textMessage,
                    nombre = Fom.Globals.USERFOM.nombre,
                    url_avatar = Fom.Globals.USERFOM.url_avatar
                });

                sourceListActivities = comments;
                SourceListActivities = sourceListActivities;

                textMessage = "";
                TextMessage = textMessage;
                OnPropertyChanged(TextMessagePropertyName);

                UserDialogs.Instance.HideLoading();
            }
        }
    }
}
