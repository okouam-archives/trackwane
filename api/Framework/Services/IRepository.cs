using Marten.Linq;
using Trackwane.Framework.Common;

namespace Trackwane.Framework.Interfaces
{
    public interface IRepository
    {
        IMartenQueryable<T> Query<T>();

        T Find<T>(string key, string applicationKey) where T : AggregateRoot;

        T Find<T>(string key, string organizationKey, string applicationKey) where T : ScopedAggregateRoot;

        void Persist(object entity);

        void Delete(object entity);
    }
}