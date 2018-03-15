using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using AppFom.Models;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMChatDetail : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private Amazon.S3.Transfer.TransferUtility transferUtility;
        private User admin;
        #endregion


        public VMChatDetail(INavigation navigation, User user)
        {
            this.Navigation = navigation;
            admin = user;
            Services = new OperationServices();

            LoadDefault();
        }

        public const string SourceCommentsPropertyName = "SourceComments";
        private List<Comentario> sourceComments;
        public List<Comentario> SourceComments
        {
            get { return sourceComments; }
            set { SetProperty(ref sourceComments, value); }
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


        public async Task LoadDefault()
        {

            try
            {
                UserDialogs.Instance.ShowLoading();

                var generic = new { usuario1 = admin.id_usuario, usuario2 = Fom.Globals.USERFOM.id_usuario };
                var request = await Services.getChatComments(generic);

                sourceComments = request.data;
                OnPropertyChanged(SourceCommentsPropertyName);

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {

                UserDialogs.Instance.Toast(ex.Message);
            }



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

                    usuario1 = admin.id_usuario,
                    usuario2 = Fom.Globals.USERFOM.id_usuario,
                    usuario_emisor = Fom.Globals.USERFOM.id_usuario,
                    mensaje = textMessage

                };

                var result = await Services.AddChatComment(comment);

                if (result.code == 0)
                {

                    await LoadDefault();

                    textMessage = "";
                    TextMessage = textMessage;
                    OnPropertyChanged(TextMessagePropertyName);
                }
                else
                {

                    UserDialogs.Instance.Toast(result.message);
                }

                UserDialogs.Instance.HideLoading();
            }
        }

    }
}

