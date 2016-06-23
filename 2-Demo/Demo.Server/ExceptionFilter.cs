using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Demo.Server
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}