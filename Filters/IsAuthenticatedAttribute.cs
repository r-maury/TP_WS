using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TP_WebService.ModelsDto;
using TP_WebService.Repositories;
using TP_WebService.Services;
using TP_WebService.Tools;

namespace TP_WebService.Filters {
    public class IsAuthenticatedAttribute : ActionFilterAttribute {
        private MyDbContext db = new MyDbContext();
        private UserService _userService;

        public IsAuthenticatedAttribute() {
            _userService = new UserService(new UserRepository(db));
        }

        public override void OnActionExecuting(HttpActionContext actionContext) {
            var tokenWithBearer = actionContext.Request.Headers.Authorization;
            var token = tokenWithBearer.ToString().Substring(7);
            string userToken = TokenManager.ValidateToken(token.ToString());
            var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) {
                Content = new StringContent("You must be connected to perform this action"),
                ReasonPhrase = "Access Denied"
            };
            //Verif du token
            if(userToken == null) {
                throw new HttpResponseException(resp);
            } else {
                UserDto u = _userService.FindByEmail(userToken);
                if(u == null || (u != null && !u.Email.Equals(userToken))) {
                    throw new HttpResponseException(resp);
                }
            }
        }
    }
}