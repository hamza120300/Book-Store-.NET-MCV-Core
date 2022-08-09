using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.repository
{
    public class AuthorRepository : BookstoreRepository<Author>
    {
        IList<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author> {

                new Author {id = 1, FullName = "Hamza" },
                new Author {id = 2, FullName = "mona" },
                new Author {id = 3, FullName = "fathy" }
            };
            
        }
        public void add(Author entity)
        {
            entity.id = authors.Max(a=> a.id) + 1;
            authors.Add(entity);
        }

        public void delete(int id)
        {
            var entity = Find(id);
            authors.Remove(entity);

        }


        public Author Find(int id)
        {
            var auth = authors.SingleOrDefault(s => s.id == id);
            return auth;
        }

        public IList<Author> ViewAll()
        {
            return authors;
        }

        public void updata(int id, Author auth)
        {
            var author = Find(id);
            
            author.FullName = auth.FullName;

        }

        public List<Author> Search(string contain)
        {
            return authors.Where(a => a.FullName.Contains(contain)).ToList();
        }

      
    }
}
