using System;
namespace AppFom.Models
{
    public class Event
    {

        public int id_evento { get; set; }
        public string descripcion { get; set; }
        public string direccion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string fecha_evento { get; set; }
        public int id_categoria { get; set; }
        public int id_estatus { get; set; }
        public string categoria { get; set; }
        public string estatus { get; set; }
    }
}
