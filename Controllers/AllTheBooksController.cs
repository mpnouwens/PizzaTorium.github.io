using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class AllTheBooksController : Controller
    {
        private LMSdbEntities db = new LMSdbEntities();

        [Authorize(Roles = "Administrator")]
        // GET: AllTheBooks
        public ActionResult Index()
        {
            var allTheBooks = db.AllTheBooks.Include(a => a.Author).Include(a => a.Category).Include(a => a.Publisher);
            return View(allTheBooks.ToList());
        }
        [Authorize(Roles = "Student, Staff")]
        public ActionResult UserIndex(string btnAdd)
        {
    

            var allTheBooks = db.AllTheBooks.Include(a => a.Author).Include(a => a.Category).Include(a => a.Publisher);
            return View(allTheBooks.ToList());
        }

        // GET: AllTheBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllTheBook allTheBook = db.AllTheBooks.Find(id);
            if (allTheBook == null)
            {
                return HttpNotFound();
            }
            return View(allTheBook);
        }

        // GET: AllTheBooks/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID_FK = new SelectList(db.Authors, "AuthorID_PK", "NameOfAuthor");
            ViewBag.CategoryID_FK = new SelectList(db.Categories, "CategoryID_PK", "NameOfCategory");
            ViewBag.PublisherID_FK = new SelectList(db.Publishers, "PublisherID_PK", "NameOfPublisher");
            return View();
        }

        // POST: AllTheBooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID_PK,Name,AuthorID_FK,Description,PublisherID_FK,ISBN,PrintLength,InterestedAge,CategoryID_FK,Image")] AllTheBook allTheBook)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.AllTheBooks.Add(allTheBook);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.AuthorID_FK = new SelectList(db.Authors, "AuthorID_PK", "NameOfAuthor", allTheBook.AuthorID_FK);
                ViewBag.CategoryID_FK = new SelectList(db.Categories, "CategoryID_PK", "NameOfCategory", allTheBook.CategoryID_FK);
                ViewBag.PublisherID_FK = new SelectList(db.Publishers, "PublisherID_PK", "NameOfPublisher", allTheBook.PublisherID_FK);
              
            }
            catch(Exception)
            {

            }
            return View(allTheBook);
        }


        // GET: AllTheBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllTheBook allTheBook = db.AllTheBooks.Find(id);
            if (allTheBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID_FK = new SelectList(db.Authors, "AuthorID_PK", "NameOfAuthor", allTheBook.AuthorID_FK);
            ViewBag.CategoryID_FK = new SelectList(db.Categories, "CategoryID_PK", "NameOfCategory", allTheBook.CategoryID_FK);
            ViewBag.PublisherID_FK = new SelectList(db.Publishers, "PublisherID_PK", "NameOfPublisher", allTheBook.PublisherID_FK);
            return View(allTheBook);
        }

        // POST: AllTheBooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID_PK,Name,AuthorID_FK,Description,PublisherID_FK,ISBN,PrintLength,InterestedAge,CategoryID_FK,Image")] AllTheBook allTheBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allTheBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID_FK = new SelectList(db.Authors, "AuthorID_PK", "NameOfAuthor", allTheBook.AuthorID_FK);
            ViewBag.CategoryID_FK = new SelectList(db.Categories, "CategoryID_PK", "NameOfCategory", allTheBook.CategoryID_FK);
            ViewBag.PublisherID_FK = new SelectList(db.Publishers, "PublisherID_PK", "NameOfPublisher", allTheBook.PublisherID_FK);
            return View(allTheBook);
        }

        // GET: AllTheBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllTheBook allTheBook = db.AllTheBooks.Find(id);
            if (allTheBook == null)
            {
                return HttpNotFound();
            }
            return View(allTheBook);
        }

        // POST: AllTheBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AllTheBook allTheBook = db.AllTheBooks.Find(id);
            db.AllTheBooks.Remove(allTheBook);
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
