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
    /// Logika interakcji dla klasy OknoGry.xaml
    /// </summary>
    public partial class OknoGry : Page
    {
        public Kwadrat kwadratWGierce = new Kwadrat(7, 0);
        public Kwadrat kwadratNastepny = new Kwadrat(7, 0);
        OknoGlowne ok = new OknoGlowne();
        public List<Kwadrat> istniejaceFigury = new List<Kwadrat> { };
        public int[,] mapa = new int[15, 27];
        public static bool pLewo;
        public static bool pPrawo;
        public static bool obrot;

        public int predkosc;
        public int licznik = 0;
        public int score;

        public OknoGry()
        {
            InitializeComponent();
        }

        //controler

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            //Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce);
            predkosc = UstawieniaStrona.predkoscKlockow;
            predkoscLabel.Content = predkosc;
            Inicjalizacja(licznik);
            Interakcje();
            SpadajaceKlockiGra();
            WyswietlNastepny(kwadratNastepny);
        }

        private void Wyjscie(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ZapiszIWyjdz(object sender, RoutedEventArgs e)
        {
            WczytIZapis.Save(mapa);
            Application.Current.Shutdown();
        }

        private void Powrot(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ok);
            GridGry.Children.Clear();
            Inicjalizacja(0);
            //kwadratWGierce = new Kwadrat(7, 0);
            //kwadratNastepny = new Kwadrat(7, 0);
        }

        //model

        public async void SpadajaceKlockiGra()
        {
            bool nieprzegrana = true;
            while (nieprzegrana)
            {
                if (Styk(kwadratWGierce) == false)
                {
                    Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce, false);
                    kwadratWGierce.wysokoscY += 1;
                    Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce);
                }
                else
                {
                    //Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce, false);
                    //kwadratWGierce.wysokoscY = -3;
                    //istniejaceFigury.Add(kwadratWGierce);
                    Linia();
                    WyswietlNastepny(kwadratNastepny, false);
                    kwadratWGierce = kwadratNastepny;
                    kwadratNastepny = new Kwadrat(7, 0);
                    WyswietlNastepny(kwadratNastepny);
                    nieprzegrana = Przegrana(kwadratWGierce);
                    score += 100;
                }
                Score.Content = score;
                await Task.Delay(100 * predkosc);
            }
        }

        public async void Interakcje()
        {
            while(true)
            {
                PrzesuniecieWLewo(pLewo);
                PrzesuniecieWPrawo(pPrawo);
                Obroc(obrot);
                await Task.Delay(1);
            }
        }

        public void Inicjalizacja(int licznikk)
        {
            licznik = licznikk;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 27; j++)
                    if (mapa[i, j] != 0)
                    {
                        licznik += 1;
                    }
            //MessageBox.Show(licznik.ToString());
            if (licznik == 0)
            {
                istniejaceFigury = null;
                for (int i = 0; i < 15; i++)
                    for (int j = 0; j < 27; j++)
                        if (j == 26) mapa[i, j] = 2;
                        else mapa[i, j] = 0;
            }
            else
            {
                licznik = 0;
                //UzupelnijKwadraty();
                WypiszZapisaneKwadraty();
            }
        }

        public bool Styk(Kwadrat kwadrat)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    if (kwadrat.grafika[i, j] == 1 && mapa[kwadrat.szerokoscX + j, kwadrat.wysokoscY + i + 1] == 2)
                    {
                        for (int ii = 0; ii < 3; ii++)
                            for (int jj = 0; jj < 3; jj++)
                                if (kwadrat.grafika[jj, ii] == 1)
                                    mapa[kwadrat.szerokoscX + ii, kwadrat.wysokoscY + jj] = 2;
                        return true;
                    }
            }
            return false;
        }

        public void PrzesuniecieWLewo(bool lewo)
        {
            if(lewo == true && kwadratWGierce.szerokoscX > 0 && LewyStyk(kwadratWGierce))
            {
                Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce, false);
                kwadratWGierce.szerokoscX -= 1;
                Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce);
                pLewo = false;
            }
        }

        public void PrzesuniecieWPrawo(bool prawo)
        {
            if (prawo == true && PrawyStyk(kwadratWGierce))
            {
                Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce, false);
                kwadratWGierce.szerokoscX += 1;
                Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce);
                pPrawo = false;
            }
        }

        public bool PrawyStyk(Kwadrat kwadrat)
        {
            if (kwadratWGierce.szerokoscX < 12)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        if (kwadrat.grafika[i, j] == 1 && mapa[kwadratWGierce.szerokoscX + j + 1, kwadratWGierce.wysokoscY + i] == 2)
                        {
                            return false;
                        }
                    }
                return true;
            }
            return false;
        }

        public bool LewyStyk(Kwadrat kwadrat)
        {
            if (kwadratWGierce.szerokoscX > 0)
            {
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        if (kwadrat.grafika[i, j] == 1 && mapa[kwadratWGierce.szerokoscX + j - 1, kwadratWGierce.wysokoscY + i + 1] == 2)
                        {
                            return false;
                        }
                return true;
            }
            return false;
        }

        public void Obroc(bool obrot1)
        {
            if(obrot1 == true)
            {
                Wypisz(kwadratWGierce.szerokoscX, kwadratWGierce.wysokoscY, kwadratWGierce, false);
                int[,] bufor = new int[3, 3];
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        bufor[2 - j, i] = kwadratWGierce.grafika[i, j];
                kwadratWGierce.grafika = bufor;
                obrot = false;
            }
        }

        private bool Przegrana(Kwadrat kwadrat)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (kwadrat.grafika[i, y] == 1)
                    {
                        if (mapa[kwadrat.szerokoscX + i, kwadrat.wysokoscY + y] == 2)
                        {
                            //MessageBox.Show("Przegrałeś!");
                            NavigationService.Navigate(ok);
                            GridGry.Children.Clear();
                            Inicjalizacja(0);
                            kwadratWGierce = new Kwadrat(7, 0);
                            kwadratNastepny = new Kwadrat(7, 0);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        
        //view

        public void WypiszZapisaneKwadraty()
        {
            int x = 0;
            for (int i = 0; i < 15; i++)
                for (int j = 0; j < 27; j++)
                {
                    if (mapa[i, j] == 2)
                    {
                        Rectangle figura = new Rectangle
                        {
                            Fill = Brushes.Pink,
                            Width = 21,
                            Height = 21,
                            StrokeThickness = 1,
                            Stroke = Brushes.White
                        };
                        GridGry.Children.Add(figura);
                        Grid.SetRow(figura, j);
                        Grid.SetColumn(figura, i);
                        x++;
                        
                    }
                }
        }

        private void Linia()
        {
            int gridRow = GridGry.RowDefinitions.Count;
            int gridColumn = GridGry.ColumnDefinitions.Count;
            int squareCount = 0;
            for (int row = gridRow; row >= 0; row--)
            {
                squareCount = 0;
                for (int column = gridColumn; column >= 0; column--)
                {
                    Rectangle square;
                    square = (Rectangle)GridGry.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == column);
                    if (square != null)
                    {
                        squareCount++;
                    }
                }
                // If squareCount == gridColumn this means tha the line is completed and must to be delete
                if (squareCount == gridColumn)
                {
                    UsunLinie(row);
                    score += 300;
                    Linia();
                }
            }
        }

        private void UsunLinie(int row)
        {
            // usun linie
            for (int i = 0; i < GridGry.ColumnDefinitions.Count; i++)
            {
                Rectangle square;
                try
                {
                    square = (Rectangle)GridGry.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == row && Grid.GetColumn(e) == i);
                    GridGry.Children.Remove(square);
                    mapa[i, row] = 0;
                }
                catch { }
            }
            // przesun reszte do dolu
            foreach (UIElement element in GridGry.Children)
            {
                Rectangle square = (Rectangle)element;
                if (Grid.GetRow(square) <= row)
                {
                    Grid.SetRow(square, Grid.GetRow(square) + 1);
                    
                }
            }
            for(int jj=row; jj>=0; jj--)
            {
                for (int ii = 0; ii < 15; ii++)
                    if (mapa[ii, jj] == 2)
                    {
                        mapa[ii, jj] = 0;
                        mapa[ii, jj + 1] = 2;
                    }
            }

        }

        public void Wypisz(int X, int Y, Kwadrat kwadrat, bool widzialnosc = true)
        {
            //if (widzialnosc == false) UsunFigure(index);
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
                            GridGry.Children.Add(figura);
                            Grid.SetRow(figura, y + Y);
                            Grid.SetColumn(figura, x + X);
                        }
                        else
                        {
                            figura = (Rectangle)GridGry.Children.Cast<UIElement>().FirstOrDefault(e => Grid.GetRow(e) == (Y + y) && Grid.GetColumn(e) == (X + x));
                            GridGry.Children.Remove(figura);
                        }
                    }
                }
            }
        }

        private void WyswietlNastepny(Kwadrat kwadrat, bool widzialnosc = true)
        {
            int X = 0;
            int Y = 0;
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
                    if (kwadrat.grafika[y, x] == 1)
                    {
                        if (widzialnosc == true)
                        {
                            NastepnyKlocek.Children.Add(figura);
                            Canvas.SetLeft(figura, 8 + X);
                            Canvas.SetTop(figura, 8 + Y);
                        }
                        else
                        {
                            NastepnyKlocek.Children.Clear();
                        }
                        X += 21;
                    }
                }
                X = 0;
                Y += 21;
            }
        }
    }
}
