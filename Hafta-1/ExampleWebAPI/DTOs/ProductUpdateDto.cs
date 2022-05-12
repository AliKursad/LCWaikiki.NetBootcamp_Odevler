using System.ComponentModel.DataAnnotations;
using System;

namespace ExampleWebAPI.DTOs
{
    public class ProductUpdateDto
    {
        [Display(Name = "Ürün Adı")]
        [MaxLength(20, ErrorMessage = "{0} {1} karakterden büyük olamaz!")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olamaz!")]
        public string Name { get; set; }
        [Display(Name = "Stok Sayısı")]
        [Range(0, Int32.MaxValue, ErrorMessage = "{0} , {1} değerinden küçük olamaz!")]
        public int InStock { get; set; }
        [Display(Name = "Fiyat")]
        [Range(1, Double.MaxValue, ErrorMessage = "{0} , {1} değerinden küçük olamaz!")]
        public decimal Price { get; set; }
    }
}
