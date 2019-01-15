using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    class Program
    {
        public delegate void temsilci();
        static void Main(string[] args)
        {
            //temsilci t = new temsilci(yaz);
            
        }

       public static void yaz()
        {

            Console.WriteLine("Yaz Metodu");
        }
    }
}
