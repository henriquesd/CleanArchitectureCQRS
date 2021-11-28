using AutoMapper;
using CleanArchitecture.Core.Dtos.Book;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Core.Mappings
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookEditDto>().ReverseMap();
            CreateMap<Book, BookDeleteDto>().ReverseMap();
            CreateMap<Book, BookAddDto>().ReverseMap();
            CreateMap<Book, BookResultDto>().ReverseMap();
        }
    }
}

