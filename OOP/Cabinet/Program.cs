using System;
using System.Collections.Generic;

namespace Cabinet
{
    internal class Program
    {
        static void PrintCardInfo(IDocumentCard card)
        {
            Console.WriteLine(card.GetInfo());
        }
        public static void Main(string[] args)
        {
            var documentManager = new DocumentManager();
            var cacheManager = new CacheManager();

            // Add sample document cards
            var patentCard1 = new PatentCard
            {
                DocumentNumber = "patent_001",
                Title = "Sample Patent 1",
                Authors = new[] { "John Doe" },
                DatePublished = new DateTime(2020, 1, 1),
                ExpirationDate = new DateTime(2030, 1, 1),
                UniqueId = "P12345"
            };
            
            var bookCard1 = new BookCard
            {
                DocumentNumber = "book_001",
                Title = "Sample Book 1",
                Authors = new[] { "Jane Smith" },
                NumberOfPages = 200,
                Publisher = "Sample Publisher",
                DatePublished = new DateTime(2019, 5, 15),
                ISBN = "978-1234567890"
            };

            var localizedBookCard1 = new LocalizedBookCard
            {
                DocumentNumber = "localizedbook_001",
                Title = "Sample Localized Book 1",
                Authors = new[] { "David Johnson" },
                NumberOfPages = 250,
                OriginalPublisher = "Original Publisher",
                CountryOfLocalization = "Country X",
                LocalPublisher = "Local Publisher",
                DatePublished = new DateTime(2021, 10, 20),
                ISBN = "978-0987654321"
            };
            
            var magazineCard1 = new MagazineCard
            {
                DocumentNumber = "magazine_001",
                Title = "Sample Magazine 1",
                Publisher = "Sample Magazine Publisher",
                ReleaseNumber = 10,
                PublishDate = new DateTime(2023, 7, 1)
            };

            documentManager.AddCard(patentCard1);
            documentManager.AddCard(bookCard1);
            documentManager.AddCard(localizedBookCard1);
            documentManager.AddCard(magazineCard1);
            
            while (true)
            {
                Console.WriteLine("Enter document number to search (or 'exit' to quit):");
                var input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                var results = documentManager.SearchCardsByNumber(input);

                if (results.Count == 0)
                {
                    Console.WriteLine("No document cards found matching the search.");
                }
                else
                {
                    foreach (var card in results)
                    {
                        PrintCardInfo(card);

                        // Check if the card type should be cached and cache it if required
                        var cacheExpirationTime = cacheManager.GetCacheExpirationTime(card.GetType());
                        if (cacheExpirationTime != TimeSpan.Zero)
                        {
                            var cachedCards = cacheManager.GetFromCache(card.GetType()) ?? new List<IDocumentCard>();
                            cachedCards.Add(card);
                            cacheManager.AddToCache(card.GetType(), cachedCards, cacheExpirationTime);
                        }
                    }
                }
            }
        }
    }
}