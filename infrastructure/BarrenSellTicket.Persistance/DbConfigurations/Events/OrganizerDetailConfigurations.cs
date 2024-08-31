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
    public class OrganizerDetailConfigurations:BaseConfigurations<OrganizerDetail>
    {
        public override void Configure(EntityTypeBuilder<OrganizerDetail> builder)
        {
            base.Configure(builder);

            builder.Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.About)
                .HasMaxLength(1000);

            builder.Property(o => o.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder
                .HasOne(x => x.ProfileImage)
                .WithOne(x => x.OrganizerDetail)
                .HasForeignKey<OrganizerDetail>(x => x.ImageId);

        }
    }
}
