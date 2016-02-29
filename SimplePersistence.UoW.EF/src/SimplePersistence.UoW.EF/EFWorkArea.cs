namespace SimplePersistence.UoW.EF
{
#if NET40 || NET45
    using System.Data.Entity;
#else
    using System;
    using Microsoft.Data.Entity;
#endif

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
#if !(NET40 || NET45)
    [CLSCompliant(false)]
#endif
    public abstract class EFWorkArea<TDbContext> : IEFWorkArea<TDbContext>
        where TDbContext : DbContext
    {
        #region Implementation of IEFWorkArea<out TDbContext>

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context { get; }

        #endregion

        /// <summary>
        /// Creates a new work area that will use the given database context
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFWorkArea(TDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Context = context;
        }
    }

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework
    /// </summary>
#if !(NET40 || NET45)
    [CLSCompliant(false)]
#endif
    public abstract class EFWorkArea : EFWorkArea<DbContext>
    {
        /// <summary>
        /// Creates a new work area that will use the given database context
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFWorkArea(DbContext context) : base(context)
        {
            
        }
    }
}
