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
    /// Логика взаимодействия для indexZ.xaml
    /// </summary>
    public partial class indexZ : Window
    {
        public indexZ()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.indexZ = "В";
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.indexZ = "П";
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.indexZ = "Н";
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.indexZ = "Д";
            this.Close();
        }
    }
}
