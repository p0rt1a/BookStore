using BookStore.DbOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book
                {
                    Title = "Lean Startup",
                    GenreId = 1, //Personel Growth
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12),
                    AuthorId = 1
                },
                new Book
                {
                    Title = "Herland",
                    GenreId = 2, //Sci-Fi
                    PageCount = 250,
                    PublishDate = new DateTime(2010, 05, 23),
                    AuthorId = 2
                },
                new Book
                {
                    Title = "Dune",
                    GenreId = 2, //Personel Growth
                    PageCount = 540,
                    PublishDate = new DateTime(2002, 12, 21),
                    AuthorId = 2
                }
            );
        }
    }
}
