using System;
using System.Text;

namespace Cabinet
{
    public class LocalizedBookCard : IDocumentCard
    {
        public string DocumentNumber { get; set; }
        public string Title { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string OriginalPublisher { get; set; }
        public string CountryOfLocalization { get; set; }
        public string LocalPublisher { get; set; }
        public DateTime DatePublished { get; set; }
        public string ISBN { get; set; }

        public string GetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine("---- Localized Book Card ----");
            sb.AppendLine($"Document Number: {DocumentNumber}");
            sb.AppendLine($"Title: {Title}");
            sb.AppendLine($"Authors: {string.Join(", ", Authors)}");
            sb.AppendLine($"Number of Pages: {NumberOfPages}");
            sb.AppendLine($"Original Publisher: {OriginalPublisher}");
            sb.AppendLine($"Country of Localization: {CountryOfLocalization}");
            sb.AppendLine($"Local Publisher: {LocalPublisher}");
            sb.AppendLine($"Date Published: {DatePublished.ToShortDateString()}");
            sb.AppendLine($"ISBN: {ISBN}");
            sb.AppendLine("----------------------------");

            return sb.ToString();
        }
    }
}