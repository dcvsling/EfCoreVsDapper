using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();

            var runner = new TestRunner();
            runner.EfCoreTrackingDapperCustomer();
            runner.EfCoreTracking();
            runner.EfCore();
            runner.Dapper();
            Action action = () =>
            {
                Console.WriteLine("-------------------");
                runner.EfCoreTrackingDapperCustomer();
                runner.EfCoreTracking();
                runner.EfCore();
                runner.Dapper();
            };

            Enumerable.Repeat(action, 10).ToList().ForEach(x => x());
            Console.Read();

        //    var times = Enumerable.Range(0, 100);
        //    var watch = new System.Diagnostics.Stopwatch();
        //    //以上可事先宣告

        //    var _runDapper = new RunDapper();
        //    var _runEfCore = new RunEfCore();

        //    do
        //    {
        //        watch.Restart();
        //        //進行測試
        //        foreach (var item in times)
        //        {
        //            //此處放測試的部份
        //            _runEfCore.Get().Count();
        //        }
        //        //測試結束
        //        watch.Stop();
        //        var elapsedMs = watch.ElapsedMilliseconds;
        //        Console.WriteLine("Time Cost:{0}", elapsedMs);
        //    } while (Console.ReadKey().Key != ConsoleKey.Escape);
        //    Console.ReadLine();

        //    do
        //    {
        //        watch.Restart();
        //        //進行測試
        //        foreach (var item in times)
        //        {
        //            //此處放測試的部份
        //            _runDapper.Get().Count();
        //        }
        //        //測試結束
        //        watch.Stop();
        //        var elapsedMs = watch.ElapsedMilliseconds;
        //        Console.WriteLine("Time Cost:{0}", elapsedMs);
        //    } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }

    [CoreJob]
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();
        public TestRunner()
        {
        }

        [Benchmark]
        public void Dapper() => Loop(nameof(Dapper),10,_test.Dapper);

        [Benchmark]
        public void EfCore() => Loop(nameof(EfCore),10, _test.EfCore);

        [Benchmark]
        public void EfCoreTracking() => Loop(nameof(EfCoreTracking),10, _test.EfCoreTracking);

        [Benchmark]
        public void EfCoreTrackingDapperCustomer() => Loop(nameof(EfCoreTrackingDapperCustomer), 10, _test.EfCoreTrackingDapperCustomer);
        private void Loop(string name,int times, Action action)
        {
            var finalaction = Enumerable.Repeat(action, times).Aggregate((left, right) => left += right);
            var watch = new Stopwatch();
            watch.Start();
            finalaction();
            watch.Stop();
            Console.WriteLine($"{name} => " + watch.ElapsedTicks);
        }
    }

    public class TestClass
    {
        private readonly RunDapper _runDapper;
        private readonly RunEfCore _runEfCore;
        private readonly RunEfCore _runEfCoreTracking;
        private readonly RunEfCore _runEfCoreTrackingDapperCustomer;

        public TestClass()
        {
            _runDapper = new RunDapper();
            _runEfCore = new RunEfCore(false);
            _runEfCoreTracking = new RunEfCore(true);
            _runEfCoreTrackingDapperCustomer = new RunEfCore(true);
        }

        public void Dapper()
        {
            var result = _runDapper.Get().Count();
        }

        public void EfCore()
        {
            var result = _runEfCore.GetCustomer().Count();
        }

        public void EfCoreTracking()
        {
            var result = _runEfCoreTracking.GetCustomer().Count();
        }
        public void EfCoreTrackingDapperCustomer()
        {
            var result = _runEfCoreTracking.GetDapperCustomer().Count();
        }
    }

}
