using System;
using System.Collections.Generic;
using System.Linq;
using UserManager.Data.Repositories.Interface;
using UserManager.DataEntities.Models;
using UserManager.Services.Interface;

namespace UserManager.Services
{
    public class GroupService : IGroupService
    {
        private IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public Group GetGroupById(int id)
        {
            try
            {
                Group group = _groupRepository.GetGroupById(id);
                return group;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting group by id at GroupService", e);
            }

        }

        public List<Group> GetGroups()
        {
            try { 
                List<Group> groups = _groupRepository.GetGroups().ToList();
                return groups;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting groups at GroupService", e);
            }
        }

        public void CreateGroup(Group group)
        {
            try
            { 
                _groupRepository.CreateGroup(group);
            }
            catch (Exception e)
            {
                throw new Exception("Error creating a group at GroupService", e);
            }
        }

        public void UpdateGroup(Group group)
        {
            try
            { 
                _groupRepository.UpdateGroup(group);
            }
            catch (Exception e)
            {
                throw new Exception("Error updating a group at GroupService", e);
            }
        }

        public void DeleteGroup(int id)
        {
            try
            { 
                _groupRepository.DeleteGroup(id);
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting a group at GroupService", e);
            }
        }

    }
}