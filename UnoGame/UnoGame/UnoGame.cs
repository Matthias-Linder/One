using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnoGame.Models;
using Xamarin.Forms;

namespace UnoGame
{
    internal class UnoGame
    {
        CardModel current;

        DeckModel deck;

        PlayerModel[] players;

        PlayerModel currentPlayer;

        int currentPlayerIndex;

        int direction = 1;

        private static Random random = new Random();

        public void Init(int playerCount)
        {
            deck = new DeckModel();
            deck.Init();
            deck.Shuffle();
            current = deck.DrawCard();
            while (current.GetNumber() < 0)
            {
                deck.reAddCard(current);
                current = deck.DrawCard();
            }

            players = new PlayerModel[playerCount];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new PlayerModel();
            }


            currentPlayer = players[0];

            foreach (PlayerModel player in players)
            {
                for (int i = 0; i <= 1; i++) //check
                {
                    player.GiveCard(deck.DrawCard());
                }
            }
        }

        public List<CardModel> GetDeck()
        {
            return deck.GetDeck();
        }

        public CardModel DrawCard()
        {
            return deck.DrawCard();
        }

        public void setCurrentCard(CardModel newCard)
        {
            deck.reAddCard(current);

            //check which player is on the turn!!
            current = newCard;

        }

        public PlayerModel GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public PlayerModel GetPlayerByIndex(int index)
        {
            return players[index];
        }

        public CardModel GetCurrentCard()
        {
            return current;
        }

        public void changeDirection()
        {
            direction *= -1;
        }

        public void nextPlayer()
        {
            currentPlayerIndex += direction;

            if(currentPlayerIndex < 0)
            {
                currentPlayerIndex = players.Length - 1;
            }
            else if (currentPlayerIndex >= players.Length)
            {
                currentPlayerIndex = 0;
            }

            currentPlayer = players.ElementAt(currentPlayerIndex);
        }
    }
}
