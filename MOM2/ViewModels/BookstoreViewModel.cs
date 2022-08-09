using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOM2.ViewModels
{
    public class BookstoreViewModel
    {
        public int Bookid { get; set; }
        public  string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author>authors { get; set; }
        
        public IFormFile file { get; set; }
        public string VImageUrl { get; set; }

    }
}
