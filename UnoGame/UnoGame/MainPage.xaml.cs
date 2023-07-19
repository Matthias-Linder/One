using Java.Lang;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnoGame.Models;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace UnoGame
{
    public partial class MainPage : ContentPage
    {
        const int REAL_PLAYER = 0;
        const int AI_PLAYER = 1;

        PlayerModel player;

        PlayerModel ai;

        UnoGame game = new UnoGame();

        private static Random random = new Random();

        Thread main;

        public MainPage()
        {
            InitializeComponent();
            game.Init(2);
            main = new Thread(Run);
            main.Start();
            //Run();
        }

        private const int TargetFPS = 40; // Desired FPS count
        private Stopwatch stopwatch = new Stopwatch();
        private bool isRunning;


        private void PlayerWin()
        {
            ShowWinPopup("You win!");
        }

        private void AIWin()
        {
            ShowWinPopup("AI wins!");
        }


        private void Run()
        {
            isRunning = true;

            Initialize();



            while (isRunning)
            {
                stopwatch.Start();

                Device.BeginInvokeOnMainThread(async () =>
                {
                    Update();
                    Render();
                });


                int timeLeft = (int)(1000 / TargetFPS - stopwatch.ElapsedMilliseconds);
                if (timeLeft > 0)
                {
                    Thread.Sleep(timeLeft);
                }
                stopwatch.Reset();
            }
        }


        private void Update()
        {
            if(player.GetCardCount() == 0)
            {
                PlayerWin();
            }
            else if(ai.GetCardCount()==0)
            {
                AIWin();
            }
            else
            {
                if (game.GetCurrentPlayer() == ai)
                {
                    if (game.GetCurrentCard().GetNumber() == 12 && !game.GetCurrentCard().drawnCards)
                    {
                        ai.GiveCard(game.DrawCard());
                        ai.GiveCard(game.DrawCard());
                        game.GetCurrentCard().drawnCards = true;
                        //draw 2 cards
                    }
                    if (game.GetCurrentCard().GetNumber() == -2 && !game.GetCurrentCard().drawnCards)
                    {
                        ai.GiveCard(game.DrawCard());
                        ai.GiveCard(game.DrawCard());
                        ai.GiveCard(game.DrawCard());
                        ai.GiveCard(game.DrawCard());
                        game.GetCurrentCard().drawnCards = true;
                    }
                    MakeAIMove();

                    foreach (CardModel card in player.GetCards())
                    {
                        card.setOpacity(0.2);
                        card.setButtonEnabled(false);
                        drawCardButton.IsEnabled = false;
                    }
                }
                else if (game.GetCurrentPlayer() == player)
                {
                    if (game.GetCurrentCard().GetNumber() == 12 && !game.GetCurrentCard().drawnCards)
                    {
                        player.GiveCard(game.DrawCard());
                        player.GiveCard(game.DrawCard());
                        game.GetCurrentCard().drawnCards = true;
                        
                        //draw 2 cards
                    }
                    if (game.GetCurrentCard().GetNumber() == -2 && !game.GetCurrentCard().drawnCards)
                    {
                        player.GiveCard(game.DrawCard());
                        player.GiveCard(game.DrawCard());
                        player.GiveCard(game.DrawCard());
                        player.GiveCard(game.DrawCard());
                        game.GetCurrentCard().drawnCards = true;
                    }

                    //player card possible moves
                    int[] moves = player.GetPossibleMoves(game.GetCurrentCard());
                    int i = 0;
                    drawCardButton.IsEnabled = true;
                    foreach (CardModel card in player.GetCards())
                    {
                        card.getButton().Clicked -= onClick;
                        card.getButton().Clicked += onClick;
                        card.getImageButton().Clicked -= onClick;
                        card.getImageButton().Clicked += onClick;
                        if (moves.Contains(i))
                        {
                            card.setOpacity(1);
                            card.getButton().IsEnabled = true;
                            card.getImageButton().IsEnabled = true;
                        }
                        else
                        {
                            card.setOpacity(0.2);
                            card.getButton().IsEnabled = false;
                            card.getImageButton().IsEnabled = false;
                        }
                        i++;
                    }

                    //check click event
                    int index = 0;
                    foreach (CardModel card in player.GetCards().ToList())
                    {
                        if (card.clicked)
                        {
                            foreach (CardModel c in player.GetCards().ToList())
                            {
                                card.getImageButton().IsEnabled = false;
                            }
                            ClickedOn(index);
                            card.clicked = false;
                        }
                        index++;
                    }
                }
            }
            


        }

        Stopwatch timeOut = new Stopwatch();
        private void MakeAIMove()
        {
            timeOut.Start();
            if (timeOut.ElapsedMilliseconds > 1000)
            {
                int[] moves = ai.GetPossibleMoves(game.GetCurrentCard());

                if (moves.Length > 0)
                {
                    CardModel card = ai.GetCard(moves[random.Next(moves.Length)]);
                    if (card.GetNumber() < 0)
                    {
                        card.setChoosenColor(ai.getMostCommonColor());
                    }
                    game.setCurrentCard(card);
                    ai.RemoveCard(card);
                    if (card.GetNumber() == 11)
                    {
                        game.changeDirection();
                        game.nextPlayer();
                    }
                    if (card.GetNumber() == 10)
                    {
                        game.nextPlayer();
                    }
                    game.nextPlayer();
                }
                else
                {
                    ai.GiveCard(game.DrawCard());
                    game.nextPlayer();
                }
                timeOut.Reset();
            }



        }
        int playedCardIndex;
        private void ClickedOn(int index)
        {
            CardModel card = player.GetCard(index);
            if (card.GetNumber() < 0)
            {
                ShowColorSelectionPopup();
                playedCardIndex = index;
            }
            else
            {
                playedCardIndex = index;
                game.setCurrentCard(card);
                player.RemoveCard(card);
                if (card.GetNumber() == 11)
                {
                    game.changeDirection();
                    game.nextPlayer();
                }
                if (card.GetNumber() == 10)
                {
                    game.nextPlayer();
                }
                game.nextPlayer();
            }
            
        }

        private void DrawCard(object sender, EventArgs e)
        {
            player.GiveCard(game.DrawCard());
            game.nextPlayer();
        }

        private void onClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            ImageButton imgBtn = sender as ImageButton;
            foreach (CardModel card in player.GetCards())
            {
                if (btn == card.getButton())
                {
                    card.clicked = true;
                }
                if (imgBtn == card.getImageButton())
                {
                    card.clicked = true;
                }
            }
        }

        int previousCardCount;
        private void Render()
        {
            //draw current card
            if (game.GetCurrentCard().GetNumber() < 0)
            {
                switch (game.GetCurrentCard().GetChosenColor())
                {
                    case 1:
                        currentCardContainer.Source = game.GetCurrentCard().AsFileNoEnding() + "_red.png";
                        break;
                    case 2:
                        currentCardContainer.Source = game.GetCurrentCard().AsFileNoEnding() + "_yellow.png";
                        break;
                    case 3:
                        currentCardContainer.Source = game.GetCurrentCard().AsFileNoEnding() + "_green.png";
                        break;
                    case 4:
                        currentCardContainer.Source = game.GetCurrentCard().AsFileNoEnding() + "_blue.png";
                        break;
                    default:
                        currentCardContainer.Source = game.GetCurrentCard().AsFileNoEnding() + ".png";
                        break;
                }
            }
            else
            {
                currentCardContainer.Source = game.GetCurrentCard().AsFileName();
            }
            //currentCardContainer.Source = game.GetCurrentCard().AsFileName();


            //draw player cards
            double scroll = scrollPlayerCards.ScrollX;
            if (player.GetCardCount() != previousCardCount)
            {
                
                playerCardContainer.Children.Clear();
                scrollPlayerCards.ScrollToAsync(scroll, 0, false);
                foreach (CardModel card in player.GetCards())
                {
                    //playerCardContainer.Children.Add(card.getButton());
                    playerCardContainer.Children.Add(card.getImageButton());
                }
                previousCardCount = player.GetCardCount();

            }


            //draw cards left ai
            cardsLeftAi.Text = ai.GetCardCount().ToString();
        }

        private void Initialize()
        {
            player = game.GetPlayerByIndex(REAL_PLAYER);
            ai = game.GetPlayerByIndex(AI_PLAYER);
        }

        Popup colorSelectionDialog;
        private void ShowColorSelectionPopup()
        {

            Button red = new Button
            {
                BackgroundColor = Color.Red,
                TextColor = Color.Black,
                Text = "Red",
            };

            Button yellow = new Button
            {
                BackgroundColor = Color.Yellow,
                TextColor = Color.Black,
                Text = "Yellow",
            };

            Button green = new Button
            {
                BackgroundColor = Color.Green,
                TextColor = Color.Black,
                Text = "Green",
            };

            Button blue = new Button
            {
                BackgroundColor = Color.Blue,
                TextColor = Color.Black,
                Text = "Blue",
            };

            red.Clicked += OnColorButtonClick;

            yellow.Clicked += OnColorButtonClick;

            green.Clicked += OnColorButtonClick;

            blue.Clicked += OnColorButtonClick;


            colorSelectionDialog = new Popup()
            {
                //IsLightDismissEnabled = false,

                Size = new Size(250, 180),

                Content = new StackLayout
                {

                    Children =
                    {
                        new Label()
                        {
                            TextColor = Color.Black,
                            Text = "Select a Color",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                        },
                        new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions= LayoutOptions.Center,
                            Orientation = StackOrientation.Horizontal,
                            Margin = new Thickness(20, 20, 20, 20),
                            Children =
                            {
                            new StackLayout
                                {
                                    Children =
                                    {
                                        red,
                                        yellow
                                    }
                                },
                            new StackLayout
                                {
                                    Children =
                                    {
                                        green,
                                        blue
                                    }
                                }
                            }

                        },
                    }
                }
            };
            Application.Current.MainPage.Navigation.ShowPopup(colorSelectionDialog);
        }

        bool winDialogShown = false;
        Popup winDialog;
        private void ShowWinPopup(string winner)
        {

            
            if(!winDialogShown)
            {
                Button restartButton = new Button
                {
                    Text = "Restart",
                };

                restartButton.Clicked += RestartClick;

                winDialog = new Popup()
                {
                    IsLightDismissEnabled = false,
                    Size = new Size(200, 150),
                    Content = new StackLayout
                    {
                        Children =
                    {
                        new Label()
                        {
                            TextColor = Color.Black,
                            Text = winner,
                            HorizontalOptions = LayoutOptions.Center,
                            FontSize = 25,
                            Margin = new Thickness(0,5,0,50)
                        },
                        restartButton
                    }
                    }
                };

                App.Current.MainPage.Navigation.ShowPopup(winDialog);
                winDialogShown = true;
                winDialog.IsVisible = true;
            }

        }
      

        private void OnColorButtonClick(object sender, EventArgs e)
        {
            int selectedColor = 0;
            Button button = sender as Button;
            if(button.BackgroundColor == Color.Red)
            {
                selectedColor = 1;
            }
            else if (button.BackgroundColor == Color.Yellow)
            {
                selectedColor= 2;
            }
            else if(button.BackgroundColor == Color.Green)
            {
                selectedColor = 3;
            }
            else if(button.BackgroundColor == Color.Blue)
            {
                selectedColor = 4;
            }

            CardModel card = player.GetCard(playedCardIndex);

            card.setChoosenColor(selectedColor);

            colorSelectionDialog.Dismiss(null);

            game.setCurrentCard(card);
            player.RemoveCard(card);
            if (card.GetNumber() == 11)
            {
                game.changeDirection();
                game.nextPlayer();
            }
            if (card.GetNumber() == 10)
            {
                game.nextPlayer();
            }
            game.nextPlayer();
        }

        private void RestartClick(object sender, EventArgs e)
        {
            winDialog.Dismiss(null);
            winDialog.IsVisible = false;
            winDialogShown = false;
            game = new UnoGame();
            game.Init(2);
            player = game.GetPlayerByIndex(REAL_PLAYER);
            ai = game.GetPlayerByIndex(AI_PLAYER);
        }
    }
}
