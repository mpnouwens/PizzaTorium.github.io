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
    public class FinesController : Controller
    {
        private LMSdbEntities db = new LMSdbEntities();


        private DateTime localDate = DateTime.Now;



        // GET: Fines
        public ActionResult Index()
        {
            var fines = db.Fines.Include(f => f.Staff).Include(f => f.Student);
            return View(fines.ToList());
        }

        // GET: Fines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = db.Fines.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // GET: Fines/Create
        public ActionResult Create()
        {
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName");
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName");
            return View();
        }

        // POST: Fines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FineID_PK,StudentID_FK,StaffID_FK,Description,SeverityOfFine,WhatBookWasIssued")] Fine fine)
        {
            if (ModelState.IsValid)
            {

                //if (User.IsInRole("Student"))
                //{
                //    var id = currentUserId();
                //    //The book is returned late       

                //    //Get the person who did it and get the book that is out of date     

                //    var Person = db.Students.Where(b => b.StudentID_PK == fine.StudentID_FK);


                //    //var BooksthaTheUserIssued = db.Fines.Include(b => b.BooksIssued.IssuedID_PK)
                //    //                            .Include(p => p.BooksIssued.ReturnDate >= localDate)
                //    //                            .Where(x => x.Student.UserID_FK == id);

                //    var theFine = new Fine()
                //    {
                //        StudentID_FK = ,

                //    };
                    
                //}
                //if (User.IsInRole("Staff"))
                //{

                //}

                db.Fines.Add(fine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", fine.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", fine.StudentID_FK);
            return View(fine);
        }

        // GET: Fines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = db.Fines.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", fine.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", fine.StudentID_FK);
            return View(fine);
        }

        // POST: Fines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FineID_PK,StudentID_FK,StaffID_FK,Description,SeverityOfFine,WhatBookWasIssued")] Fine fine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffID_FK = new SelectList(db.Staffs, "StaffID_PK", "FirstName", fine.StaffID_FK);
            ViewBag.StudentID_FK = new SelectList(db.Students, "StudentID_PK", "FirstName", fine.StudentID_FK);
            return View(fine);
        }

        // GET: Fines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fine fine = db.Fines.Find(id);
            if (fine == null)
            {
                return HttpNotFound();
            }
            return View(fine);
        }

        // POST: Fines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fine fine = db.Fines.Find(id);
            db.Fines.Remove(fine);
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
