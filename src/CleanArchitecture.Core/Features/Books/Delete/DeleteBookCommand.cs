using MediatR;

namespace CleanArchitecture.Core.Features.Books.Delete
{
    public class DeleteBookCommand : IRequest<DeleteBookCommandResult>
    {
        public int BookId { get; set; }

        public DeleteBookCommand(int bookId)
        {
            BookId = bookId;
        }
    }
}
