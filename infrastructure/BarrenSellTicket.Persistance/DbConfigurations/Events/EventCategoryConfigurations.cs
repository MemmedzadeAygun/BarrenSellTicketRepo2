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
    public class EventCategoryConfigurations:BaseConfigurations<EventCategory>
    {
        public override void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasColumnName("CategoryName");
        }
    }
}
