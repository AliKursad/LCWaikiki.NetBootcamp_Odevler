using ProductAPI.Entities.Concrete;
using ProductAPI.Shared.Entities.Abstract;
using ProductAPI.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Entities.Dtos
{
    public class ProductDto:DtoGetBase
    {
        public Product Product { get; set; }
    }
}
