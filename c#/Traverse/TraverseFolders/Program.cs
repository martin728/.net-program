using System;

namespace Traversing
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var visitor = new FileSystemVisitor("../../../../testFolder");
            
            Console.WriteLine("===== Result from first visitor ======");
            foreach (var file in visitor.Explore("../../../../testFolder"))
            {
                Console.WriteLine(file);
            }
            
            var visitor2 = new FileSystemVisitor("../../../../testFolder",FilterByCsExtension);
            
            Console.WriteLine("===== Result from second visitor ======");
            
            foreach (var file in visitor2.Explore("../../../../testFolder"))
            {
                Console.WriteLine(file);
            }
            */
            var visitor3 = new FileSystemVisitor("../../../../testFolder",FilterByCsExtension,ShowStartMessage,ShowFinishMessage,FileFoundEvent);
            
            foreach (var file in visitor3.Explore("../../../../testFolder"))
            {
                Console.WriteLine(file);
            }
        }

        static bool FileFoundEvent(string file,CustomEventArgs args)
        {
            var abortionFile = "../../../../testFolder/file1.cs";
            var e = new CustomEventArgs();
            if (file == abortionFile)
            {
                Console.WriteLine("File:" + file + " found. Abortion...");
                e.AllowAbort = true;
            }
            return e.AllowAbort;
        }
        
        /*static void DirectoryFoundEvent(string dir,CustomEventArgs args)
        {
            var abortionFile = "../../../../testFolder/testFolder1cs";
            
            if (dir == abortionFile)
            {
                Console.WriteLine("Directory:" + dir + " found. Abortion...");
                args.AllowAbort = true;
            }
        }*/
        
        static bool FilterByCsExtension(string name)
        {
            return name.Contains("cs");
        }

        static void ShowStartMessage()
        {
            Console.WriteLine("Process started");
        }
        
        static void ShowFinishMessage()
        {
            Console.WriteLine("Process finished");
        }
        
    }

}
        
