using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryWebApi.Domain.Models
{
    public class BorrowedBook
    {
        [Key]
        public int BorrowedID { get; set; }

        [Required]
        public int BookID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime BorrowDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        
        [JsonIgnore]
        public virtual Book? Book { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
