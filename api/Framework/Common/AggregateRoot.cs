using System.Collections.Generic;

namespace Trackwane.Framework.Common
{
    public abstract class AggregateRoot
    {
        /* Public */
       
        public string ApplicationKey { get; set; }

        public string Key { get; set; }

        // Required for Martern persistence
        public string Id
        {
            get { return Key; }
            set { Key = value; }
        }

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
