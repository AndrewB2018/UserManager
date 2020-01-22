using System;
using System.Collections.Generic;
using System.Linq;
using UserManager.Data.Repositories.Interface;
using UserManager.DataEntities.Models;
using UserManager.Services.Interface;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int id)
        {
            try
            {
                User user = _userRepository.GetUserById(id);
                return user;
            }
            catch(Exception e)
            {
                throw new Exception("Failed at GetUserById in UserService", e);
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                List<User> users = _userRepository.GetUsers().ToList();
                return users;
            }
            catch (Exception e)
            {
                throw new Exception("Failed at GetUsers in UserService", e);
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                _userRepository.CreateUser(user);
            }
            catch (Exception e)
            {
                throw new Exception("Failed at CreateUser in UserService", e);
            }
        }

        public User GetUserByUsername(string username)
        {
            try
            { 
                IEnumerable<User> users = GetUsers();

                User user = users.FirstOrDefault(x => x.Username == username);

                return user;
            }
            catch (Exception e)
            {
                throw new Exception("Failed at GetUserByUsername in UserService", e);
            }
        }

        public void UpdateUser(User user)
        {
            try
            { 
                _userRepository.UpdateUser(user);
            }
            catch (Exception e)
            {
                throw new Exception("Failed at UpdateUser in UserService", e);
            }
        }

        public void DeleteUser(int id)
        {
            try
            { 
                _userRepository.DeleteUser(id);
            }
            catch (Exception e)
            {
                throw new Exception("Failed at DeleteUser in UserService", e);
            }
        }
    }
}