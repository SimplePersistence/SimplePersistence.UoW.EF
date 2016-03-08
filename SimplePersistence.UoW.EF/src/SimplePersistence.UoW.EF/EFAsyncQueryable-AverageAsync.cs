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
#if NET40 || NET45
    using System.Data.Entity;
#else
    using Microsoft.Data.Entity;
#endif

    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions using the Entity Framework.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public partial class EFAsyncQueryable<T>
    {
        public Task<decimal> AverageAsync(Expression<Func<T, decimal>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<decimal?> AverageAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<double> AverageAsync(Expression<Func<T, int>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<double?> AverageAsync(Expression<Func<T, int?>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<double> AverageAsync(Expression<Func<T, long>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<double?> AverageAsync(Expression<Func<T, long?>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<double> AverageAsync(Expression<Func<T, double>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<double?> AverageAsync(Expression<Func<T, double?>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<float> AverageAsync(Expression<Func<T, float>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public Task<float?> AverageAsync(Expression<Func<T, float?>> selector, CancellationToken ct = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
