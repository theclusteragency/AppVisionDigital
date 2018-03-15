using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AppFom.Helpers;
using AppFom.Interfaces;
using AppFom.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppFom.Implementations
{
    public class OperationServices : IOperationServices
    {
        public OperationServices() { }

        public async Task<OperResult<User>> CheckLogin<T>(T generic)
        {
            var responseObj = new OperResult<User>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));
            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.loginURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    responseObj = JsonConvert.DeserializeObject<OperResult<User>>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = null;
            }

            return responseObj;
        }

        public async Task<OperResult<List<Event>>> GetEvents()
        {

            var responseObj = new OperResult<List<Event>>();
            //var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().GetAsync(Endpoints.allEventsURI);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);

                    responseObj = JsonConvert.DeserializeObject<OperResult<List<Event>>>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = null;
            }

            return responseObj;
        }

        public async Task<OperResult<EventDetail>> GetEventDetail(int idevento)
        {

            var generic = new { id_evento = idevento };
            var responseObj = new OperResult<EventDetail>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.eventDetailURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    responseObj = JsonConvert.DeserializeObject<OperResult<EventDetail>>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = null;
            }

            return responseObj;
        }

        public async Task<OperResult<bool>> UpdateUser(User user)
        {
            var responseObj = new OperResult<bool>();
            Debug.WriteLine(JsonConvert.SerializeObject(user));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.updUserURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    //responseObj = JsonConvert.DeserializeObject<OperResult<User>>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = false;
            }

            return responseObj;
        }


        public async Task<OperResult<List<Event>>> GetOperEvents()
        {

            var generic = new { id_operador = Fom.Globals.USERFOM.id_usuario };
            var responseObj = new OperResult<List<Event>>();
            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().GetAsync(Endpoints.allEventsURI);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);

                    responseObj = JsonConvert.DeserializeObject<OperResult<List<Event>>>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = null;
            }

            return responseObj;
        }


        public async Task<OperResult<int>> AddComment<T>(T generic)
        {

            var responseObj = new OperResult<int>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.addCommentURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    //responseObj = JsonConvert.DeserializeAnonymousType(json,anonimo );
                    responseObj.code = 0;
                    responseObj.message = "Exito";
                    responseObj.data = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = 100;
            }

            return responseObj;

        }

        public async Task<OperResult<int>> AddPhoto<T>(T generic)
        {

            var responseObj = new OperResult<int>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.addPhotoURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    //responseObj = JsonConvert.DeserializeAnonymousType(json,anonimo );
                    responseObj.code = 0;
                    responseObj.message = "Exito";
                    responseObj.data = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = 100;
            }

            return responseObj;

        }

        public async Task<OperResult<int>> UpdStatusEvent<T>(T generic)
        {

            var responseObj = new OperResult<int>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.updStatusEventURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    //responseObj = JsonConvert.DeserializeAnonymousType(json,anonimo );
                    responseObj.code = 0;
                    responseObj.message = "Exito";
                    responseObj.data = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = 100;
            }

            return responseObj;
        }

        public async Task<OperResult<int>> UpdStatusActivity<T>(T generic)
        {

            var responseObj = new OperResult<int>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.updActivityURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    //responseObj = JsonConvert.DeserializeAnonymousType(json,anonimo );
                    responseObj.code = 0;
                    responseObj.message = "Exito";
                    responseObj.data = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = 100;
            }

            return responseObj;
        }

        public async Task<OperResult<List<User>>> getAllUsers()
        {

            var responseObj = new OperResult<List<User>>();
            //var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().GetAsync(Endpoints.getUsersURI);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);

                    responseObj = JsonConvert.DeserializeObject<OperResult<List<User>>>(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = null;
            }

            return responseObj;
        }

        public async Task<OperResult<List<Comentario>>> getChatComments<T>(T generic)
        {

            var responseObj = new OperResult<List<Comentario>>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.getChat, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    responseObj = JsonConvert.DeserializeObject<OperResult<List<Comentario>>>(json);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = null;
            }

            return responseObj;
        }

        public async Task<OperResult<int>> AddChatComment<T>(T generic)
        {

            var responseObj = new OperResult<int>();
            Debug.WriteLine(JsonConvert.SerializeObject(generic));

            var contentHttp = new StringContent(JsonConvert.SerializeObject(generic), Encoding.UTF8, "application/json");

            try
            {
                var response = await GetCustomHttpClient().PostAsync(Endpoints.addChatCommentURI, contentHttp);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Content: " + json);
                    //responseObj = JsonConvert.DeserializeAnonymousType(json,anonimo );
                    responseObj.code = 0;
                    responseObj.message = "Exito";
                    responseObj.data = 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception Message:" + ex.Message);
                responseObj.message = ex.Message;
                responseObj.code = 100;
                responseObj.data = 100;
            }

            return responseObj;
        }


        #region HTTPCLIENTE

        public HttpClient GetCustomHttpClient()
        {

            var _handler = new HttpClientHandler()
            {
                //Credentials = new System.Net.NetworkCredential("x-api-key", Liar.Globals.APIKEY)
            };

            var http = new HttpClient(_handler) { MaxResponseContentBufferSize = 256000 };
            http.DefaultRequestHeaders.Add("x-api-key", Fom.Globals.APIKEY);
            return http;
        }

        #endregion

    }
}
