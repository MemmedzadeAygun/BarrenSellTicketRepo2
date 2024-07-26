using BarrenSellTicket.Domain.Entities.Accounts;
using BarrenSellTicket.Domain.Entities.Events;
using BarrenSellTicket.Persistance.DbConfigurations.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.DbConfigurations.Accounts
{
    public class UserConfigurations:UserBaseConfigurations<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Email)
                .IsUnique();

            builder.Property(x => x.Email)
                .IsRequired();

            builder
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.OrganizerDetail)
                .WithOne(x => x.User)
                .HasForeignKey<OrganizerDetail>(x => x.UserId);
        }
    }
}
