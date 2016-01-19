
using System;
using System.Reflection;
using System.Collections;

namespace Jita.Common
{
    /// <summary>
    /// Class ReflectionHelper
    /// 可反射调用DLL中具体方法，功能类，不可继承。
    /// <code></code>
    /// </summary>
    public sealed class com_ReflectionHelper
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ReflectionHelper" /> class from being created.
        /// </summary>
        private com_ReflectionHelper() { ;}

        /// <summary>
        /// Gets the data.
        /// <code></code>
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="prams">反射出方法的具体参数（注意参数的顺序）</param>
        /// <returns>System.Object.</returns>
        public static object GetData(string assemblyPath, string className, string methodName, params object[] prams)
        {
            Type type = Type.GetType(Assembly.CreateQualifiedName(assemblyPath, className));
            MethodInfo mi = type.GetMethod(methodName);
            var instance = mi.IsStatic ? null : Activator.CreateInstance(type);
            ParameterInfo[] pis = mi.GetParameters();
            int index = 0;
            ArrayList al = new ArrayList(pis.Length);
            foreach (ParameterInfo pi in pis)
            {
                al.Add(Convert.ChangeType(prams[index], pi.ParameterType));
            }
            var data = mi.Invoke(instance, al.ToArray());
            return data;
        }
    }
}
