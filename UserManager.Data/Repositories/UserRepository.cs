using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UserManager.Data.Context;
using UserManager.Data.Repositories.Interface;
using UserManager.DataEntities.Models;

namespace UserManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserManagerContext db = new UserManagerContext();

        public IEnumerable<User> GetUsers()
        {
            try
            { 
                IEnumerable<User> users = db.Users;

                return users;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting users from database", e);
            }
        }

        public User GetUserById(int id)
        {
            try
            { 
                User user = db.Users
                       .Include(i => i.UserGroups)
                       .Where(i => i.UserId == id)
                       .Single();

                return user;
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting user by id:" +id, e);
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
            
            }
            catch (Exception e)
            {
                throw new Exception("Error creating a new user in the database", e);
            }
        }

        public void UpdateUser(User user)
        {
            //db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            try
            { 
                User user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting a user from database", e);
            }
        }

        public void DeleteUsersGroup(UserGroup userGroup)
        {
            try
            { 
                db.UserGroups.Remove(userGroup);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting a users group from database", e);
            }
        }



    }
}