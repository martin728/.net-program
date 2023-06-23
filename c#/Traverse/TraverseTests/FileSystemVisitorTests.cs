namespace TraverseTests;
using Traversing;

[TestFixture]
public class FileSystemVisitorTests
{
    private readonly string _path = "../../../testDir";
    
    [Test]
    public void TestFilterByCsException_AsValid()
    {
        // Arrange
        var parser = new FileSystemVisitor(_path, Program.FilterByCsExtension);
        var files = new List<string>();
        var expectedCount = 1;
        
        // Act
        foreach (var file in parser.Explore())
        {
            files.Add(file);
        }
        
        // Assert
        Assert.AreEqual(expectedCount, files.Count);
    }
    
    [Test]
    public void TestWithoutFilterByCsException_AsValid()
    {
        // Arrange
        var parser = new FileSystemVisitor(_path);
        var files = new List<string>();
        var expectedCount = 2;
        
        // Act
        foreach (var file in parser.Explore())
        {
            files.Add(file);
        }
        
        // Assert
        Assert.AreEqual(expectedCount, files.Count);
    } 
    
    [Test]
    public void TestIfProcessStarted_AsValid()
    {
        // Arrange
        var parser = new FileSystemVisitor(_path);
        var log = "Process started";
        var files = new List<string>();
        var stringWriter = new StringWriter();
        
        parser.OnStart += Program.ShowStartMessage;
        Console.SetOut(stringWriter);
        
        // Act
        foreach (var file in parser.Explore())
        {
            files.Add(file);
        }
        var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        // Assert
        Assert.AreEqual(log, outputLines[0]);
    }
    
    [Test]
    public void TestIfProcessFinished_AsValid()
    {
        // Arrange
        var parser = new FileSystemVisitor(_path);
        var log = "Process finished";
        var files = new List<string>();
        var stringWriter = new StringWriter();
        
        parser.OnFinish += Program.ShowFinishMessage;
        Console.SetOut(stringWriter);
        
        // Act
        foreach (var file in parser.Explore())
        {
            files.Add(file);
        }        
        var outputLines = stringWriter.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
       
        // Assert
        Assert.AreEqual(log, outputLines[0]);
    }
}
