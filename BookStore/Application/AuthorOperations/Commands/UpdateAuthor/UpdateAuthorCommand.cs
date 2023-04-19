using AutoMapper;
using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public UpdateAuthorModel Model { get; set; }
        public int AuthorId { get; set; }

        public UpdateAuthorCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            if (_dbContext.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower()))
                throw new InvalidOperationException("Yazar zaten mevcut");

            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;

            _dbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
