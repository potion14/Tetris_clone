using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Tetris2
{
    public class Kwadrat
    {
        public int szerokoscX;
        public int wysokoscY;
        public int typ;
        public int szybkosc;
        //public int obrot = 0;
        public int[,] grafika;
        public Random los = new Random();
        public Color kolor;

        public Kwadrat(int szerokosc, int wysokosc, int typ = -1)
        {
            szerokoscX = szerokosc;
            wysokoscY = wysokosc;
            int id = 4;
            for (int i = 0; i < wysokoscY + szerokoscX; i++) id = los.Next(1, 6);
            if (typ == -1) for (int i = 0; i < szerokoscX; i++) typ = los.Next(0, 7);
            grafika = DejGrafike(typ);
            switch (id)
            {
                case 1: kolor = Colors.Magenta; szybkosc = 1; break;
                case 2: kolor = Colors.DarkBlue; szybkosc = 1; break;
                case 3: kolor = Colors.Red; szybkosc = 1; break;
                case 4: kolor = Colors.Gold; szybkosc = 1; break;
                case 5: kolor = Colors.Gray; szybkosc = 1; break;
            }
        }

        public int[] wysokosc = new int[7] { 2, 3, 2, 2, 3, 3, 3 };
        //public int[] szerokosc = new int[7] { 1, 2, 2, 3, 1, 2, 3 };

        public int[,] DejGrafike(int typ)
        {
            switch (typ)
            {
                case 0: return new int[,] { { 0, 0, 0 },
                                            { 0, 1, 0 },
                                            { 0, 0, 0 } };

                case 1: return new int[,] { { 1, 0, 0 },
                                            { 1, 1, 1 },
                                            { 1, 0, 0 } };

                case 2: return new int[,] { { 1, 0, 0 },
                                            { 1, 1, 0 },
                                            { 0, 0, 0 } };

                case 3: return new int[,] { { 0, 0, 0 },
                                            { 1, 1, 1 },
                                            { 1, 0, 0 } };

                case 4: return new int[,] { { 1, 0, 0 },
                                            { 1, 0, 0 },
                                            { 1, 0, 0 } };

                case 5: return new int[,] { { 1, 0, 0 },
                                            { 1, 1, 0 },
                                            { 0, 1, 0 } };

                case 6: return new int[,] { { 0, 0, 0 },
                                            { 1, 1, 1 },
                                            { 0, 1, 0 } };
            }
            return new int[0, 0];
        }
        //Do tych klocków co spadają w menu

        //public void Obroc()
        //{
        //    Wypisz(false);
        //    int[,] bufor = new int[3, 3];
        //    for (int i = 0; i < 3; i++)
        //        for (int j = 0; j < 3; j++)
        //            bufor[2 - j, i] = grafika[i, j];
        //    grafika = bufor;
        //}
        
        public LinearGradientBrush getGradientColor(Color clr)
        {
            LinearGradientBrush gradientColor = new LinearGradientBrush();
            gradientColor.StartPoint = new Point(0, 0);
            gradientColor.EndPoint = new Point(1, 1.5);
            GradientStop black = new GradientStop();
            black.Color = Colors.Black;
            black.Offset = -1.5;
            gradientColor.GradientStops.Add(black);
            GradientStop other = new GradientStop();
            other.Color = clr;
            other.Offset = 0.70;
            gradientColor.GradientStops.Add(other);
            return gradientColor;
        }
    }
}
