using MediatR;

namespace CleanArchitecture.Application.Features.Books.Read.GetBookById
{
    public class GetBookByIdQuery : IRequest<GetBookByIdQueryResult>
    {
        public int BookId { get; }

        public GetBookByIdQuery(int bookId)
        {
            BookId = bookId;
        }
    }
}
