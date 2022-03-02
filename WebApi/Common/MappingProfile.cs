using AutoMapper;
using WebaApi;
using WebaApi.Common;

using static WebaApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebaApi.BookOperations.GetBooks.CreateBook.CreateBookCommand;
using static WebaApi.BookOperations.GetBooksQuery;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();//CreateBookModel objesi book modele maplene bilir olsun demek
            CreateMap<Book,BookDetailViewModel>().ForMember(dest => dest.Genre,opt=>opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book,BooksViewModel>().ForMember(dest => dest.Genre,opt=>opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}