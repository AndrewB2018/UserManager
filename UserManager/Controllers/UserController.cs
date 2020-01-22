﻿using System.Net;
using System.Web.Mvc;
using System;
using UserManager.Services.Interface;
using UserManager.DataEntities.Models;
using UserManager.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace UserManager.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;
        private IGroupService _groupService;

        public UserController(IUserService userService, IGroupService groupService)
        {
            _userService = userService;
            _groupService = groupService;
        }

        // GET: User
        public ActionResult Index()
        {
            try
            {
                return View(_userService.GetUsers());
            }
            catch (Exception e)
            {
                throw new Exception("Error loading user index page", e);
            }
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _userService.GetUserById((int)id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Would normally put this in a view model but adding to viewbag to save time
            ViewBag.Groups = _groupService.GetGroups();

            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Username,Password,PasswordStrength,FirstName,LastName,DateOfBirth,Email,Phone,Mobile")] User user, string[] selectedGroups)
        {
            ModelState.Merge(_userService.ValidateUser(user), "");

            if (ModelState.IsValid)
            {
                _userService.CreateUser(user);
                _userService.UpdateUserGroups(selectedGroups, user);

                _userService.UpdateUser(user);

                return RedirectToAction("Index");
                
            }

            ViewBag.Groups = _groupService.GetGroups();
            ViewBag.SelectedGroups = new List<int>(user.UserGroups.Select(c => c.GroupId));

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = _userService.GetUserById((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Would normally put this in a view model but adding to viewbag to save time
            ViewBag.Groups = _groupService.GetGroups();
            ViewBag.SelectedGroups = new List<int>(user.UserGroups.Select(c => c.GroupId));

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Username,Password,FirstName,LastName,DateOfBirth,Email,Phone,Mobile")] User user, string[] selectedGroups)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = _userService.GetUserById(user.UserId);

                if (TryUpdateModel(userToUpdate))
                {
                    _userService.UpdateUserGroups(selectedGroups, userToUpdate);

                    _userService.UpdateUser(user);
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Groups = _groupService.GetGroups();
            ViewBag.SelectedGroups = new List<int>(user.UserGroups.Select(c => c.GroupId));

            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _userService.GetUserById((int)id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _userService.DeleteUser(id);

            return RedirectToAction("Index");
        }

    }
}
