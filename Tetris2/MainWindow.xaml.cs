using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tetris2
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Kwadrat> kwadratyWMeni = new List<Kwadrat> { new Kwadrat(1, 3), new Kwadrat(5, 10),
            new Kwadrat(10, 5), new Kwadrat(29, 12), new Kwadrat(33, 2), new Kwadrat(38, 15)};
        

        //public DateTime sleep = DateTime.Now;
        //public double klatkiNaSekunde = 100;

        public MainWindow()
        {
            InitializeComponent();
            OknoGlowne ok = new OknoGlowne();
            frame.Content = ok;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            //Kwadrat kwadrat = new Kwadrat(10, 10);
            //kwadrat.Wypisz();
            WypisywanieSpadajacychFigur();
        }

        //view

        public void Wypisz(int X, int Y, Kwadrat kwadrat, bool widzialnosc = true)
        {
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    Rectangle figura = new Rectangle
                    {
                        Fill = kwadrat.getGradientColor(kwadrat.kolor),
                        Width = 21,
                        Height = 21,
                        StrokeThickness = 1,
                        Stroke = Brushes.White
                    };
                    if (kwadrat.grafika[y, x] == 1 && Y + y < 26 && Y + y >= 0)
                    {
                        if (widzialnosc == true)
                        {
                            GridTetrisaTlo.Children.Add(figura);
                            Grid.SetRow(figura, y + Y);
                            Grid.SetColumn(figura, x + X);
                        }
                        else
                        {
                            figura = (Rectangle)GridTetrisaTlo.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == (Y + y) && Grid.GetColumn(e) == (X + x));
                            GridTetrisaTlo.Children.Remove(figura);
                        }
                    }
                }
            }
        }

        public async void WypisywanieSpadajacychFigur()
        {
            while(true)
            {
                for (int i = 0; i < kwadratyWMeni.Count(); i++)
                {
                    if (kwadratyWMeni[i].wysokoscY < 26)
                    {
                        Wypisz(kwadratyWMeni[i].szerokoscX, kwadratyWMeni[i].wysokoscY, kwadratyWMeni[i], false);
                        kwadratyWMeni[i].wysokoscY += 1;
                        Wypisz(kwadratyWMeni[i].szerokoscX, kwadratyWMeni[i].wysokoscY, kwadratyWMeni[i]);
                    }
                    else
                    {
                        Wypisz(kwadratyWMeni[i].szerokoscX, kwadratyWMeni[i].wysokoscY, kwadratyWMeni[i], false);
                        kwadratyWMeni[i].wysokoscY = -3;
                    }
                }
                await Task.Delay(100 * UstawieniaStrona.predkoscKlockow);
            }
        }

        //controler

        private void ObslugaKlawiszy(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    OknoGry.obrot = true;
                    break;
                case Key.Left:
                    OknoGry.pLewo = true;
                    break;
                case Key.Right:
                    OknoGry.pPrawo = true;
                    break;
            }
        }
        
    }
}
