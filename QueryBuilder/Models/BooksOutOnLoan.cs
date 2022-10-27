using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class BooksOutOnLoan : IClassModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }

        public BooksOutOnLoan(int id, int bookId, int userId)
        {
            Id = id;
            BookId = bookId;
            UserId = userId;
        }
    }
}
