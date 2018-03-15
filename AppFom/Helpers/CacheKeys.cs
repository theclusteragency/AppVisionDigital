using System;
namespace AppFom.Helpers
{
    public static class CacheKeys
    {
        /// <summary>
        /// Llave para acceder al modelo RTDUser
        /// </summary>
        public static readonly string User = "User";


        /// <summary>
        /// Llave para acceder al ARN del dispositivo.
        /// Amazon Resource Name del token para mandar notificaciones
        /// </summary>
        public static readonly string ARN = "ARN";

    }
}