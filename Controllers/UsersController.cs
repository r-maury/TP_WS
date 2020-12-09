using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TP_WebService.Models;
using TP_WebService.ModelsDto;
using TP_WebService.Repositories;
using TP_WebService.Services;
using TP_WebService.Tools;

namespace TP_WebService.Controllers {
    public class UsersController : ApiController {
        private MyDbContext db = new MyDbContext();
        private IUserService _userService;

        public UsersController() {
            _userService = new UserService(new UserRepository(db));
        }

        [HttpPost]
        [Route("CheckLogin")]
        public HttpResponseMessage CheckLogin([FromBody] UserDto obj) {

            if(obj == null || obj.Email == null || obj.Password == null) {
                var message = string.Format("Authentication Error");
                return Request.CreateResponse(HttpStatusCode.NotFound, message);
            }

            UserDto item = _userService.CheckLogin(obj.Email, obj.Password);
            if(item == null) {
                var message = string.Format("Authentication Error");
                return Request.CreateResponse(HttpStatusCode.NotFound, message);
            }
            string token = TokenManager.GenerateToken(item.Email);

            return Request.CreateResponse(HttpStatusCode.OK, token);
        }
        public HttpResponseMessage Post([FromBody] UserDto u) {
            if(!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            _userService.Insert(u);
            var rep = Request.CreateResponse(HttpStatusCode.OK, u);
            return rep;

        }
        //// GET api/<controller>
        //public IEnumerable<string> Get() {
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id) {
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value) {
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value) {
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id) {
        //}
    }
}