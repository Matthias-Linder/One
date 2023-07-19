using UnoGame.Models;

DeckModel deck = new DeckModel();

deck.Init();

foreach(CardModel card in deck.GetDeck())
{
    Console.WriteLine(card.AsFileName());
}