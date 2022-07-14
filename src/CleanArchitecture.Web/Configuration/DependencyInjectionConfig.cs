using CleanArchitecture.Application.Features.Books;
using CleanArchitecture.Application.Features.Books.Add;
using CleanArchitecture.Application.Features.Books.Delete;
using CleanArchitecture.Application.Features.Books.Edit;
using CleanArchitecture.Application.Features.Books.Read.GetAllBooks;
using CleanArchitecture.Application.Features.Books.Read.GetBookById;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Services;
using CleanArchitecture.Infrastructure.Context;
using CleanArchitecture.Infrastructure.Repositories;
using MediatR;

namespace CleanArchitecture.Web.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<CleanArchitectureDbContext>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IBookRepository, BookRepository>();

            services.AddScoped<IRequestHandler<GetAllBooksQuery, GetAllBooksQueryResult>, BooksQueryHandler>();
            services.AddScoped<IRequestHandler<GetBookByIdQuery, GetBookByIdQueryResult>, BooksQueryHandler>();
            services.AddScoped<IRequestHandler<AddBookCommand, AddBookCommandResult>, BooksCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBookCommand, UpdateBookCommandResult>, BooksCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBookCommand, DeleteBookCommandResult>, BooksCommandHandler>();

            return services;
        }
    }
}
