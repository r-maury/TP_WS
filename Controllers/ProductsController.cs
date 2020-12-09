using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TP_WebService.Filters;
using TP_WebService.ModelsDto;
using TP_WebService.Repositories;
using TP_WebService.Services;

namespace TP_WebService.Controllers {
    [IsAuthenticated]
    public class ProductsController : ApiController {
        private MyDbContext db = new MyDbContext();
        private ProductService _productService;

        public ProductsController() {
            _productService = new ProductService(new ProductRepository(db));
        }

        // GET api/<controller>
        public IEnumerable<ProductDto> Get() {
            return _productService.FindAll(1, 15, p => p.Id, p => p.Id > 1);
        }

        public HttpResponseMessage GetById(int id) {
            ProductDto p = _productService.FindById(id);
            if(p == null) {
                var message = string.Format("User with id = {0} not found", id);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, p);
        }

        [IsManager, IsSale]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] ProductDto p) {
            if(!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            _productService.Insert(p);
            var res = Request.CreateResponse(HttpStatusCode.OK, p);
            res.Headers.Add("Location", "http://localhost:44390/api/products/" + p.Id);
            return res;
        }

        [IsManager, IsSale]
        // PUT api/<controller>/5
        public HttpResponseMessage Put([FromBody] ProductDto p) {
            if(!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            _productService.Update(p);
            var res = Request.CreateResponse(HttpStatusCode.OK, p);
            res.Headers.Add("Location", "http://localhost:44390/api/products/" + p.Id);
            return res;
        }

        [IsManager]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id) {
            ProductDto p = _productService.FindById(id);
            if(p == null) {
                var message = string.Format("User with id = {0} not found", id);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            _productService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, p);
        }
    }
}