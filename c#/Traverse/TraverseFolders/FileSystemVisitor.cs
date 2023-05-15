public delegate bool ReturnedDelegate(string filename, string extention);

class FileSystemVisitor
{
    public FileSystemVisitor(string filename)
    {
        foreach (string file in Explore(filename)) {
            Console.WriteLine(file);
        }
    }

    public FileSystemVisitor(string filename, ReturnedDelegate delegat)
    {
        foreach (string file in Explore(filename,delegat)) {
            Console.WriteLine(file);
        }
    }
    
    public IEnumerable<string> Explore(string path, ReturnedDelegate delegat)
    {
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
        {
            if (delegat(entry,"txt"))
            {
                yield return ShortenPath(entry);
            }

        }
        
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            Console.WriteLine(ShortenPath(subDir));
            foreach (var entry in Explore(subDir))
            {
                yield return ShortenPath(entry);
            }
        }
    }
    public IEnumerable<string> Explore(string path)
    {
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
        {
            if (Filter(entry,"txt"))
            {
                yield return ShortenPath(entry);
            }

        }
        
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            Console.WriteLine(ShortenPath(subDir));
            foreach (var entry in Explore(subDir))
            {
                yield return ShortenPath(entry);
            }
        }
    }
    
    static string ShortenPath(string filename)
    {
        string[] newStr = filename.Split("/");

        return newStr[newStr.Length -1];
    }

    bool Filter(string filename, string extention)
    {
        string[] newstring = ShortenPath(filename).Split(".");
        return newstring[newstring.Length -1] == extention;
    }
}