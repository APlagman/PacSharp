using System;

namespace PacSharpApp.Utils
{
    public class Benchmark
    {
        private static DateTime startDate = DateTime.MinValue;
        private static DateTime endDate = DateTime.MinValue;

        public static TimeSpan Span => endDate.Subtract(startDate);

        public static void Start() => startDate = DateTime.Now;

        public static void End() => endDate = DateTime.Now;

        public static double GetSeconds()
            => (endDate == DateTime.MinValue) ? 0.0 : Span.TotalSeconds;
    }
}
