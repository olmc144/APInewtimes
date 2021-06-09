using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsTimeAPI.Models
{
    public class NoticiaTime
    {               
        public List<News> news { get; set; }

        public List<Currentweather> current_weather { get; set; }
        
    }

    public class Currentweather
    {
        public DateTime observation_time { get; set; }
        public int temperature { get; set; }

        public List<Weatherdescriptions> weather_descriptions { get; set; }

        public string wind_speed { get; set; }

        public string wind_degree { get; set; }

        public string wind_dir { get; set; }

        public string pressure { get; set; }
    }

    public class Weatherdescriptions
    {
        public string description { get; set; }
    }

    public class News
    {
        public string author { get; set; }

        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }
        public string urlToImage { get; set; }

        public DateTime publishedAt { get; set; }

        public string Content { get; set; }
        
    }    
}