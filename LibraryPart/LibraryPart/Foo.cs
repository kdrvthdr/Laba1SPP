using System;


namespace TracerLibrary
{
    public class Foo
    {
        private readonly Bar _bar;
        private readonly ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace(); // Начало трассировки

            // Логика вашего метода MyMethod
            Console.WriteLine("MyMethod is executing...");

            // Пример какой-то длительной операции в методе MyMethod
            for (int i = 0; i < 1000000; i++)
            {
                // Просто замедляем выполнение, чтобы увидеть результаты трассировки
                int result = i * i;
            }

            _bar.InnerMethod(); // Вызов другого метода, который также будет трассироваться

            Console.WriteLine("MyMethod execution completed.");

            _tracer.StopTrace(); // Окончание трассировки
        }
    }

    public class Bar
    {
        private readonly ITracer _tracer;

        public Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            // Логика выполнения метода InnerMethod
            System.Threading.Thread.Sleep(100); // Приостановка выполнения потока на 100 миллисекунд для имитации работы метода
            _tracer.StopTrace();
        }
    }
}
