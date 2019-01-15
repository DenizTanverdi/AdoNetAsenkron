using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace TreadSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart job = new ThreadStart(ThreadJob);
            Thread thread = new Thread(job);
            thread.Start();
            
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Ana thread: {0}", i);
                Thread.Sleep(1000);
            }


        }

        static void ThreadJob()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Alt thread: {0}", i);
                Thread.Sleep(500);
            }
        }
    }
    
}
