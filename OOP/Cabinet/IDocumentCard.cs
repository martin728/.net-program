namespace Cabinet
{
    public interface IDocumentCard
    {
        string DocumentNumber { get; }
        string GetInfo();
    }
}