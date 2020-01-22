using System;
using System.Collections.Generic;
using System.Data.Entity;
using UserManager.Data.Context;
using UserManager.Data.Repositories.Interface;
using UserManager.DataEntities.Models;

namespace UserManager.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {

        private UserManagerContext db = new UserManagerContext();

        public IEnumerable<Group> GetGroups()
        {
            try
            { 
                IEnumerable<Group> groups = db.Groups;

                return groups;
            }
            catch (Exception e)
            {
                throw new Exception("Error getting groups from database", e);
            }
        }

        public Group GetGroupById(int id)
        {
            try
            { 
                Group group = db.Groups.Find(id);

                return group;
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting group by id: " + id, e);
            }
        }

        public void CreateGroup(Group group)
        {
            try
            { 

                db.Groups.Add(group);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error creating a new group", e);
            }

        }

        public void UpdateGroup(Group group)
        {
            try
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
            
            }
            catch (Exception e)
            {
                throw new Exception("Error updating a group", e);
            }
        }

        public void DeleteGroup(int id)
        {
            try
            { 
                Group group = db.Groups.Find(id);
                db.Groups.Remove(group);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting a group", e);
            }
        }

    }
}