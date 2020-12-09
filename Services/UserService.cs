using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP_WebService.Models;
using TP_WebService.ModelsDto;
using TP_WebService.Repositories;
using TP_WebService.Tools;

namespace TP_WebService.Services {
    public class UserService : GenericService<User, UserDto>, IUserService {
        private IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
            : base(userRepository) {
            this.userRepository = userRepository;
        }

        public UserDto FindByEmail(string email) {
            var z = userRepository.FindBy(u => u.Email.Equals(email)).First<User>();
            return DtoTools.Convert<User, UserDto>(z);
        }

        public UserDto CheckLogin(string email, string password) {
            string msg = "Erreur : identifiants incorrects !";
            //hasher le mot de passe
            string cryptedPwd = HashTools.ComputeSha256Hash(password);

            //récupérer l'utilisateur qui a cet email
            UserDto u = FindByEmail(email);
            if(u == null || !u.Password.Equals(cryptedPwd))
                throw new Exception(msg);

            return u;
        }

        public override void Insert(UserDto t) {
            t.Password = HashTools.ComputeSha256Hash(t.Password);
            base.Insert(t);
        }
    }
}