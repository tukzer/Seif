using System.Collections;
using System.Collections.Generic;
using Seif.Soa.Configuration;

namespace Seif.Soa.Registry
{
    public interface IRegistry
    {
        string Name { get; }

        string Url { get; }

        void Import(RegistryMetta serviceRegistry);

        RegistryMetta ReferByName(string serviceName);

        void Close();
    }
}