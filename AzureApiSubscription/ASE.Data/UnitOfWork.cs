namespace ASE.Data
{
    using ASE.Data.Contracts;
    using ASE.Data.Repositories;
    using ASE.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AzureApiSubDbContext azureApiSubDbContext;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork(AzureApiSubDbContext azureApiSubDbContext)
        {
            this.azureApiSubDbContext = azureApiSubDbContext;
        }

        public IGenericRepository<User> Users
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IGenericRepository<Subscription> Subscriptions
        {
            get
            {
                return this.GetRepository<Subscription>();
            }
        }

        public DbContext Context
        {
            get
            {
                return this.azureApiSubDbContext;
            }
        }

        public int SaveChanges()
        {
            return this.azureApiSubDbContext.SaveChanges();
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.azureApiSubDbContext));
            }

            return (IGenericRepository<T>)this.repositories[typeof(T)];
        }
    }
}