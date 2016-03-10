namespace SimplePersistence.UoW.EF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data.Entity;

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
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

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return GetByIdAsync(CancellationToken.None, ids);
        }
#else
        public async Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return await GetByIdAsync(CancellationToken.None, ids);
        }
#endif

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="ids">The entity unique identifier</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(CancellationToken ct, params object[] ids)
        {
            return Task.Factory.StartNew(() => Set.Find(ids), ct);
        }
#else
        public async Task<TEntity> GetByIdAsync(CancellationToken ct, params object[] ids)
        {
            return await Set.FindAsync(ct, ids);
        }
#endif

        /// <summary>
        /// Adds the entity to the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to add</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entity
        /// </returns>
#if NET40
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            var tcs = new TaskCompletionSource<TEntity>();
            tcs.SetResult(Add(entity));
            return tcs.Task;
        }
#else
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            return await Task.FromResult(Add(entity));
        }
#endif

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            return AddAsync(ct, entities.ToArray());
        }
#else
        public async Task<IEnumerable<TEntity>> AddAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            return await AddAsync(ct, entities.ToArray());
        }
#endif

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The entity to add</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities)
        {
            return AddAsync(CancellationToken.None, entities);
        }
#else
        public async Task<IEnumerable<TEntity>> AddAsync(params TEntity[] entities)
        {
            return await AddAsync(CancellationToken.None, entities);
        }
#endif

        /// <summary>
        /// Adds a range of entities to the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param><param name="entities">The entity to add</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities)
        {
            var tcs = new TaskCompletionSource<IEnumerable<TEntity>>();
            tcs.SetResult(Add(entities));
            return tcs.Task;
        }
#else
        public async Task<IEnumerable<TEntity>> AddAsync(CancellationToken ct, params TEntity[] entities)
        {
            return await Task.FromResult(Add(entities));
        }
#endif

        /// <summary>
        /// Updates the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entity
        /// </returns>
#if NET40
        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            var tcs = new TaskCompletionSource<TEntity>();
            tcs.SetResult(Update(entity));
            return tcs.Task;
        }
#else
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            return await Task.FromResult(Update(entity));
        }
#endif

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            return UpdateAsync(ct, entities.ToArray());
        }
#else
        public async Task<IEnumerable<TEntity>> UpdateAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            return await UpdateAsync(ct, entities.ToArray());
        }
#endif

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities)
        {
            return UpdateAsync(CancellationToken.None, entities);
        }
#else
        public async Task<IEnumerable<TEntity>> UpdateAsync(params TEntity[] entities)
        {
            return await UpdateAsync(CancellationToken.None, entities);
        }
#endif

        /// <summary>
        /// Updates a range of entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param><param name="entities">The entities to update</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities)
        {
            var tcs = new TaskCompletionSource<IEnumerable<TEntity>>();
            tcs.SetResult(Update(entities));
            return tcs.Task;
        }
#else
        public async Task<IEnumerable<TEntity>> UpdateAsync(CancellationToken ct, params TEntity[] entities)
        {
            return await Task.FromResult(Update(entities));
        }
#endif

        /// <summary>
        /// Deletes the entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entity
        /// </returns>
#if NET40
        public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            var tcs = new TaskCompletionSource<TEntity>();
            tcs.SetResult(Delete(entity));
            return tcs.Task;
        }
#else
        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken ct = new CancellationToken())
        {
            return await Task.FromResult(Delete(entity));
        }
#endif

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            return DeleteAsync(ct, entities.ToArray());
        }
#else
        public async Task<IEnumerable<TEntity>> DeleteAsync(IEnumerable<TEntity> entities, CancellationToken ct = new CancellationToken())
        {
            return await DeleteAsync(ct, entities.ToArray());
        }
#endif

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities)
        {
            return DeleteAsync(CancellationToken.None, entities);
        }
#else
        public async Task<IEnumerable<TEntity>> DeleteAsync(params TEntity[] entities)
        {
            return await DeleteAsync(CancellationToken.None, entities);
        }
#endif

        /// <summary>
        /// Deletes a range of entity in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param><param name="entities">The entities to delete</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the entities
        /// </returns>
#if NET40
        public Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities)
        {
            var tcs = new TaskCompletionSource<IEnumerable<TEntity>>();
            tcs.SetResult(Delete(entities));
            return tcs.Task;
        }
#else
        public async Task<IEnumerable<TEntity>> DeleteAsync(CancellationToken ct, params TEntity[] entities)
        {
            return await Task.FromResult(Delete(entities));
        }
#endif

        /// <summary>
        /// Gets the total entities in the repository asynchronously
        /// </summary>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> containing the total
        /// </returns>
#if NET40
        public Task<long> TotalAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => Query().LongCount(), ct);
        }
#else
        public async Task<long> TotalAsync(CancellationToken ct = new CancellationToken())
        {
            return await Query().LongCountAsync(ct);
        }
#endif

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
#if NET40
        public Task<bool> ExistsAsync(params object[] ids)
        {
            return ExistsAsync(CancellationToken.None, ids);
        }
#else
        public async Task<bool> ExistsAsync(params object[] ids)
        {
            return await ExistsAsync(CancellationToken.None, ids);
        }
#endif

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param><param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
#if NET40
        public Task<bool> ExistsAsync(CancellationToken ct, params object[] ids)
        {
            return Task.Factory.StartNew(() => QueryById(ids).Any(), ct);
        }
#else
        public async Task<bool> ExistsAsync(CancellationToken ct, params object[] ids)
        {
            return await QueryById(ids).AnyAsync(ct);
        }
#endif

        #endregion

        #region Implementation of ISyncRepository<TEntity>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public TEntity GetById(params object[] ids)
        {
            return Set.Find(ids);
        }

        /// <summary>
        /// Adds the entity to the repository
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>
        /// The entity
        /// </returns>
        public TEntity Add(TEntity entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                return Set.Add(entity);

            dbEntityEntry.State = EntityState.Added;
            return dbEntityEntry.Entity;
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>
        /// The range of entities added
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Set.AddRange(entities);
        }

        /// <summary>
        /// Adds a range of entities to the repository
        /// </summary>
        /// <param name="entities">The entities to add</param>
        /// <returns>
        /// The range of entities added
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEnumerable<TEntity> Add(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Set.AddRange(entities);
        }

        /// <summary>
        /// Updates the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <returns>
        /// The entity
        /// </returns>
        public TEntity Update(TEntity entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                Set.Attach(entity);
            if (dbEntityEntry.State != EntityState.Added && dbEntityEntry.State != EntityState.Deleted)
                dbEntityEntry.State = EntityState.Modified;

            return dbEntityEntry.Entity;
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Update(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Update(entities.ToArray());
        }

        /// <summary>
        /// Updates a range of entities in the repository
        /// </summary>
        /// <param name="entities">The entities to update</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Update(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));

            var result = new TEntity[entities.Length];
            for (var i = 0; i < entities.Length; i++)
                result[i] = Update(entities[i]);
            return result;
        }

        /// <summary>
        /// Deletes the entity in the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <returns>
        /// The entity
        /// </returns>
        public TEntity Delete(TEntity entity)
        {
            var dbEntityEntry = Context.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
                return dbEntityEntry.Entity;
            }

            Set.Attach(entity);
            Set.Remove(entity);

            return entity;
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Set.RemoveRange(entities);
        }

        /// <summary>
        /// Deletes a range of entity in the repository
        /// </summary>
        /// <param name="entities">The entities to delete</param>
        /// <returns>
        /// The entities
        /// </returns>
        public IEnumerable<TEntity> Delete(params TEntity[] entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return Set.RemoveRange(entities);
        }

        /// <summary>
        /// Gets the total entities in the repository
        /// </summary>
        /// <returns>
        /// The total
        /// </returns>
        public long Total()
        {
            return Query().LongCount();
        }

        public bool Exists(params object[] ids)
        {
            return QueryById(ids).Any();
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
