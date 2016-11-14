﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_Market.Models
{
    public class OrderAPI
    {
        public int OrderID { get; set; }

        public Customer Customer { get; set; }

        public DateTime DateOrder { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

    }
}