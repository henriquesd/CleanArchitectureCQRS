using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<Book> Add(Book book);
        Task<Book> Update(Book book);
        Task<bool> Remove(Book book);
        Task<IEnumerable<Book>> Search(string bookName);
    }
}
