namespace TraverseTests;
using Traversing;

[TestFixture]
public class Tests
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

        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        // Act
        parser.Explore();
        
        // Assert
        var output = stringWriter.ToString();

        Assert.AreEqual(log, output);
    }
}
