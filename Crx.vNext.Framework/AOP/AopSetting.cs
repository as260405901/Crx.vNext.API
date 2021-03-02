using Autofac;
using Crx.vNext.Common.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crx.vNext.Framework.AOP
{
    public static class AopSetting
    {
        public static Type[] GetAopList(this ContainerBuilder builder)
        {
            // --->先执行
            var list = new List<Type>();
            if(Appsettings.GetBool(new[] {"SystemFrame","AOP", "Log" }) ?? false)
            {
                builder.RegisterType<LogAOP>();
                list.Add(typeof(LogAOP));
            }
            if ((Appsettings.GetBool(new[] { "SystemFrame", "EnabledMiniProfiler" }) ?? false) 
                && (Appsettings.GetBool(new[] { "SystemFrame", "AOP", "MiniProfiler" }) ?? false))
            {
                builder.RegisterType<MiniProfilerAOP>();
                list.Add(typeof(MiniProfilerAOP));
            }
            // --->后执行
            return list.ToArray();
        }
    }
}
