using CleanArchitecture.Core.Dtos.Book;

namespace CleanArchitecture.Core.Features.Books.Read.GetAllBooks
{
    public class GetAllBooksQueryResult
    {
        public IEnumerable<BookResultDto> Books { get; }

        public GetAllBooksQueryResult(IEnumerable<BookResultDto> books)
        {
            Books = books;
        }
    }
}
