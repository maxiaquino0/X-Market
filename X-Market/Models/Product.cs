using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace X_Market.Models
{
    public class Product
    {
        [Key]
        [Display(Name ="Product")]
        public int ProductID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "You must enter the field {0}")]        
        public string Description { get; set; }

        [Required(ErrorMessage = "You must enter the field {0}")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString ="{0:N2}", ApplyFormatInEditMode =false)]
        [DataType(DataType.Currency)]
        public float Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}