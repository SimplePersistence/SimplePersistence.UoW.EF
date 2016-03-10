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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data.Entity;

    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions using the Entity Framework.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public partial class EFAsyncQueryable<T> : IEFAsyncQueryable<T>
    {
        private readonly IQueryable<T> _queryable;

        #region Implementation of IQueryable

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </returns>
        public Expression Expression => _queryable.Expression;

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.
        /// </returns>
        public Type ElementType => _queryable.ElementType;

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.
        /// </returns>
        public IQueryProvider Provider => _queryable.Provider;

        #endregion

        /// <summary>
        /// Creates a new instance that will wrapp the given <see cref="IQueryable{T}"/>
        /// </summary>
        /// <param name="queryable">The <see cref="IQueryable{T}"/> to be wrapped</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EFAsyncQueryable(IQueryable<T> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));

            _queryable = queryable;
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return _queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IAsyncQueryable<T>

        /// <summary>
        /// Asynchronously enumerates the query result into memory
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
#if NET40
        public Task LoadAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() =>
            {
                _queryable.Load();
            }, ct);
        }
#else
        public async Task LoadAsync(CancellationToken ct = new CancellationToken())
        {
            await _queryable.LoadAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element.
        /// </summary>
        /// <param name="action">The action to perform on each element.</param><param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// </returns>
#if NET40
        public Task ForEachAsync(Action<T> action, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var entity in _queryable)
                {
                    action(entity);
                }
            }, ct);
        }
#else
        public async Task ForEachAsync(Action<T> action, CancellationToken ct = new CancellationToken())
        {
            await _queryable.ForEachAsync(action, ct);
        }
#endif

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.List`1"/> from an <see cref="T:System.Linq.IQueryable"/> by enumerating it asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///             The task result contains a <see cref="T:System.Collections.Generic.List`1"/> that contains elements from the input sequence.
        /// </returns>
#if NET40
        public Task<List<T>> ToListAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.ToList(), ct);
        }
#else
        public async Task<List<T>> ToListAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToListAsync(ct);
        }
#endif

        /// <summary>
        /// Creates an array from an <see cref="T:System.Linq.IQueryable`1"/> by enumerating it asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///             The task result contains an array that contains elements from the input sequence.
        /// </returns>
#if NET40
        public Task<T[]> ToArrayAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.ToArray(), ct);
        }
#else
        public async Task<T[]> ToArrayAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToArrayAsync(ct);
        }
#endif

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.Dictionary`2"/> from an <see cref="T:System.Linq.IQueryable`1"/> by enumerating it
        ///                 asynchronously
        ///                 according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/> .</typeparam>
        /// <param name="keySelector">A function to extract a key from each element. </param><param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2"/> that contains selected keys and values.
        /// </returns>
#if NET40
        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.ToDictionary(keySelector), ct);
        }
#else
        public async Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, ct);
        }
#endif

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.Dictionary`2"/> from an <see cref="T:System.Linq.IQueryable`1"/> by enumerating it
        ///                 asynchronously
        ///                 according to a specified key selector function and a comparer.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/> .</typeparam>
        /// <param name="keySelector">A function to extract a key from each element. </param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to compare keys.</param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2"/> that contains selected keys and values.
        /// </returns>
#if NET40
        public Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(
            Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.ToDictionary(keySelector, comparer), ct);
        }
#else
        public async Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(
            Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, comparer, ct);
        }
#endif

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.Dictionary`2"/> from an <see cref="T:System.Linq.IQueryable`1"/> by enumerating it
        ///                 asynchronously
        ///                 according to a specified key selector and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/> .</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from each element. </param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2"/> that contains values of type
        ///                 <typeparamref name="TElement"/> selected from the input sequence.
        /// </returns>
#if NET40
        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.ToDictionary(keySelector, elementSelector), ct);
        }
#else
        public async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, elementSelector, ct);
        }
#endif

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.Dictionary`2"/> from an <see cref="T:System.Linq.IQueryable`1"/> by enumerating it
        ///                 asynchronously
        ///                 according to a specified key selector function, a comparer, and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/> .</typeparam>
        /// <typeparam name="TElement">The type of the value returned by <paramref name="elementSelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from each element. </param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element. </param>
        /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1"/> to compare keys.</param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2"/> that contains values of type
        ///                 <typeparamref name="TElement"/> selected from the input sequence.
        /// </returns>
#if NET40
        public Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.ToDictionary(keySelector, elementSelector, comparer), ct);
        }
#else
        public async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, elementSelector, comparer, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the first element of a sequence or a default value if no such element is found.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///             The task result contains the first element in source.
        /// </returns>
#if NET40
        public Task<T> FirstOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.FirstOrDefault(), ct);
        }
#else
        public async Task<T> FirstOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstOrDefaultAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///             The task result contains the first element in source.
        /// </returns>
#if NET40
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.FirstOrDefault(predicate), ct);
        }
#else
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstOrDefaultAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///             The task result contains the first element in source.
        /// </returns>
#if NET40
        public Task<T> FirstAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.First(), ct);
        }
#else
        public async Task<T> FirstAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///             The task result contains the first element in source.
        /// </returns>
#if NET40
        public Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.First(predicate), ct);
        }
#else
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        ///             The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
#if NET40
        public Task<bool> AnyAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Any(), ct);
        }
#else
        public async Task<bool> AnyAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.AnyAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        ///             The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
#if NET40
        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Any(predicate), ct);
        }
#else
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.AnyAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously determines whether all the elements of a sequence satisfy a condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains <c>true</c> if every element of the source sequence passes the test in the specified
        ///                 predicate; otherwise, <c>false</c>.
        /// </returns>
#if NET40
        public Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.All(predicate), ct);
        }
#else
        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.AllAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the number of elements in the input sequence.
        /// </returns>
#if NET40
        public Task<int> CountAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Count(), ct);
        }
#else
        public async Task<int> CountAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.CountAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the number of elements in the input sequence.
        /// </returns>
#if NET40
        public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Count(predicate), ct);
        }
#else
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.CountAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the number of elements in the input sequence.
        /// </returns>
#if NET40
        public Task<long> LongCountAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.LongCount(), ct);
        }
#else
        public async Task<long> LongCountAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LongCountAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the number of elements in the input sequence.
        /// </returns>
#if NET40
        public Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.LongCount(predicate), ct);
        }
#else
        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LongCountAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the last element.
        /// </returns>
#if NET40
        public Task<T> LastAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Last(), ct);
        }
#else
        public async Task<T> LastAsync(CancellationToken ct = new CancellationToken())
        {
            return await Task.Factory.StartNew(() => _queryable.Last(), ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the last element that passes the test in
        ///                 <paramref name="predicate"/>.
        /// </returns>
#if NET40
        public Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Last(predicate), ct);
        }
#else
        public async Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await Task.Factory.StartNew(() => _queryable.Last(predicate), ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains <c>default</c> ( <typeparamref name="T"/> ) if empty; otherwise, the last element.
        /// </returns>
#if NET40
        public Task<T> LastOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.LastOrDefault(), ct);
        }
#else
        public async Task<T> LastOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return await Task.Factory.StartNew(() => _queryable.LastOrDefault(), ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains <c>default</c> ( <typeparamref name="T"/> ) if empty; otherwise, the last element.
        /// </returns>
#if NET40
        public Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.LastOrDefault(predicate), ct);
        }
#else
        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await Task.Factory.StartNew(() => _queryable.LastOrDefault(predicate), ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///                 and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the single element of the sequence.
        /// </returns>
#if NET40
        public Task<T> SingleAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Single(), ct);
        }
#else
        public async Task<T> SingleAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///                 and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate">A function to test an element for a condition. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the single element of the input sequence that satisfies the condition in
        ///                 <paramref name="predicate"/>.
        /// </returns>
#if NET40
        public Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Single(predicate), ct);
        }
#else
        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///                 a default value if no such element exists; this method throws an exception if more than one element
        ///                 satisfies the condition.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the single element of the sequence, or <c>default</c> ( <typeparamref name="T"/> ) if no such element is found.
        /// </returns>
#if NET40
        public Task<T> SingleOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.SingleOrDefault(), ct);
        }
#else
        public async Task<T> SingleOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleOrDefaultAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///                 a default value if no such element exists; this method throws an exception if more than one element
        ///                 satisfies the condition.
        /// </summary>
        /// <param name="predicate">A function to test an element for a condition. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the single element of the input sequence that satisfies the condition in
        ///                 <paramref name="predicate"/>, or <c>default</c> ( <typeparamref name="T"/> ) if no such element is found.
        /// </returns>
#if NET40
        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.SingleOrDefault(predicate), ct);
        }
#else
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleOrDefaultAsync(predicate, ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///                 that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the minimum value in the sequence.
        /// </returns>
#if NET40
        public Task<T> MinAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Min(), ct);
        }
#else
        public async Task<T> MinAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MinAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        /// Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///                 that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the minimum value in the sequence.
        /// </returns>
#if NET40
        public Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Min(selector), ct);
        }
#else
        public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MinAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the maximum value in the sequence.
        /// </returns>
#if NET40
        public Task<T> MaxAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Max(), ct);
        }
#else
        public async Task<T> MaxAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MaxAsync(ct);
        }
#endif

        /// <summary>
        /// Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by the function represented by <paramref name="selector"/> .</typeparam>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the maximum value in the sequence.
        /// </returns>
#if NET40
        public Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Max(selector), ct);
        }
#else
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MaxAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="item">The object to locate in the sequence. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains <c>true</c> if the input sequence contains the specified value; otherwise, <c>false</c>.
        /// </returns>
#if NET40
        public Task<bool> ContainsAsync(T item, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Contains(item), ct);
        }
#else
        public async Task<bool> ContainsAsync(T item, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ContainsAsync(item, ct);
        }
#endif

        #endregion
    }
}
