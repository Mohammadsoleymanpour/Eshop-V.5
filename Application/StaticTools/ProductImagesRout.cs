using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.StaticTools
{
    public static class ProductImagesRout
    {
        public static string productRout = "wwwroot/Images/img";
        public static string productThumbRout = "wwwroot/Images/thumb";
     
        public static string productServerRout = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/img");
        public static string productThumbServerRout = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/thumb");
    }
}
