using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsTimeAPI;

namespace ClassLibrary1
{
    public class Class1
    {      
        public string Action()
        {
            NewsTimeAPI.Controllers.HistorialsController nuevo = new NewsTimeAPI.Controllers.HistorialsController();
            var historial = new Historial()
            {
                IDHistorial = 1,
                hstIDciudad = 1,
                info = "
            }
            nuevo.PostHistorial();
            return "ok";
        }
        class List<Historial>
        {
            public int IDHistorial { get; set; }
            public int hstIDciudad { get; set; }
            public string info { get; set; }
        }
    }
}
