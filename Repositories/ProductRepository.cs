using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_WebService.Models;

namespace TP_WebService.Repositories {
    public class ProductRepository : GenericRepository<Product>, IProductRepository {
        public ProductRepository(MyDbContext DataContext) : base(DataContext) {

        }
    }
}