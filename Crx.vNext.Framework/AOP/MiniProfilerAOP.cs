using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace Crx.vNext.Framework.AOP
{
    /// <summary>
    /// MiniProfiler 性能监控
    /// </summary>
    public class MiniProfilerAOP : IInterceptor
    {
        public readonly ILogger<MiniProfilerAOP> _logger;
        public MiniProfilerAOP(ILogger<MiniProfilerAOP> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            using (MiniProfiler.Current.Step(invocation.InvocationTarget + "." + invocation.Method.Name))
            {
                invocation.Proceed();
            }
        }
    }
}
