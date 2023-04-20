using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if (author is null)
                throw new InvalidOperationException("Yazar bulunamadı");

            var books = _dbContext.Books.Where(x => x.Author.Name == author.Name).ToList<Book>();

            if (books.Count > 0)
                throw new InvalidOperationException("Kayıtlı kitabı bulunan yazar silinemez!");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
