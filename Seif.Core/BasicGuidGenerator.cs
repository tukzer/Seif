using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core
{
    /// <summary>
    /// An unique identifier generator based on the .NET <see cref="Guid"/> class.
    /// </summary>
    public class BasicGuidGenerator : IUniqueIdentifierGenerator
    {
        /// <summary>
        /// Generates a new <see cref="Guid"/> based on the <see cref="Guid.NewGuid()"/> method.
        /// </summary>
        /// <returns>A new generated <see cref="Guid"/>.</returns>
        public Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}
