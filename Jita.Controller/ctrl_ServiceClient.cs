using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Jita.Common;
using Jita.Controller.model;
using KouBei.Cache.Service;

namespace Jita.Controller
{
    /// <summary>
    /// 上层调用服务端入口
    /// <code></code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ctrl_ServiceClient
    {

        public static void UpdateService<T>(bool isCache, string invokeKey, object[] prams) where T : new()
        {
            if (invokeKey == null || invokeKey.Length == 0) return;
            m_InvokeConfig mic = ctrl_InvokeConfig.UpdateConfig(invokeKey);

            if (mic == null) return;
            Type type = Type.GetType(Assembly.CreateQualifiedName(mic.AssemblyPath, mic.ClassName));
            MethodInfo mi = type.GetMethod(mic.MethodName);
            var instance = mi.IsStatic ? null : Activator.CreateInstance(type);
            //同步验证过滤内容
            //var res = srv_FilterService.FilterContent(mi, prams);
            //if (!res)
            //{
            //    return;
            //}
            var seeds = mi.Invoke(instance, prams);//调用完成

            //分词消息
            //srv_UgcService.SyncData(mi, prams, seeds);
            //同步清缓存
            srv_CacheManager.RemoveCache(isCache, mi, prams);

            //异步发消息
            //srv_MsgService.SyncData(mi, prams);
            //srv_MsgService.SyncData(mi, prams, seeds);

            //异步积分操作
            //srv_CreditService.ChangeCredit(mi, prams);
            //srv_CreditService.AddCredit(mi, prams, seeds);

            //写日志
            //srv_WriteLogService.WriteLog(mi, prams, seeds);
            //SrvWriteLog.WriteSubjectLog(mi, prams, seeds);

            //发系统站短消息
            //srv_SysMsgService.SendSysMsg(mi, prams);
            //统计发系统站短消息用户（30天未发口碑）
            //srv_SysMsgService.StatsSendSysMsgUser(mi, prams);
        }

        public static List<T> GetService<T>(bool isCache, string invokeKey, object[] prams) where T : new()
        {
            if (invokeKey == null || invokeKey.Length == 0) return null;
            m_InvokeConfig mic = ctrl_InvokeConfig.GetConfig(invokeKey);

            if (mic == null) return null;

            //Type type = Type.GetType(Assembly.CreateQualifiedName(mic.AssemblyPath, mic.ClassName));
            //MethodInfo mi = type.GetMethod(mic.MethodName);
            //var instance = mi.IsStatic ? null : Activator.CreateInstance(type);

            ArrayList objMethod = com_GlobalDic.Pop(mic.ID) as ArrayList;
            MethodInfo mi = objMethod[1] as MethodInfo;
            var seeds = mi.Invoke(objMethod[0], prams);

            if (seeds == null) return null;
            #region
            //此位置预留二级缓存
            #endregion
            //处理返回值成字符串数据
            var obj = srv_CacheManager.CacheData(isCache, mi, seeds);
            if (obj == null) return null;
            Type t = obj.GetType();
            List<T> list = new List<T>();
            if (t.IsCollection())
            {
                var arry = (IEnumerable)obj;
                foreach (var item in arry)
                {
                    T o = (T)Convert.ChangeType(item, typeof(T));
                    list.Add(o);
                }
            }
            else
            {
                T o = (T)Convert.ChangeType(obj, typeof(T));
                list.Add(o);
            }

            return (list.Count == 0) ? null : list;
        }
    }
}
