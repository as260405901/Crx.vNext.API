using Castle.DynamicProxy;
using Crx.vNext.Common.Helper;
using Microsoft.Extensions.Logging;

namespace Crx.vNext.Framework.AOP
{
    /// <summary>
    /// 记录接口入参出参
    /// </summary>
    public class LogAOP : IInterceptor
    {
        public readonly ILogger<LogAOP> _logger;
        public LogAOP(ILogger<LogAOP> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            var paras = invocation.Method.GetParameters();
            var paraArray = new string[paras.Length];
            for (int i = 0; i < paras.Length; i++)
            {
                paraArray[i] = paras[i].Name + "\":\"" + invocation.Arguments[i];
            }
            string strParas;
            if (paraArray.Length > 0)
            {
                strParas = "{\"" + string.Join("\",\"", paraArray) + "\"}";
            }
            else 
            { 
                strParas = "null"; 
            }
            _logger.LogInformation($"【日志入参】方法名：{invocation.InvocationTarget}.{invocation.Method.Name}\r\n参数：{strParas}");
            invocation.Proceed();
            _logger.LogInformation($"【日志出参】方法名：{invocation.InvocationTarget}.{invocation.Method.Name}\r\n返回值：{ (invocation.ReturnValue == null ? null : JsonHelper.Serialize(invocation.ReturnValue))}");
        }
    }
}
