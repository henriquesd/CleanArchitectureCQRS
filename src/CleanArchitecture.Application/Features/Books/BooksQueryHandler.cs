using AutoMapper;
using CleanArchitecture.Application.Features.Books.Read.GetAllBooks;
using CleanArchitecture.Application.Features.Books.Read.GetBookById;
using CleanArchitecture.Core.Dtos.Book;
using CleanArchitecture.Core.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.Features.Books
{
    public class BooksQueryHandler :
         IRequestHandler<GetAllBooksQuery, GetAllBooksQueryResult>,
         IRequestHandler<GetBookByIdQuery, GetBookByIdQueryResult>
    {
        private IBookRepository _repository;
        private IMapper _mapper;

        public BooksQueryHandler(IBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAllBooksQueryResult> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAll();
            var booksDto = _mapper.Map<BookResultDto[]>(books);
            return new GetAllBooksQueryResult(booksDto);
        }

        public async Task<GetBookByIdQueryResult> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _repository.GetById(request.BookId);
            var bookDto = _mapper.Map<BookResultDto>(book);
            return new GetBookByIdQueryResult(bookDto);
        }
    }
}