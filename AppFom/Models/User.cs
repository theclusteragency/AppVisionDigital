using System;
namespace AppFom.Models
{
    public class User
    {

        public User() { }

        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string correo { get; set; }
        public string contrasena { get; set; }
        public int edad { get; set; }
        public string nick { get; set; }
        //public int id_rol { get { return 2; } }
        public int id_rol { get; set; }
        public string url_avatar { get; set; }
        public string token { get; set; }
        public int id_usuario { get; set; }
        public string descripcion_rol { get; set; }
        public int es_activo { get; set; }
        public long telefono { get; set; }
        public int id_estado { get; set; }
        public string estado { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public bool onSession { get; set; }
    }
}
