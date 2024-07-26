using BarrenSellTicket.Domain.Entities.Events;
using BarrenSellTicket.Persistance.DbConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.DbConfigurations.Events
{
    public class CouponConfigurations:BaseConfigurations<Coupon>
    {
        public override void Configure(EntityTypeBuilder<Coupon> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o=>o.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.DiscountType)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(o => o.DiscountEnd)
                .IsRequired();

            builder.Property(o => o.Time)
                .IsRequired();

            builder
                .HasMany(x=>x.OrderCoupons)
                .WithOne(x=>x.Coupon)
                .HasForeignKey(x=>x.CouponId);
        }
    }
}
