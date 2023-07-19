using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoCore.Models
{
    internal class PlayerModel
    {
        List<CardModel> cards;

        public void GiveCard(CardModel card)
        {
            cards.Add(card);
        }

        public CardModel GetCard(int index)
        {
            return cards[index];
        }

        public int[] GetPossibleMoves(CardModel current)
        {
            List<int> indexes = new List<int>();
            int index;
            foreach (CardModel card in cards)
            {
                if(IsPossibleMove(current, card))
                {
                    indexes.Add(cards.IndexOf(card));
                }
            }
            return indexes.ToArray();
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
    }
}
