using Papara_Bootcamp.Models;

namespace Papara_Bootcamp.Extensions
{
    public static class BookExtensions
    {
        public static bool IsClassic(this Book book)
        {
            // 1950 yılından önce yayımlanan kitapları klasik olarak kabul ediyoruz
            return book.Year < 1950;
        }

    }
}
