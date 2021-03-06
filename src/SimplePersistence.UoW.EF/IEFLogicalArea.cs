﻿#region License
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
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Represents an area used to aggregate Unit of Work logic, 
    /// like data transformations or procedures, specialized for the Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public interface IEFLogicalArea<out TDbContext> : ILogicalArea
        where TDbContext : DbContext
    {
        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        TDbContext Context { get; }

        /// <summary>
        /// Prepares an <see cref="IQueryable{T}"/> for the specified entity type.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <returns>The <see cref="IQueryable{T}"/> for the specified entity type.</returns>
        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }

    /// <summary>
    /// Represents an area used to aggregate Unit of Work logic, 
    /// like data transformations or procedures, specialized for the Entity Framework.
    /// </summary>
    public interface IEFLogicalArea : IEFLogicalArea<DbContext>
    {
        
    }
}