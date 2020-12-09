using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TP_WebService.Models;
using TP_WebService.ModelsDto;
using TP_WebService.Repositories;
using TP_WebService.Services;
using TP_WebService.Tools;

namespace TP_WebService.Filters {
    public class IsSaleAttribute : ActionFilterAttribute {
        private MyDbContext db = new MyDbContext();
        private UserService _userService;

        public IsSaleAttribute() {
            _userService = new UserService(new UserRepository(db));
        }

        public override void OnActionExecuting(HttpActionContext actionContext) {
            var tokenWithBearer = actionContext.Request.Headers.Authorization;
            var token = tokenWithBearer.ToString().Substring(7);
            string userToken = TokenManager.ValidateToken(token.ToString());
            UserDto u = _userService.FindByEmail(userToken);

            var resp = new HttpResponseMessage(HttpStatusCode.BadRequest) {
                Content = new StringContent("You do not have the right to perform this action"),
                ReasonPhrase = "Access Denied"
            };

            if (!u.Role.Equals(User.UserRole.Sale)) {
                throw new HttpResponseException(resp);
            }
        }
    }
}