using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.repository
{
    public class BookReprository : BookstoreRepository<Book>
    {
        List<Book> books;

        // constractor 
        public BookReprository()
        {
            books = new List<Book>()
            {
                new Book
                { id =1, Title = "C# Book",
                    Description="new book coming  ",ImageUrl="w1.jpg",
                    Author =new Author() },
                new Book
                { id =2, Title = "java Book", Description="later book coming  " ,ImageUrl="w2.jpg",
                    Author = new Author()},
                new Book
                { id =3, Title = "C++ Book", Description="no book coming  ",ImageUrl="w3.jpg",
                    Author = new Author() },
            };
        }

        public void add(Book entity)
        {
            entity.id = books.Max(b=> b.id)+1;
            books.Add(entity);
        }

        public void delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(s => s.id == id);

            return book;
        }

        public IList<Book> ViewAll()
        {
            return books;
        }

        public void updata(int id ,Book newbook )
        {
            var book = Find(id);
            book.Title  = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImageUrl = newbook.ImageUrl;
        }

        public List<Book> Search(string contain)
        {
            return books.Where(a => a.Title.Contains(contain)).ToList();
        }
    }
}
