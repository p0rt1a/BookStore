using AutoMapper;
using BookStore.Common;
using BookStore.DbOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x => x.Author).Include(x => x.Genre).OrderBy(x => x.Id).ToList<Book>();

            List<BookViewModel> vm = _mapper.Map<List<BookViewModel>>(bookList);

            return vm;
        }
    }

    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
    }
}
