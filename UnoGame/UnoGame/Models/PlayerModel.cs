using System;
using System.Collections.Generic;
using System.Linq;

namespace UnoGame.Models
{
    internal class PlayerModel
    {
        List<CardModel> cards = new List<CardModel>();

        public void GiveCard(CardModel card)
        {
            cards.Add(card);
        }

        public CardModel GetCard(int index)
        {
            return cards.ElementAt(index);
        }

        public int GetCardCount()
        {
            return cards.Count;
        }

        public List<CardModel> GetCards() {  return cards; }

        public void  RemoveCard(CardModel card)
        {
            cards.Remove(card);
        }

        public int[] GetPossibleMoves(CardModel current)
        {
            List<int> indices = new List<int>();

            foreach (CardModel card in cards)
            {
                if(IsPossibleMove(current, card))
                {
                    indices.Add(cards.IndexOf(card));
                }
            }
            return indices.ToArray();
        }

        private bool IsPossibleMove(CardModel current, CardModel playerCard)
        {
            if(current.GetColor() != 0)
            {
                if(playerCard.GetColor() == current.GetColor())
                {
                    return true;
                }
                else if (playerCard.GetNumber() == current.GetNumber())
                {
                    return true;
                }
            }
            if(current.GetColor() == 0 && playerCard.GetColor() == current.GetChosenColor())
            {
                return true;
            }
            if(playerCard.GetColor() == 0)
            {
                return true;
            }
            return false;
        }

        public int getMostCommonColor()
        {
            int red = 0;
            int green = 0;
            int blue = 0;
            int yellow = 0;

            foreach (CardModel card in cards)
            {
                switch (card.GetNumber())
                {
                    case 1:
                        red++;
                        break;
                    case 2:
                        yellow++;
                        break;
                    case 3:
                        green++;
                        break;
                    case 4:
                        blue++;
                        break;
                    default:
                        break;
                }
            }


            int greatest = Math.Max(Math.Max(Math.Max(red, green), blue), yellow);

            if(greatest == red)
            {
                return 1;
            }
            else if(greatest == yellow)
            {
                return 2;
            }
            else if (greatest == green)
            {
                return 3;
            }
            else if (greatest == blue)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }
    }
}
