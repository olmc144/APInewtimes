using NewsTimeAPI.Models;
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
            news_timeEntities3 sd = new news_timeEntities3();            

            //estructura para entregar un objeto complejo en json
            var query = (from c in sd.Ciudades
                         //join n in sd.Noticia on c.CiudadID equals n.IDCiudad                         
                         select new NoticiaTime
                         {
                             news = (from j in sd.Noticia
                                     where j.IDCiudad == idciudad
                                     select new News
                                     {
                                         author = j.autor,
                                         title = j.title,
                                         description = j.description,
                                         url = j.url,
                                         urlToImage = j.urlToImage,
                                         publishedAt = (DateTime)j.publishedAt,
                                         Content = j.content
                                     }).ToList(),
                             current_weather = (from t in sd.Estados_tiempo
                                                where t.IDCiudad == idciudad
                                                select new Currentweather
                                 {
                                     observation_time = (DateTime)t.observation_time,
                                     temperature = (int)t.temperature,
                                     pressure = t.pressure,
                                     wind_degree = t.wind_degree,
                                     wind_dir = t.wind_dir,
                                     wind_speed = t.wind_speed,
                                     weather_descriptions = new List<Weatherdescriptions>()
                                     {
                                         new Weatherdescriptions() { description = t.weather_descriptions }
                                     }
                                 }                            
                             ).ToList()
                         }).FirstOrDefault();

            HistorialsController historialsController = new HistorialsController();

            Historial historial = new Historial();
            Ciudades ciudades = new Ciudades();

            //verificamos 

            var ciud = (from ci in sd.Ciudades
                        where ci.CiudadID == idciudad
                        select ci.CiudadNombre).FirstOrDefault();                        
            
            historial.hstIDciudad = idciudad;
            historial.info = ciud;

            historialsController.PostHistorial(historial);
            
                                   

            return Ok(query);
        }
    }
}
