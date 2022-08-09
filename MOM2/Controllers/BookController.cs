using BookStore.Models;
using BookStore.Models.repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOM2.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MOM2.Controllers
{
    public class BookController : Controller
    {
       
        private readonly BookstoreRepository<Book> bookstoreRepository;

        private readonly BookstoreRepository<Author> authorRepository;
        [Obsolete]
        private readonly IHostingEnvironment hosting;

        [Obsolete]
        public BookController(BookstoreRepository<Book> bookstoreRepository ,
            BookstoreRepository<Author>authorRepository, IHostingEnvironment hosting)
        {
            this.bookstoreRepository = bookstoreRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }


        // GET: BoookController1
        public ActionResult Index()
        {
            
            var book = bookstoreRepository.ViewAll();
            return View(book);
        }


        // GET: BoookController1/Create
        public ActionResult Create()
        {
            var model = new BookstoreViewModel
            {
                authors = FillSelectList()
            };
            return View(model);
        }

        // POST: BoookController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create(BookstoreViewModel model)
         {
            
            try
            {
                 // to upload Image 
                    string fileName = string.Empty;
                    string uploads = Path.Combine(hosting.WebRootPath);
                    fileName = model.file.FileName;
                    string fullpath = Path.Combine(uploads, fileName);
                    //"C:\\Users\\Electronica\\source\\repos\\MOM2\\MOM2\\wwwroot\\Uploads\\fffff.txt"
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        model.file.CopyTo(stream);
                    }
               
                if (model.AuthorId==-1 || model.Title==null || model.Description==null || model.file==null) {
                     ViewBag.Message = "please complet the Form list!!";
                    //ViewBag.Message = "Hamza see that  " + ex;
                    var vmodel = new BookstoreViewModel
                    {
                        authors = FillSelectList()
                    };
                    return View(vmodel);
                }

               
                Book book = new Book
                {
                    id = model.Bookid, Title = model.Title, Description = model.Description,
                    Author = authorRepository.Find(model.AuthorId) ,ImageUrl= fileName

                };
                bookstoreRepository.add(book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
              // if some error happen i will activited that fun 
             /*   Book book = new Book
                {
                    id = model.Bookid,
                    Title = model.Title,
                    Description = model.Description,
                    Author = authorRepository.Find(model.AuthorId)
                   

                };
                bookstoreRepository.add(book);
                return RedirectToAction(nameof(Index));*/
               
                return View();
            }
        }

        // GET: BoookController1/Details/5
        public ActionResult Details(int id)
        {
            var book = bookstoreRepository.Find(id);
            return View(book);
        }

      
       
            
        // GET: BoookController1/Edit/5
        public ActionResult Edit(int id )
        {
            var book = bookstoreRepository.Find(id);

            //if there no Author for Book So  made that =>
            //var authorID = book.Author == null ? book.Author.id = 0 : book.Author.id;
            var viewModel = new BookstoreViewModel {
                Bookid = book.id, Title = book.Title, Description = book.Description,
                VImageUrl=book.ImageUrl,
                AuthorId = book.Author.id, authors = authorRepository.ViewAll().ToList()
                
            };

            return View(viewModel);
        }
        //عندي مشكله الصوره في التعديل متنساش 
        // POST: BoookController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( BookstoreViewModel model)
        {
            // to upload Image
            string fileName = string.Empty;
            string rootW = Path.Combine(hosting.WebRootPath);
            fileName = model.file.FileName;
            string fullpath = Path.Combine(rootW, fileName);
            // frist we have to delete old file 
            // bookSRepro.Find == object so that .ImageUrl ;
            string OldFileName = model.VImageUrl;
            string FullOldPath = Path.Combine(rootW, OldFileName);
            
            //System.IO.File.Delete(FullOldPath); 
            //save the Image in location
            using (var stream = new FileStream(fullpath, FileMode.Create))
            {
                model.file.CopyTo(stream);
            }

            try
            {
                Book book = new Book
                {
                    id = model.Bookid,
                    Title = model.Title,
                    Description = model.Description,ImageUrl=fileName,
                    Author = authorRepository.Find(model.AuthorId)

                };
                bookstoreRepository.updata(model.Bookid,book);
                    
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BoookController1/Delete/5
        public ActionResult Delete(int id)
        {
            var b = bookstoreRepository.Find(id);
            return View(b);
        }

        // POST: BoookController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Book book)
        {
            try
            {
               // var b = bookstoreRepository.Find(id);
                bookstoreRepository.delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Search(string  contins) {
           
            var result = bookstoreRepository.Search(contins);
            return View("Index", result);
        }

        List<Author> FillSelectList()  // for make first item in list of Author like that=> 
        {
            var authors = authorRepository.ViewAll().ToList();
            authors.Insert(0, new Author { id = -1, FullName = "--- Please select an author ---" });

            return authors;
        }
    }
}
