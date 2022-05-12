using ProductAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Entities.Dtos
{
    public class ProductAddDto
    {
        [DisplayName("Ürün Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz!")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz!")]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklaması")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz!")]
        public string Description { get; set; }
        [DisplayName("Ürün Fiyatı")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$",ErrorMessage = "{0} virgülden sonra en fazla iki basamaklı olmalı!")]
        [Range(0, 999999.99,ErrorMessage = "{0} {1}'den küçük {2}'den büyük olamaz!")]
        public decimal Price { get; set; }
        [DisplayName("Ürün Stok Miktarı")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        public int UnitsInStock { get; set; }
        [DisplayName("Ürün Kategorisi")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
