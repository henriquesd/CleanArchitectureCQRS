using MediatR;

namespace CleanArchitecture.Core.Features.Books.Read.GetBookById
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
