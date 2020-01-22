using System.Collections.Generic;
using UserManager.DataEntities.Models;

namespace UserManager.Services.Interface
{
    public interface IGroupService
    {
        List<Group> GetGroups();
        Group GetGroupById(int id);
        void CreateGroup(Group group);
        void UpdateGroup(Group group);
        void DeleteGroup(int id);

    }
}
