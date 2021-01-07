using BookManager.Api.Services;
using BookManager.Data;
using BookManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BookManager.Api.Test
{
    public class BookRepositoryTests
    {
        [Fact]
        public void GetBooks_PageSizeIsThree_ReturnThreeBooks()
        {
            var option = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase("BookDbForTesting")
                .Options;

            using (var context = new BookContext(option))
            {
                context.Books.Add(new Book { Id = 1, Title = "C#" });
                context.Books.Add(new Book { Id = 2, Title = "Java" });
                context.Books.Add(new Book { Id = 3, Title = "Node js" });

                context.SaveChanges();

                var bookRepo = new BookRepository(context);

                var books = bookRepo.GetBooks(1, 3);

                Assert.Equal(3, books.Count());
            }

            
        }
    }
}
