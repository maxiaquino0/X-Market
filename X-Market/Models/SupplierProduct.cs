using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace X_Market.Models
{
    public class SupplierProduct
    {
        [Key]
        public int SupplierProductID { get; set; }

        public int SupplierID { get; set; }

        public int ProductID { get; set; }

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; }

        [JsonIgnore]
        public virtual Product Product { get; set; }
    }
}