using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Window
    {
        int answerID = 0;
        DataTable data = new DataTable();
        Button[] bts = new Button[1000];
        String[] variants = new String[1000];
        RadioButton[] rds = new RadioButton[100];
        int[] intt = new int[100];

        public TestPage()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from answers", conn);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            
            ad.Fill(data);
            conn.Dispose();
            int w = 0;
            int h = 0;

            for (int i = 0; i < data.Rows.Count; i++)
            {
                Button b = new Button();
                bts[i] = b;
                b.Content = data.Rows[i][0].ToString();
                b.HorizontalAlignment = HorizontalAlignment.Left;
                b.VerticalAlignment = VerticalAlignment.Top;
                b.Width = 40;
                b.Height = 30;
                b.BorderThickness = new Thickness(2, 2, 2, 2);
                b.Background = Brushes.LightGray;
                b.Foreground = Brushes.Black;
                b.BorderBrush = Brushes.Transparent;
                b.Name = "bb"+i.ToString();
                b.Click += B_Click;
                w = 45 * (i -(int)(i / 3)*3);
                if (w == 135)
                {
                    w = 0;
                }
                h = (int)(i/3);
                b.Margin = new Thickness(w, h*35, 0, 0);

                answersList.Children.Add(b);
            }

            for (int i = 0; i < 100; i++)
            {
                intt[i] = 99;
            }

            updateAnswer();
            updateVariants();

            bts[0].BorderBrush = Brushes.Gray;
        }

        private void B_Click(object sender, RoutedEventArgs e)
        {
            int id = Int32.Parse((sender as Button).Name.Replace("bb",""));
            bts[answerID].BorderBrush = Brushes.Transparent;
            answerID = id;

            bts[answerID].BorderBrush = Brushes.Gray;

            updateAnswer();
            updateVariants();

            if (bts[answerID].Foreground == Brushes.Red)
            {
                image.Source = new BitmapImage(new Uri(@"Resourses/flag-variant-on.png", UriKind.Relative));
            }
            else
            {
                image.Source = new BitmapImage(new Uri(@"Resourses/flag-variant-off.png", UriKind.Relative));
            }
        }

        private void updateAnswer()
        {
            answer.Document.Blocks.Clear();

            answer.AppendText(data.Rows[answerID][1].ToString());


        }

        private void updateVariants()
        {
            variantsGrid.Children.Clear();

            SQLiteConnection conn1 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn1.Open();
            SQLiteCommand cmd1 = new SQLiteCommand("select variant_text from variants where id_answer = '"+(answerID+1).ToString()+"'", conn1);
            SQLiteDataAdapter ad1 = new SQLiteDataAdapter(cmd1);
            DataTable data1 = new DataTable();
            ad1.Fill(data1);
            conn1.Dispose();

            for (int i = 0; i < data1.Rows.Count; i++)
            {
                RadioButton r = new RadioButton();
                rds[i] = r;
                r.Name = "rrrr"+i.ToString();
                r.Content = data1.Rows[i][0].ToString();
                r.Margin = new Thickness(0, 40 * i, 0, 0);
                r.Click += R_Click;
                
                variantsGrid.Children.Add(r);
            }
            if (intt[answerID]!= 99){
                rds[intt[answerID]].IsChecked = true;
            }
        }

        private void R_Click(object sender, RoutedEventArgs e)
        {
            variants[answerID] = (sender as RadioButton).Content.ToString();
            intt[answerID] = Int32.Parse((sender as RadioButton).Name.Replace("rrrr",""));
            SolidColorBrush cr = new SolidColorBrush();
            cr.Color = Color.FromRgb(0, 150, 136);
            bts[answerID].Background = cr;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bts[answerID].Foreground == Brushes.Red)
            {
                bts[answerID].Foreground = Brushes.Black;
                image.Source = new BitmapImage(new Uri(@"Resourses/flag-variant-off.png", UriKind.Relative));
            }
            else
            {
                bts[answerID].Foreground = Brushes.Red;
                image.Source = new BitmapImage(new Uri(@"Resourses/flag-variant-on.png", UriKind.Relative));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            double oneproc = (double)100 / data.Rows.Count;
            double result = 0;

            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("select max(id) from resTest", conn);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            DataTable data2 = new DataTable();
            ad.Fill(data2);
            conn.Dispose();
            int id;
            if (data2.Rows[0][0].ToString() != "")
            {
                id = Int32.Parse(data2.Rows[0][0].ToString()) + 1;
            }
            else
            {
                id = 1;
            }
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int rightV = 0; 
                if (data.Rows[i][2].ToString() == variants[i])
                {
                    result = result + oneproc;
                    rightV = 1;
                }

                SQLiteConnection conn1 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn1.Open();
                SQLiteCommand cmd1 = new SQLiteCommand("insert into resTest values('"+id+"', '"+Properties.Settings.Default.userName + "', '"+i.ToString()+"', '"+ variants[i] + "', '"+ rightV + "')", conn1);
                SQLiteDataAdapter ad1 = new SQLiteDataAdapter(cmd1);
                DataTable data1 = new DataTable();
                ad1.Fill(data1);
                conn1.Dispose();
            }


            SQLiteConnection conn2 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn2.Open();
            SQLiteCommand cmd2 = new SQLiteCommand("select count(id)+1 from result", conn2);
            SQLiteDataAdapter ad2 = new SQLiteDataAdapter(cmd2);
            DataTable data3 = new DataTable();
            ad2.Fill(data3);
            conn2.Dispose();

            SQLiteConnection conn4 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn4.Open();
            SQLiteCommand cmd4 = new SQLiteCommand("insert into result values('" + data3.Rows[0][0].ToString() +"', '"+ DateTime.Now.Date.ToString().Replace("0:00:00", "") + "', '"+ result.ToString().Replace(",",".") + "', '"+ Properties.Settings.Default.userName + "')", conn4);
            SQLiteDataAdapter ad4 = new SQLiteDataAdapter(cmd4);
            DataTable data4 = new DataTable();
            ad4.Fill(data4);
            conn4.Dispose();

            this.Close();
        }
    }
}
