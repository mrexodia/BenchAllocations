using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BenchSharp
{
    class Program
    {
        public class Point
        {
            public int X = 0;
            public int Y = 0;

            [MethodImpl(MethodImplOptions.NoOptimization)]
            public static void Use(Point p)
            {
            }
        };

        const int totalAllocations = 1000000;

        static void programLoopalloc()
        {
            for (int i = 0; i < totalAllocations; i++)
            {
                var p = new Point();
                p.X = i;
                p.Y = -i;
                Point.Use(p);
                //delete p;
            }
        }

        static void programPrealloc()
        {
            var p = new Point();
            for (int i = 0; i < totalAllocations; i++)
            {
                p.X = i;
                p.Y = -i;
                Point.Use(p);
                //delete p;
            }
        }

        static void Main(string[] args)
        {
            var total = 0;
            const int count = 100;
            for (int i = 0; i < count; i++)
            {
                var ticks = Environment.TickCount;
                programLoopalloc();
                var ms = Environment.TickCount - ticks;
                total += ms;
                Console.WriteLine($"program ticks: {ms}ms");
            }
            Console.WriteLine($"loopalloc total: {total}ms, average: {total / count}ms");
            total = 0;
            for (int i = 0; i < count; i++)
            {
                var ticks = Environment.TickCount;
                programPrealloc();
                var ms = Environment.TickCount - ticks;
                total += ms;
                Console.WriteLine($"program ticks: {ms}ms");
            }
            Console.WriteLine($"prealloc total: {total}ms, average: {total / count}ms");
        }
    }
}
