using AutoMapper;
using CleanArchitecture.Core.Dtos.Book;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Books.Add;
using CleanArchitecture.Core.Features.Books.Delete;
using CleanArchitecture.Core.Features.Books.Edit;
using CleanArchitecture.Core.Interfaces;
using MediatR;

namespace CleanArchitecture.Core.Features.Books
{
    public class BooksCommandHandler : IRequestHandler<AddBookCommand, AddBookCommandResult>,
        IRequestHandler<UpdateBookCommand, UpdateBookCommandResult>,
        IRequestHandler<DeleteBookCommand, DeleteBookCommandResult>
    {
        private IBookRepository _repository;
        private IMapper _mapper;

        public BooksCommandHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AddBookCommandResult> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var bookToAdd = _mapper.Map<Book>(request.BookAddDto);
            await _repository.Add(bookToAdd);
            var bookToReturn = _mapper.Map<BookResultDto>(bookToAdd);

            return new AddBookCommandResult(bookToReturn);
        }

        public async Task<UpdateBookCommandResult> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var bookToUpdate = _mapper.Map<Book>(request.BookEditDto);
            await _repository.Update(bookToUpdate);
            var bookToReturn = _mapper.Map<BookResultDto>(bookToUpdate);

            return new UpdateBookCommandResult(bookToReturn);
        }

        public async Task<DeleteBookCommandResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var bookToDelete = await _repository.GetById(request.BookId);
            await _repository.Remove(bookToDelete);
            var bookToReturn = _mapper.Map<BookResultDto>(bookToDelete);

            return new DeleteBookCommandResult(bookToReturn);
        }
    }
}
