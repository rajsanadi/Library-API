using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryWebApi.Domain.Models
{
    public class User
    {
        
        public int UserID { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
    }
}

