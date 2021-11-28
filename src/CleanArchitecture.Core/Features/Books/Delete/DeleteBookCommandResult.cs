using CleanArchitecture.Core.Dtos.Book;

namespace CleanArchitecture.Core.Features.Books.Delete
{
    public class DeleteBookCommandResult
    {
        public BookResultDto BookResultDto { get; set; }

        public DeleteBookCommandResult(BookResultDto bookResultDto)
        {
            BookResultDto = bookResultDto;
        }
    }
}
