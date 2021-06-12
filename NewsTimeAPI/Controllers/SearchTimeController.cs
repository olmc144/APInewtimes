using NewsTimeAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace NewsTimeAPI.Controllers
{
    public class SearchTimeController : ApiController
    {
        
        private news_timeEntities3 context = new news_timeEntities3();

        [HttpGet]
        [Route("api/SearchNombCiudades/{nomb_ciudad}")]
        public IHttpActionResult GetCiudadesNombre(string nomb_ciudad)
        {
            news_timeEntities3 sd = new news_timeEntities3();                        

            var query = from c in sd.Ciudades                                           
                        where c.CiudadNombre.Contains(nomb_ciudad)
                        select c ;            
                       
            return Ok(query);
        }

        [HttpGet]
        [Route("api/SearchCiudades/{idciudad}")]
        public IHttpActionResult GetCiudades(int idciudad)
        {
            //news_timeEntities3 sd = new news_timeEntities3();            

            ////estructura para entregar un objeto complejo en json
            //var query = (from c in sd.Ciudades
            //             //join n in sd.Noticia on c.CiudadID equals n.IDCiudad                         
            //             select new NoticiaTime
            //             {
            //                 news = (from j in sd.Noticia
            //                         where j.IDCiudad == idciudad
            //                         select new News
            //                         {
            //                             author = j.autor,
            //                             title = j.title,
            //                             description = j.description,
            //                             url = j.url,
            //                             urlToImage = j.urlToImage,
            //                             publishedAt = (DateTime)j.publishedAt,
            //                             Content = j.content
            //                         }).ToList(),
            //                 current_weather = (from t in sd.Estados_tiempo
            //                                    where t.IDCiudad == idciudad
            //                                    select new Currentweather
            //                     {
            //                         observation_time = (DateTime)t.observation_time,
            //                         temperature = (int)t.temperature,
            //                         pressure = t.pressure,
            //                         wind_degree = t.wind_degree,
            //                         wind_dir = t.wind_dir,
            //                         wind_speed = t.wind_speed,
            //                         weather_descriptions = new List<Weatherdescriptions>()
            //                         {
            //                             new Weatherdescriptions() { description = t.weather_descriptions }
            //                         }
            //                     }                            
            //                 ).ToList()
            //             }).FirstOrDefault();

            //HistorialsController historialsController = new HistorialsController();

            //Historial historial = new Historial();
            //Ciudades ciudades = new Ciudades();

            ////verificamos 

            //var ciud = (from ci in sd.Ciudades
            //            where ci.CiudadID == idciudad
            //            select ci.CiudadNombre).FirstOrDefault();                        
            
            //historial.hstIDciudad = idciudad;
            //historial.info = ciud;

            //historialsController.PostHistorial(historial);
            
                                   

            return Ok("");
        }

        [HttpGet]
        [Route("api/ConsultaCiudad/{ciudad}")]
        public IHttpActionResult GetRespuesta(string ciudad)
        {            
            //invocar servicio REST de estado del tiempo
            HttpClient client = new HttpClient();

            //invocar servicio REST de noticias
            HttpClient news_s = new HttpClient();

            //url de la api estado del tiempo

            //q=New York&appid=097b2fe98be3948311bb0556a72f0aed
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //url de la api noticias
            //?country=us&apiKey=48238d7c4eb34e11a50b32c0c41db15b
            news_s.BaseAddress = new Uri("https://newsapi.org/v2/top-headlines");
            news_s.DefaultRequestHeaders.Accept.Clear();
            news_s.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            //valor a consultar por pais en la api noticias
            string country_n = "?country=";

            //para traer una cantidad especifica de noticias se agrega a la url lo siguiente: &pageSize=cantidad

            //key de la api noticias
            string key_n = "&apiKey=48238d7c4eb34e11a50b32c0c41db15b";

            //campos de busquedas para la api
            //?q corresponde a la ciudad a buscar
            string ciudad_q = "?q=";

            //key de la api para poder realizar consultas
            string key = "&appid=097b2fe98be3948311bb0556a72f0aed&lang=es";
            
            //concatenamos toda la url para proceder a general la peticion
            var url_api = String.Concat(ciudad_q, ciudad, key);

            //generamos la peticion GET
            var request = client.GetAsync(url_api).Result;

            if (request.IsSuccessStatusCode)
            {                

                //var resulString = request.Content.ReadAsStringAsync().Result;
                //var estadotiempo = JsonConvert.DeserializeObject<List<CurrentWeather>>(resulString);                
                CurrentWeather current = request.Content.ReadAsAsync<CurrentWeather>().Result;
                

                //concatenamos la url para realizar la peticion de las noticias del pais
                var url_api_n = String.Concat(country_n, current.sys.country, key_n);

                //generamos la peticion GET
                var request_n = news_s.GetAsync(url_api_n).Result;

                //procedemos a leer la respuesta
                NewsT news_w = request_n.Content.ReadAsAsync<NewsT>().Result;

                //se crear un diccionario de objetos para tener todos los datos en uno solo y mandarlo de respuesta
                var data_consol = new Dictionary<string, object>();
                data_consol.Add("news", news_w);
                data_consol.Add("current_weather", current);

                return Ok(data_consol);
            }
            else
            {
                return NotFound();
            }               

        }
    }    
}
