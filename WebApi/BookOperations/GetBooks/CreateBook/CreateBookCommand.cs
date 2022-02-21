using System.Collections.Generic;
using WebaApi.DBOperations;
using System.Linq;
using System;
using WebaApi;
using WebaApi.Common;

namespace WebaApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public CreateBookCommand(BookStoreDbContext dbContext)
        {
           _dbContext=dbContext;
        }

        public void Handel()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Book is already available");

            book = new Book()
            {
                Title = Model.Title,
                GenreId= Model.GenreId,
                PageCount = Model.PageCount,
                PublishDate = Model.PublishDate

            };

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }

        public class CreateBookModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }
            public int PageCount { get; set; }

            public DateTime PublishDate { get; set;}

        }
    }
}