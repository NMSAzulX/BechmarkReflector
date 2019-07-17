using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace TypeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            
            BenchmarkRunner.Run<TestRun>();
            Console.ReadKey();

        }
    }

}
