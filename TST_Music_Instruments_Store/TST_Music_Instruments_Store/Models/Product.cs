using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

public enum Manufacturer
{
    Gibson,
    Fender,
    Shure,
    Yamaha,
    Sennheinser
}

public enum Category
{
    Guitar,
    Violin,
    Viola,
    Cello,
    Bass
}

namespace TST_Music_Instruments_Store.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "You have to set the name of the instrument")]
        [Display(Name = "Name of the instrument")]
        public string NameOfProduct { get; set; }
        [Required(ErrorMessage = "You have to set the instrument category")]
        [Display(Name = "Instrument category")]
        public string ProductCategory { get; set; }
        [Required(ErrorMessage = "You have to set the instrument subcategory")]
        [Display(Name = "Instrument subcategory")]
        public string ProductSubCategory { get; set; }
        [Required(ErrorMessage = "You have to set the string count of the instrument")]
        [Display(Name = "String count")]
        public int StringCount { get; set; }
        [Required(ErrorMessage = "You have to set the string size of the instrument")]
        [Display(Name = "String size")]
        public int StringSize { get; set; }
        [Required(ErrorMessage = "You have to set the price of the instrument")]
        public double Price { get; set; }
        [Required(ErrorMessage = "You have to set the image of the instrument")]
        [Display(Name = "Image")]
        public string ImagePath { get; set; }
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
        [Required(ErrorMessage = "You have to set the manufacturer of the instrument")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "You have to set the color of the instrument")]
        public string Color { get; set; }
        public double Rating { get; set; }

    }
}