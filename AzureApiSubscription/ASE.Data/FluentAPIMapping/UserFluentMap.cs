namespace ASE.Data.FluentAPIMapping
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using ASE.Models;

    public class UserFluentMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Id).UseSqlServerIdentityColumn();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();
        }
    }
}
