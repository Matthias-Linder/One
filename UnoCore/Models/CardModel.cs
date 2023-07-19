using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnoCore.Models
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


        int choosenColor;
        //only for black cards

        public int GetChosenColor()
        {
            return choosenColor;
        }

        public CardModel(int num, int color)
        {
            this.num = num;
            this.color = color;
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
    }
}
