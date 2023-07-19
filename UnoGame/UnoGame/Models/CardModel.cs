using MetalKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnoGame.Models
{
    public class CardModel
    {
        int num;
        //0 to 9 = normal Cards
        //10 = skip
        //11 = reverse
        //12 = +2
        //-1 = wild
        //-2 wild + 4

        int color;
        //1 = red
        //2 = yellow
        //3 = green
        //4 = blue
        //0 = black

        public bool drawnCards = false;

        Image img;

        Button btn;

        ImageButton imgBtn;

        public bool clicked = false;

        int choosenColor;
        //only for black cards

        public int GetChosenColor()
        {
            return choosenColor;
        }

        public void setChoosenColor(int color)
        {
            choosenColor = color;
        }

        public CardModel(int num, int color)
        {
            this.num = num;
            this.color = color;
            img = new Image() { Source = AsFileName() };
            btn = new Button() { ImageSource = AsFileName() };
            //btn.Margin = new Thickness(2, 20, 2, 20);
            btn.Padding = new Thickness(2, 2, 2, 2);
            btn.BackgroundColor = Color.White;

            imgBtn = new ImageButton() { Source = AsFileName(), HeightRequest = img.Height};
        }

        public Image getImage()
        {
            return img;
        }

        public Button getButton()
        {
            return btn;
        }

        public ImageButton getImageButton()
        {
            return imgBtn;
        }

        public void setOpacity(double opacity)
        {
            img.Opacity = opacity;
            btn.Opacity = opacity;
            imgBtn.Opacity = opacity;
        }

        public void setButtonEnabled(bool enabled)
        {
            btn.IsEnabled = enabled;
        }

        public int GetNumber() { return num; }

        public int GetColor() { return color; }

        public string AsString()
        {
            string color = "";
            string number = "";

            switch (this.num)
            {
                case 0:
                    number = "Zero";
                    break;
                case 1:
                    number = "One";
                    break;
                case 2:
                    number = "Two";
                    break;
                case 3:
                    number = "Three";
                    break;
                case 4:
                    number = "Four";
                    break;
                case 5:
                    number = "Five";
                    break;
                case 6:
                    number = "Six";
                    break;
                case 7:
                    number = "Seven";
                    break;
                case 8:
                    number = "Eight";
                    break;
                case 9:
                    number = "Nine";
                    break;
                case 10:
                    number = "Skip";
                    break;
                case 11:
                    number = "Reverse";
                    break;
                case 12:
                    number = "Plus Two";
                    break;
                case -1:
                    number = "Wild";
                    break;
                case -2:
                    number = "Wild and draw Four";
                    break;
                default:
                    number = "";
                    break;
            }

            switch (this.color)
            {
                case 1:
                    color = "red ";
                    break;
                case 2:
                    color = "yellow ";
                    break;
                case 3:
                    color = "green ";
                    break;
                case 4:
                    color = "blue ";
                    break;
                case -1:
                    color = "";
                    break;
            }

            return color + number;
        }

        public string AsFileNoEnding()
        {
            string color = "";
            string number = "";

            switch (this.num)
            {
                case 0:
                    number = "zero";
                    break;
                case 1:
                    number = "one";
                    break;
                case 2:
                    number = "two";
                    break;
                case 3:
                    number = "three";
                    break;
                case 4:
                    number = "four";
                    break;
                case 5:
                    number = "five";
                    break;
                case 6:
                    number = "six";
                    break;
                case 7:
                    number = "seven";
                    break;
                case 8:
                    number = "eight";
                    break;
                case 9:
                    number = "nine";
                    break;
                case 10:
                    number = "skip";
                    break;
                case 11:
                    number = "reverse";
                    break;
                case 12:
                    number = "plustwo";
                    break;
                case -1:
                    number = "wild";
                    break;
                case -2:
                    number = "wildplusfour";
                    break;
                default:
                    number = "";
                    break;
            }

            switch (this.color)
            {
                case 1:
                    color = "red_";
                    break;
                case 2:
                    color = "yellow_";
                    break;
                case 3:
                    color = "green_";
                    break;
                case 4:
                    color = "blue_";
                    break;
                case -1:
                    color = "";
                    break;
            }

            return color + number;
        }

        public string AsFileName()
        {
            return AsFileNoEnding() + ".png";
        }


    }
}
