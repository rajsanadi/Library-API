using System;
using System.Threading.Tasks;


namespace LibraryWebApi.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IBorrowedBookRepository BorrowedBooks { get; }
        IUserRepository Users { get; }
        Task<int> CompleteAsync();
    }
}
