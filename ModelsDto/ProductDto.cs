using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP_WebService.ModelsDto {
    public class ProductDto {
        public int? Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}