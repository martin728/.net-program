using System;
using System.Text;

namespace Cabinet
{
    public class BookCard : IDocumentCard
    {
        public string DocumentNumber { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }

        public string GetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("---- Book Card ----");
            sb.AppendLine($"Document Number: {DocumentNumber}");
            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Authors: {string.Join(", ", Authors)}");
            sb.AppendLine($"Number of Pages: {NumberOfPages}");
            sb.AppendLine($"Publisher: {Publisher}");
            sb.AppendLine($"Date Published: {DatePublished.ToShortDateString()}");
            sb.AppendLine($"ISBN: {ISBN}");
            sb.AppendLine("--------------------");

            return sb.ToString();
        }
    }
}