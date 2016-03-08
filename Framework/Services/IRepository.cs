using Raven.Abstractions.Data;
using Raven.Client.Linq;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Interfaces
{
    public interface IRepository
    {
        IRavenQueryable<T> Query<T>();

        T Load<T>(string key) where T : AggregateRoot;

        T Load<T>(string key, string organizationKey) where T : ScopedAggregateRoot;

        JsonDocument Get(string id);

        void Persist(object entity);

        void Delete(object entity);
    }
}