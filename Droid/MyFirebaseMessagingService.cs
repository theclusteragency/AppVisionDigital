using System;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Firebase.Messaging;

namespace AppFom.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";
        public override void OnMessageReceived(RemoteMessage message)
        {
            Debug.WriteLine("From:    " + message.From);
            Debug.WriteLine("Message: " + message.GetNotification().Title);
            Debug.WriteLine("Message: " + message.GetNotification().Body);

            SendNotification(message.GetNotification().Title, message.GetNotification().Body);

        }

        /// <summary>
        /// Creamos una notificación en el teléfono
        /// </summary>
        /// <param name="message">Mensaje a mostrar.</param>
        /// <param name="activityType">Tipo de Actividad a crear.</param>
        /// <param name="notificationExtra">Información extra de la notificación.</param>
        void SendNotification(string title, string message)
        {

            var notificationBuilder = new Notification.Builder(this)
                                                      .SetSmallIcon(Resource.Drawable.icon)
                                                      .SetContentTitle(title)
                                                      .SetContentText(message)
                                                      .SetAutoCancel(true)
                                                      .SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate)
                                                      .SetPriority(2);

            var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
            notificationManager.Notify(0, notificationBuilder.Build());


        }

        /// <summary>
        /// Crea un nuevo Intent. para las notificaciones que lo requieran
        /// </summary>
        /// <returns>The intent.</returns>
        /// <param name="activityType">Tipo de actividad a crear.</param>
        /// <param name="notificationExtra">Información extra de la notificación.</param>
        Intent CreateIntent(int activityType, string notificationExtra)
        {
            Intent result = null;
            if (activityType >= 0)
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                intent.AddCategory("NOTIFICATION");

                if (!string.IsNullOrEmpty(notificationExtra))
                {
                    intent.PutExtra("notificationParams", notificationExtra);
                    intent.PutExtra("type", activityType.ToString());
                }
                result = intent;
            }
            return result;
        }
    }
}
