using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TP_WebService.Models {
    public class User : BaseEntity {
        public string Name { get; set; }
        [MaxLength(150)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

        public enum UserRole {
            Customer,
            Sale,
            Manager
        };
    }
}