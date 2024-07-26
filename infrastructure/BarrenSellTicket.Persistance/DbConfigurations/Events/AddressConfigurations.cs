using BarrenSellTicket.Domain.Entities.Events;
using BarrenSellTicket.Persistance.DbConfigurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.DbConfigurations.Events
{
    public class AddressConfigurations:BaseConfigurations<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);

            builder.Property(k => k.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(k=>k.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(k=>k.Addres)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .HasMany(k => k.Events)
                .WithOne(k => k.Address)
                .HasForeignKey(k => k.AddressId);

            builder
                .HasMany(k => k.Customers)
                .WithOne(k => k.Address)
                .HasForeignKey(k => k.AddressId);

            builder
                .HasMany(k => k.OrganizerDetails)
                .WithOne(k => k.Address)
                .HasForeignKey(k => k.AddressId);

        }
    }
}
