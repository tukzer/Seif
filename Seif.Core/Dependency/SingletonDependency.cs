using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core.Dependency
{
    /// <summary>
    /// Used to get a singleton of any class which can be resolved using <see cref="WindsorIocManager.Instance"/>.
    /// Important: Use classes by injecting wherever possible. This class is for cases that's not possible.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonDependency<T>
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static T Instance { get { return LazyInstance.Value; } }
        private static readonly Lazy<T> LazyInstance;

        static SingletonDependency()
        {
            LazyInstance = new Lazy<T>(() => WindsorIocManager.Instance.Resolve<T>());
        }
    }
}
