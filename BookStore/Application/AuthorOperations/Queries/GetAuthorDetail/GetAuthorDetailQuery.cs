using AutoMapper;
using BookStore.DbOperations;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            AuthorDetailViewModel vm = _mapper.Map<AuthorDetailViewModel>(author);

            return vm;
        }
    }

    public class AuthorDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }
}
