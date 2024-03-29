﻿using System.Configuration;
using System.Web.Http;
using Microsoft.Practices.Unity;
using TestResultsViewer.Parser;
using TestResultsViewer.Parser.Interfaces;
using TestResultsViewer.Web.Infrastructure;
using MongoResultStorage = TestResultsViewer.Web.Storages.MongoResultStorage;

namespace TestResultsViewer.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureResolver(config);

            config.Routes.MapHttpRoute(
                name: "TestResultFiles",
                routeTemplate: "api/{controller}/{filename}",
                defaults: new { filename = RouteParameter.Optional}
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void ConfigureResolver(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IResultsStorage, MongoResultStorage>(new HierarchicalLifetimeManager(), 
                new InjectionConstructor(ConfigurationManager.AppSettings["ResultFilesOutputDirectory"]));
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
