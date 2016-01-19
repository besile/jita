using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jita.Common
{
    public class com_SingletonHelp<T> where T : class, new()
    {
        com_SingletonHelp() { }

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T UniqueInstance
        {
            get { return instance.Value; }
        }
    }
}
