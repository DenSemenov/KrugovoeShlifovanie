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
    /// Логика взаимодействия для strukt.xaml
    /// </summary>
    public partial class strukt : Window
    {
        public strukt()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.strukt = "1-3";
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.strukt = "3-4";
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.strukt = "5-6";
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.strukt = "7-8";
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.strukt = "9-12";
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.strukt = "14-16";
            this.Close();
        }
    }
}
