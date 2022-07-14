using CleanArchitecture.Core.Dtos.Book;

namespace CleanArchitecture.Application.Features.Books.Read.GetBookById
{
    public class GetBookByIdQueryResult
    {
        public BookResultDto Book { get; }

        public GetBookByIdQueryResult(BookResultDto book)
        {
            Book = book;
        }
    }
}
