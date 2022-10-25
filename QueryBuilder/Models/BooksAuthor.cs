using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class BooksAuthor : IClassModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int BookId { get; set; }
    }
}
