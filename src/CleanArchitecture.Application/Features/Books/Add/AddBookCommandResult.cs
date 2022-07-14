using CleanArchitecture.Core.Dtos.Book;

namespace CleanArchitecture.Application.Features.Books.Add
{
    public class AddBookCommandResult
    {
        public BookResultDto BookResultDto { get; set; }

        public AddBookCommandResult(BookResultDto bookResultDto)
        {
            BookResultDto = bookResultDto;
        }
    }
}
