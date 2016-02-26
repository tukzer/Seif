using System;

namespace Seif.Core.Logging
{
    public interface ILogFactory
    {
        ILogger GetLogger(Type classType);
    }
}