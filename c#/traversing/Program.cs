using System;
using System.IO;

namespace Traversing
{
    class FileSystemVisitor
    {
        static void Main(string[] args)
        {
            var fileSystemVisitor = new FileSystemVisitor("../../../../testFolder", 0);
        }

        public FileSystemVisitor(string filename, int level)
        {
            Explore(filename, level);
        }

        void Explore(string filename, int level)
        {
            Console.Write( new String (' ' , level * 2));

            if (File.Exists(filename))
            {
                Console.WriteLine("File:" + Filter(filename));
            }
            else if(Directory.Exists(filename))
            {
                Console.WriteLine("Directory:" + Filter(filename));
                string[] files = Directory.GetFiles(filename);
                string[] dirs = Directory.GetDirectories(filename);

                foreach (string file in files) { Explore(file, level + 1); }
                foreach (string dir in dirs) { Explore(dir, level + 1); }

            }
            else
            {
                Console.WriteLine("Unknown:" + filename);
            }
        }

        string Filter(string filename)
        {
            string[] newStr = filename.Split("/");

            return newStr[newStr.Length -1];
        }
    }
}