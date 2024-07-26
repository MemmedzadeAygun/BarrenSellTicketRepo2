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
    public class ContactListConfigurations:BaseConfigurations<ContactList>
    {
        public override void Configure(EntityTypeBuilder<ContactList> builder)
        {
            base.Configure(builder);

            builder
                .Property(y => y.ListName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(y => y.Description)
                .HasMaxLength(1000);

            builder.Property(y => y.FirstName).HasMaxLength(50);
            builder.Property(y => y.LastName).HasMaxLength(50);

            builder
                .HasOne(y => y.User)
                .WithMany(y => y.ContactLists)
                .HasForeignKey(y => y.UserId);
        }
    }
}
