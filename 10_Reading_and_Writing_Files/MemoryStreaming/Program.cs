using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryStreaming
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.WriteLine("The value is 125.34");
                }
                Console.WriteLine(Encoding.UTF8.GetString(ms.ToArray()));
            }
            Console.ReadKey();
        }
    }
}
