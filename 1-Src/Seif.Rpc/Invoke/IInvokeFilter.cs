using System;

namespace Seif.Rpc.Invoke
{
    public interface IInvokeFilter
    {
    }

    public interface IPreInvokeFilter : IInvokeFilter
    {
        void Execute(InvokeContext context);
    }

    public interface IPostInvokeFilter : IInvokeFilter
    {
        void Execute(InvokeContext context);
    }

    public interface IExceptionFilter : IInvokeFilter
    {
        void Execute(InvokeContext context);
    }
}