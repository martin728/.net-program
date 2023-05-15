using System;

namespace Traversing
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileSystemVisitor1 = new FileSystemVisitor("../../../../testFolder");
            
            ReturnedDelegate filter = delegate(string filename, string extention)
            {
                string[] newStr = filename.Split("/");
                string[] newstring = newStr[newStr.Length -1].Split(".");
                return newstring[newstring.Length -1] == extention;
            };
            var fileSystemVisitor2 = new FileSystemVisitor("../../../../testFolder", filter);
        }
        
    }

}
        
