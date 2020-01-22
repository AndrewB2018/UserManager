using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UserManager.Data.Repositories.Interface;
using UserManager.DataEntities.Models;
using UserManager.Services.Interface;

namespace UserManager.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IGroupRepository _groupRepository;

        public UserService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
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

        // Could be moved to a specific validation service
        public Dictionary<string, string> ValidateUser(User user)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            if (GetUserByUsername(user.Username) != null)
            {
                errors.Add("Username", "Username already exists");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if(hasNumber.IsMatch(user.Password) && hasUpperChar.IsMatch(user.Password) && hasMinimum8Chars.IsMatch(user.Password))
            {
                // Good password
            }
            else
            {
                errors.Add("Password", "Password should be minimum 8 characters in length and contain both letters and numbers, at least one uppercase letter.");
            }           

            return errors;
        }

        public void UpdateUserGroups(string[] selectedGroups, User userToUpdate)
        {
            if (selectedGroups == null)
            {
                if (userToUpdate.UserGroups.Any())
                {
                    foreach (UserGroup userGroupToRemove in userToUpdate.UserGroups)
                    {
                        _userRepository.DeleteUsersGroup(userGroupToRemove);
                    }
                }

                return;

            }

            var selectedGroupsHS = new HashSet<string>(selectedGroups);
            var userGroups = new HashSet<int>
                (userToUpdate.UserGroups.Select(c => c.GroupId));
            foreach (var group in _groupRepository.GetGroups())
            {
                if (selectedGroupsHS.Contains(group.GroupId.ToString()))
                {
                    if (!userGroups.Contains(group.GroupId))
                    {
                        userToUpdate.UserGroups.Add(new UserGroup { GroupId = group.GroupId, UserId = userToUpdate.UserId });
                    }
                }
                else
                {
                    if (userGroups.Contains(group.GroupId))
                    {
                        UserGroup userGroupToRemove = userToUpdate.UserGroups.FirstOrDefault(x => x.GroupId == group.GroupId); //returns a single item.

                        if (userGroupToRemove != null)
                        {
                            _userRepository.DeleteUsersGroup(userGroupToRemove);
                        }
                    }
                }
            }
        }
    }
}