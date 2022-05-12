using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductAPI.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Data.Concrete.EntityFramework.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(30);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();
            builder.Property(p => p.ModifiedDate).IsRequired();
            builder.Property(p => p.Description).HasColumnType("NVARCHAR(MAX)");
            builder.Property(p => p.Price).HasPrecision(8, 2);
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.UnitsInStock).IsRequired();
            builder.HasOne<Category>(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId)/*.OnDelete(DeleteBehavior.SetNull)*/;
            builder.ToTable("Products");

            builder.HasData(
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Yünlü Kazak",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = "Kalın %100 yün kazak.",
                    Price = 350,
                    UnitsInStock = 2500,
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Boğazlı Kazak",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = "Kalın %100 yün boğazlı kazak.",
                    Price = 300,
                    UnitsInStock = 1800,
                },
                new Product
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Polo Yaka Tişört",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = "Polo yaka yazlık tişört.",
                    Price = 150,
                    UnitsInStock = 3200,
                },
                new Product
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "V Yaka Tişört",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = "V Yaka yazlık tişört.",
                    Price = 120,
                    UnitsInStock = 3900,
                },
                new Product
                {
                    Id = 5,
                    CategoryId = 3,
                    Name = "Spor Ayakkabı",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = "Esnek, rahat tabanlı spor ayakkabı.",
                    Price = 200,
                    UnitsInStock = 1500,
                },
                new Product
                {
                    Id = 6,
                    CategoryId = 3,
                    Name = "Bot",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Description = "Su geçirmez, soğuğa dayanıklı bağcıklı bot.",
                    Price = 450,
                    UnitsInStock = 2300,
                });
        }
    }
}
