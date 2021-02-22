using System;
using System.Collections.Generic;
using System.Text;

namespace Crx.vNext.Common.Base
{
    /// <summary>
    /// 消息返回体
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// 错误状态码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 相应内容
        /// </summary>
        public object Response { get; set; }

        public MessageResponse()
        {
            ErrorCode = "00000000";
        }

        public MessageResponse(PageResponse pageResponse) : this()
        {
            Response = pageResponse;
        }
        /*
        public MessageResponse(SysExceptionEnum sysExceptionEnum)
        {
            ErrorCode = "Sys" + sysExceptionEnum.GetHashCode();
            Message = sysExceptionEnum.GetDescription();
        }
        public MessageResponse(object response)
        {
            if(response is SysExceptionEnum)
            {
                var sysExceptionEnum = response as SysExceptionEnum?;
                ErrorCode = "Sys" + sysExceptionEnum.GetHashCode();
                Message = sysExceptionEnum.GetDescription();
            }
            else
            {
                ErrorCode = "00000000";
                Response = response;
            }
        }

        public MessageResponse(SysExceptionEnum sysExceptionEnum, object response)
        {
            ErrorCode = "Sys" + sysExceptionEnum.GetHashCode();
            Message = sysExceptionEnum.GetDescription();
            Response = response;
        }
        */

        public MessageResponse(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public MessageResponse(string errorCode, string message, object response)
        {
            ErrorCode = errorCode;
            Message = message;
            Response = response;
        }
    }

    /// <summary>
    /// 分页数据消息返回体
    /// </summary>
    public class PageResponse
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Data { get; set; }
    }
}
