using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GUDB.UI.Models
{
    public class SearchEvent
    {



            public DateTime start { get; set; }


       
            public DateTime end { get; set; }



            public double minlong { get; set; }



            public double maxlong { get; set; }



            public double minlat { get; set; }



            public double maxlat { get; set; }



            public double maxlevel { get; set; }



            public double minlevel { get; set; }

        
            public string tid { get; set; }
        }


    
}