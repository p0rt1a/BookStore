using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                    return;

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Authors.AddRange(
                    new Author
                    {
                        Name = "Jacqueline",
                        Surname = "Woodson",
                        DateOfBirth = new DateTime(1960, 06, 21)
                    },
                    new Author
                    {
                        Name = "Maddison",
                        Surname = "Ivy",
                        DateOfBirth = new DateTime(1982, 02, 01)
                    },
                    new Author
                    {
                        Name = "Adam",
                        Surname = "Swing",
                        DateOfBirth = new DateTime(1989, 12, 30)
                    }
                );

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

                context.SaveChanges();
            }
        }
    }
}
