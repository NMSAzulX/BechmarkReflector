using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace TypeTest
{
    public class TestRun
    {

        public FieldInfo NameInfo = typeof(TestBechmark).GetField("Name");
        public readonly Action<TestBechmark, string> EmitSetString;
        public TestRun()
        {
            DynamicMethod method = new DynamicMethod("GetString", null, new Type[] { typeof(TestBechmark), typeof(string) });
            ILGenerator il = method.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, typeof(TestBechmark).GetField("Name"));
            il.Emit(OpCodes.Ret);
            EmitSetString = (Action<TestBechmark, string>)(method.CreateDelegate(typeof(Action<TestBechmark, string>)));
        }

        [Benchmark]
        public void Origin()
        {

            TestBechmark instance = new TestBechmark();
            instance.Name = "123456789";

        }

        [Benchmark]
        public void Dynamic()
        {
            TestBechmark instance = new TestBechmark();
            RunDynamic(instance);
        }

        [Benchmark]
        public void Reflect()
        {
            TestBechmark instance = new TestBechmark();
            NameInfo.SetValue(instance, "123456789");
        }

        [Benchmark]
        public void Emit()
        {
            TestBechmark instance = new TestBechmark();
            EmitSetString(instance, "123456789");
        }

        public void RunDynamic(dynamic instance)
        {
            instance.Name = "123456789";
        }
    }

    public class TestBechmark
    {
        public string Name;
    }
}
