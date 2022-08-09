using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.repository;
using Microsoft.AspNetCore.Http;

namespace MOM2.Controllers
{
    public class AuthorController : Controller
    {
       
        private readonly BookstoreRepository<Author> authorepo ;
       //private  AuthorRepository authorRepository;
     
       
        public AuthorController(BookstoreRepository<Author> authorepo)
        {
            this.authorepo = authorepo;
        }

        public IActionResult Index()
        {

            var authors1 = authorepo.ViewAll();
            return View(authors1);
          
        }
        public IActionResult Details(int id )
        {
            var auth = authorepo.Find(id);
            return View(auth);
        }

        public IActionResult Edit(int id ) {
            var auth = authorepo.Find(id);
            return View(auth);
        }

        public IActionResult Delete(int id )
        {
            var authr = authorepo.Find(id);
            return View(authr);
        }
        public IActionResult Create()
        {

            return View();
        }
        // POST: AutherController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            try
            {
                authorepo.add(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int  id , Author author)
        {
            try
            {   
                authorepo.updata(id,author);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                authorepo.delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
