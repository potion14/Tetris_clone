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
    /// Logika interakcji dla klasy UstawieniaStrona.xaml
    /// </summary>
    public partial class UstawieniaStrona : Page
    {
        public static int predkoscKlockow = 3;

        public UstawieniaStrona()
        {
            InitializeComponent();
            predkosc.Content = predkoscKlockow;
            predkosc.Foreground = Brushes.Green;
        }

        public delegate void dajPredkosc(int predkosKlockow);

        private void ZmianaPoziomu(object sender, RoutedEventArgs e)
        {
            if (predkoscKlockow == 1)
            {
                predkoscKlockow += 1;
                predkosc.Foreground = Brushes.Orange;
            }
            else if (predkoscKlockow == 2)
            {
                predkoscKlockow += 1;
                predkosc.Foreground = Brushes.Green;
            }
            else if (predkoscKlockow == 3)
            {
                predkoscKlockow = 1;
                predkosc.Foreground = Brushes.Red;
            }
            predkosc.Content = predkoscKlockow;
        }

        private void PowrotDoGlownegoOkna(object sender, RoutedEventArgs e)
        {
            OknoGlowne ok = new OknoGlowne();
            NavigationService.Navigate(ok);
        }
    }
}
