using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Crx.vNext.Framework.Filter
{
    /// <summary>
    /// 全局请求参数校验过滤器
    /// </summary>
    public class GlobalRequestParameterVerificationFilter : IResultFilter
    {

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validate = new BadRequestObjectResult(context.ModelState).Value as Dictionary<string, object>;
                // 请求参数不能为空
                if (validate.Count == 1 && validate.ContainsKey(string.Empty))
                {
                    context.Result = MsgResponse.Error(MsgErrorEnum.RequestParameterCannotEmpty);
                    return;
                }
                // 请求参数校验失败
                context.Result = MsgResponse.Error(MsgErrorEnum.RequestParameterVerificationFailed, validate);
            }
        }
    }
}
