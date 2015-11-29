using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core.Configuration
{
    public interface IEnvironmentConfiguration
    {
        /// <summary>
        /// Tries to get the specified instance.
        /// </summary>
        /// <typeparam name="T">The type of the instance to get.</typeparam>
        /// <param name="result">The result.</param>
        /// <returns>A indication whether the instance could be get or not.</returns>
        Boolean TryGet<T>(out T result) where T : class;
    }
}
