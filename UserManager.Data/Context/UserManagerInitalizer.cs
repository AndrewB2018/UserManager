using System;
using System.Collections.Generic;
using System.Data.Entity;
using UserManager.DataEntities.Models;

namespace UserManager.Data.Context
{
    public class UserManagerInitializer : DropCreateDatabaseIfModelChanges<UserManagerContext>
    {
        protected override void Seed(UserManagerContext context)
        {
            var users = new List<User>
            {
            new User{Username="Carson",Password="Password1",FirstName="Dean", LastName="Roy",DateOfBirth=DateTime.Parse("2002-09-01"), Email="test@test.com", Phone="2121132123121", Mobile="321232131232"},
            new User{Username="John",Password="Password2",FirstName="John", LastName="Roy",DateOfBirth=DateTime.Parse("2002-09-01"), Email="test1@test.com", Phone="2121132123121", Mobile="321232131232"},
            new User{Username="Mark",Password="Password3",FirstName="Mark", LastName="Roy",DateOfBirth=DateTime.Parse("2002-09-01"), Email="test2@test.com", Phone="2121132123121", Mobile="321232131232"}

            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            var groups = new List<Group>
            {
            new Group{GroupName="Admin", Description="Admin group"},
            new Group{GroupName="Superuser", Description="Superuser group"},
            new Group{GroupName="Read", Description="Read group"}
            };
            groups.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();

            var userGroups = new List<UserGroup>
            {
            new UserGroup{UserId=1, GroupId=1},
            new UserGroup{UserId=1, GroupId=2},
            new UserGroup{UserId=2, GroupId=2}

            };
            userGroups.ForEach(s => context.UserGroups.Add(s));
            context.SaveChanges();


        }
    }
}