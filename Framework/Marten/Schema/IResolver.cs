﻿using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Marten.Services;

namespace Marten.Schema
{
    public interface IResolver<T>
    {
        T Resolve(int startingIndex, DbDataReader reader, IIdentityMap map);


        T Build(DbDataReader reader, ISerializer serializer);

        T Resolve(IIdentityMap map, ILoader loader, object id);
        Task<T> ResolveAsync(IIdentityMap map, ILoader loader, CancellationToken token, object id);

    }
}