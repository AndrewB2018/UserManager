using System.Collections.Generic;
using System.Data.Entity;
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
            User user = db.Users.Find(id);

            return user;
        }

        public void CreateUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
        }

    }
}