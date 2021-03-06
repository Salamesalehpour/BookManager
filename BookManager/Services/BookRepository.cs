﻿using BookManager.Data;
using BookManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Api.Services
{
    public class BookRepository : IDisposable
    {
        private readonly BookContext bookContext;

        public BookRepository(BookContext context)
        {
            bookContext = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return bookContext.Books.ToList();
        }

        public IEnumerable<Book> GetBooks(int pageNumber = 1, int pageSize = 5)
        {
            return bookContext.Books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Book GetBook(Guid bookId)
        {
            if (bookId == Guid.Empty)
            {
                throw new ArgumentException(nameof(bookId));
            }

            return bookContext.Books.FirstOrDefault(b => b.Id == bookId);
        }

        public void AddBook(Book book)
        {
            try
            {
                if (book.Author == null)
                {
                    book.Author = new Author { Id = 1, Name = "Salami" };
                }

                bookContext.Books.Add(book);

                bookContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
