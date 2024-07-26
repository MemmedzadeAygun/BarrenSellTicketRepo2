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
    public class EventTypeConfigurations:BaseConfigurations<EventType>
    {
        public override void Configure(EntityTypeBuilder<EventType> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("EventName");
        }
    }
}
