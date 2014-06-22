using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace TestResultsViewer.Web.Infrastructure
{
    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return Enumerable.Empty<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var childScope = _container.CreateChildContainer();
            return new UnityResolver(childScope);
        }
    }
}