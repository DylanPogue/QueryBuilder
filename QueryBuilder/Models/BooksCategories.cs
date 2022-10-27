using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class BooksCategories : IClassModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CategoriesId { get; set; }

        public BooksCategories(int id, int bookId, int categoriesId)
        {
            Id = id;
            BookId = bookId;
            CategoriesId = categoriesId;
        }
    }
}
