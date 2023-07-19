using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnoCore.Models;

namespace UnoCore
{
    internal class UnoGame
    {
        CardModel current;

        DeckModel deck;

        PlayerModel[] players;

        PlayerModel currentPlayer;

        int direction = 1;

        private static Random random = new Random();

        public void Init(int players)
        {
            deck = new DeckModel();
            deck.Init();
            deck.Shuffle();
            current = deck.DrawCard();
            this.players = new PlayerModel[players];
            currentPlayer = this.players[random.Next(0, players)];
            foreach (PlayerModel player in this.players)
            {
                for (int i = 0; i <= 7; i++)
                {
                    player.GiveCard(deck.DrawCard());
                }
            }
        }

        public PlayerModel getCurrentPlayer()
        {
            return currentPlayer;
        }

        private void changeDirection()
        {
            direction *= -1;
        }

        private void nextPlayer()
        {

        }
    }
}
