using System.Collections.Generic;

namespace Cabinet
{
    public class DocumentManager
    {
        private Dictionary<string, IDocumentCard> documentCards;

        public DocumentManager()
        {
            documentCards = new Dictionary<string, IDocumentCard>();
        }

        public void AddCard(IDocumentCard card)
        {
            documentCards[card.DocumentNumber] = card;
        }

        public IDocumentCard GetCard(string documentNumber)
        {
            if (documentCards.TryGetValue(documentNumber, out var card))
            {
                return card;
            }

            return null;
        }

        public List<IDocumentCard> SearchCardsByNumber(string searchNumber)
        {
            var results = new List<IDocumentCard>();

            foreach (var card in documentCards.Values)
            {
                if (card.DocumentNumber.Contains(searchNumber))
                {
                    results.Add(card);
                }
            }

            return results;
        }
    }

}