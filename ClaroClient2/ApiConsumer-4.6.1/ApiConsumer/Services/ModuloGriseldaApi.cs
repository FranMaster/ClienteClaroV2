
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiConsumer.Services
{
    using ApiConsumer.Services.Modulogriselda.login.Request;
    using ApiConsumer.Services.Modulogriselda.login.Response;
    using ApiConsumer.Services.Modulogriselda.Recarags.Request;
    using ApiConsumer.Services.Modulogriselda.Recarags.Response;
    using ApiConsumer.Services.Modulogriselda.RecaragsListados.Response;
    using System.Threading.Tasks;

    public class ModuloGriseldaApi:BaseService
    {
        public GenericResponse<UserResponse> LogIN(UserRequest Usuario)
        {
       
            string uri = "https://backendclarov2.herokuapp.com";
            string controller = "pcr/login";
            return base.Post<UserResponse>(uri, controller, Usuario,null,"application/x-www-form-urlenconded");
        }

        public GenericResponse<GetListadoResponse> ObtenerRecargas(GetRecargasRequest email,string token)
        {
            string uri = "https://backendclarov2.herokuapp.com";
            string controller = "recarga/listRecargas";
            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("token", $"{token}");
            return base.Post<GetListadoResponse>(uri, controller,email , headers, "application/x-www-form-urlenconded");
        }

        public GenericResponse<RecargaResponse> InsertarRecarga(InsertRecargasRequest datos,string token)
        {
            string uri = "https://backendclarov2.herokuapp.com";
            string controller = "recarga/add";
            IDictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("token", $"{token}");
            return base.Post<RecargaResponse>(uri, controller, datos, headers, "application/x-www-form-urlenconded");
        }

    }


}


namespace ApiConsumer.Services.Modulogriselda.login.Request
{
    public class UserRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }

}
namespace ApiConsumer.Services.Modulogriselda.login.Response
{

    public class Usuario
    {
        [JsonProperty("role")]
        public string role { get; set; }
        [JsonProperty("estado")]
        public bool estado { get; set; }
        [JsonProperty("google")]
        public bool google { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }

    }

    public class Pcr
    {
        [JsonProperty("estado")]
        public bool estado { get; set; }
        [JsonProperty("emailCaminantes")]
        public List<object> emailCaminantes { get; set; }
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("nombre")]
        public string nombre { get; set; }
        [JsonProperty("direccion")]
        public string direccion { get; set; }
        [JsonProperty("pin")]
        public string pin { get; set; }
        [JsonProperty("asignado")]
        public string asignado { get; set; }


    }

    public class Data
    {
        [JsonProperty("usuario")]
        public Usuario usuario { get; set; }
        [JsonProperty("pcr")]
        public Pcr pcr { get; set; }

    }

    public class UserResponse
    {
        [JsonProperty("ok")]
        public bool ok { get; set; }
        [JsonProperty("data")]
        public Data data { get; set; }
        [JsonProperty("token")]
        public string token { get; set; }

    }




}
namespace ApiConsumer.Services.Modulogriselda.Recarags.Response
{
    public class Recarga
    {
        [JsonProperty("_id")]
        public string _id { get; set; }
        [JsonProperty("fechaRecarga")]
        public DateTime fechaRecarga { get; set; }
        [JsonProperty("pcr")]
        public string pcr { get; set; }
        [JsonProperty("mensaje")]
        public string mensaje { get; set; }
     

    }

    public class RecargaResponse
    {
        [JsonProperty("conteo")]
        public int conteo { get; set; }
        [JsonProperty("ok")]
        public bool ok { get; set; }
        [JsonProperty("recarga")]
        public List<Recarga> recarga { get; set; }

    }

}
namespace ApiConsumer.Services.Modulogriselda.Recarags.Request
{
  public  class GetRecargasRequest
    {
        public string email { get; set; }
    }

    public class InsertRecargasRequest
    {

        [JsonProperty("pcr")]
        public string pcr { get; set; }
        [JsonProperty("mensaje")]
        public string mensaje { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }

    }
}
namespace ApiConsumer.Services.Modulogriselda.RecaragsListados.Response
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class GetRecarga
    {
       
        [JsonProperty("fechaRecarga")]
        public DateTime fechaRecarga { get; set; }
 
        [JsonProperty("mensaje")]
        public string mensaje { get; set; }


    }

    public class GetListadoResponse
    {
        [JsonProperty("conteo")]
        public int conteo { get; set; }
        [JsonProperty("ok")]
        public bool ok { get; set; }
        [JsonProperty("recarga")]
        public List<GetRecarga> recarga { get; set; }

    }
}