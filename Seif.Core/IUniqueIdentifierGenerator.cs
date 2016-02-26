using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seif.Core
{
    /// <summary>
    /// Generator that generates unique identifiers.
    /// </summary>
    public interface IUniqueIdentifierGenerator
    {
        /// <summary>
        /// Generates the new identifier.
        /// </summary>
        /// <returns>A new <see cref="Guid"/>.</returns>
        Guid GenerateNewId();
    }
}
