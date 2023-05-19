public delegate bool FilterFunctionDelegate(string name);
public delegate void Notify();
public delegate void FindFolderDelegate(string filename,CustomEventArgs eventArgs);
class FileSystemVisitor
{
    private readonly string _path;
    private readonly FilterFunctionDelegate _filterFunc;

    public event Notify OnStart;
    public event Notify OnFinish;
    public event FindFolderDelegate OnFileFound;
    public event FindFolderDelegate OnFolderExclude;

    public FileSystemVisitor(string path)
    {
        _path = path;
        _filterFunc = (_) => true;
    }

    public FileSystemVisitor(string filename, FilterFunctionDelegate delegat) : this(filename)
    {
        _filterFunc = delegat;
    }

    public IEnumerable<string> Explore(string path)
    {
        OnStart.Invoke();
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
        {
            var i = new CustomEventArgs();
            OnFileFound.Invoke(entry,i);
            if (i.AllowAbort)
            {
                break;
            }
            if (_filterFunc(entry))
            {
                yield return entry;
            }
        }
        
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            var i = new CustomEventArgs();
            OnFolderExclude.Invoke(subDir,i);
            if (!i.AllowExclude)
            {
                if (_filterFunc(subDir))
                {
                    yield return subDir;
                }
                else
                {
                    continue;
                }
            }

            foreach (var entry in Explore(subDir))
            {
                yield return entry;
            }
        }
        OnFinish();
    }
}

public class CustomEventArgs : EventArgs
{
    public bool AllowAbort { get; set; }
    public bool AllowExclude { get; set; }
}