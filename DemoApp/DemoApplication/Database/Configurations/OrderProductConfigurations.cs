using DemoApplication.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApplication.Database.Configurations
{
    public class OrderProductConfigurations : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder
                .ToTable("OrderProducts");

            builder
              .HasOne(op => op.Order)
              .WithMany(order => order.OrderProducts)
              .HasForeignKey(op => op.Orderİd);

            builder
              .HasOne(op => op.Book)
              .WithMany(book => book.OrderProducts)
              .HasForeignKey(op => op.BookId);

        }
    }
}
