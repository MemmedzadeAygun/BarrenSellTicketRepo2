using BarrenSellTicket.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.DbConfigurations.Events
{
    public class OrderCouponConfigurations : IEntityTypeConfiguration<OrderCoupon>
    {
        public void Configure(EntityTypeBuilder<OrderCoupon> builder)
        {
            builder.HasKey(oc => new {oc.OrderId, oc.CouponId});
        }
    }
}
