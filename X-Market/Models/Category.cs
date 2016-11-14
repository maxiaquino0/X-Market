using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace X_Market.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage ="You must enter the fiel {0}.")]
        [Display(Name ="Description")]
        public string Description { get; set; }
    }
}