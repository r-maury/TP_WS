using TP_WebService.Models;
using TP_WebService.ModelsDto;

namespace TP_WebService.Services {
    public interface IUserService : IGenericService<User, UserDto> {
        UserDto CheckLogin(string email, string password);
        UserDto FindByEmail(string email);
    }
}