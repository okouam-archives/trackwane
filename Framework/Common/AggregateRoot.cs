using System.Collections.Generic;

namespace Trackwane.Framework.Common
{
    public abstract class AggregateRoot
    {
        /* Public */
       
        public string Key { get; set; }

        public IEnumerable<DomainEvent> GetUncommittedChanges()
        {
            foreach (var change in uncommittedChanges)
            {
                yield return change;
            }

            uncommittedChanges = new List<DomainEvent>();
        }

        /* Protected */
        protected abstract void Apply(DomainEvent evt);

        protected void Causes(DomainEvent evt)
        {
            uncommittedChanges.Add(evt);
            Apply(evt);
        }

        /* Private */

        protected IList<DomainEvent> uncommittedChanges = new List<DomainEvent>();
    }
}
