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
using System.Windows.Shapes;

namespace KrugovoeShlifovanie
{
    /// <summary>
    /// Логика взаимодействия для Characters.xaml
    /// </summary>
    public partial class Characters : Window
    {
        public Characters()
        {
            InitializeComponent();

            foreach (var elem in _1grid.Children)
            {
                if (elem is Button)
                {
                    (elem as Button).Click += getElem;
                }
            }

            foreach (var elem in _2grid.Children)
            {
                if (elem is Button)
                {
                    (elem as Button).Click += getElem;
                }
            }

            foreach (var elem in _3grid.Children)
            {
                if (elem is Button)
                {
                    (elem as Button).Click += getElem;
                }
            }

        }

        private void getElem(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.chark = (sender as Button).Content.ToString();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            
        }
    }
}
