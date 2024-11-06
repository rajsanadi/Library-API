using AutoMapper;
using LibraryWebApi.Domain.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryWebApi.Domain.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
           
            CreateMap<Book, Book>();  

            
            CreateMap<User, User>();  

            
            CreateMap<BorrowedBook, BorrowedBook>();  
        }
    }
}
