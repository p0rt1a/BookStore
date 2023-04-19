using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model { get; set; }

        public CreateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Name == Model.Name);

            if (author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut");

            author = _mapper.Map<Author>(Model);
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
