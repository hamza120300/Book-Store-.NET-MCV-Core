using BookStore.Models;
using BookStore.Models.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOM2.Models.repository
{
    public class AurthorDbRepository : BookstoreRepository<Author>
    {
        BookstoreDbContext db;
        public AurthorDbRepository(BookstoreDbContext _db)
        {
            db = _db;
            
        }
        public void add(Author entity)
        {//
          //  entity.id = db.Authors.Max(a => a.id) + 1;//////
            db.Authors.Add(entity);
            db.SaveChanges();//مش صح  في طريقه تاني بس هنتعلمها بعد الكورس دا
        }

        public void delete(int id)
        {
            var entity = Find(id);
            db.Authors.Remove(entity);
            db.SaveChanges();//مش صح  في طريقه تاني بس هنتعلمها بعد الكورس دا
        }


        public Author Find(int id)
        {
            var auth = db.Authors.SingleOrDefault(s => s.id == id);
            return auth;
        }

        public IList<Author> ViewAll()
        {
            return db.Authors.ToList();
        }

        public void updata(int id, Author auth)
        {
            db.Update(auth);
            db.SaveChanges();//مش صح  في طريقه تاني بس هنتعلمها بعد الكورس دا
        }

        public List<Author> Search(string contain)
        {
            return db.Authors.Where(a => a.FullName.Contains(contain)).ToList();
        }
    }
}

