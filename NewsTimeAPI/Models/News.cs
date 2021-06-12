using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsTimeAPI.Models
{
    public class News
    {
        public NewsT news { get; set; }
        public Currentweather current_weather { get; set; }
    }
}