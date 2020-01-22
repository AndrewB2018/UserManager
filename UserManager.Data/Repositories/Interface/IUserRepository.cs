using System.Collections.Generic;
using UserManager.DataEntities.Models;

namespace UserManager.Data.Repositories.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        void DeleteUsersGroup(UserGroup userGroup);
    }
}
