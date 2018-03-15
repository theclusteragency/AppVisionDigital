using System;
using System.Collections.Generic;

namespace AppFom.Models
{
    public class EventDetail
    {

        public Evento evento { get; set; }
        public List<Actividade> actividades { get; set; }
        public List<Operadore> operadores { get; set; }
        public List<Analista> analistas { get; set; }
        public List<Comentario> comentarios { get; set; }
        public List<Foto> fotos { get; set; }

    }
}

public class Evento
{
    public string descripcion { get; set; }
    public int id_evento { get; set; }
    public string direccion { get; set; }
    public string latitud { get; set; }
    public string longitud { get; set; }
    public DateTime fecha_evento { get; set; }
    public int id_categoria { get; set; }
    public int id_estatus { get; set; }
    public string categoria { get; set; }
    public string estatus { get; set; }
}

public class Actividade
{
    public int id_actividad { get; set; }
    public string descripcion { get; set; }
    public int hecha { get; set; }
    public int id_usuario { get; set; }
    public string nombre { get; set; }
    public string apellido_paterno { get; set; }
    public string apellido_materno { get; set; }
    public string url_avatar { get; set; }
    public string valor { get; set; }

}

public class Operadore
{
    public int id_usuaro { get; set; }
    public string nombre { get; set; }
    public string url_avatar { get; set; }
    public int encargado { get; set; }

}

public class Analista
{
    public int id_usuaro { get; set; }
    public string nombre { get; set; }
    public string url_avatar { get; set; }
}

public class Comentario
{
    public object id_comentario { get; set; }
    public object descripcion { get; set; }
    public object nombre { get; set; }
    public object apellido_paterno { get; set; }
    public object apellido_materno { get; set; }
    public object id_usuario { get; set; }
    public object url_avatar { get; set; }
    public object fecha_comentario { get; set; }
    public object mensaje { get; set; }
}

public class Foto
{
    public int? id_foto { get; set; }
    public string url_foto { get; set; }
    public int? id_usuario { get; set; }
    public string apellido_paterno { get; set; }
    public string apellido_materno { get; set; }
    public string nombre { get; set; }
    public string url_avatar { get; set; }
}

