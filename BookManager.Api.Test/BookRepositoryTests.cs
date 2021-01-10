using BookManager.Api.Services;
using BookManager.Data;
using BookManager.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                .UseInMemoryDatabase($"BookDbForTesting{Guid.NewGuid()}")
                .Options;

            using (var context = new BookContext(option))
            {
                var salamiAuthor = new Author { Id = 1, Name = "Salami" };

                context.Books.Add(new Book { Id = Guid.NewGuid(), Title = "C#", Author = salamiAuthor });
                context.Books.Add(new Book { Id = Guid.NewGuid(), Title = "Java" });
                context.Books.Add(new Book { Id = Guid.NewGuid(), Title = "Node js" });

                context.SaveChanges();

                var bookRepo = new BookRepository(context);

                var books = bookRepo.GetBooks(1, 3);

                Assert.Equal(3, books.Count());
            }
        }

        [Fact]
        public void GetBook_GuidIsEmpty_ThrowArgumentException()
        {
            var option = new DbContextOptionsBuilder<BookContext>()
               .UseInMemoryDatabase($"BookDbForTesting{Guid.NewGuid()}")
               .Options;

            using (var context = new BookContext(option))
            {
                var bookRepo = new BookRepository(context);

                Assert.Throws<ArgumentException>(() => bookRepo.GetBook(Guid.Empty));
            }
        }

        [Fact]
        public void AddBook_BookWithoutAuthor_BookHasSalamiAsAuthor()
        {
            var option = new DbContextOptionsBuilder<BookContext>()
               .UseInMemoryDatabase($"BookDbForTesting{Guid.NewGuid()}")
               .Options;

            using (var context = new BookContext(option))
            {

                var bookRepo = new BookRepository(context);

                var bookGuid = Guid.NewGuid();

                bookRepo.AddBook(new Book { Id = bookGuid, Title = "Java" }) ;

                var book = bookRepo.GetBook(bookGuid);
                Assert.Equal("Salami", book.Author.Name);
            }
        }
    }
}
