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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Exceptions;
#if NET40 || NET45
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
#else
    using Microsoft.Data.Entity;
#endif

    /// <summary>
    /// An implementation compatible with Entity Framework for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
#if !(NET40 || NET45)
    [CLSCompliant(false)]
#endif
    public abstract class EFUnitOfWork<TDbContext> : ScopeEnabledUnitOfWork, IEFUnitOfWork<TDbContext>, IDisposable
        where TDbContext : DbContext
    {
#if NET40
        private readonly Task<bool> _cachedCompletedTask;
#endif

#region Implementation of IEFUnitOfWork<out TDbContext>

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context { get; }

#endregion

        /// <summary>
        /// Creates a new object associated with the given database context
        /// </summary>
        /// <param name="context">The EF database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFUnitOfWork(TDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
#if NET40
            var tcs = new TaskCompletionSource<bool>();
            tcs.SetResult(true);
            _cachedCompletedTask = tcs.Task;
#endif
        }

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~EFUnitOfWork()
        {
            Dispose(false);
        }

#region Overrides of ScopeEnabledUnitOfWork

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        ///             current instance for any subsequent work
        /// </summary>
        protected override void OnBegin()
        {

        }

        /// <summary>
        /// Invoked once for any given scope, it should prepare the
        ///             current instance for any subsequent work
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// The task for this operation
        /// </returns>
        protected override Task OnBeginAsync(CancellationToken ct)
        {
#if NET40
            return _cachedCompletedTask;
#else
            return Task.FromResult(true);
#endif
        }

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
        ///             made by this instance
        /// </summary>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
        protected override void OnCommit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException(e);
            }
        }

        /// <summary>
        /// Invoked once for any given scope, it should commit any work
        ///             made by this instance
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// The task for this operation
        /// </returns>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.CommitException">
        /// Thrown when the work failed to commit
        /// </exception>
        /// <exception cref="T:SimplePersistence.UoW.Exceptions.ConcurrencyException">
        /// Thrown when the work can't be committed due to concurrency conflicts
        /// </exception>
#if NET40
        protected override Task OnCommitAsync(CancellationToken ct)
        {
            return Task.Factory.StartNew(OnCommit, ct);
        }
#else
        protected override async Task OnCommitAsync(CancellationToken ct)
        {
            try
            {
                await Context.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new ConcurrencyException(e);
            }
        }
#endif

        /// <summary>
        /// Prepares a given <see cref="T:System.Linq.IQueryable`1"/> for asynchronous work.
        /// </summary>
        /// <typeparam name="T">The query item type</typeparam><param name="queryable">The query to wrap</param>
        /// <returns>
        /// An <see cref="T:SimplePersistence.UoW.IAsyncQueryable`1"/> instance, wrapping the given query
        /// </returns>
        public override IAsyncQueryable<T> PrepareAsyncQueryable<T>(IQueryable<T> queryable)
        {
            return new EFAsyncQueryable<T>(queryable);
        }

        #endregion

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the EF database context
        /// </summary>
        /// <param name="disposing">Disposes if true, else does nothing</param>
        protected virtual void Dispose(bool disposing)
        {
            if(disposing)
                Context.Dispose();
        }

#endregion
    }

    /// <summary>
    /// An implementation compatible with Entity Framework for the Unit of Work pattern.
    /// Underline, it also uses work scopes (see: <see cref="ScopeEnabledUnitOfWork"/>).
    /// </summary>
#if !(NET40 || NET45)
    [CLSCompliant(false)]
#endif
    public abstract class EFUnitOfWork : EFUnitOfWork<DbContext>, IEFUnitOfWork
    {
        /// <summary>
        /// Creates a new object associated with the given database context
        /// </summary>
        /// <param name="context">The EF database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFUnitOfWork(DbContext context) : base(context)
        {
            
        }
    }
}
