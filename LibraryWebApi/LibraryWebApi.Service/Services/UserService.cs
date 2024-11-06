using LibraryWebApi.Domain.Models;
using LibraryWebApi.Repository;
using LibraryWebApi.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryWebApi.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
           
            if (user == null)
                throw new ArgumentNullException(nameof(user));

           
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var existingUser = await _userRepository.GetUserByIdAsync(user.UserID);
            if (existingUser == null)
                throw new KeyNotFoundException($"User with ID {user.UserID} not found.");

            
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

           
            await _userRepository.DeleteUserAsync(id);
        }
    }
}
