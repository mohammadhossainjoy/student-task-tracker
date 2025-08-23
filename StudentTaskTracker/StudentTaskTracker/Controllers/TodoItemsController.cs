using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StudentTaskTracker.Models;

namespace StudentTaskTracker.Controllers
{
    public class TodoItemsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: TodoItems
        public ActionResult Index(string q, string sortOrder)
        {
            // keep search text in the box
            ViewBag.CurrentFilter = q;

            // toggle sorting
            ViewBag.TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSort = sortOrder == "date" ? "date_desc" : "date";

            var items = db.TodoItems.AsQueryable();

            // search filter
            if (!String.IsNullOrWhiteSpace(q))
            {
                items = items.Where(t =>
                    (t.Title != null && t.Title.Contains(q)) ||
                    (t.Notes != null && t.Notes.Contains(q))
                );
            }

            // ↕ sorting logic
            switch (sortOrder)
            {
                case "title_desc":
                    items = items.OrderByDescending(t => t.Title);
                    break;
                case "date":
                    items = items.OrderBy(t => t.DueDate);
                    break;
                case "date_desc":
                    items = items.OrderByDescending(t => t.DueDate);
                    break;
                default:
                    items = items.OrderBy(t => t.Title);
                    break;
            }

            return View(items.ToList());
        }

        // GET: TodoItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // GET: TodoItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Notes,IsDone,DueDate,CreatedAt")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                db.TodoItems.Add(todoItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Notes,IsDone,DueDate,CreatedAt")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(todoItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todoItem);
        }

        // GET: TodoItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TodoItem todoItem = db.TodoItems.Find(id);
            if (todoItem == null)
            {
                return HttpNotFound();
            }
            return View(todoItem);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TodoItem todoItem = db.TodoItems.Find(id);
            db.TodoItems.Remove(todoItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
