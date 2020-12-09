using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_WebService.Models {
    public class Product : BaseEntity {
        public string Description { get; set; }
        public double Price { get; set; }
    }
}