using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebaApi;
using WebaApi.BookOperations;

using WebaApi.BookOperations.DeleteBook;
using WebaApi.BookOperations.GetBookDetail;
using WebaApi.BookOperations.GetBooks.CreateBook;
using WebaApi.BookOperations.UpdateBook;
using WebaApi.DBOperations;

using static WebaApi.BookOperations.GetBookDetail.GetBookDetailQuery;
using static WebaApi.BookOperations.GetBooks.CreateBook.CreateBookCommand;
using static WebaApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery Query = new GetBooksQuery(_context,_mapper);
           
            
            var result = Query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
                query.BookId = id;
                GetBookDetailValidator validator = new GetBookDetailValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handel();
                // if (!result.IsValid)//tüm kurallardan geçmediyse
                // foreach (var item in result.Errors)
                // {
                //     Console.WriteLine("Özellik"+item.PropertyName+"-Error Message:"+item.ErrorMessage);
                // }
                // else
                     //command.Handel();
            
               
              
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
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                 DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                 validator.ValidateAndThrow(command);
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