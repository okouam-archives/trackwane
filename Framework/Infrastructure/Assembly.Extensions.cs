using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Trackwane.Framework.Common;
using Trackwane.Framework.Infrastructure.Requests;

namespace Trackwane.Framework.Infrastructure
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetDomainEvents(this Assembly self)
        {
            return self.GetTypes().Where(x => x.IsSubclassOf(typeof (DomainEvent)));
        }

        public static IEnumerable<Type> GetCommands(this Assembly self)
        {
            return self.GetTypes().Where(x => x.IsSubclassOf(typeof(SystemCommand)));
        }

        public static IEnumerable<Type> GetListeners(this Assembly self)
        {
            return self.GetTypes().Where(x => x.IsSubClassOfGeneric(typeof(TransactionalListener<>)));
        }

        public static IEnumerable<Type> GetHandlers(this Assembly self)
        {
            return self.GetTypes().Where(x => x.IsSubClassOfGeneric(typeof(TransactionalHandler<>)));
        }
    }
}
