using BarrenSellTicket.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Persistance.DbConfigurations.Common
{
    public class BaseConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(k=>k.Id).HasColumnName
                ("Id").UseIdentityColumn();
            builder.Property(k => k.CreatedDate).HasColumnName
                ("CreatedDateTime").IsRequired();
            builder.Property(k => k.UpdatedDate).HasColumnName
                ("UpdatedDateTime").IsRequired();
            builder.Property(k => k.CreatedId).HasColumnName
                ("CreatedId");
            builder.Property(k => k.UpdatedId).HasColumnName
                ("UpdatedId");
        }
    }
}
