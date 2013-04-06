using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waito.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Serves { get; set; }        
        [AllowHtml]
        public string Description { get; set; }        
        [AllowHtml]
        public string Ingredient { get; set; }
        public string MediumImage { get; set; }
        public string LargeImage { get; set; }
    }
}