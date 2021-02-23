using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crx.vNext.Framework.Filter
{
    /// <summary>
    /// 全局请求返回值过滤器，
    /// 强制使用统一的请求响应格式，防止开发人员编码不规范导致返回类型格式不统一
    /// </summary>
    public class GlobalResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var result = (ObjectResult)context.Result;
                if(result.StatusCode == 200)
                {
                    context.Result = MsgResponse.Ok(result.Value);
                }
                else if(result.StatusCode == 400)
                {
                    context.Result = MsgResponse.Error(MsgErrorEnum.BadRequest);
                }
                return;
            }
            if(context.Result is EmptyResult || context.Result is OkResult)
            {
                context.Result = MsgResponse.Ok();
            }
        }
    }
}
