using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookStore.Models.repository
{
  public  interface BookstoreRepository<TEntity>
    {
        // https://localhost:44373/ n;
        IList<TEntity> ViewAll();
        TEntity Find(int id);
        void updata(int id,TEntity entity);
        void add (TEntity entity);
        void delete(int id  );
        List<TEntity> Search(string contain);

    }
}
