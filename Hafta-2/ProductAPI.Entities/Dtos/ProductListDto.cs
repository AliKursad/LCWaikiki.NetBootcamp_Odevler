using ProductAPI.Entities.Concrete;
using ProductAPI.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Entities.Dtos
{
    public class ProductListDto:DtoGetBase
    {
        public IList<Product> Products { get; set; }
    }
}
