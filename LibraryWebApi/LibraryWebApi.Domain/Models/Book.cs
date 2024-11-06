using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LibraryWebApi.Domain.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }

        
        [JsonIgnore]
        public ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
    }
}
//OLD