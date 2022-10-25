using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class Users : IClassModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string OtherUserDetails { get; set; }
        public decimal AmountOfFine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
