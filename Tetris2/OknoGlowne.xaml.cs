using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
    /// Logika interakcji dla klasy OknoGlowne.xaml
    /// </summary>
    public partial class OknoGlowne : Page
    {
        public bool wczytano = false;

        public OknoGlowne()
        {
            InitializeComponent();
        }

        private void Wyjscie(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WyswietlInstrukcje(object sender, RoutedEventArgs e)
        {
            Instrukcja instrukcja = new Instrukcja();
            NavigationService.Navigate(instrukcja);
        }

        private void WyswietlUstawienia(object sender, RoutedEventArgs e)
        {
            UstawieniaStrona us = new UstawieniaStrona();
            NavigationService.Navigate(us);
        }

        private void Wczytaj(object sender, RoutedEventArgs e)
        {
            wczytano = true;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            OknoGry og = new OknoGry();
            if (wczytano == true)
            {
                og.mapa = WczytIZapis.Load();
            }
            NavigationService.Navigate(og);
        }

        private void Przelacz(object sender, RoutedEventArgs e)
        {
            Process.Start("C:/Users/Piotr/source/repos/Tetris2/ConsoleApp3.exe");
            Application.Current.Shutdown();
        }
    }
}
