using BookStore.Models;
using BookStore.Models.repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOM2.Models.repository
{
    public class BookDbRepository : BookstoreRepository<Book>
    {

        BookstoreDbContext db;

        // constractor 
        public BookDbRepository(BookstoreDbContext _db)
        {
            db = _db;
        }

        public void add(Book entity)
        {        
            db.books.Add(entity);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var book = Find(id);
            db.books.Remove(book);
            db.SaveChanges();
        }

        public Book Find(int id)
        {
            var book = db.books.Include(a => a.Author).SingleOrDefault(s => s.id == id);

            return book;
        }

        public IList<Book> ViewAll()
        {
            //Include(a=> a.Author) علشان اجيب المالف معاها 
            return db.books.Include(a=> a.Author).ToList();
        }

        public void updata(int id, Book newbook)
        {
            db.Update(newbook);
            db.SaveChanges();
        }
        public List<Book> Search(string contain)
        {
            var result = db.books.Include(a => a.Author)
                .Where(b => b.Title.Contains(contain)
                        || b.Description.Contains(contain)
                        || b.Author.FullName.Contains(contain)).ToList();

            return result;
        }
    }
}

