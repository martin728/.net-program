public delegate bool FilterFunctionDelegate(string name);
public delegate void Notify();
public delegate void FindFolderDelegate(string filename,CustomEventArgs eventArgs);
class FileSystemVisitor
{
    private readonly string _path;
    private readonly FilterFunctionDelegate _filterFunc;

    public event Notify? OnStart;
    public event Notify? OnFinish;
    public event FindFolderDelegate? OnFileFound;
    public event FindFolderDelegate? OnFilteredFileFound;
    public event FindFolderDelegate? OnDirectoryFound;
    public event FindFolderDelegate? OnFilteredDirectoryFound;

    public FileSystemVisitor(string path)
    {
        _path = path;
        _filterFunc = (_) => true;
    }

    public FileSystemVisitor(string filename, FilterFunctionDelegate delegat) : this(filename)
    {
        _filterFunc = delegat;
    }
    public IEnumerable<string> Explore()
    {
        OnStart?.Invoke();

        foreach (var entry in Explore(_path))
        {
            yield return entry;
        }
        
        OnFinish?.Invoke();
    }
    private IEnumerable<string> Explore(string path)
    {
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
        {
            var customArgs = new CustomEventArgs();
            
            OnFileFound?.Invoke(entry,customArgs);
            if (customArgs.AllowAbort)
            {
                break;
            }
            
            if (_filterFunc(entry))
            {
                OnFilteredFileFound?.Invoke(entry,customArgs);
                yield return entry;
            }
        }
        
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            var customArgs = new CustomEventArgs();
            
            OnDirectoryFound?.Invoke(subDir,customArgs);
            if (!customArgs.AllowExclude)
            {
                if (_filterFunc(subDir))
                {
                    OnFilteredDirectoryFound?.Invoke(subDir,customArgs);
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
    }
}

public class CustomEventArgs : EventArgs
{
    public bool AllowAbort { get; set; }
    public bool AllowExclude { get; set; }
}