using AutoMapper;
using CleanArchitecture.Application.Features.Books.Add;
using CleanArchitecture.Application.Features.Books.Delete;
using CleanArchitecture.Application.Features.Books.Edit;
using CleanArchitecture.Core.Dtos.Book;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Features.Books
{
    public class BooksCommandHandler : IRequestHandler<AddBookCommand, AddBookCommandResult>,
        IRequestHandler<UpdateBookCommand, UpdateBookCommandResult>,
        IRequestHandler<DeleteBookCommand, DeleteBookCommandResult>
    {
        private IBookService _service;
        private IMapper _mapper;

        public BooksCommandHandler(IBookService service, IMapper mapper)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<AddBookCommandResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var bookToAdd = _mapper.Map<Book>(request.BookAddDto);
            var book = await _service.Add(bookToAdd);
            var bookToReturn = _mapper.Map<BookResultDto>(book);

            return new AddBookCommandResult(bookToReturn);
        }

        public async Task<UpdateBookCommandResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = _mapper.Map<Book>(request.BookEditDto);
            var book = await _service.Update(bookToUpdate);
            var bookToReturn = _mapper.Map<BookResultDto>(bookToUpdate);

            return new UpdateBookCommandResult(bookToReturn);
        }

        public async Task<DeleteBookCommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToDelete = await _service.GetById(request.BookId);
            if (bookToDelete == null) return null;

            await _service.Remove(bookToDelete);
            var bookToReturn = _mapper.Map<BookResultDto>(bookToDelete);

            return new DeleteBookCommandResult(bookToReturn);
        }
    }
}