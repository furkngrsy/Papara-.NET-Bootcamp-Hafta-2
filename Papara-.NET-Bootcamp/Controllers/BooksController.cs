using Papara_Bootcamp.Models;
using Papara_Bootcamp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Papara_Bootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<ApiResponse<IEnumerable<Book>>> GetBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(new ApiResponse<IEnumerable<Book>>
            {
                Success = true,
                Message = "Kitaplar başarılı bir şekilde getirildi.",
                Data = books,
                StatusCode = 200
            });
        }

        [HttpGet("{id}")]
        public ActionResult<ApiResponse<Book>> GetBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new ApiResponse<Book>
                {
                    Success = false,
                    Message = "Kitap bulunamadı.",
                    StatusCode = 404
                });
            }
            return Ok(new ApiResponse<Book>
            {
                Success = true,
                Message = "Kitap başarılı bir şekilde getirildi.",
                Data = book,
                StatusCode = 200
            });
        }

        [HttpPost]
        public ActionResult<ApiResponse<Book>> PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors);
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Doğrulama hatası.",
                    Data = errors,
                    StatusCode = 400
                });
            }
            _bookService.AddBook(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, new ApiResponse<Book>
            {
                Success = true,
                Message = "Kitap başarılı bir şekilde eklendi.",
                Data = book,
                StatusCode = 201
            });
        }

        [HttpPut("{id}")]
        public IActionResult PutBook(int id, [FromBody] Book book)
        {
            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound(new ApiResponse<Book>
                {
                    Success = false,
                    Message = "Kitap bulunamadı.",
                    StatusCode = 404
                });
            }

            _bookService.UpdateBook(id, book);

            return Ok(new ApiResponse<Book>
            {
                Success = true,
                Message = "Kitap başarılı bir şekilde güncellendi.",
                Data = existingBook,
                StatusCode = 200
            });
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBook(int id, [FromBody] Book updatedFields)
        {
            var existingBook = _bookService.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound(new ApiResponse<Book>
                {
                    Success = false,
                    Message = "Kitap bulunamadı.",
                    StatusCode = 404
                });
            }

            if (!string.IsNullOrEmpty(updatedFields.Name))
            {
                existingBook.Name = updatedFields.Name;
            }
            if (!string.IsNullOrEmpty(updatedFields.Author))
            {
                existingBook.Author = updatedFields.Author;
            }
            if (updatedFields.PageCount != 0)
            {
                existingBook.PageCount = updatedFields.PageCount;
            }
            if (updatedFields.Year != 0)
            {
                existingBook.Year = updatedFields.Year;
            }

            return Ok(new ApiResponse<Book>
            {
                Success = true,
                Message = "Kitap başarılı bir şekilde güncellendi.",
                Data = existingBook,
                StatusCode = 200
            });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound(new ApiResponse<Book>
                {
                    Success = false,
                    Message = "Kitap bulunamadı.",
                    StatusCode = 404
                });
            }

            _bookService.DeleteBook(id);

            return Ok(new ApiResponse<Book>
            {
                Success = true,
                Message = "Kitap başarılı bir şekilde silindi.",
                Data = book,
                StatusCode = 200
            });
        }

        [HttpGet("list")]
        public ActionResult<ApiResponse<IEnumerable<Book>>> ListBooks([FromQuery] string name) // name parametresi alarak listeleme
        {
            var filteredBooks = _bookService.GetAllBooks().AsQueryable();

            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new ApiResponse<IEnumerable<Book>>
                {
                    Success = false,
                    Message = "Geçersiz parametre.",
                    StatusCode = 400
                });
            }

            filteredBooks = filteredBooks.Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            filteredBooks = filteredBooks.OrderBy(b => b.Id); // id'ye göre sıralama işlemi

            return Ok(new ApiResponse<IEnumerable<Book>>
            {
                Success = true,
                Message = "Kitap başarılı bir şekilde getirildi.",
                Data = filteredBooks.ToList(),
                StatusCode = 200
            });
        }
    }
}
