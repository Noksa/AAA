using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace AT_Core_Specflow.Extensions
{
    public class AssertAll
    {
        public static ThreadLocal<List<Exception>> ExThreadLocal = new ThreadLocal<List<Exception>>();

        public static List<Exception> Exceptions
        {
            get
            {
                if (!ExThreadLocal.IsValueCreated) ExThreadLocal.Value = new List<Exception>();
                return ExThreadLocal.Value;
            }
        }
        public static void Execute(Action assertionsToRun)
        {
            try
            {
                assertionsToRun.Invoke();
            }
            catch (Exception exc)
            {
                Exceptions.Add(exc);
            }
        }

        public static void Throws()
        {
            if (Exceptions.Count == 0) return;
            var message = new StringBuilder();
            Exceptions.ForEach(_ => message.Append($"{_.Message}{_.StackTrace}\n\n"));
            Exceptions.Clear();
            throw new MultipleAssertException(message.ToString());
        }
    }
}

