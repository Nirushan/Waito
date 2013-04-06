using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Waito.Models
{
    public class Page
    {
        public int PageId { get; set; }        
        [Required]
        public string PageName { get; set; }
        [AllowHtml]        
        public string PageContent { get; set; }
    }
}