using Microsoft.AspNetCore.Mvc;
using Crx.vNext.Common.Helper;
using System.ComponentModel;

namespace Crx.vNext.Framework
{
    /// <summary>
    /// 消息返回体
    /// </summary>
    public class MsgResponse
    {
        public static IActionResult Ok()
        {
            return new JsonResult(new MsgModel());
        }

        public static IActionResult Ok(object data)
        {
            return new JsonResult(new MsgModel(data));
        }

        public static IActionResult Error(MsgErrorEnum data)
        {
            return new JsonResult(new MsgModel(data));
        }

        public static IActionResult Error(string code, string Msg)
        {
            return new JsonResult(new MsgModel(code, Msg));
        }
        public static IActionResult Error(MsgErrorEnum errorEnum, object data)
        {
            return new JsonResult(new MsgModel(errorEnum, data));
        }

        public static IActionResult Error(string code, string msg, object data)
        {
            return new JsonResult(new MsgModel(code, msg, data));
        }
    }

    /// <summary>
    /// 分页数据消息返回体
    /// </summary>
    public class MsgPageModel
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public object Rows { get; set; }
    }

    /// <summary>
    /// 返回消息实体
    /// </summary>
    internal class MsgModel
    {
        /// <summary>
        /// 错误状态码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 相应内容
        /// </summary>
        public object Data { get; set; }


        public MsgModel()
        {
            Code = "00000000";
        }

        public MsgModel(object data) : this()
        {
            if(data is MsgErrorEnum)
            {
                var errorEnum = (MsgErrorEnum)data;
                Code = errorEnum.GetHashCode().ToString();
                Msg = errorEnum.GetDescription();
            }
            else
            {
                Data = data;
            }
        }

        public MsgModel(string code, string msg)
        {
            Code = code;
            Msg = msg;
        }

        public MsgModel(MsgErrorEnum errorEnum, object data)
        {
            Code = errorEnum.GetHashCode().ToString();
            Msg = errorEnum.GetDescription();
            Data = data;
        }

        public MsgModel(string code, string msg, object data)
        {
            Code = code;
            Msg = msg;
            Data = data;
        }
    }

    /// <summary>
    /// 系统异常常用枚举
    /// </summary>
    public enum MsgErrorEnum
    {
        #region 系统级 90000***
        /// <summary>
        /// 未知系统异常
        /// </summary>
        [Description("未知系统异常！")]
        UnknownException = 90000000,
        /// <summary>
        /// 请求失败
        /// </summary>
        [Description("请求失败！")]
        BadRequest = 90000001,
        /// <summary>
        /// 请求参数不能为空
        /// </summary>
        [Description("请求参数不能为空！")]
        RequestParameterCannotEmpty = 90000002,

        /// <summary>
        /// 参数校验失败
        /// </summary>
        [Description("请求参数校验失败！")]
        RequestParameterVerificationFailed = 90000003,
        #endregion

        #region 账号级 90001***
        /// <summary>
        /// 该账号不存在
        /// </summary>
        [Description("该账号不存在！")]
        AccountNotExist = 90001001,

        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("密码错误！")]
        PasswordError = 90001002,

        /// <summary>
        /// 账号已禁用
        /// </summary>
        [Description("账号已禁用！")]
        AccountDisabled = 90001003,

        /// <summary>
        /// 账号已经被注册
        /// </summary>
        [Description("手机号已经被注册！")]
        AccountAlreadyExists = 90001004,

        /// <summary>
        /// 账号注册失败
        /// </summary>
        [Description("账号注册失败，请稍后再试！")]
        AccountRegistrationFailed = 90001005,

        /// <summary>
        /// 身份证号不正确
        /// </summary>
        [Description("身份证号不正确！")]
        IdCardError = 90001006,

        #endregion

        #region 短信  90002***
        /// <summary>
        /// 短信发送频率过快
        /// </summary>
        [Description("短信发送频率过快，请稍后再试！")]
        SmsSendFrequencyFast = 90002001,
        /// <summary>
        /// 短信服务异常
        /// </summary>
        [Description("短信服务异常，请稍后再试！")]
        SmsServiceException = 90002002,
        /// <summary>
        /// 验证码错误
        /// </summary>
        [Description("验证码错误！")]
        VerificationCodeError = 90002003,
        /// <summary>
        /// 验证码已过期
        /// </summary>
        [Description("验证码已过期，请重新获取！")]
        VerificationCodeExpired = 90002004,
        #endregion

    }
}
