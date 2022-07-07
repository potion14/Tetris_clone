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
    /// Logika interakcji dla klasy Instrukcja.xaml
    /// </summary>
    public partial class Instrukcja : Page
    {
        public Instrukcja()
        {
            InitializeComponent();
        }

        private void Powrot(object sender, RoutedEventArgs e)
        {
            OknoGlowne ok = new OknoGlowne();
            NavigationService.Navigate(ok);
        }
    }
}
