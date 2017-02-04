using System;
using System.ComponentModel;

namespace Seif.Rpc.Invoke
{
    public class ResultHandler
    {
        public virtual object ProcessResult(InvokeResult result, Type returnType, ISerializer serializer)
        {
            var returnVal = PreCheckResult(result.Result, returnType, serializer);

            switch (result.Status)
            {
                case ResultStatus.Success:
                    return ConvertType(returnVal, returnType);
                case ResultStatus.BusinessError:
                    throw new Exception(result.Message, result.Exceptions[0]);
                case ResultStatus.FrameworkError:
                    throw new SeifException(result.Message, result.Exceptions[0]);
                case ResultStatus.ServerNotReachable:
                    throw new Exception(result.Message, result.Exceptions[0]);
                case ResultStatus.UnknownError:
                    throw new Exception(result.Message, result.Exceptions[0]);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected object ConvertType(object result, Type returnType)
        {
            if (result == null) return null;
            if (returnType == typeof (void)) return null;

            return Convert.ChangeType(result, returnType);
        }

        protected virtual object PreCheckResult(object result, Type returnType, ISerializer serializer)
        {
            if (result != null && !returnType.IsPrimitive && returnType != typeof(void))
            {
                return serializer.Deserialize(returnType, result.ToString());
            }

            return result;
        }
    }
}