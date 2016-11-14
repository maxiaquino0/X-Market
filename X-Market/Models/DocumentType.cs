using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace X_Market.Models
{
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }

        [StringLength(30)]
        [Display(Name ="Document Type")]
        [Required(ErrorMessage = "You must enter the field {0}")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Customer> Customers { get; set; }

        [JsonIgnore]
        public ICollection<Employee> Employees { get; set; }
    }
}