using System;
using System.Collections.Generic;
using System.Reflection;
using StructureMap;
using Trackwane.Framework.Interfaces;

namespace Trackwane.Framework.Infrastructure
{
    public class ServiceLocator<T> : IServiceLocator<T> where T : StructureMap.Registry, new()
    {
        private readonly IServiceLocationFactory serviceFactory;

        public ServiceLocator(IServiceLocationFactory serviceFactory)
        {
            if (serviceFactory == null) throw new ArgumentNullException("serviceFactory");

            this.serviceFactory = serviceFactory;
        }

        public IContainer GetContainer()
        {
            return serviceFactory.GetContainer(new T(), new Registry());
        }
    }
}
