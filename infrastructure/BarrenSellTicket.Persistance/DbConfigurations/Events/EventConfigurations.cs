using BarrenSellTicket.Domain.Entities.Common;
using BarrenSellTicket.Domain.Entities.Events;
using BarrenSellTicket.Persistance.DbConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.DbConfigurations.Events
{
    public class EventConfiguration: BaseConfigurations<Event>
    {
        public override void Configure(EntityTypeBuilder<Event> builder)
        {
            base.Configure(builder);

            builder.Property(e=>e.Name).IsRequired()
                .HasColumnName("EventName")
                .HasMaxLength(200);

            builder.Property(e => e.Duration)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.EventDate)
                .IsRequired();

            builder.Property(e => e.BeginTime)
                .IsRequired();

            builder.Property(e => e.EndTime)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(1000);

            builder
                .HasOne(e=>e.EventType)
                .WithMany(e=>e.Events)
                .HasForeignKey(e=>e.EventTypeId);

            builder
                .HasOne(e=>e.EventCategory)
                .WithMany(e=>e.Events)
                .HasForeignKey(e=>e.EventCategoryId);

            builder
                .HasOne(e => e.Image)
                .WithOne(e => e.Event)
                .HasForeignKey<Event>(e => e.ImageId);

            builder
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId);
        }
    }
}
