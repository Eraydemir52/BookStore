using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebaApi;
using WebaApi.BookOperations;
using WebaApi.BookOperations.CreateBook;
using WebaApi.BookOperations.DeleteBook;
using WebaApi.BookOperations.GetBookDetail;
using WebaApi.BookOperations.UpdateBook;
using WebaApi.DBOperations;
using static WebaApi.BookOperations.CreateBook.CreateBookCommand;
using static WebaApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebaApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery Query = new GetBooksQuery(_context);
            var result = Query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(result);
        }

        //  [HttpGet]//FromQueray ile GetById de yazıla bilir
        // public Book Get([FromQuery] string id)
        // {
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id) ).SingleOrDefault();
        //    return book;
        // }


        //************Post************
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handel();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();



        }
        //************Put************
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Model = updateBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok();


        }
        //************Delete************
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
             return  BadRequest(ex.Message);
            }
            return Ok();

        }



    }
}