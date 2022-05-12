using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Entities.Dtos
{
    public class CategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilemez!")]
        [MaxLength(30, ErrorMessage = "{0} alanı {1} karakterden uzun olamaz!")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz!")]
        public string Name { get; set; }
        [DisplayName("Kategori Açıklaması")]
        [MinLength(10, ErrorMessage = "{0} alanı {1} karakterden kısa olamaz!")]
        public string Description { get; set; }

    }
}
