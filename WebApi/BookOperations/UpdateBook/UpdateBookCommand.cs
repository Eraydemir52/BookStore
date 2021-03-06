using System;
using System.Linq;
using WebaApi.DBOperations;


namespace WebaApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel  Model { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
             var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
             
            if (book is null)
               throw new InvalidOperationException("Book not found");

            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;//değer girilmise genreId de girilen değeri kullanır kullanımmaışsa mevcut genre ıd kullanır
           // book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;//*****************Bunlaraı kısıtladık*********************************
          //  book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
           

        }

        public class UpdateBookModel
        {
             public string Title { get; set; }
             public int  GenreId { get; set; }
        }


    }
}