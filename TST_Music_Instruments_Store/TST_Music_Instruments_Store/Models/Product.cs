using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TST_Music_Instruments_Store.Models
{
    public class Product
    {
        
        public int ProductsId { get; set; }
        public string NameOfProduct { get; set; }
        public string ProductCategory { get; set; } 
        public string ProductSubCategory { get; set; }
        public int StringCount { get; set; }
        public int StringSize { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
        public HttpPostedFileBase Image { get; set; } 
        public string Manufacturer { get; set; }
        public string Color { get; set; }
        public double Rating { get; set; }

    }
}