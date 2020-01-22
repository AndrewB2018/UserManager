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
            IEnumerable<User> users = db.Users;

            return users;
        }

        public User GetUserById(int id)
        {
            User user = db.Users
                   .Include(i => i.UserGroups)
                   .Where(i => i.UserId == id)
                   .Single();

            return user;
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            //db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void DeleteUsersGroup(UserGroup userGroup)
        {
            db.UserGroups.Remove(userGroup);
            db.SaveChanges();
        }



    }
}