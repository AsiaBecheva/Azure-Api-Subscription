﻿namespace ASE.Data.Contracts
{
    using System;
    using System.Linq;

    public interface IGenericRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        void Detach(T entity);

        int SaveChanges();
    }
}