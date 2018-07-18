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
    /// Логика взаимодействия для Material.xaml
    /// </summary>
    public partial class Material : Window
    {
        float Cn;
        float r;
        float x;
        float y;
        float q;
        float z;

        public Material()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            g1.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            g1.Visibility = Visibility.Hidden;
        }

        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            g2.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave_1(object sender, MouseEventArgs e)
        {
            g2.Visibility = Visibility.Hidden;
        }

        private void Button_MouseEnter_2(object sender, MouseEventArgs e)
        {
            g3.Visibility = Visibility.Visible;
        }

        private void Button_MouseEnter_3(object sender, MouseEventArgs e)
        {
            g4.Visibility = Visibility.Visible;
        }

        private void Button_MouseEnter_4(object sender, MouseEventArgs e)
        {
            g5.Visibility = Visibility.Visible;
        }

        private void Button_MouseEnter_5(object sender, MouseEventArgs e)
        {
            g6.Visibility = Visibility.Visible;
        }

        private void Button_MouseEnter_6(object sender, MouseEventArgs e)
        {
            g7.Visibility = Visibility.Visible;
        }

        private void Button_MouseEnter_7(object sender, MouseEventArgs e)
        {
            g8.Visibility = Visibility.Visible;
        }

        private void Button_MouseLeave_2(object sender, MouseEventArgs e)
        {
            g3.Visibility = Visibility.Hidden;
        }

        private void Button_MouseLeave_3(object sender, MouseEventArgs e)
        {
            g4.Visibility = Visibility.Hidden;
        }

        private void Button_MouseLeave_4(object sender, MouseEventArgs e)
        {
            g5.Visibility = Visibility.Hidden;
        }

        private void Button_MouseLeave_5(object sender, MouseEventArgs e)
        {
            g6.Visibility = Visibility.Hidden;
        }

        private void Button_MouseLeave_6(object sender, MouseEventArgs e)
        {
            g7.Visibility = Visibility.Hidden;
        }

        private void Button_MouseLeave_7(object sender, MouseEventArgs e)
        {
            g8.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Cn = 1.3f;
            r = 0.75f;
            x = 0.85f;
            y = 0.7f;
            q = 0;
            z = 0;

            closeBut();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cn = 2.2f;
            r = 0.5f;
            x = 0.5f;
            y = 0.55f;
            q = 0;
            z = 0;

            closeBut();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Cn = 2.65f;
            r = 0.5f;
            x = 0.5f;
            y = 0.55f;
            q = 0;
            z = 0;

            closeBut();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Cn = 0.14f;
            r = 0.8f;
            x = 0.8f;
            y = 0;
            q = 0.2f;
            z = 1;

            closeBut();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Cn = 0.27f;
            r = 0.5f;
            x = 0.4f;
            y = 0.4f;
            q = 0.3f;
            z = 0;

            closeBut();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Cn = 0.36f;
            r = 0.35f;
            x = 0.4f;
            y = 0.4f;
            q = 0.3f;
            z = 0;

            closeBut();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Cn = 0.3f;
            r = 0.35f;
            x = 0.4f;
            y = 0.4f;
            q = 0.3f;
            z = 0;

            closeBut();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Cn = 0.81f;
            r = 0.55f;
            x = 1;
            y = 0.7f;
            q = 0.3f;
            z = 0;

            closeBut();
        }

        private void closeBut()
        {
            Properties.Settings.Default.Cn = Cn;
            Properties.Settings.Default.r = r;
            Properties.Settings.Default.x = x;
            Properties.Settings.Default.y = y;
            Properties.Settings.Default.q = q;
            Properties.Settings.Default.z = z;

            Properties.Settings.Default.Cheked = true;

            this.Close();
        }
    }
}
