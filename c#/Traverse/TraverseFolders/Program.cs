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
            var visitor3 = new FileSystemVisitor("../../../../testFolder",FilterByCsExtension);
            visitor3.OnStart += ShowStartMessage;
            visitor3.OnFinish += ShowFinishMessage;
            visitor3.OnFileFound += FileFoundEvent;
            visitor3.OnFolderExclude += FileExcludeEvent;
            foreach (var file in visitor3.Explore("../../../../testFolder"))
            {
                Console.WriteLine(file);
            }
        }

        static void FileFoundEvent(string file,CustomEventArgs args)
        {
            var abortionFile = "../../../../testFolder/file1.cs";
            if (file == abortionFile)
            {
                Console.WriteLine("File:" + file + " found. Abortion...");
                args.AllowAbort = true;
            }
        }
        static void FileExcludeEvent(string file,CustomEventArgs args)
        {
            var excludeFolder = "../../../../testFolder/testFolder1cs";
            if (file == excludeFolder)
            {
                Console.WriteLine("File:" + file + " found. Exclusion...Folder skipped");
                args.AllowExclude = true;
            }
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
        
