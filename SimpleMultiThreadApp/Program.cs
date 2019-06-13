using System;
using System.Threading;

namespace SimpleMultiThreadApp
{
    class Printer
    {
        private object myLockToken = new object();
        
        public void AddOne(object data)
        {
            // увеличить значение переменной на 1
            int value = (int)data;
            Interlocked.Increment(ref value);
            Console.WriteLine(value);
        }

        public void SafeAssignment(object data)
        {
            // Присвоить значение 83 переменной
            int myInt = (int)data;
            Interlocked.Exchange(ref myInt, 83);
        }

        public void CompareAndExchange(object data)
        {
            // Если значение i равно 83, заменить его на 99.
            int i = (int)data;
            Interlocked.CompareExchange(ref i, 99, 83);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Printer printer = new Printer();
            int numb = 10;

            Thread[] threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ParameterizedThreadStart(printer.AddOne));
                Console.WriteLine("Поток {0}", threads[i].ManagedThreadId);
            }

            foreach (var thread in threads)
                thread.Start(numb);

            Console.ReadLine();
        }
    }
}
