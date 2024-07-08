using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Papara_Bootcamp.Models
{
    public class Book
    {
        [Required]
        [Range(minimum:1,maximum:10000)]
        [DisplayName("Kitap Id")]
        public int Id { get; set; }


        [Required]
        [StringLength(maximumLength:50,MinimumLength =5)]
        [DisplayName("Kitap Adı")]
        public string Name { get; set; }


        [StringLength(maximumLength: 50, MinimumLength = 5)]
        [DisplayName("Kitap Yazar Bilgisi")]
        public string Author { get; set; }


        [Range(minimum: 50, maximum: 400)]
        [DisplayName("Kitap Sayfa Sayısı")]
        public int PageCount { get; set; }


        [Range(minimum: 1850, maximum: 2024)]
        [DisplayName("Kitap Yılı")]
        public int Year { get; set; }

    }
}
