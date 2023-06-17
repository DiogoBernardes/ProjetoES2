using BusinessLogic.Entities;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using BusinessLogic.Models.User;

namespace Backend.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetUsers();
        Task<UserModel> GetUser(Guid id);
        Task<UserModel> CreateUser(CreateUserModel entity);
        Task UpdateUser(UpdateUserModel user);
        Task DeleteUser(Guid id);
        Task<UserModel?> GetUserByEmail(string email);
      
    }
}