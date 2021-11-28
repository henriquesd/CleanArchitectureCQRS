using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(CleanArchitectureDbContext context) : base(context) { }

        public override async Task<List<Book>> GetAll()
        {
            return await Db.Books.AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public override async Task<Book> GetById(int id)
        {
            return await Db.Books.AsNoTracking()
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
