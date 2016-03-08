namespace SimplePersistence.UoW.EF
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
#if NET40 || NET45
    using System.Data.Entity;
#else
    using Microsoft.Data.Entity;
#endif

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
#if !(NET40 || NET45)
    [CLSCompliant(false)]
#endif
    public abstract class EFQueryableRepository<TEntity> : IQueryableRepository<TEntity>
        where TEntity : class 
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// The <see cref="DbSet{TEntity}"/> of this repository entity
        /// </summary>
        protected DbSet<TEntity> Set { get; }

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        protected EFQueryableRepository(DbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
            Set = context.Set<TEntity>();
        }

        #region Implementation of IAsyncRepository<TEntity>

        public Task<TEntity> GetByIdAsync(params object[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(CancellationToken ct, params object[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public Task<long> TotalAsync(CancellationToken ct = new CancellationToken())
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(params object[] ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> ExistsAsync(CancellationToken ct, params object[] ids)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Implementation of ISyncRepository<TEntity>

        public TEntity GetById(params object[] ids)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Add(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Add(params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Update(params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public TEntity Delete(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TEntity> Delete(params TEntity[] entities)
        {
            throw new System.NotImplementedException();
        }

        public long Total()
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(params object[] ids)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Implementation of IExposeQueryable<TEntity>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/>
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public IQueryable<TEntity> Query()
        {
            return Set;
        }

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public abstract IQueryable<TEntity> QueryById(params object[] ids);

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> 
        ///             that will also fetch, on execution, all the entity navigation properties
        /// </summary>
        /// <param name="propertiesToFetch">The navigation properties to also fetch on query execution</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public IQueryable<TEntity> QueryFetching(params Expression<Func<TEntity, object>>[] propertiesToFetch)
        {
            if (propertiesToFetch == null) throw new ArgumentNullException(nameof(propertiesToFetch));

            return propertiesToFetch.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
                Set, (current, expression) => current.Include(expression));
        }

        #endregion
    }
}
