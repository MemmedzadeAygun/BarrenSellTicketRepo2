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
    public class TicketConfigurations:BaseConfigurations<Ticket>
    {
        public override void Configure(EntityTypeBuilder<Ticket> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.AvailableCount)
                .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder
                .HasMany(x => x.Orders)
                .WithOne(x => x.Ticket)
                .HasForeignKey(x => x.TicketId);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Tickets)
                .HasForeignKey(x=>x.UserId);
        }
    }
}
