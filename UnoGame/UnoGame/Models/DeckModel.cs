using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoGame.Models
{
    public class DeckModel
    {
        List<CardModel> deck = new List<CardModel>();

        public void Init()
        {
            bool generatedZeros = false;
            for(int i = 0; i <= 2; i++)
            {
                for (int c = 1; c <= 4; c++)
                {
                    for (int n = 0; n <= 12; n++)
                    {
                        if (n != 0)
                        {
                            deck.Add(new CardModel(n, c));
                        }
                        else if (!generatedZeros)
                        {
                            deck.Add(new CardModel(n, c));
                        }
                    }
                }
                generatedZeros = true;
            }
            for (int i = 0; i <= 4; i++)
            {
                deck.Add(new CardModel(-1, 0));
                deck.Add(new CardModel(-2, 0));
            }
        }

        public void Shuffle()
        {
            Randomize(deck);
        }


        int untilShuffle = 20;
        public void reAddCard(CardModel card)
        {
            deck.Add(card);
            untilShuffle--;
            if (untilShuffle <= 0)
            {
                Randomize(deck);
                untilShuffle = 20;
            }
        }

        public List<CardModel> GetDeck()
        {
            return deck;
        }

        public CardModel DrawCard()
        {
            CardModel card = deck.FirstOrDefault();
            deck.Remove(card);

            return card;
        }

        public static void Randomize<T>(List<T> list)
        {
            Random random = new Random();

            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

    }
}
