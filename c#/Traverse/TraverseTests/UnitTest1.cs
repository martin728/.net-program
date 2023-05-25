namespace TraverseTests;

public class Tests
{
    [Test]
    public void Test1()
    {
        if (!FilterByCsExtension("testFolder/testFolder2cs"))
        {
            
        }
    }
    
    bool FilterByCsExtension(string name)
    {
        return name.Contains("cs");
    }
}