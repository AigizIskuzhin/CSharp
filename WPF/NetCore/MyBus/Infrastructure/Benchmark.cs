using System;
using System.Diagnostics;
using System.Windows;

namespace core5
{
    public static class Benchmark
    {
        public static void Run(Action act, int iterations = 1)
        {
            GC.Collect();
            act.Invoke();
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                act.Invoke();
            }
            sw.Stop();
            MessageBox.Show($"{sw.Elapsed} {act.Method.Name} ");
        }
    }
}
