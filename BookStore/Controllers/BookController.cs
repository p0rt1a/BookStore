using AutoMapper;
using BookStore.Application.BookOperations.CreateBook;
using BookStore.Application.BookOperations.DeleteBook;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.BookOperations.UpdateBook;
using BookStore.DbOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.Application.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all books
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();

            try
            {
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(result);
        }

        //[HttpGet]
        //public Book Get([FromQuery]string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).FirstOrDefault();

        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody]CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();

            validator.ValidateAndThrow(command);
            command.Handle();
            
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody]UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            try
            {
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            try
            {
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
