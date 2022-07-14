using CleanArchitecture.Core.Dtos.Book;
using MediatR;

namespace CleanArchitecture.Application.Features.Books.Edit
{
    public class UpdateBookCommand : IRequest<UpdateBookCommandResult>
    {
        public BookEditDto BookEditDto { get; set; }

        public UpdateBookCommand(BookEditDto bookEditDto)
        {
            BookEditDto = bookEditDto;
        }
    }
}
