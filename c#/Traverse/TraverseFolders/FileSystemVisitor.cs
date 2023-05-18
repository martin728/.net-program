public delegate bool FilterFunctionDelegate(string name);

public delegate void Notify();

public delegate bool MyCustomEvent(string filename,CustomEventArgs eventArgs);

class FileSystemVisitor
{
    private readonly string _path;
    private readonly FilterFunctionDelegate _filterFunc;
    private readonly CustomEventArgs _eventArgs;

    public event Notify OnStart;
    public event Notify OnFinish;
    public event MyCustomEvent OnFileFound;

    public FileSystemVisitor(string filename)
    {
        _path = filename;
        _filterFunc = (_) => true;
        OnStart = () => Console.WriteLine("Process started");
        OnFinish = () => Console.WriteLine("Process finished");
    }

    public FileSystemVisitor(string filename, FilterFunctionDelegate delegat)
    {
        _path = filename;
        _filterFunc = delegat;
        OnStart = () => Console.WriteLine("Process started");
        OnFinish = () => Console.WriteLine("Process finished");
    }
    
    public FileSystemVisitor(string filename, FilterFunctionDelegate delegat, Notify started, Notify finished, MyCustomEvent fileFoundEvent)
    {
        _path = filename;
        _filterFunc = delegat;
        OnStart = started;
        OnFinish = finished;
        OnFileFound = fileFoundEvent;
    }
    
    public IEnumerable<string> Explore(string path)
    {
        OnStart();
        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
        {
            if (OnFileFound(entry, _eventArgs))
            {
                break;
            };
            
            if (_filterFunc(entry))
            {
                yield return entry;
            }
        }
        
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            if (_filterFunc(subDir))
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
        OnFinish();
    }
}

public class CustomEventArgs : EventArgs
{
    public bool AllowAbort { get; set; }
    
}