using System;
using System.Linq;

namespace Trackwane.Framework.Common
{
    public class DomainEvent : Request
    {
        public string ApplicationKey { get; set; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
        }

        public override string ToString()
        {
            return GetType().Name.Split('.').Last() + ":" + Id;
        }
    }
}
