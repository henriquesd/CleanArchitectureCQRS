using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        new Task<List<Book>> GetAll();
        new Task<Book> GetById(int id);
    }
}
