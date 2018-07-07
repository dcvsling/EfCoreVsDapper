using System;
using System.Collections;
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
        public void Dapper() => _test.Dapper();

        [Benchmark]
        public void EfCore() => _test.EfCore();
    }

    public class TestClass
    {
        private readonly RunDapper _runDapper;
        private readonly RunEfCore _runEfCore;

        public TestClass()
        {
            _runDapper = new RunDapper();
            _runEfCore = new RunEfCore();
        }

        public void Dapper()
        {
            var result = _runDapper.Get().Count();
        }

        public void EfCore()
        {
            var result = _runEfCore.Get().Count();
        }
    }

}
