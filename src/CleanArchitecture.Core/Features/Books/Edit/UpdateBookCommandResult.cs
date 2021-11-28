using CleanArchitecture.Core.Dtos.Book;

namespace CleanArchitecture.Core.Features.Books.Edit
{
    public class UpdateBookCommandResult
    {
        public BookResultDto BookResultDto { get; set; }

        public UpdateBookCommandResult(BookResultDto bookResultDto)
        {
            BookResultDto = bookResultDto;
        }
    }
}
