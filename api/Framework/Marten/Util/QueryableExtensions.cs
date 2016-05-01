﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Marten.Linq;

namespace Marten.Util
{
    public static class QueryableExtensions
    {
        #region Explain

        public static QueryPlan Explain<T>(this IQueryable<T> queryable)
        {
            var martenQueryable = CastToMartenQueryable(queryable);
            return martenQueryable.Explain();
        }
        #endregion
        #region ToListJson

        public static async Task<string> ToListJsonAsync<T>(this IQueryable<T> queryable,
            CancellationToken token = default(CancellationToken))
        {
            var martenQueryable = CastToMartenQueryable(queryable);
            var enumerable = await martenQueryable.ExecuteCollectionToJsonAsync(token).ConfigureAwait(false);
            return $"[{string.Join(",", enumerable)}]";
        }

        public static string ToListJson<T>(this IQueryable<T> queryable)
        {
            var martenQueryable = CastToMartenQueryable(queryable);

            var enumerable = martenQueryable.ExecuteCollectionToJson();
            return $"[{string.Join(",", enumerable)}]";
        }

        #endregion

        #region ToList

        public static async Task<List<T>> ToListAsync<T>(this IQueryable<T> queryable,
            CancellationToken token = default(CancellationToken))
        {
            var martenQueryable = queryable as IMartenQueryable<T>;
            if (martenQueryable == null)
            {
                throw new InvalidOperationException($"{typeof (T)} is not IMartenQueryable<>");
            }

            var enumerable = await martenQueryable.ExecuteCollectionAsync(token).ConfigureAwait(false);
            return enumerable.ToList();
        }

        #endregion

        #region Any

        private static readonly MethodInfo _any = GetMethod(nameof(Queryable.Any));

        public static Task<bool> AnyAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, bool>(_any, source, cancellationToken);
        }

        private static readonly MethodInfo _anyPredicate = GetMethod(nameof(Queryable.Any), 1);

        public static Task<bool> AnyAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, bool>(_anyPredicate, source, predicate, cancellationToken);
        }

        #endregion

        #region Aggregate Functions

        public static Task<TResult> SumAsync<TSource, TResult>(
            this IQueryable<TSource> source, Expression<Func<TSource, TResult>> expression,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sum = GetMethod(nameof(Queryable.Sum), 1, mi => mi.ReturnType == typeof (TResult));
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TResult>(sum, source, expression, cancellationToken);
        }

        private static readonly MethodInfo _max = GetMethod(nameof(Queryable.Max), 1);

        public static Task<TResult> MaxAsync<TSource, TResult>(
            this IQueryable<TSource> source, Expression<Func<TSource, TResult>> expression,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TResult>(_max, source, expression, cancellationToken);
        }

        private static readonly MethodInfo _min = GetMethod(nameof(Queryable.Min), 1);

        public static Task<TResult> MinAsync<TSource, TResult>(
            this IQueryable<TSource> source, Expression<Func<TSource, TResult>> expression,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TResult>(_min, source, expression, cancellationToken);
        }

        public static Task<double> AverageAsync<TSource, TMember>(
            this IQueryable<TSource> source, Expression<Func<TSource, TMember>> expression,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var avg = GetMethod(nameof(Queryable.Average), 1,
                mi => (mi.GetParameters()[1]
                    .ParameterType.GenericTypeArguments[0]
                    .GenericTypeArguments[1] == typeof (TMember)));
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, double>(avg, source, expression, cancellationToken);
        }

        #endregion

        #region Count/LongCount/Sum

        private static readonly MethodInfo _count = GetMethod(nameof(Queryable.Count));

        public static Task<int> CountAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, int>(_count, source, cancellationToken);
        }

        private static readonly MethodInfo _countPredicate = GetMethod(nameof(Queryable.Count), 1);

        public static Task<int> CountAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, int>(_countPredicate, source, predicate, cancellationToken);
        }

        private static readonly MethodInfo _longCount = GetMethod(nameof(Queryable.LongCount));

        public static Task<long> LongCountAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, long>(_longCount, source, cancellationToken);
        }

        private static readonly MethodInfo _longCountPredicate = GetMethod(nameof(Queryable.LongCount), 1);

        public static Task<long> LongCountAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, long>(_longCountPredicate, source, predicate, cancellationToken);
        }

        #endregion

        #region First/FirstOrDefault

        private static readonly MethodInfo _first = GetMethod(nameof(Queryable.First));

        public static Task<TSource> FirstAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_first, source, cancellationToken);
        }

        public static Task<string> FirstJsonAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJsonAsync(_first, source, cancellationToken);
        }

        public static string FirstJson<TSource>(
            this IQueryable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJson(_first, source);
        }

        private static readonly MethodInfo _firstPredicate = GetMethod(nameof(Queryable.First), 1);

        public static Task<TSource> FirstAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, TSource>(_firstPredicate, source, predicate, cancellationToken);
        }

        public static Task<string> FirstJsonAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJsonAsync(_firstPredicate, source, predicate, cancellationToken);
        }

        public static string FirstJson<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJson(_firstPredicate, source, predicate);
        }

        private static readonly MethodInfo _firstOrDefault = GetMethod(nameof(Queryable.FirstOrDefault));

        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_firstOrDefault, source, cancellationToken);
        }

        public static Task<string> FirstOrDefaultJsonAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJsonAsync(_firstOrDefault, source, cancellationToken);
        }


        public static string FirstOrDefaultJson<TSource>(
            this IQueryable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJson(_firstOrDefault, source);
        }

        private static readonly MethodInfo _firstOrDefaultPredicate = GetMethod(nameof(Queryable.FirstOrDefault), 1);

        public static Task<TSource> FirstOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, TSource>(_firstOrDefaultPredicate, source, predicate, cancellationToken);
        }

        public static Task<string> FirstOrDefaultJsonAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJsonAsync(_firstOrDefaultPredicate, source, predicate, cancellationToken);
        }

        public static string FirstOrDefaultJson<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJson(_firstOrDefaultPredicate, source, predicate);
        }

        #endregion

        #region Single/SingleOrDefault

        private static readonly MethodInfo _single = GetMethod(nameof(Queryable.Single));

        public static Task<TSource> SingleAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken token = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_single, source, token);
        }

        public static Task<string> SingleJsonAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken token = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJsonAsync(_single, source, token);
        }

        public static string SingleJson<TSource>(
            this IQueryable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJson(_single, source);
        }

        private static readonly MethodInfo _singlePredicate = GetMethod(nameof(Queryable.Single), 1);

        public static Task<TSource> SingleAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken token = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, TSource>(_singlePredicate, source, predicate, token);
        }

        public static Task<string> SingleJsonAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken token = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJsonAsync(_singlePredicate, source, predicate, token);
        }

        public static string SingleJson<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJson(_singlePredicate, source, predicate);
        }

        private static readonly MethodInfo _singleOrDefault = GetMethod(nameof(Queryable.SingleOrDefault));

        public static Task<TSource> SingleOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_singleOrDefault, source, cancellationToken);
        }

        public static Task<string> SingleOrDefaultJsonAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJsonAsync(_singleOrDefault, source, cancellationToken);
        }

        public static string SingleOrDefaultJson<TSource>(
            this IQueryable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteJson(_singleOrDefault, source);
        }

        private static readonly MethodInfo _singleOrDefaultPredicate = GetMethod(nameof(Queryable.SingleOrDefault), 1);

        public static Task<TSource> SingleOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, TSource>(_singleOrDefaultPredicate, source, predicate, cancellationToken);
        }

        public static Task<string> SingleOrDefaultJsonAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJsonAsync(_singleOrDefaultPredicate, source, predicate, cancellationToken);
        }

        public static string SingleOrDefaultJson<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteJson(_singleOrDefaultPredicate, source, predicate);
        }

        #endregion

        #region Last/LastOrDefault

        private static readonly MethodInfo _last = GetMethod(nameof(Queryable.Last));

        public static Task<TSource> LastAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_last, source, cancellationToken);
        }

        private static readonly MethodInfo _lastOrDefault = GetMethod(nameof(Queryable.LastOrDefault));

        public static Task<TSource> LastOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return ExecuteAsync<TSource, TSource>(_lastOrDefault, source, cancellationToken);
        }

        private static readonly MethodInfo _lastOrDefaultPredicate = GetMethod(nameof(Queryable.LastOrDefault), 1);

        public static Task<TSource> LastOrDefaultAsync<TSource>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return ExecuteAsync<TSource, TSource>(_lastOrDefaultPredicate, source, predicate, cancellationToken);
        }

        #endregion

        #region Shared

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            CancellationToken token = default(CancellationToken))
        {
            var provider = source.Provider as IMartenQueryProvider;
            if (provider == null)
            {
                throw new InvalidOperationException($"{source.Provider.GetType()} is not IMartenQueryProvider");
            }

            if (operatorMethodInfo.IsGenericMethod)
            {
                operatorMethodInfo = operatorMethodInfo.MakeGenericMethod(typeof (TSource));
            }

            return provider.ExecuteAsync<TResult>(
                Expression.Call(null, operatorMethodInfo, source.Expression),
                token);
        }

        private static Task<string> ExecuteJsonAsync<TSource>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            CancellationToken token = default(CancellationToken))
        {
            var provider = source.Provider as IMartenQueryProvider;
            if (provider == null)
            {
                throw new InvalidOperationException($"{source.Provider.GetType()} is not IMartenQueryProvider");
            }

            if (operatorMethodInfo.IsGenericMethod)
            {
                operatorMethodInfo = operatorMethodInfo.MakeGenericMethod(typeof (TSource));
            }

            return provider.ExecuteJsonAsync<TSource>(
                Expression.Call(null, operatorMethodInfo, source.Expression),
                token);
        }

        private static string ExecuteJson<TSource>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source)
        {
            var provider = source.Provider as IMartenQueryProvider;
            if (provider == null)
            {
                throw new InvalidOperationException($"{source.Provider.GetType()} is not IMartenQueryProvider");
            }

            if (operatorMethodInfo.IsGenericMethod)
            {
                operatorMethodInfo = operatorMethodInfo.MakeGenericMethod(typeof (TSource));
            }

            return provider.ExecuteJson<TSource>(Expression.Call(null, operatorMethodInfo, source.Expression));
        }

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            LambdaExpression expression,
            CancellationToken token = default(CancellationToken))
        {
            return ExecuteAsync<TSource, TResult>(operatorMethodInfo, source, Expression.Quote(expression), token);
        }

        private static Task<string> ExecuteJsonAsync<TSource>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            LambdaExpression expression,
            CancellationToken token = default(CancellationToken))
        {
            return ExecuteJsonAsync(operatorMethodInfo, source, Expression.Quote(expression), token);
        }

        private static string ExecuteJson<TSource>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            LambdaExpression expression)
        {
            return ExecuteJson(operatorMethodInfo, source, Expression.Quote(expression));
        }

        private static Task<TResult> ExecuteAsync<TSource, TResult>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            Expression expression,
            CancellationToken token = default(CancellationToken))
        {
            var provider = source.Provider as IMartenQueryProvider;
            if (provider == null)
            {
                throw new InvalidOperationException($"{source.Provider.GetType()} is not IMartenQueryProvider");
            }

            operatorMethodInfo
                = operatorMethodInfo.GetGenericArguments().Length == 2
                    ? operatorMethodInfo.MakeGenericMethod(typeof (TSource), typeof (TResult))
                    : operatorMethodInfo.MakeGenericMethod(typeof (TSource));

            return provider.ExecuteAsync<TResult>(
                Expression.Call(
                    null,
                    operatorMethodInfo,
                    new[] {source.Expression, expression}),
                token);
        }

        private static Task<string> ExecuteJsonAsync<TSource>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            Expression expression,
            CancellationToken token = default(CancellationToken))
        {
            var provider = source.Provider as IMartenQueryProvider;
            if (provider == null)
            {
                throw new InvalidOperationException($"{source.Provider.GetType()} is not IMartenQueryProvider");
            }

            operatorMethodInfo
                = operatorMethodInfo.GetGenericArguments().Length == 2
                    ? operatorMethodInfo.MakeGenericMethod(typeof (TSource), typeof (string))
                    : operatorMethodInfo.MakeGenericMethod(typeof (TSource));

            return provider.ExecuteJsonAsync<TSource>(
                Expression.Call(
                    null,
                    operatorMethodInfo,
                    new[] {source.Expression, expression}),
                token);
        }

        private static string ExecuteJson<TSource>(
            MethodInfo operatorMethodInfo,
            IQueryable<TSource> source,
            Expression expression)
        {
            var provider = source.Provider as IMartenQueryProvider;
            if (provider == null)
            {
                throw new InvalidOperationException($"{source.Provider.GetType()} is not IMartenQueryProvider");
            }

            operatorMethodInfo
                = operatorMethodInfo.GetGenericArguments().Length == 2
                    ? operatorMethodInfo.MakeGenericMethod(typeof (TSource), typeof (string))
                    : operatorMethodInfo.MakeGenericMethod(typeof (TSource));

            return provider.ExecuteJson<TSource>(
                Expression.Call(
                    null,
                    operatorMethodInfo,
                    new[] {source.Expression, expression}));
        }

        private static MethodInfo GetMethod(
            string name,
            int parameterCount = 0,
            Func<MethodInfo, bool> predicate = null)
        {
            return typeof (Queryable)
                .GetTypeInfo()
                .GetDeclaredMethods(name)
                .Single(methodInfo =>
                {
                    if (methodInfo.GetParameters().Length != parameterCount + 1)
                    {
                        return false;
                    }

                    return predicate == null || predicate(methodInfo);
                });
        }

        private static IMartenQueryable<T> CastToMartenQueryable<T>(IQueryable<T> queryable)
        {
            var martenQueryable = queryable as IMartenQueryable<T>;
            if (martenQueryable == null)
            {
                throw new InvalidOperationException($"{typeof (T)} is not IMartenQueryable<>");
            }

            return martenQueryable;
        }

        #endregion
    }
}