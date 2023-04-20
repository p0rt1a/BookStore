using BookStore.DbOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
        }
    }
}
