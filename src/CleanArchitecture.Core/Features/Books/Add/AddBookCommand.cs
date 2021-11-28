using CleanArchitecture.Core.Dtos.Book;
using MediatR;

namespace CleanArchitecture.Core.Features.Books.Add
{
    public class AddBookCommand : IRequest<AddBookCommandResult>
    {
        public BookAddDto BookAddDto { get; set; }

        public AddBookCommand(BookAddDto bookAddDto)
        {
            BookAddDto = bookAddDto;
        }
    }
}
