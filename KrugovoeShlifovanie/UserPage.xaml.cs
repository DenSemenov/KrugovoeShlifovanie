using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
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
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        public SqlConnection constr = new SqlConnection(@"Data Source = .\SQLEXPRESS; Integrated Security = true; Initial Catalog = Shlifovanie");
        public Button[] resultsArray = new Button[100];
        bool isHidden = false;

        public UserPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            DataTable data = new DataTable();
            SQLiteCommand cmd = new SQLiteCommand("select * from result where userName = '" + Properties.Settings.Default.userName + "' order by id desc", conn);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            ad.Fill(data);
            conn.Dispose();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                Grid g = new Grid();
                g.Width = 583;
                g.Height = 45;
                g.VerticalAlignment = VerticalAlignment.Top;
                g.Margin = new Thickness(0, i * 45, 0, 0);

                Label dateL = new Label();
                dateL.Content = data.Rows[i][1].ToString().Replace("0:00:00", "");
                dateL.Width = 300;
                dateL.HorizontalAlignment = HorizontalAlignment.Left;
                dateL.BorderBrush = Brushes.Black;

                Label resultL = new Label();
                resultL.Content = data.Rows[i][2].ToString();
                resultL.Width = 133;
                resultL.HorizontalAlignment = HorizontalAlignment.Left;
                resultL.BorderBrush = Brushes.Black;
                resultL.Margin = new Thickness(300, 0, 0, 0);

                Button resB = new Button();
                resB.Content = "Результат";
                resB.FontSize = 24;
                resB.Width = 145;
                resB.Height = 41;

                Color cr = new Color();
                cr = Color.FromRgb(245, 245, 245);

                resB.Background = new SolidColorBrush(cr);
                resB.BorderBrush = new SolidColorBrush(cr);
                resB.Foreground = Brushes.DimGray;
                resB.Margin = new Thickness(433, 0, 0, 0);
                resB.Name = "Bt" + data.Rows[i][0].ToString();
                resB.Click += ResB_Click;
                resultsArray[i] = resB;

                g.Children.Add(dateL);
                g.Children.Add(resultL);
                g.Children.Add(resB);

                mainGrid.Children.Add(g);
            }
        }

        private void ResB_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.testID = (sender as Button).Name.Replace("Bt","");
            Results r = new Results();
            r.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isHidden == false)
            {
                Environment.Exit(0);
            }
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            TestPage tp = new TestPage();
            tp.ShowDialog();

            mainGrid.Children.Clear();
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginTest lt = new LoginTest();
            lt.Show();
            isHidden = true;
            this.Close();
        }
    }
}
