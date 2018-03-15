using System;
using Xamarin.Forms;

namespace AppFom.Helpers
{
    public class DeviceManager
    {
        /// <summary>
        /// Contine el valor para saber si el dispositivo es un telefono celular
        /// </summary>
        /// <value><c>true</c> si es un telefono celular; de otra forma, <c>false</c>.</value>
        public bool IsPhone
        {
            get
            {
                return Device.Idiom.ToString().Equals("Phone");
            }
        }

        /// <summary>
        /// Retorna true si el sistema operativo del dospositivo es Andorid.
        /// Retorna false si el sistema operativo del dospositivo es iOS.
        /// </summary>
        /// <value>Cadena de texto con el nombre del Sistema Opertivo.</value>
        public bool IsDroid
        {
            get
            {
                return Device.OS.ToString().Equals("Android") ? true : false;
            }
        }

    }
}
