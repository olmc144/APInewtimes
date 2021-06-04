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
        [Route("api/SearchTime/{idciudad}")]
        public IHttpActionResult Gettables(int idciudad)
        {
            news_timeEntities3 sd = new news_timeEntities3();
            //List<Ciudades> lciudades = sd.Ciudades.ToList();
            //List<Noticia> lnoticias = sd.Noticia.ToList();

            var id_ciudad = Convert.ToInt32(idciudad);

            var query = from c in sd.Ciudades
                        join n in sd.Noticia on c.CiudadID equals n.IDCiudad                    
                        where n.IDCiudad == id_ciudad
                        select new NoticiaTime
                        {
                            //CiudadID = c.CiudadID,
                            //NombreCiudad = c.CiudadNombre,
                            //Autornoticia = n.autor,
                            //TitleNoticia = n.title,
                            //DescriptionNoticia = n.description,
                            //UrlNoticia = n.url,
                            //UrlToImage = n.urlToImage,
                            //PublishedAt = (DateTime)n.publishedAt,
                            //Content = n.content
                        };            
                       

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
                                     weather_descriptions = new List<Weatherdescriptions>()
                                     {
                                         new Weatherdescriptions() { description = t.weather_descriptions }
                                     }
                                 }                            
                             ).ToList()
                         }).FirstOrDefault();

            HistorialsController historialsController = new HistorialsController();                        

            return Ok(query);
        }
    }
}
