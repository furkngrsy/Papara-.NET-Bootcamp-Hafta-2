using Papara_Bootcamp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Papara_Bootcamp.Services
{
    public class FakeBookService : IBookService
    {
        private List<Book> books = new List<Book>
        {
            new Book { Id = 1, Name = "Suc ve Ceza", Author = "Fyodor Dostoyevski", PageCount = 600, Year = 1866 },
            new Book { Id = 2, Name = "Seker Portakalı", Author = "Jose Mauro de Vasconcelos", PageCount = 600, Year = 1968 },
        };

        public IEnumerable<Book> GetAllBooks()
        {
            return books;
        }

        public Book GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void AddBook(Book book)
        {
            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);
        }

        public void UpdateBook(int id, Book book)
        {
            var existingBook = books.FirstOrDefault(b => b.Id == id);
            if (existingBook != null)
            {
                existingBook.Name = book.Name;
                existingBook.Author = book.Author;
                existingBook.PageCount = book.PageCount;
                existingBook.Year = book.Year;
            }
        }

        public void DeleteBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
        }



    }
}