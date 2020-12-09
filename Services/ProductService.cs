using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_WebService.Models;
using TP_WebService.ModelsDto;
using TP_WebService.Repositories;

namespace TP_WebService.Services {
    public class ProductService : GenericService<Product, ProductDto> {
        private IProductRepository productRepository;
        public ProductService(IGenericRepository<Product> genericRepository) 
            : base(genericRepository) {
        }

        public ProductService(IProductRepository productRepository, IGenericRepository<Product> genericRepository) 
            : base(genericRepository) {
            this.productRepository = productRepository;
        }
    }
}