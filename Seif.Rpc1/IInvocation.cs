﻿using System;
using System.Collections.Generic;

namespace Seif.Rpc
{
    public interface IInvocation
    {
        string ServiceName { get; }
        string MethodName { get; }

        IDictionary<Type, object> Parameters { get; }
    }
}