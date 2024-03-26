using lab1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static ITracer _tracer;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            _tracer = new Tracer();
        }

        [TestMethod]
        public void Test_TraceResult_NotNull()
        {
            // Arrange
            ITracer tracer = new Tracer();

            // Act
            tracer.StartTrace();
            tracer.StopTrace();
            TraceResult traceResult = tracer.GetTraceResult();

            // Assert
            Assert.IsNotNull(traceResult);
        }


        [TestMethod]
        public void Test_TraceResult_MethodName()
        {
            const string methodName = "Test_TraceResult_MethodName";
            _tracer.StartTrace();
            _tracer.StopTrace();
            TraceResult traceResult = _tracer.GetTraceResult();
            Assert.AreEqual(methodName, traceResult.MethodName);
        }

        [TestMethod]
        public void Test_TraceResult_ClassName()
        {
            const string className = "UnitTest1";
            _tracer.StartTrace();
            _tracer.StopTrace();
            TraceResult traceResult = _tracer.GetTraceResult();
            Assert.AreEqual(className, traceResult.ClassName);
        }

        [TestMethod]
        public void Test_TraceResult_Time()
        {
            _tracer.StartTrace();
            Thread.Sleep(100); // Simulate some work
            OuterMethod(_tracer);
            _tracer.StopTrace();
            TraceResult traceResult = _tracer.GetTraceResult();
            Assert.IsNotNull(traceResult.Time);
        }

        [TestMethod]
        public void Test_Nested_Method_Tracing()
        {
            _tracer.StartTrace();
            OuterMethod(_tracer);
            _tracer.StopTrace();
            TraceResult traceResult = _tracer.GetTraceResult();
            Assert.AreEqual(1, traceResult.Methods.Count); // One for OuterMethod, one for InnerMethod
        }

        private void OuterMethod(ITracer tracer)
        {
            tracer.StartTrace();
            InnerMethod(tracer);
            tracer.StopTrace();
        }

        private void InnerMethod(ITracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(50); // Simulate some work
            tracer.StopTrace();
        }
    }
}
