﻿using System;
using Autofac;

namespace Seif.Rpc.Default
{
    public class AutofacTypeBuilder : ITypeBuilder
    {
        private readonly ContainerBuilder _containerBuilder = new ContainerBuilder();
        private IContainer _container;

        public void RegisterType<T, TImpl>() where TImpl : class
        {
            _containerBuilder.RegisterType<TImpl>().As<T>();
        }

        public void RegisterType<T>(T instance) where T : class
        {
            _containerBuilder.RegisterInstance(instance).As<T>();
        }

        public void Build()
        {
           _container = _containerBuilder.Build();
        }

        public void RegisterType<T, TImpl>(TImpl instance) where TImpl : class
        {
            _containerBuilder.RegisterInstance(instance).As<T>();
        }

        public T ResolveType<T>()
        {
            return _container.Resolve<T>();
        }

        public object ResolveType(string interfaceName)
        {
            var type = Type.GetType(interfaceName);

            return _container.Resolve(type);

        }
    }
}