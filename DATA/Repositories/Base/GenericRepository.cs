using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DATA.Contexts;
using DOMAIN.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DATA.Repositories.Base
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : DOMAIN.Models.Base.Base
    {
        protected AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<T> GetAll()
        {
            return this._context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsyn()
        {
            return await this._context.Set<T>().ToListAsync();
        }

        public virtual T Get(int id)
        {
            return this._context.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await this._context.Set<T>().FindAsync(id);
        }

        public virtual T Add(T t)
        {
            this._context.Set<T>().Add(t);
            this._context.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            this._context.Set<T>().Add(t);
            await this._context.SaveChangesAsync();
            return t;
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return this._context.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await this._context.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return this._context.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await this._context.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete(T entity)
        {
            this._context.Set<T>().Remove(entity);
            this._context.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn(T entity)
        {
            this._context.Set<T>().Remove(entity);
            return await this._context.SaveChangesAsync();
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = this._context.Set<T>().Find(key);
            if (exist != null)
            {
                this._context.Entry(exist).CurrentValues.SetValues(t);
                this._context.SaveChanges();
            }

            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await this._context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                this._context.Entry(exist).CurrentValues.SetValues(t);
                await this._context.SaveChangesAsync();
            }

            return exist;
        }

        public int Count()
        {
            return this._context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await this._context.Set<T>().CountAsync();
        }

        public virtual void Save()
        {
            this._context.SaveChanges();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await this._context.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = this._context.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await this._context.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
                queryable = queryable.Include(includeProperty);

            return queryable;
        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    this._context.Dispose();
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}