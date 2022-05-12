using ProductAPI.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Entities.Concrete
{
    public class Product:EntityBase,IEntity
    {
        public decimal Price { get; set; }
        public int UnitsInStock { get; set; }
        public int/*?*/ CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
