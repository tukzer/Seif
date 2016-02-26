﻿namespace Seif.Soa.Transport
{
    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}