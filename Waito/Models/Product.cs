using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waito.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]        
        public string Description { get; set; }        
        [AllowHtml]
        public string IngredientList { get; set; }        
        [AllowHtml]
        public string Cooking { get; set; }
        [Required]
        public string ConsumerDetail { get; set; }
        public string MediumImage { get; set; }
        public string LargeImage { get; set; }
    }
}