using Crx.vNext.Common.Base;
using Crx.vNext.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;
using System.Text.Json;

namespace Crx.vNext.Framework.Filter
{
    /// <summary>
    /// 全局请求返回值过滤器，
    /// 强制使用统一的请求响应格式，防止开发人员编码不规范导致返回类型格式不统一
    /// </summary>
    public class GlobalResultFilter : IResultFilter
    {
        private readonly ILogger<GlobalResultFilter> _logger;
        public GlobalResultFilter(ILogger<GlobalResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {

        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var result = context.Result as ObjectResult;
                if(result.Value is MessageResponse)
                {
                    return;
                }
                if(result.StatusCode == 200 || result.StatusCode == null)
                {
                    context.Result = new JsonResult(MessageResponse.Ok(result.Value));
                    if (result.Value is string)
                    {
                        _logger.LogWarning("【不规范的出参】 " + result.Value);
                    }
                    else
                    {
                        _logger.LogWarning("【不规范的出参】 " + JsonHelper.Serialize(result.Value));
                    }
                    return;
                }
                if(result.StatusCode == 400)
                {
                    _logger.LogWarning("【不规范的出参】 400");
                    context.Result = new JsonResult(MessageResponse.Error(MesssageErrorEnum.BadRequest));
                }
                return;
            }
            if(context.Result is EmptyResult || context.Result is OkResult)
            {
                _logger.LogInformation("【不规范的出参】 void");
                context.Result = new JsonResult(MessageResponse.Ok());
            }
        }
    }
}
