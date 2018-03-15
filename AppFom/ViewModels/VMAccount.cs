using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using AppFom.Helpers;
using AppFom.Implementations;
using AppFom.Interfaces;
using Plugin.Media;
using Xamarin.Forms;

namespace AppFom.ViewModels
{
    public class VMAccount : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private Amazon.S3.Transfer.TransferUtility transferUtility;

        #endregion


        public VMAccount(INavigation navigation)
        {
            this.Navigation = navigation;
            Services = new OperationServices();

            LoadDefault();

        }

        private ImageSource imgSourceUser;
        public ImageSource ImgSourceUser
        {
            get { return imgSourceUser; }
            set { SetProperty(ref imgSourceUser, value); }
        }

        private string textName;
        public string TextName
        {
            get { return textName; }
            set { SetProperty(ref textName, value); }
        }

        private string textDesRol;
        public string TextDesRol
        {
            get { return textDesRol; }
            set { SetProperty(ref textDesRol, value); }
        }

        private string textDesJob;
        public string TextDesJob
        {
            get { return textDesJob; }
            set { SetProperty(ref textDesJob, value); }
        }

        private string textEmail;
        public string TextEmail
        {
            get { return textEmail; }
            set { SetProperty(ref textEmail, value); }
        }


        Command commandCamera;
        public const string CommandCameraPropertyName = "CommandCamera";
        public Command CommandCamera
        {
            get
            {
                return commandCamera ??
                    (commandCamera = new Command(async () => await ExecuteCommandCamera()));
            }
        }


        public void LoadDefault()
        {
            imgSourceUser = string.IsNullOrEmpty(Fom.Globals.USERFOM.url_avatar) ? ImageSource.FromResource("AppFom.Images.img_fom_nopic.png") : ImageSource.FromUri(new Uri(Fom.Globals.USERFOM.url_avatar));
            textName = Fom.Globals.USERFOM.nombre;
            textDesRol = Fom.Globals.USERFOM.descripcion_rol;
            textDesJob = Fom.Globals.USERFOM.descripcion_rol;
            textEmail = Fom.Globals.USERFOM.correo;

        }


        public async Task ExecuteCommandCamera()
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    Debug.WriteLine("No Camera", ":( No camera available.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    //CompressionQuality = 10,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                    Directory = "Sample",
                    Name = "rafagana.jpg"
                });

                if (file == null)
                    return;

                UserDialogs.Instance.ShowLoading("");

                // Upload S3 Foto
                await UploadPhoto(file);

                // Update foto user
                await Services.UpdateUser(Fom.Globals.USERFOM);

                // Cargamos imagen del menu
                Fom.VMmenu.UpdateMenuPage();

                // Recargamos imagen               
                ImgSourceUser = ImageSource.FromUri(new Uri(Fom.Globals.USERFOM.url_avatar));

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }

        }

        async Task UploadPhoto(Plugin.Media.Abstractions.MediaFile file)
        {

            // Obtenemos key de la imagen
            var keyStr = string.Format("{0}{1}{2}{3}{4}.png"
                                       , Fom.Globals.USERFOM.id_usuario
                                       , DateTime.Now.Year
                                       , DateTime.Now.Day
                                       , DateTime.Now.Hour
                                       , DateTime.Now.Minute);

            // Accedemos a S3
            var awsAccessKey = "AKIAJZ7FMVDC6FSODO6A";
            var awsSecretKey = "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";
            var existingBucketName = "rafaganaimage";
            var s3Client = new AmazonS3Client(new BasicAWSCredentials(awsAccessKey, awsSecretKey), RegionEndpoint.USEast1);
            transferUtility = new TransferUtility(s3Client);

            // Subimos archivo a S3
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = file.GetStream(),
                BucketName = existingBucketName,
                CannedACL = S3CannedACL.PublicRead,
                Key = keyStr
            };
            await transferUtility.UploadAsync(uploadRequest);

            // Cargamos imagen en  pantalla
            var urlPhoto = "https://s3.amazonaws.com/rafaganaimage/" + keyStr;
            Debug.WriteLine("Photo: " + urlPhoto);

            // Cargamos variiable url a obj user
            Fom.Globals.USERFOM.url_avatar = urlPhoto;

        }
    }
}
