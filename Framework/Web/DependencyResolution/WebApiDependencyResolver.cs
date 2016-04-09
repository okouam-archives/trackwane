using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using StructureMap;

namespace Trackwane.Framework.Web.DependencyResolution
{
    public class WebApiDependencyResolver : IDependencyResolver
    {
        /* Public */

        public WebApiDependencyResolver(IContainer container, bool isNestedResolver)
        {
            if (container == null) throw new ArgumentNullException("container");

            this.container = container;
            this.isNestedResolver = isNestedResolver;
        }

        public object GetService(Type serviceType)
        {
            if (disposed) throw new ObjectDisposedException("WebApiDependencyResolver");

            if (serviceType == null) throw new ArgumentNullException("serviceType");

            if (serviceType.IsAbstract || serviceType.IsInterface)
            {
                return container.TryGetInstance(serviceType);
            }

            return container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public IDependencyScope BeginScope()
        {
            if (disposed) throw new ObjectDisposedException("WebApiDependencyResolver");

            return new WebApiDependencyResolver(container.GetNestedContainer(), true);
        }

        public void Dispose()
        {
            if (disposed) return;

            if (!isNestedResolver) return;

            container.Dispose();
            disposed = true;
        }

        /* Private */

        private bool disposed;
        private readonly IContainer container;
        private readonly bool isNestedResolver;
    }
}