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
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions using the Entity Framework.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public partial class EFAsyncQueryable<T>
    {
        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<int> SumAsync(Expression<Func<T, int>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<int> SumAsync(Expression<Func<T, int>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<int?> SumAsync(Expression<Func<T, int?>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<int?> SumAsync(Expression<Func<T, int?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<long> SumAsync(Expression<Func<T, long>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<long> SumAsync(Expression<Func<T, long>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<long?> SumAsync(Expression<Func<T, long?>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<long?> SumAsync(Expression<Func<T, long?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<double> SumAsync(Expression<Func<T, double>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<double> SumAsync(Expression<Func<T, double>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<double?> SumAsync(Expression<Func<T, double?>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<double?> SumAsync(Expression<Func<T, double?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<float> SumAsync(Expression<Func<T, float>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<float> SumAsync(Expression<Func<T, float>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif

        /// <summary>
        /// Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///                 each element of the input sequence.
        /// </summary>
        /// <param name="selector">A projection function to apply to each element. </param>
        /// <param name="ct">A <see cref="T:System.Threading.CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        ///                 The task result contains the sum of the projected values..
        /// </returns>
#if NET40
        public Task<float?> SumAsync(Expression<Func<T, float?>> selector, CancellationToken ct = new CancellationToken())
        {
            return Task.Factory.StartNew(() => _queryable.Sum(selector), ct);
        }
#else
        public async Task<float?> SumAsync(Expression<Func<T, float?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }
#endif
    }
}
