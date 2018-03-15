using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class VMGalery : ViewModelMaster
    {
        #region Vars & Properties

        private readonly IOperationServices Services;
        private Amazon.S3.Transfer.TransferUtility transferUtility;
        private ObservableCollection<Foto> photos;
        private int _idevento;
        private string urlPhoto;
        #endregion


        public VMGalery(INavigation navigation, List<Foto> fotos, int idevento)
        {
            this.Navigation = navigation;
            Services = new OperationServices();
            photos = new ObservableCollection<Foto>();
            foreach (var item in fotos)
            {

                photos.Add(item);
            }
            _idevento = idevento;
            LoadDefault();

        }

        private ObservableCollection<Foto> sourceListPhotos;
        public ObservableCollection<Foto> SourceListPhotos
        {
            get { return sourceListPhotos; }
            set { SetProperty(ref sourceListPhotos, value); }
        }


        Command commandPhoto;
        public const string CommandPhotoPropertyName = "CommandPhoto";
        public Command CommandPhoto
        {
            get
            {
                return commandPhoto ??
                    (commandPhoto = new Command(async () => await ExecuteCommandPhoto()));
            }
        }


        public void LoadDefault()
        {
            sourceListPhotos = photos;
        }

        public async Task ExecuteCommandPhoto()
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

                // Update foto galery
                var generic = new
                {
                    id_evento = _idevento,
                    url_foto = urlPhoto,
                    id_usuario = Fom.Globals.USERFOM.id_usuario
                };
                await Services.AddPhoto(generic);


                // Recargamos lista
                var nphot = new Foto
                {

                    id_foto = 0,
                    url_foto = urlPhoto,
                    id_usuario = Fom.Globals.USERFOM.id_usuario,
                    nombre = Fom.Globals.USERFOM.nombre

                };
                sourceListPhotos.Insert(0, nphot);
                SourceListPhotos = sourceListPhotos;

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
            urlPhoto = "https://s3.amazonaws.com/rafaganaimage/" + keyStr;
            Debug.WriteLine("Photo: " + urlPhoto);

            // Cargamos variiable url a obj user
            //Fom.Globals.USERFOM.url_avatar = urlPhoto;

        }
    }
}
