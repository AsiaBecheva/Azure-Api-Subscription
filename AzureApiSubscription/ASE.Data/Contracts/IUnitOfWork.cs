namespace ASE.Data.Contracts
{
    using ASE.Models;
    using Microsoft.EntityFrameworkCore;

    public interface IUnitOfWork
    {
        DbContext Context { get; }

        IGenericRepository<User> Users { get; }

        IGenericRepository<Subscription> Subscriptions { get; }

        int SaveChanges();
    }
}