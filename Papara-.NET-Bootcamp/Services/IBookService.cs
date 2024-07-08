using Papara_Bootcamp.Models;
using System.Collections.Generic;

namespace Papara_Bootcamp.Services
{   
    public interface IBookService
    {
        IEnumerable<Book> GetAllBooks();
        Book GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(int id, Book book);
        void DeleteBook(int id);
    }
}
