#region License
// The MIT License (MIT)
// 
// Copyright (c) 2016 SimplePersistence
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
namespace SimplePersistence.UoW.EF
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey">The entity id first type</typeparam>
    public abstract class EFQueryableRepository<TEntity, TKey>
        : EFQueryableRepository<TEntity>, IQueryableRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        protected EFQueryableRepository(DbContext context) : base(context)
        {

        }

        #region Implementation of IAsyncRepository<TEntity,in TKey>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity first unique identifier value</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(TKey id)
        {
            return GetByIdAsync(id, CancellationToken.None);
        }
#else
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await GetByIdAsync(id, CancellationToken.None);
        }
#endif

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id">The entity first unique identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct)
        {
            return GetByIdAsync(ct, id);
        }
#else
        public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken ct)
        {
            return await GetByIdAsync(ct, id);
        }
#endif

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity first unique identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
#if NET40
        public Task<bool> ExistsAsync(TKey id, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => QueryById(id).Any(), ct);
        }
#else
        public async Task<bool> ExistsAsync(TKey id, CancellationToken ct = new CancellationToken())
        {
            return await QueryById(id).AnyAsync(ct);
        }
#endif

        #endregion

        #region Implementation of ISyncRepository<TEntity,in TKey>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id">The entity first unique identifier value</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public TEntity GetById(TKey id)
        {
            return base.GetById(id);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id">The entity first unique identifier value</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
        public bool Exists(TKey id)
        {
            return QueryById(id).Any();
        }

        #endregion

        #region Implementation of IExposeQueryable<TEntity,in TKey>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="id">The entity first unique identifier value</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public abstract IQueryable<TEntity> QueryById(TKey id);

        #endregion

        #region Overrides of EFQueryableRepository<TEntity>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public override IQueryable<TEntity> QueryById(params object[] ids)
        {
            return QueryById((TKey)ids[0]);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    public abstract class EFQueryableRepository<TEntity, TKey01, TKey02>
        : EFQueryableRepository<TEntity>, IQueryableRepository<TEntity, TKey01, TKey02>
        where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        protected EFQueryableRepository(DbContext context) : base(context)
        {

        }

        #region Implementation of IAsyncRepository<TEntity,in TKey01,in TKey02>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, CancellationToken ct = new CancellationToken())
        {
            return GetByIdAsync(ct, id01, id02);
        }
#else
        public async Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, CancellationToken ct = new CancellationToken())
        {
            return await GetByIdAsync(ct, id01, id02);
        }
#endif

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
#if NET40
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => QueryById(id01, id02).Any(), ct);
        }
#else
        public async Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, CancellationToken ct = new CancellationToken())
        {
            return await QueryById(id01, id02).AnyAsync(ct);
        }
#endif

        #endregion

        #region Implementation of ISyncRepository<TEntity,in TKey01,in TKey02>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public TEntity GetById(TKey01 id01, TKey02 id02)
        {
            return base.GetById(id01, id02);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
        public bool Exists(TKey01 id01, TKey02 id02)
        {
            return QueryById(id01, id02).Any();
        }

        #endregion

        #region Implementation of IExposeQueryable<TEntity,in TKey01,in TKey02>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public abstract IQueryable<TEntity> QueryById(TKey01 id01, TKey02 id02);

        #endregion

        #region Overrides of EFQueryableRepository<TEntity>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public override IQueryable<TEntity> QueryById(params object[] ids)
        {
            return QueryById((TKey01) ids[0], (TKey02) ids[1]);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    /// <typeparam name="TKey03">The entity id third type</typeparam>
    public abstract class EFQueryableRepository<TEntity, TKey01, TKey02, TKey03>
        : EFQueryableRepository<TEntity>, IQueryableRepository<TEntity, TKey01, TKey02, TKey03>
        where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        protected EFQueryableRepository(DbContext context) : base(context)
        {

        }

        #region Implementation of IAsyncRepository<TEntity,in TKey01,in TKey02,in TKey03>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, CancellationToken ct = new CancellationToken())
        {
            return GetByIdAsync(ct, id01, id02, id03);
        }
#else
        public async Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, CancellationToken ct = new CancellationToken())
        {
            return await GetByIdAsync(ct, id01, id02, id03);
        }
#endif

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
#if NET40
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => QueryById(id01, id02, id03).Any(), ct);
        }
#else
        public async Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, CancellationToken ct = new CancellationToken())
        {
            return await QueryById(id01, id02, id03).AnyAsync(ct);
        }
#endif

        #endregion

        #region Implementation of ISyncRepository<TEntity,in TKey01,in TKey02,in TKey03>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public TEntity GetById(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return base.GetById(id01, id02, id03);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
        public bool Exists(TKey01 id01, TKey02 id02, TKey03 id03)
        {
            return QueryById(id01, id02, id03).Any();
        }

        #endregion

        #region Implementation of IExposeQueryable<TEntity,in TKey01,in TKey02,in TKey03>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public abstract IQueryable<TEntity> QueryById(TKey01 id01, TKey02 id02, TKey03 id03);

        #endregion

        #region Overrides of EFQueryableRepository<TEntity>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public override IQueryable<TEntity> QueryById(params object[] ids)
        {
            return QueryById((TKey01)ids[0], (TKey02)ids[1], (TKey03)ids[2]);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    /// <typeparam name="TKey01">The entity id first type</typeparam>
    /// <typeparam name="TKey02">The entity id second type</typeparam>
    /// <typeparam name="TKey03">The entity id third type</typeparam>
    /// <typeparam name="TKey04">The entity id fourth type</typeparam>
    public abstract class EFQueryableRepository<TEntity, TKey01, TKey02, TKey03, TKey04> 
        : EFQueryableRepository<TEntity>, IQueryableRepository<TEntity, TKey01, TKey02, TKey03, TKey04>
        where TEntity : class
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context"></param>
        protected EFQueryableRepository(DbContext context) : base(context)
        {

        }

        #region Implementation of IAsyncRepository<TEntity,in TKey01,in TKey02,in TKey03,in TKey04>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository asynchronously
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task`1"/> that will fetch the entity
        /// </returns>
#if NET40
        public Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04, CancellationToken ct = new CancellationToken())
        {
            return GetByIdAsync(ct, id01, id02, id03, id04);
        }
#else
        public async Task<TEntity> GetByIdAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04, CancellationToken ct = new CancellationToken())
        {
            return await GetByIdAsync(ct, id01, id02, id03, id04);
        }
#endif

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <param name="ct">The <see cref="T:System.Threading.CancellationToken"/> for the returned task</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
#if NET40
        public Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => QueryById(id01, id02, id03, id04).Any(), ct);
        }
#else
        public async Task<bool> ExistsAsync(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04, CancellationToken ct = new CancellationToken())
        {
            return await QueryById(id01, id02, id03, id04).AnyAsync(ct);
        }
#endif

        #endregion

        #region Implementation of ISyncRepository<TEntity,in TKey01,in TKey02,in TKey03,in TKey04>

        /// <summary>
        /// Gets an entity by its unique identifier from this repository
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>
        /// The entity or null if not found
        /// </returns>
        public TEntity GetById(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return base.GetById(id01, id02, id03, id04);
        }

        /// <summary>
        /// Checks if an entity with the given key exists
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third identifier value</param>
        /// <param name="id04">The entity fourth identifier value</param>
        /// <returns>
        /// True if entity exists
        /// </returns>
        public bool Exists(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04)
        {
            return QueryById(id01, id02, id03, id04).Any();
        }

        #endregion

        #region Implementation of IExposeQueryable<TEntity,in TKey01,in TKey02,in TKey03,in TKey04>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="id01">The entity first unique identifier value</param>
        /// <param name="id02">The entity second unique identifier value</param>
        /// <param name="id03">The entity third unique identifier value</param>
        /// <param name="id04">The entity fourth unique identifier value</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public abstract IQueryable<TEntity> QueryById(TKey01 id01, TKey02 id02, TKey03 id03, TKey04 id04);

        #endregion

        #region Overrides of EFQueryableRepository<TEntity>

        /// <summary>
        /// Gets an <see cref="T:System.Linq.IQueryable`1"/> filtered by
        ///             the entity id
        /// </summary>
        /// <param name="ids">The entity unique identifiers</param>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryable`1"/> object
        /// </returns>
        public override IQueryable<TEntity> QueryById(params object[] ids)
        {
            return QueryById((TKey01) ids[0], (TKey02) ids[1], (TKey03) ids[2], (TKey04) ids[3]);
        }

        #endregion
    }

    /// <summary>
    /// Implementation of an <see cref="IQueryableRepository{TEntity}"/> for the Entity Framework
    /// exposing both sync and async operations. It also exposes an <see cref="IQueryable{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
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
