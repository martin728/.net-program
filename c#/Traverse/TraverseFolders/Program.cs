using System;

namespace Traversing
{
    class Program
    {
        static void Main(string[] args)
        {
            var visitor = new FileSystemVisitor("../../../../testFolder",FilterByCsExtension);
            visitor.OnStart += ShowStartMessage;
            visitor.OnFinish += ShowFinishMessage;
            visitor.OnFileFound += FileFound;
            visitor.OnFilteredFileFound += FilteredFileFound;
            visitor.OnDirectoryFound += DirectoryFound;
            visitor.OnFilteredDirectoryFound += FilteredDirectoryFound;
            foreach (var file in visitor.Explore())
            {
                Console.WriteLine(file);
            }
        }

        static void FileFound(string file,CustomEventArgs args)
        {
            var abortionFile = "../../../../testFolder/testFolder1cs/file1.txt";
            var excludeFile = "../../../../testFolder/testFolder1cs/file1.txt";

            if (file == abortionFile)
            {
                Console.WriteLine("File:" + file + " found. Abortion...");
                args.AllowAbort = true;
            }
            
            if (file == excludeFile)
            {
                Console.WriteLine("File:" + file + " found. Exclusion...File skipped");
                args.AllowExclude = true;
            }
        }
        
        static void DirectoryFound(string file,CustomEventArgs args)
        {
            var abortionFolder = "../../../../testFolder/testFolder2cs";
            var excludeFolder = "../../../../testFolder/testFolder1cs";
            
            if (file == abortionFolder)
            {
                Console.WriteLine("Directory:" + file + " found. Abortion...");
                args.AllowAbort = true;
            }
            
            if (file == excludeFolder)
            {
                Console.WriteLine("Directory:" + file + " found. Exclusion...Folder skipped");
                args.AllowExclude = true;
            }
        }

        static void FilteredFileFound(string file,CustomEventArgs args)
        {
            var abortionFile = "../../../../testFolder/testFolder2cs/file2.cs";
            var excludeFile = "../../../../testFolder/testFolder1cs/file2.cs";

            if (file == abortionFile)
            {
                Console.WriteLine("Filtered file:" + file + " found. Abortion...");
                args.AllowAbort = true;
            }
            
            if (file == excludeFile)
            {
                Console.WriteLine("Filtered file:" + file + " found. Exclusion...File skipped");
                args.AllowExclude = true;
            }
        }
        
        static void FilteredDirectoryFound(string file,CustomEventArgs args)
        {
            var abortionFolder = "../../../../testFolder/testFolder2cs";
            var excludeFolder = "../../../../testFolder/testFolder1cs";
            
            if (file == abortionFolder)
            {
                Console.WriteLine("Filtered Directory:" + file + " found. Abortion...");
                args.AllowAbort = true;
            }
            
            if (file == excludeFolder)
            {
                Console.WriteLine("Filtered Directory:" + file + " found. Exclusion...Folder skipped");
                args.AllowExclude = true;
            }
        }
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
        
