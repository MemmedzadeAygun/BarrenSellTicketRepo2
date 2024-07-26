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
    public class CustomerConfigurations:BaseConfigurations<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);

            builder
                .Property(z => z.FirstName)
                .HasMaxLength(50);

            builder
                .Property(z => z.LastName)
                .HasMaxLength(50);

            builder
                .HasOne(z => z.User)
                .WithOne(z => z.Customer)
                .HasForeignKey<Customer>(z => z.UserId);

            builder
                .HasMany(z => z.BankAccounts)
                .WithOne(z => z.Customer)
                .HasForeignKey(z => z.CustomerId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
        }
    }
}
