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
    public class PayoutsConfigurations:BaseConfigurations<Payouts>
    {
        public override void Configure(EntityTypeBuilder<Payouts> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.DatePaid)
                .IsRequired();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.TransactionID)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.CardNumber)
                .IsRequired()
                .HasMaxLength(16);

            builder.Property(p => p.CVV)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(p => p.ExpiryDate)
                .IsRequired();

            builder.HasOne(p => p.Order)
                .WithMany(p => p.Payouts)
                .HasForeignKey(p => p.OrderId);

            builder.HasOne(p => p.BankAccount)
                .WithMany(p => p.Payouts)
                .HasForeignKey(p => p.BankAccountId);
        }
    }
}
