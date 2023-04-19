using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorViewModel> Handle()
        {
            var authors = _dbContext.Authors.ToList<Author>();

            List<AuthorViewModel> vm = _mapper.Map<List<AuthorViewModel>>(authors);

            return vm;
        }
    }

    public class AuthorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
