public delegate bool ReturnedDelegate(string filename);
class FileSystemVisitor
{
    public readonly string Path;
    public readonly ReturnedDelegate filterFunc;
    public FileSystemVisitor(string filename)
    {
        Path = filename;
        filterFunc = (_) => true;
    }

    public FileSystemVisitor(string filename, ReturnedDelegate delegat)
    {
        Path = filename;
        filterFunc = delegat;
    }
    public IEnumerable<string> Explore(string path)
    {
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
        {
            if (filterFunc(entry))
            {
                yield return entry;
            }

        }
        
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            if (filterFunc(subDir))
            {
                yield return subDir;
            }
            else
            {
                continue;
            }
            
            foreach (var entry in Explore(subDir))
            {
                yield return entry;
            }
        }
    }
}