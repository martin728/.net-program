using System;
using System.Text;

namespace Cabinet
{
    public class PatentCard : IDocumentCard
    {
        public string DocumentNumber { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UniqueId { get; set; }

        public string GetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("---- Patent Card ----");
            sb.AppendLine($"Document Number: {DocumentNumber}");
            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Authors: {string.Join(", ", Authors)}");
            sb.AppendLine($"Date Published: {DatePublished.ToShortDateString()}");
            sb.AppendLine($"Expiration Date: {ExpirationDate.ToShortDateString()}");
            sb.AppendLine($"Unique ID: {UniqueId}");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }
    }
}