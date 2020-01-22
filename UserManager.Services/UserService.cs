using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UserManager.Data.Repositories.Interface;
using UserManager.DataEntities.Enum;
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
            try
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
                    // Probably a good password, check strength is right
                    if (user.PasswordStrength == (int)SharedEnum.PasswordStrength.Short)
                    {
                        errors.Add("Password", "Password is too short.");
                    }

                    if (user.PasswordStrength == (int)SharedEnum.PasswordStrength.Weak)
                    {
                        errors.Add("Password", "Password is too weak.");
                    }
                }
                else
                {
                    errors.Add("Password", "Password should be minimum 8 characters in length and contain both letters and numbers, at least one uppercase letter.");
                }
            
            

                return errors;
            }
            catch (Exception e)
            {
                throw new Exception("Error when validating a user in the UserService", e);
            }
        }

        public void UpdateUserGroups(string[] selectedGroups, User userToUpdate)
        {
            try
            {
                // If theres no groups selected then remove any that currently exist and return
                if (selectedGroups == null)
                {
                    if (userToUpdate.UserGroups != null && userToUpdate.UserGroups.Count > 0)
                    {
                        // Cast users to new list so we can interate and delete from the user groups collection
                        List<UserGroup> userGroupsToRemove = userToUpdate.UserGroups.ToList();

                        foreach (UserGroup userGroupToRemove in userGroupsToRemove)
                        {
                            _userRepository.DeleteUsersGroup(userGroupToRemove);
                        }
                    }
                    return;
                }

                // Selected groups
                HashSet<string> selectedGroupsHS = new HashSet<string>(selectedGroups);

                // Empty user groups to add the ones the user previously had to
                HashSet<int> userGroups = new HashSet<int>();

                // Are there any previous groups
                if (userToUpdate.UserGroups != null)
                {
                    userGroups = new HashSet<int>(userToUpdate.UserGroups.Select(c => c.GroupId));
                }
            
                foreach (var group in _groupRepository.GetGroups())
                {
                    if (selectedGroupsHS.Contains(group.GroupId.ToString()))
                    {
                        if (!userGroups.Contains(group.GroupId))
                        {
                            // If a user group doesnt currently exist for this user, create a new list to add to
                            if (userToUpdate.UserGroups == null)
                            {
                                userToUpdate.UserGroups = new List<UserGroup>();
                            }

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
            catch (Exception e)
            {
                throw new Exception("Error updating user groups", e);
            }
        }
    }
}