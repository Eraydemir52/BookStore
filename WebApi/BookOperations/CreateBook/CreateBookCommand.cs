using System.Collections.Generic;
using WebaApi.DBOperations;
using System.Linq;
using System;
using WebaApi;
using AutoMapper;
using WebaApi.Common;

namespace WebaApi.BookOperations.GetBooks.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handel()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Book is already available");

            book = _mapper.Map<Book>(Model);//book mappledi createmodele 
            

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }

        public class CreateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }
            public int PageCount { get; set; }

            public DateTime PublishDate { get; set; }

        }
    }
}