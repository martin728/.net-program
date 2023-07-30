using System;
using System.Text;

namespace Cabinet
{
    public class MagazineCard: IDocumentCard
    {
        public string DocumentNumber { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public int ReleaseNumber { get; set; }
        public DateTime PublishDate { get; set; }

        public string GetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("---- Magazine Card ----");
            sb.AppendLine($"Document Number: {DocumentNumber}");
            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Publisher: {Publisher}");
            sb.AppendLine($"Release Number: {ReleaseNumber}");
            sb.AppendLine($"Publish Date: {PublishDate.ToShortDateString()}");
            sb.AppendLine("-----------------------");

            return sb.ToString();
        }
    }
}