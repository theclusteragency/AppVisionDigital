using System;
namespace AppFom.Helpers
{
    public static class Endpoints
    {

        //public const string EndPointUri = "https://i0mqxn71n1.execute-api.us-west-2.amazonaws.com/dev/"; // FOM
        //public const string EndPointUri = "https://1e4fm5yxil.execute-api.us-west-2.amazonaws.com/digital/"; // FOM BYProvitec
        public const string EndPointUri = "https://1e4fm5yxil.execute-api.us-west-2.amazonaws.com/digital"; // Visión Digital


        //------FOM
        public const string loginURI = EndPointUri + "/login";

        public const string allEventsURI = EndPointUri + "/obtener_eventos";

        public const string eventsOperURI = EndPointUri + "/obtener_eventos_por_operador";

        public const string eventDetailURI = EndPointUri + "/obtener_evento_general";

        public const string updUserURI = EndPointUri + "/actualiza_usuario";

        public const string addCommentURI = EndPointUri + "/agrega_comentario";

        public const string addPhotoURI = EndPointUri + "/agrega_imagen";

        public const string updStatusEventURI = EndPointUri + "/actualiza_evento_status";

        public const string updActivityURI = EndPointUri + "/actualiza_actividad_evento";

        public const string getUsersURI = EndPointUri + "/obtener_usuarios";

        public const string getChat = EndPointUri + "/obtener_mensajes";

        public const string addChatCommentURI = EndPointUri + "/agregar_mensaje";

    }
}
