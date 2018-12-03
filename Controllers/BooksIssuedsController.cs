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
using Microsoft.AspNet.Identity.EntityFramework;

 
namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BooksIssuedsController : Controller
    {
        private LMSdbEntities db = new LMSdbEntities();
        private string currentUserId()
        {
            return User.Identity.GetUserId();
        }

        private DateTime localDate = DateTime.Now;


        // GET: BooksIssueds
        public ActionResult Index()
        {
            var id = currentUserId();
            if (User.IsInRole("Student"))
            {
                var studend = db.BooksIssueds.Include(b => b.AllTheBook).Include(b => b.Student).Where(x => x.Student.UserID_FK == id);
                studend.ToList();
                return View(studend);
            }
            if (User.IsInRole("Staff"))
            {
                var staffend = db.BooksIssueds.Include(b => b.AllTheBook).Include(b => b.Staff).Where(x => x.Staff.UserID_FK == id);
                staffend.ToList();
                return View(staffend);
            }
            if (User.IsInRole("Administrator"))
            {
                var staffend = db.BooksIssueds.Include(b => b.AllTheBook).Include(b => b.Staff).Include(b => b.Student);
                staffend.ToList();
                return View(staffend);
            }
            return View();
        }


        // GET: BooksIssueds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksIssued booksIssued = db.BooksIssueds.Find(id);
            if (booksIssued == null)
            {
                return HttpNotFound();
            }
            return View(booksIssued);
        }

        // GET: BooksIssueds/Create
        public ActionResult Create()
        {
            ViewBag.IssusedID_FK = new SelectList(db.AllTheBooks, "BookID_PK", "Name");
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName");
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName");
            return View();
        }

        // POST: BooksIssueds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IssuedID_PK,StudentID_FK,StaffID_FK,IssusedID_FK,ReturnDate,Status")] BooksIssued booksIssued)
        {
            if (ModelState.IsValid)
            {
                db.BooksIssueds.Add(booksIssued);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IssusedID_FK = new SelectList(db.AllTheBooks, "BookID_PK", "Name", booksIssued.IssusedID_FK);
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", booksIssued.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", booksIssued.StudentID_FK);
            return View(booksIssued);
        }



        // GET: BooksIssueds/Create
        public ActionResult CreateUsers()
        {
            //var currentUserId = User.Identity.GetUserId();
            currentUserId();
            ViewBag.IssusedID_FK = new SelectList(db.AllTheBooks, "BookID_PK", "Name");
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName");
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName");
            return View();
        }

        // POST: BooksIssueds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUsers([Bind(Include = "IssuedID_PK,StudentID_FK,StaffID_FK,IssusedID_FK,ReturnDate,Status")] BooksIssued booksIssued)
        {
            if (ModelState.IsValid)
            {
             

              
                //Student's ID

                if (User.IsInRole("Student"))
                {
                    var id = currentUserId();
                    var currentUserStudent = db.Students.FirstOrDefault(x => x.UserID_FK == id);
                    booksIssued.StudentID_FK = currentUserStudent.StudentID_PK;
                    booksIssued.StaffID_FK = null;
                    booksIssued.ReturnDate = localDate.AddMinutes(5);
                    db.BooksIssueds.Add(booksIssued);
                    db.SaveChanges();
                }

                if (User.IsInRole("Staff"))
                {
                    var id = currentUserId();
                    var currentUserStaff = db.Staffs.FirstOrDefault(x => x.UserID_FK == id);
                    booksIssued.StudentID_FK = null;
                    booksIssued.StaffID_FK = currentUserStaff.StaffID_PK;
                    booksIssued.ReturnDate = localDate.AddMinutes(14);
                    db.BooksIssueds.Add(booksIssued);
                    db.SaveChanges();
                }
     
                return RedirectToAction("Index");

            }

            ViewBag.IssusedID_FK = new SelectList(db.AllTheBooks, "BookID_PK", "Name", booksIssued.IssusedID_FK);
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", booksIssued.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", booksIssued.StudentID_FK);
            return View(booksIssued);
        }

        // GET: BooksIssueds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksIssued booksIssued = db.BooksIssueds.Find(id);
            if (booksIssued == null)
            {
                return HttpNotFound();
            }
            ViewBag.IssusedID_FK = new SelectList(db.AllTheBooks, "BookID_PK", "Name", booksIssued.IssusedID_FK);
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", booksIssued.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", booksIssued.StudentID_FK);
            return View(booksIssued);
        }


        public ActionResult SeeIfIHaveFine(int bookId)
        {
            var id = currentUserId();

            var book = db.BooksIssueds.Find(bookId);
            var thereturndate = book.ReturnDate;



           TimeSpan diff = localDate - thereturndate;
            var days = diff.TotalDays;
            var hours = diff.TotalHours;
            var minutes = diff.TotalMinutes;
            var seconds = diff.TotalSeconds;
            var muli = diff.TotalMilliseconds;


            if ((days>0)|| (hours>0) || (minutes>0) || (seconds>0) || (muli>0))
            {
                if (User.IsInRole("Student"))
                {

                    book.Status = "Fined";
                    db.SaveChanges();

                    var newFine = new Fine()
                    {
                        StudentID_FK = book.StudentID_FK,
                        StaffID_FK = null,
                        Description = "Overdue book",
                        SeverityOfFine = 1,
                        
                        WhatBookWasIssued = book.IssuedID_PK


                };
                    db.Fines.Add(newFine);
                    db.SaveChanges();


                    return RedirectToAction("index", "Fines");

                    //return RedirectToAction("index", "Fines", new { newFine });
                }

                if (User.IsInRole("Staff"))
                {


                    var newFine = new Fine()
                    {
                        StaffID_FK = book.StaffID_FK,
                        StudentID_FK = null,
                        Description = "Overdue book",
                        SeverityOfFine = 1,
                        WhatBookWasIssued = book.IssuedID_PK

                    };
                    db.Fines.Add(newFine);
                    db.SaveChanges();

                    
                    return RedirectToAction("index", "Fines");
                    //return RedirectToAction("index", "Fines", new { newFine });
                    //return RedirectToAction("Create", "Fines", new { id = booksIssued.IssuedID_PK, booksIssued.Staff.StaffID_PK });

                }
            }
            else
            {
                if (User.IsInRole("Student"))
                {
                    var issuedbookReturn = db.BooksIssueds.FirstOrDefault(i => i.StudentID_FK == book.StudentID_FK);
                    db.BooksIssueds.Remove(issuedbookReturn);
                    db.SaveChanges();
                }
                if (User.IsInRole("Staff"))
                {
                    //single
                    var issuedbookReturn = db.BooksIssueds.FirstOrDefault(i => i.StaffID_FK == book.StaffID_FK);
                    db.BooksIssueds.Remove(issuedbookReturn);
                    db.SaveChanges();
                }
            }

            //Remove book from your issuedbooks



            //Redirect to index
            return RedirectToAction("Index");
        }


        // POST: BooksIssueds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IssuedID_PK,StudentID_FK,StaffID_FK,IssusedID_FK,ReturnDate")] BooksIssued booksIssued)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksIssued).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IssusedID_FK = new SelectList(db.AllTheBooks, "BookID_PK", "Name", booksIssued.IssusedID_FK);
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", booksIssued.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", booksIssued.StudentID_FK);
            return View(booksIssued);
        }

        // GET: BooksIssueds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksIssued booksIssued = db.BooksIssueds.Find(id);
            if (booksIssued == null)
            {
                return HttpNotFound();
            }
            return View(booksIssued);
        }

        // POST: BooksIssueds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksIssued booksIssued = db.BooksIssueds.Find(id);
            db.BooksIssueds.Remove(booksIssued);
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
