namespace ASE.Data
{
    using Microsoft.EntityFrameworkCore;

    using ASE.Models;
    using ASE.Data.FluentAPIMapping;

    public class AzureApiSubDbContext : DbContext
    {
        public AzureApiSubDbContext(DbContextOptions<AzureApiSubDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserFluentMap());
            modelBuilder.ApplyConfiguration(new SubscriptionFluentMap());

            //modelBuilder.Entity<Subscription>()
            //    .HasOne(s => s.User)
            //    .WithMany(u => u.Subscriptions)
            //    .HasForeignKey(s => s.UserId);
            //modelBuilder.Entity<User>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }
    }
}