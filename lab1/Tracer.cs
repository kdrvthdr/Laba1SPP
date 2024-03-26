using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace lab1
{
    [Serializable]
    public class TraceResult
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public string Time { get; set; }
        public List<TraceResult> Methods { get; } = new List<TraceResult>();
    }

    public interface ITracer
    {
        // вызывается в начале замеряемого метода
        void StartTrace();

        // вызывается в конце замеряемого метода 
        void StopTrace();

        // получить результаты измерений  
        TraceResult GetTraceResult();
    }

    public class Tracer : ITracer
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        public  Stack<TraceResult> _methodStack = new Stack<TraceResult>();
        private TraceResult _rootResult;

        public void StartTrace()
        {
            var methodInfo = GetCallingMethodInfo();
            var traceResult = new TraceResult
            {
                MethodName = methodInfo.MethodName,
                ClassName = methodInfo.ClassName
            };

            if (_methodStack.Count == 0)
            {
                _rootResult = traceResult;
            }
            else
            {
                var parent = _methodStack.Peek();
                parent.Methods.Add(traceResult);
            }

            _methodStack.Push(traceResult);

            _stopwatch.Start();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
            var elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

            var completedMethod = _methodStack.Pop();
            completedMethod.Time = elapsedMilliseconds + "ms";

            _stopwatch.Reset();
        }

        public TraceResult GetTraceResult()
        {
            return _rootResult;
        }

        private (string MethodName, string ClassName) GetCallingMethodInfo()
        {
            var stackTrace = new StackTrace();
            var callingFrame = stackTrace.GetFrame(2);
            var method = callingFrame.GetMethod();
            var methodName = method.Name;
            var className = method.DeclaringType.Name;

            return (methodName, className);
        }
    }

  
}
