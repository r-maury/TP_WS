using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_WebService.Models;

namespace TP_WebService.Repositories {
    public class UserRepository : GenericRepository<User>, IUserRepository {

        public UserRepository(MyDbContext db) : base(db) {

        }
    }
}