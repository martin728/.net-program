using System;

namespace Traversing
{
    class Program
    {
        static void Main(string[] args)
        {
            var visitor = new FileSystemVisitor("../../../../testFolder");
            var visitor2 = new FileSystemVisitor("../../../../testFolder",FilterByCsExtension);
            Console.WriteLine("===== Result from first visitor ======");
            foreach (var file in visitor.Explore("../../../../testFolder"))
            {
                Console.WriteLine(file);
            }
            Console.WriteLine("===== Result from second visitor ======");
            foreach (var file in visitor2.Explore("../../../../testFolder"))
            {
                Console.WriteLine(file);
            }
        }
        static bool FilterByCsExtension(string filename)
        {
            return filename.Contains("cs");
        }
    }

}
        
