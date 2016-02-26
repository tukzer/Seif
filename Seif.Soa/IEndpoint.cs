using System;

namespace Seif.Soa
{
    public interface IEndpoint
    {
        Uri Url { get; }

        void Close();
    }
}