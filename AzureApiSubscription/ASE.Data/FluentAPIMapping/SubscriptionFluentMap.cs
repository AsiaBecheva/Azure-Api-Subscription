namespace ASE.Data.FluentAPIMapping
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using ASE.Models;

    public class SubscriptionFluentMap : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("Subscriptions");
            builder.Property(s => s.Id).UseSqlServerIdentityColumn();
            builder.Property(s => s.TenantId).IsRequired();
            builder.Property(s => s.GrantType).IsRequired();
            builder.Property(s => s.ClientId).IsRequired();
            builder.Property(s => s.ClientSecret).IsRequired();
            builder.Property(s => s.Resource).IsRequired();
            builder.Property(s => s.BearerToken).IsRequired();

            builder.HasOne(s => s.User)
                                     .WithMany(u => u.Subscriptions)
                                     .HasForeignKey(s => s.UserId);
        }
    }

}
