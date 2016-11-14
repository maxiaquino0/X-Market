using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace X_Market.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string LastName { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Adress { get; set; }

        [StringLength(15)]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Document { get; set; }

        [Required(ErrorMessage = "You must enter the field {0}")]
        [Display(Name = "Document Type")]
        public int DocumentTypeID { get; set; }

        public string FullName { get {return string.Format("{0} {1}", LastName, Name); } }

        [JsonIgnore]
        public virtual DocumentType DocumentType { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}