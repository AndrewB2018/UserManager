using System.Collections.Generic;
using UserManager.DataEntities.Models;

namespace UserManager.Data.Repositories.Interface
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetGroups();
        Group GetGroupById(int id);
        void CreateGroup(Group group);
        void UpdateGroup(Group group);
        void DeleteGroup(int id);
    }
}
