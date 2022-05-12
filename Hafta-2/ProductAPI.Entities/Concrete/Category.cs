using ProductAPI.Entities.Dtos;
using ProductAPI.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Entities.Concrete
{
    public class Category:EntityBase,IEntity
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        public ICollection<Product> Products { get; set; }

    }
}
