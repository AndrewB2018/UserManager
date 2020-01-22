using System;
using System.Net;
using System.Web.Mvc;
using UserManager.DataEntities.Models;
using UserManager.Services.Interface;

namespace UserManager.Controllers
{
    public class GroupController : Controller
    {
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: Group
        public ActionResult Index()
        {
            try
            { 
                return View(_groupService.GetGroups());
            }
            catch (Exception e)
            {
                throw new Exception("Error getting groups", e);
            }
        }

        // GET: Group/Details/5
        public ActionResult Details(int? id)
        {
            try
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = _groupService.GetGroupById((int)id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting group details for id: " + id, e);
            }
        }

        // GET: Group/Create
        public ActionResult Create()
        {
            try
            { 
                return View();

            }
            catch (Exception e)
            {
                throw new Exception($"Error getting create page", e);
            }
        }

        // POST: Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupId,GroupName,Description")] Group group)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    _groupService.CreateGroup(group);

                    return RedirectToAction("Index");
                }

                return View(group);
            }
            catch (Exception e)
            {
                throw new Exception($"Error creating a new group", e);
            }
        }

        // GET: Group/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            { 
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = _groupService.GetGroupById((int)id);
                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            catch (Exception e)
            {
                throw new Exception($"Error loading the edit page for id: " + id, e);
            }
        }

        // POST: Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupId,GroupName,Description")] Group group)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    _groupService.UpdateGroup(group);

                    return RedirectToAction("Index");
                }
                return View(group);
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating the group", e);
            }
        }

        // GET: Group/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Group group = _groupService.GetGroupById((int)id);

                if (group == null)
                {
                    return HttpNotFound();
                }
                return View(group);
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting the delete page for id: " + id, e);
            }

        }

        // POST: Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            { 
                _groupService.DeleteGroup(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw new Exception($"Error when attempting to delete group with id: " + id, e);
            }
        }
    }
}
