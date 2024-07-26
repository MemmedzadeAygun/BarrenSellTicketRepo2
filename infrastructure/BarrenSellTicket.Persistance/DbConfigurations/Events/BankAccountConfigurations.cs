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
    public class BankAccountConfigurations:BaseConfigurations<BankAccount>
    {
        public override void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.AccountName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.BankName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.AccountNumber)
                .IsRequired()
                .HasColumnType("bigint");

            builder.Property(b => b.SwiftCode)
               .IsRequired()
               .HasMaxLength(11);

            builder.Property(b => b.Iban)
              .IsRequired()
              .HasMaxLength(34);
        }
    }
}
