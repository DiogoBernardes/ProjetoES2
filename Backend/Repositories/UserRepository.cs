using Backend.Interface;
using BusinessLogic.Context;
using BusinessLogic.Entities;
using BusinessLogic.Models;
using BusinessLogic.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ES2DbContext _context;

        public UserRepository(ES2DbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetUsers()
        {
            return await _context.Set<user>().Select(user => new UserModel()
            {
                ID = user.id,
                Email = user.email,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                Phone = user.phone,
                Role = new RoleModel()
                {
                    ID = user.role.id,
                    Name = user.role.name
                }
            }).ToListAsync();
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            return _context.Set<user>().Select(user => new UserModel()
            {
                ID = user.id,
                Email = user.email,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                Phone = user.phone,
                Role = new RoleModel()
                {
                    ID = user.role.id,
                    Name = user.role.name
                }
            }).FirstOrDefault(u => u.ID == id) ?? throw new InvalidOperationException("User not found!");
        }

        public async Task<UserModel> CreateUser(CreateUserModel entity)
        {
            _context.Set<user>().Add(new user()
            {
                email = entity.Email,
                password = entity.Password,
                name = entity.Name,
                username = entity.Username,
                phone = entity.Phone,
                role_id = entity.Role_id
            });
            await _context.SaveChangesAsync();
            return await GetUserByEmail(entity.Email);
        }

        public async Task UpdateUser(UpdateUserModel user)
        {
            var existingUser = await _context.users.FirstOrDefaultAsync(u => u.id == user.ID);

            if (existingUser == null)
            {
                throw new ArgumentException("User not found");
            }

            existingUser.name = user.Name;
            existingUser.username = user.Username;
            existingUser.password = user.Password;
            existingUser.email = user.Email;
            existingUser.phone = user.Phone;
            existingUser.role_id = user.Role_id;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.id == id);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel?> GetUserByEmail(string email)
        {
            return _context.Set<user>().Select(user => new UserModel()
            {
                ID = user.id,
                Email = user.email,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                Phone = user.phone,
                Role = new RoleModel()
                {
                    ID = user.role.id,
                    Name = user.role.name
                }
            }).FirstOrDefault(u => u.Email.Equals(email));
        }

        public async Task<UserModel?> GetUserByPhone(string phone)
        {
            return _context.Set<user>().Select(user => new UserModel()
            {
                ID = user.id,
                Email = user.email,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                Phone = user.phone,
                Role = new RoleModel()
                {
                    ID = user.role.id,
                    Name = user.role.name
                }
            }).FirstOrDefault(u => u.Phone.Equals(phone));
        }

        public async Task<UserModel?> GetUserByUsername(string username)
        {
            return _context.Set<user>().Select(user => new UserModel()
            {
                ID = user.id,
                Email = user.email,
                Name = user.name,
                Username = user.username,
                Password = user.password,
                Phone = user.phone,
                Role = new RoleModel()
                {
                    ID = user.role.id,
                    Name = user.role.name
                }
            }).FirstOrDefault(u => u.Username.Equals(username));
        }

        public async Task<List<RoleModel>> GetRole()
        {
            return await _context.Set<role>().Select(role => new RoleModel()
            {
                ID = role.id,
                Name = role.name
            }).ToListAsync();
        }
    }
}
