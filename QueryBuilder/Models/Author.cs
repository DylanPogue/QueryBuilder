using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class Author : IClassModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public Author(int id, string firstName, string surname)
        {
            Id = id;
            FirstName = firstName;
            Surname = surname;
        }

        public Author()
        {

        }

        public override string ToString()
        {
            return 
                $"ID: \t\t{Id}\n" +
                $"First Name: \t\t{FirstName}\n" +
                $"Surname: \t\t{Surname}";
        }
    }
}
