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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KrugovoeShlifovanie
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SqlConnection constr = new SqlConnection(@"Data Source = .\SQLEXPRESS; Integrated Security = true; Initial Catalog = Shlifovanie");
        
        public bool hidden = false;
        Label[] forInfoLabels = new Label[100];
        Button[] forInfoButtons = new Button[100];
        Grid[] forInfoGrids = new Grid[100];

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (speedK.Text.Length != 0 && 
                speedZ.Text.Length != 0 && 
                glubinaT.Text.Length != 0 && 
                prodolPodachaS.Text.Length != 0 && 
                diam.Text.Length != 0 && 
                check.IsChecked == true && 
                stanokComboBox.SelectedIndex != -1 && 
                dlina.Text.Length!=0 && 
                charkText.Text.Length!=0 && 
                indexZText.Text.Length!=0 && 
                struktText.Text.Length!=0)
            {
                OK.IsEnabled = true;
            }
            else
            {
                OK.IsEnabled = false;
            }
        }

        private void AddStal_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddStanok_Click(object sender, RoutedEventArgs e)
        {
            if (typeComboBox.SelectedIndex == 0)
            {
                AddStanokForm asf = new AddStanokForm();
                asf.ShowDialog();
                updateStanokVnut();
                
            }
            else
            {
                AddStanokForm2 asf2 = new AddStanokForm2();
                asf2.ShowDialog();
                updateStanokVnesh();
                
            }
        }

        private void AddKrug_Click(object sender, RoutedEventArgs e)
        {
        }

        private void updateStanokVnut()
        {
            stanokComboBox.Items.Clear();

            Button addStanok = new Button();
            addStanok.Content = "Добавить станок";
            addStanok.FontSize = 24;
            addStanok.Height = 44;
            addStanok.Click += AddStanok_Click;
            stanokComboBox.Items.Add(addStanok);
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            DataTable data = new DataTable();
            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cmd = new SQLiteCommand("select name from vnutrenniyStanok", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(data);
            }
            conn.Dispose();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                stanokComboBox.Items.Add(data.Rows[i][0].ToString());
            }
        }

        private void updateStanokVnesh()
        {
            stanokComboBox.Items.Clear();

            Button addStanok = new Button();
            addStanok.Content = "Добавить станок";
            addStanok.FontSize = 24;
            addStanok.Height = 44;
            addStanok.Click += AddStanok_Click;
            stanokComboBox.Items.Add(addStanok);

            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            DataTable data = new DataTable();
            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cmd = new SQLiteCommand("select name from naruzhniyStanok", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(data);
            }
            conn.Dispose();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                stanokComboBox.Items.Add(data.Rows[i][0].ToString());
            }
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void stanokComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (stanokComboBox.SelectedIndex == 0)
            {
                stanokComboBox.SelectedIndex = -1;
            }

            if (stanokComboBox.SelectedIndex > 0)
            {
                if (typeComboBox.SelectedIndex == 0)
                {
                    SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                    conn.Open();
                    DataTable data = new DataTable();
                    SQLiteCommand cmd = new SQLiteCommand("select naibDiametr, naibDlina from vnutrenniyStanok where name = '" + stanokComboBox.SelectedValue + "'", conn);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(data);
                    conn.Dispose();

                    diamInt.Content = "<" + data.Rows[0][0].ToString();
                    dlinaInt.Content = "<" + data.Rows[0][1].ToString();
                }
                else
                {
                    SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                    conn.Open();
                    DataTable data = new DataTable();
                    SQLiteCommand cmd = new SQLiteCommand("select naibDiam, naibDlina from naruzhniyStanok where name = '" + stanokComboBox.SelectedValue + "'", conn);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(data);
                    conn.Dispose();

                    diamInt.Content = "<" + data.Rows[0][0].ToString();
                    dlinaInt.Content = "<" + data.Rows[0][1].ToString();
                }
            }
            
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable d = new DataTable();
            if (typeComboBox.SelectedIndex != -1)
            {
                stanokComboBox.IsEnabled = true;
                
                charachter.IsEnabled = true;

            }
            else
            {
                stanokComboBox.IsEnabled = false;
                charachter.IsEnabled = false;
            }

            if (typeComboBox.SelectedIndex == 0)
            {
                updateStanokVnut();
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand cmd = new SQLiteCommand("select character from rezhimi where type = 'Внутреннее'", conn);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(d);
                }
                conn.Dispose();
            }
            else
            {
                updateStanokVnesh();
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand cmd = new SQLiteCommand("select character from rezhimi where type = 'Наружное'", conn);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(d);
                }
                conn.Dispose();
            }

            charachter.Items.Clear();

            for (int i = 0; i < d.Rows.Count; i++)
            {
                charachter.Items.Add(d.Rows[i][0].ToString());
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            StartPage sp = new StartPage();
            sp.Show();
            hidden = true;
            this.Close();
        }
        

        private void Window_Closed(object sender, EventArgs e)
        {
            if (hidden == false)
            {
                Environment.Exit(0);
            }
        }

        private void stalComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void charachter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            speedK.Clear();
            speedZ.Clear();
            glubinaT.Clear();
            prodolPodachaS.Clear();

            string type = null;

            if (typeComboBox.SelectedIndex == 0)
            {
                type = "Внутреннее";
            }
            else if (typeComboBox.SelectedIndex == 1)
            {
                type = "Наружное";
            }

            if (charachter.SelectedIndex != -1)
            {
                
                CheckGrid.Visibility = Visibility.Visible;
                zagotovkaGrid.Visibility = Visibility.Visible;
                lastGrid.Visibility = Visibility.Visible;

                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn.Open();
                DataTable d = new DataTable();
                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand cmd = new SQLiteCommand("select * from rezhimi where type = '" + type + "' and character = '" + charachter.SelectedValue + "'", conn);
                    SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                    ad.Fill(d);
                }
                conn.Dispose();

                intVK.Content = d.Rows[0][2].ToString();
                intVZ.Content = d.Rows[0][3].ToString();
                intT.Content = d.Rows[0][4].ToString();
                intS.Content = d.Rows[0][5].ToString();
            }
            else
            {
                CheckGrid.Visibility = Visibility.Hidden;
                zagotovkaGrid.Visibility = Visibility.Hidden;
                lastGrid.Visibility = Visibility.Hidden;
            }

            if (intS.Content.ToString() == "-")
            {
                prodolPodachaS.IsEnabled = false;
            }
            else
            {
                prodolPodachaS.IsEnabled = true;
            }

            if (intT.Content.ToString() == "-")
            {
                glubinaT.IsEnabled = false;
            }
            else
            {
                glubinaT.IsEnabled = true;
            }
        }

        private void speedK_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void speedZ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void glubinaT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void prodolPodachaS_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.otchetType = typeComboBox.Text;
            Properties.Settings.Default.otchetStanok = stanokComboBox.Text;
            Properties.Settings.Default.otchetCharakter = charachter.Text;
            Properties.Settings.Default.otchetSkorostK = speedK.Text;
            Properties.Settings.Default.otchetSkorostZ = speedZ.Text;
            Properties.Settings.Default.otchetGlubina = glubinaT.Text;
            Properties.Settings.Default.otchetProdol = prodolPodachaS.Text;
            Properties.Settings.Default.otchetDiam = diam.Text;
            Properties.Settings.Default.otchetDlina = dlina.Text;
            Properties.Settings.Default.otchetPripusk = pripusk.Text;
            Properties.Settings.Default.otchetCharK = charkText.Text;
            Properties.Settings.Default.otchetIndex = indexZText.Text;
            Properties.Settings.Default.otchetStruktura = struktText.Text;

            if (typeComboBox.SelectedIndex == 1)
            {
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn.Open();
                DataTable d = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand("select * from naruzhniyStanok where name = '" + stanokComboBox.Text + "'", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(d);
                conn.Dispose();

                float Bk = float.Parse(d.Rows[0][6].ToString().Replace(".", ",")); // ширина круга
                double S = float.Parse(prodolPodachaS.Text.Replace(".", ",")) * Bk; //продольная подача
                float Dk = float.Parse(d.Rows[0][5].ToString().Replace(".", ",")); //диаметр круга
                float nz = Math.Abs((1000 * float.Parse(speedZ.Text.Replace(".", ","))) / (float.Parse("3,14") * float.Parse(diam.Text))); //частота вращения заготовки
                float nk = (1000 * float.Parse(speedK.Text.Replace(".", ",")) * 60) / (float.Parse("3,14") * Dk); //частота вращения заготовки
                float nzRdy = 0; //частота вращения заготовки с учетом станка
                float nkRdy = 0; //частота вращения круга с учетом станка
                float Vkd = 0; //Vкд
                double Vst = 0; //скорость движения стола
                float VstRdy = 0; //скорость движения стола с учетом станка

                if (d.Rows[0][7].ToString().Contains(';'))
                {
                    String[] substrings = d.Rows[0][7].ToString().Split(';');

                    float raznica = Math.Abs(float.Parse(substrings[0]) - nz);
                    int k = 0;

                    for (int i = 1; i < substrings.Length; i++)
                    {
                        if (raznica > Math.Abs(float.Parse(substrings[i]) - nz))
                        {
                            raznica = Math.Abs(float.Parse(substrings[i]) - nz);
                            k = i;
                        }
                    }

                    nzRdy = float.Parse(substrings[k]);

                }
                else if (d.Rows[0][7].ToString().Contains('-'))
                {
                    String[] substrings = d.Rows[0][7].ToString().Split('-');
                    if (nz >= float.Parse(substrings[0]) && nz <= float.Parse(substrings[1]))
                    {
                        nz = Math.Abs(nz);
                    }
                    else if (nz < float.Parse(substrings[0]))
                    {
                        nz = float.Parse(substrings[0]);
                    }
                    else if (nz > float.Parse(substrings[1]))
                    {
                        nz = float.Parse(substrings[1]);
                    }

                    nzRdy = nz;
                }

                if (d.Rows[0][8].ToString().Contains(';'))
                {
                    String[] substrings = d.Rows[0][8].ToString().Split(';');

                    float raznica = Math.Abs(float.Parse(substrings[0]) - nk);
                    int k = 0;

                    for (int i = 1; i < substrings.Length; i++)
                    {
                        if (raznica > Math.Abs(float.Parse(substrings[i]) - nk))
                        {
                            raznica = Math.Abs(float.Parse(substrings[i]) - nk);
                            k = i;
                        }
                    }

                    nkRdy = float.Parse(substrings[k]);

                }
                else if (d.Rows[0][8].ToString().Contains('-'))
                {
                    String[] substrings = d.Rows[0][8].ToString().Split('-');
                    if (nk >= float.Parse(substrings[0]) && nk <= float.Parse(substrings[1]))
                    {
                        nk = Math.Abs(nk);
                    }
                    else if (nk < float.Parse(substrings[0]))
                    {
                        nk = float.Parse(substrings[0]);
                    }
                    else if (nk > float.Parse(substrings[1]))
                    {
                        nk = float.Parse(substrings[1]);
                    }

                    nkRdy = nk;
                }

                Vkd = (float.Parse("3,14") * float.Parse(d.Rows[0][5].ToString().Replace(".", ",")) * nzRdy) / (1000 * 60);
                if (S - Math.Truncate(S) >= 0.5)
                {
                    S = Math.Truncate(S) + 1;
                }
                else
                {
                    S = Math.Truncate(S);
                }

                Vst = S * Math.Truncate(nzRdy);

                if (d.Rows[0][9].ToString().Contains(';'))
                {
                    String[] substrings = d.Rows[0][9].ToString().Split(';');

                    float raznica = Math.Abs(float.Parse(substrings[0]) - float.Parse(Vst.ToString()));
                    int k = 0;

                    for (int i = 1; i < substrings.Length; i++)
                    {
                        if (raznica > Math.Abs(float.Parse(substrings[i]) - Vst))
                        {
                            raznica = Math.Abs(float.Parse(substrings[i]) - float.Parse(Vst.ToString()));
                            k = i;
                        }
                    }

                    VstRdy = float.Parse(substrings[k]);

                }
                else if (d.Rows[0][9].ToString().Contains('-'))
                {
                    String[] substrings = d.Rows[0][9].ToString().Split('-');
                    if (Vst >= float.Parse(substrings[0].Replace(".", ",")) && Vst <= float.Parse(substrings[1].Replace(".", ",")))
                    {
                        Vst = Math.Abs(Vst);
                    }
                    else if (nz < float.Parse(substrings[0].Replace(".", ",")))
                    {
                        Vst = float.Parse(substrings[0].Replace(".", ","));
                    }
                    else if (Vst > float.Parse(substrings[1].Replace(".", ",")))
                    {
                        Vst = float.Parse(substrings[1].Replace(".", ","));
                    }

                    VstRdy = float.Parse(Vst.ToString());
                }
                float f = Properties.Settings.Default.r;
                decimal decimalValue = System.Convert.ToDecimal(f);
                double doubleValue = System.Convert.ToDouble(decimalValue);

                float f2 = Properties.Settings.Default.x;
                decimal decimalValue2 = System.Convert.ToDecimal(f2);
                double doubleValue2 = System.Convert.ToDouble(decimalValue2);

                float f3 = Properties.Settings.Default.y;
                decimal decimalValue3 = System.Convert.ToDecimal(f3);
                double doubleValue3 = System.Convert.ToDouble(decimalValue3);

                float f4 = Properties.Settings.Default.q;
                decimal decimalValue4 = System.Convert.ToDecimal(f4);
                double doubleValue4 = System.Convert.ToDouble(decimalValue4);

                float Nrez = Properties.Settings.Default.Cn * float.Parse((Math.Pow(double.Parse(speedZ.Text.Replace(".", ",")), doubleValue)).ToString()) * float.Parse((Math.Pow(double.Parse(glubinaT.Text.Replace(".", ",")), doubleValue2)).ToString()) * float.Parse(Math.Pow(S, doubleValue3).ToString()) * float.Parse((Math.Pow(double.Parse(diam.Text.Replace(".", ",")), doubleValue4).ToString()));

                float Nshp = float.Parse(d.Rows[0][3].ToString()) * float.Parse(d.Rows[0][4].ToString());

                float Lpx1 = float.Parse(dlina.Text) + 0.2f * Bk;
                float Lpx2 = float.Parse(dlina.Text) + 0.4f * Bk;
                float kk = 0;
                if (charachter.SelectedIndex == 0 || charachter.SelectedIndex == 3)
                {
                    kk = 1.1f;
                }
                else
                {
                    kk = 1.4f;
                }
                float Tm1 = 0;
                float Tm2 = 0;

                if (charachter.SelectedIndex == 0 || charachter.SelectedIndex == 1) {
                    Tm1 = (Lpx1 * float.Parse(pripusk.Text.Replace(".", ",")) * kk) / (nzRdy * (float)S * float.Parse(glubinaT.Text.Replace(".", ",")));
                    Tm2 = (Lpx2 * float.Parse(pripusk.Text.Replace(".", ",")) * kk) / (nzRdy * (float)S * float.Parse(glubinaT.Text.Replace(".", ",")));
                }

                if (charachter.SelectedIndex == 3 || charachter.SelectedIndex == 4)
                {
                    Tm1 = ((Lpx1 * float.Parse(pripusk.Text.Replace(".", ","))) / ((float)S * nzRdy * float.Parse(glubinaT.Text.Replace(".", ",")))) * kk;
                    Tm2 = ((Lpx2 * float.Parse(pripusk.Text.Replace(".", ","))) / ((float)S * nzRdy * float.Parse(glubinaT.Text.Replace(".", ",")))) * kk;
                }

                Properties.Settings.Default.Nrez = Math.Round(Nrez, 1);
                Properties.Settings.Default.Nshp = Math.Round(Nshp, 1);
                Properties.Settings.Default.ProdolPodach = Math.Truncate(S).ToString();
                Properties.Settings.Default.ChastotaZ = Math.Truncate(nzRdy).ToString();
                Properties.Settings.Default.ChastotaK = Math.Truncate(nkRdy).ToString();
                Properties.Settings.Default.SkorostStola = Math.Truncate(VstRdy).ToString();
                Properties.Settings.Default.Tm1 = Math.Round(Tm1, 1);
                Properties.Settings.Default.Tm2 = Math.Round(Tm2, 1);
                Properties.Settings.Default.charkText = charkText.Text;
                Properties.Settings.Default.indexZText = indexZText.Text;
                Properties.Settings.Default.struktText = struktText.Text;

                RezhimiNaruzh rn = new RezhimiNaruzh();
                rn.ShowDialog();

            }
            else
            {
                SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn.Open();
                DataTable d = new DataTable();
                SQLiteCommand cmd = new SQLiteCommand("select * from vnutrenniyStanok where name = '" + stanokComboBox.Text + "'", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(d);
                conn.Dispose();

                float Dk = 0.9f * float.Parse(diam.Text.Replace(".", ",")); //диаметр круга
                float Bk = float.Parse(diam.Text.Replace(".", ",")) - 15; //ширина круга

                SQLiteConnection conn1 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
                conn1.Open();
                DataTable d1 = new DataTable();
                SQLiteCommand cmd1 = new SQLiteCommand("select do from diams where Dk = '" + Dk.ToString() + "'", conn1);
                SQLiteDataAdapter ad1 = new SQLiteDataAdapter(cmd1);
                ad1.Fill(d1);
                conn1.Dispose();

                //int Do = Int32.Parse(d1.Rows[0][0].ToString()); //диаметр отверстия
                double S = float.Parse(prodolPodachaS.Text.Replace(".", ",")) * Bk; //продольная подача
                float Sp;

                String[] substrings = d.Rows[0][11].ToString().Replace(".", ",").Split(';');

                float raznica = Math.Abs(float.Parse(substrings[0]) - float.Parse(glubinaT.Text.Replace(".", ",")));
                int k = 0;

                for (int i = 1; i < substrings.Length; i++)
                {
                    if (raznica > Math.Abs(float.Parse(substrings[i]) - float.Parse(glubinaT.Text.Replace(".", ","))))
                    {
                        raznica = Math.Abs(float.Parse(substrings[i]) - float.Parse(glubinaT.Text.Replace(".", ",")));
                        k = i;
                    }
                }

                Sp = float.Parse(substrings[k]);

                float nk = (1000 * float.Parse(speedK.Text.Replace(".", ",")) * 60) / (float.Parse("3,14") * Dk); //частота вращения заготовки
                float NkRdy = 0;

                if (d.Rows[0][9].ToString().Contains(';'))
                {
                    String[] substrings1 = d.Rows[0][9].ToString().Split(';');

                    float raznica1 = Math.Abs(float.Parse(substrings1[0]) - float.Parse(nk.ToString()));
                    int k1 = 0;

                    for (int i = 1; i < substrings1.Length; i++)
                    {
                        if (raznica > Math.Abs(float.Parse(substrings1[i]) - nk))
                        {
                            raznica = Math.Abs(float.Parse(substrings1[i]) - float.Parse(nk.ToString()));
                            k1 = i;
                        }
                    }

                    NkRdy = float.Parse(substrings1[k1]);

                }
                else if (d.Rows[0][9].ToString().Contains('-'))
                {
                    String[] substrings1 = d.Rows[0][9].ToString().Split('-');
                    if (nk >= float.Parse(substrings1[0].Replace(".", ",")) && nk <= float.Parse(substrings1[1].Replace(".", ",")))
                    {
                        nk = Math.Abs(nk);
                    }
                    else if (nk < float.Parse(substrings1[0].Replace(".", ",")))
                    {
                        nk = float.Parse(substrings1[0].Replace(".", ","));
                    }
                    else if (nk > float.Parse(substrings1[1].Replace(".", ",")))
                    {
                        nk = float.Parse(substrings1[1].Replace(".", ","));
                    }

                    NkRdy = float.Parse(nk.ToString());
                }

                float nz = (1000 * float.Parse(speedZ.Text.Replace(".", ","))) / ((float)Math.PI * float.Parse(diam.Text.Replace(".", ",")));
                float NzRdy = 0;
                if (d.Rows[0][8].ToString().Contains(';'))
                {
                    String[] substrings1 = d.Rows[0][8].ToString().Split(';');

                    float raznica1 = Math.Abs(float.Parse(substrings1[0]) - float.Parse(nz.ToString()));
                    int k1 = 0;

                    for (int i = 1; i < substrings1.Length; i++)
                    {
                        if (raznica > Math.Abs(float.Parse(substrings1[i]) - nz))
                        {
                            raznica = Math.Abs(float.Parse(substrings1[i]) - float.Parse(nz.ToString()));
                            k1 = i;
                        }
                    }

                    NzRdy = float.Parse(substrings1[k1]);

                }
                else if (d.Rows[0][8].ToString().Contains('-'))
                {
                    String[] substrings1 = d.Rows[0][8].ToString().Split('-');
                    if (nz >= float.Parse(substrings1[0].Replace(".", ",")) && nz <= float.Parse(substrings1[1].Replace(".", ",")))
                    {
                        nz = Math.Abs(nz);
                    }
                    else if (nz < float.Parse(substrings1[0].Replace(".", ",")))
                    {
                        nz = float.Parse(substrings1[0].Replace(".", ","));
                    }
                    else if (nz > float.Parse(substrings1[1].Replace(".", ",")))
                    {
                        nz = float.Parse(substrings1[1].Replace(".", ","));
                    }

                    NzRdy = float.Parse(nz.ToString());
                }

                float Vkd = ((float)Math.PI * Dk * NkRdy) / (1000 * 60);
                float Vst = ((float)S * NzRdy) / 1000;

                float f = Properties.Settings.Default.r;
                decimal decimalValue = System.Convert.ToDecimal(f);
                double doubleValue = System.Convert.ToDouble(decimalValue);

                float f2 = Properties.Settings.Default.x;
                decimal decimalValue2 = System.Convert.ToDecimal(f2);
                double doubleValue2 = System.Convert.ToDouble(decimalValue2);

                float f3 = Properties.Settings.Default.y;
                decimal decimalValue3 = System.Convert.ToDecimal(f3);
                double doubleValue3 = System.Convert.ToDouble(decimalValue3);

                float f4 = Properties.Settings.Default.q;
                decimal decimalValue4 = System.Convert.ToDecimal(f4);
                double doubleValue4 = System.Convert.ToDouble(decimalValue4);

                float Nrez = Properties.Settings.Default.Cn * float.Parse((Math.Pow(double.Parse(speedZ.Text.Replace(".", ",")), doubleValue)).ToString()) * float.Parse((Math.Pow(double.Parse(glubinaT.Text.Replace(".", ",")), doubleValue2)).ToString()) * float.Parse(Math.Pow(S, doubleValue3).ToString()) * float.Parse((Math.Pow(double.Parse(diam.Text.Replace(".", ",")), doubleValue4).ToString()));

                float Nshp = float.Parse(d.Rows[0][4].ToString()) * float.Parse(d.Rows[0][5].ToString());

                float Lpx1 = float.Parse(dlina.Text) + 0.2f * Bk;
                float Lpx2 = float.Parse(dlina.Text) + 0.4f * Bk;
                float kk = 0;
                if (charachter.SelectedIndex == 0 || charachter.SelectedIndex == 3)
                {
                    kk = 1.1f;
                }
                else
                {
                    kk = 1.4f;
                }
                float Tm1 = 0;
                float Tm2 = 0;

                Tm1 = (2 * Lpx1 * float.Parse(pripusk.Text.Replace(".", ",")) * kk) / (NzRdy * (float)S * float.Parse(glubinaT.Text.Replace(".", ",")));
                Tm2 = (2 * Lpx2 * float.Parse(pripusk.Text.Replace(".", ",")) * kk) / (NzRdy * (float)S * float.Parse(glubinaT.Text.Replace(".", ",")));


                Properties.Settings.Default.Nrez = Math.Round(Nrez, 1);
                Properties.Settings.Default.Nshp = Math.Round(Nshp, 1);
                Properties.Settings.Default.ProdolPodach = Math.Truncate(S).ToString();
                Properties.Settings.Default.ChastotaZ = Math.Truncate(NzRdy).ToString();
                Properties.Settings.Default.ChastotaK = Math.Truncate(NkRdy).ToString();
                Properties.Settings.Default.SkorostStola = Math.Round(Vst, 1).ToString();
                Properties.Settings.Default.Tm1 = Math.Round(Tm1, 1);
                Properties.Settings.Default.Tm2 = Math.Round(Tm2, 1);
                Properties.Settings.Default.charkText = charkText.Text;
                Properties.Settings.Default.indexZText = indexZText.Text;
                Properties.Settings.Default.struktText = struktText.Text;

                RezhimiNaruzh rn = new RezhimiNaruzh();
                rn.ShowDialog();
            }

            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Characters c = new Characters();
            c.ShowDialog();

            charkText.Text = Properties.Settings.Default.chark;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            indexZ iz = new indexZ();
            iz.ShowDialog();

            indexZText.Text = Properties.Settings.Default.indexZ;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            strukt s = new strukt();
            s.ShowDialog();

            struktLabel.Content = Properties.Settings.Default.strukt;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Material m = new Material();
            m.ShowDialog();

            if (Properties.Settings.Default.Cheked)
            {
                check.IsChecked = true;
            }
        }

        private void speedK_LostFocus(object sender, RoutedEventArgs e)
        {
            if (speedK.Text.Length != 0)
            {
                String[] substrings = intVK.Content.ToString().Split('-');
                float min = float.Parse(substrings[0].Replace(".", ","));
                float max = float.Parse(substrings[1].Replace(".", ","));

                if (float.Parse(speedK.Text.Replace(".", ",")) < min || float.Parse(speedK.Text.Replace(".", ",")) > max)
                {
                    speedK.BorderBrush = Brushes.Red;
                }
                else
                {
                    SolidColorBrush cr = new SolidColorBrush();
                    cr.Color = Color.FromRgb(0, 150, 136);
                    speedK.BorderBrush = cr;
                }
            }
        }

        private void speedK_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void speedZ_LostFocus(object sender, RoutedEventArgs e)
        {
            if (speedZ.Text.Length != 0)
            {
                String[] substrings = intVZ.Content.ToString().Split('-');
                float min = float.Parse(substrings[0].Replace(".", ","));
                float max = float.Parse(substrings[1].Replace(".", ","));

                if (float.Parse(speedZ.Text.Replace(".", ",")) < min || float.Parse(speedZ.Text.Replace(".", ",")) > max)
                {
                    speedZ.BorderBrush = Brushes.Red;
                }
                else
                {
                    SolidColorBrush cr = new SolidColorBrush();
                    cr.Color = Color.FromRgb(0, 150, 136);
                    speedZ.BorderBrush = cr;
                }
            }
        }

        private void glubinaT_LostFocus(object sender, RoutedEventArgs e)
        {
            if (glubinaT.Text.Length != 0 && intT.Content.ToString() != "-")
            {
                String[] substrings = intT.Content.ToString().Split('-');
                float min = float.Parse(substrings[0].Replace(".",","));
                float max = float.Parse(substrings[1].Replace(".", ","));

                if (float.Parse(glubinaT.Text.Replace(".", ",")) < min || float.Parse(glubinaT.Text.Replace(".", ",")) > max)
                {
                    glubinaT.BorderBrush = Brushes.Red;
                }
                else
                {
                    SolidColorBrush cr = new SolidColorBrush();
                    cr.Color = Color.FromRgb(0, 150, 136);
                    glubinaT.BorderBrush = cr;
                }
            }
        }

        private void prodolPodachaS_LostFocus(object sender, RoutedEventArgs e)
        {
            if (prodolPodachaS.Text.Length != 0 && intS.Content.ToString() != "-")
            {
                String[] substrings = intS.Content.ToString().Split('-');
                float min = float.Parse(substrings[0].Replace(".", ","));
                float max = float.Parse(substrings[1].Replace(".", ","));

                if (float.Parse(prodolPodachaS.Text.Replace(".", ",")) < min || float.Parse(prodolPodachaS.Text.Replace(".", ",")) > max)
                {
                    prodolPodachaS.BorderBrush = Brushes.Red;
                }
                else
                {
                    SolidColorBrush cr = new SolidColorBrush();
                    cr.Color = Color.FromRgb(0, 150, 136);
                    prodolPodachaS.BorderBrush = cr;
                }
            }
        }

        private void intVK_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            String[] substrings = intVK.Content.ToString().Split('-');
            var windowPosition = Mouse.GetPosition(this);
            if (windowPosition.X > (this.ActualWidth-CheckGrid.Width)/2+CheckGrid.Width - intVK.ActualWidth-10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intVK.ActualWidth / 2-10)
            {
                speedK.Text = substrings[0];
            }
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intVK.ActualWidth / 2-10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width)
            {
                speedK.Text = substrings[1];
            }
        }

        private void intVZ_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            String[] substrings = intVZ.Content.ToString().Split('-');
            var windowPosition = Mouse.GetPosition(this);
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intVZ.ActualWidth - 10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intVZ.ActualWidth / 2 - 10)
            {
                speedZ.Text = substrings[0];
            }
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intVZ.ActualWidth / 2 - 10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width)
            {
                speedZ.Text = substrings[1];
            }
        }

        private void intT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            String[] substrings = intT.Content.ToString().Split('-');
            var windowPosition = Mouse.GetPosition(this);
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intT.ActualWidth - 10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intT.ActualWidth / 2 - 10)
            {
                glubinaT.Text = substrings[0];
            }
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intT.ActualWidth / 2 - 10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width)
            {
                glubinaT.Text = substrings[1];
            }
        }

        private void intS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            String[] substrings = intS.Content.ToString().Split('-');
            var windowPosition = Mouse.GetPosition(this);
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intS.ActualWidth - 10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intS.ActualWidth / 2 - 10)
            {
                prodolPodachaS.Text = substrings[0];
            }
            if (windowPosition.X > (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width - intS.ActualWidth / 2 - 10 && windowPosition.X < (this.ActualWidth - CheckGrid.Width) / 2 + CheckGrid.Width)
            {
                prodolPodachaS.Text = substrings[1];
            }
        }

        private void diam_LostFocus(object sender, RoutedEventArgs e)
        {
            if (diam.Text.Length != 0 && float.Parse(diamInt.Content.ToString().Replace("<", "")) <= float.Parse(diam.Text.Replace(".", ",")))
            {
                diam.BorderBrush = Brushes.Red;
            }
            else
            {
                SolidColorBrush cr = new SolidColorBrush();
                cr.Color = Color.FromRgb(0, 150, 136);
                diam.BorderBrush = cr;
            }
        }

        private void pripusk_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pripusk_LostFocus(object sender, RoutedEventArgs e)
        {
            if (pripusk.Text.Length != 0 && prInt.Content.ToString() != "-")
            {
                String[] substrings = prInt.Content.ToString().Split('-');
                float min = float.Parse(substrings[0].Replace(".", ","));
                float max = float.Parse(substrings[1].Replace(".", ","));

                if (float.Parse(pripusk.Text.Replace(".", ",")) < min || float.Parse(pripusk.Text.Replace(".", ",")) > max)
                {
                    pripusk.BorderBrush = Brushes.Red;
                }
                else
                {
                    SolidColorBrush cr = new SolidColorBrush();
                    cr.Color = Color.FromRgb(0, 150, 136);
                    pripusk.BorderBrush = cr;
                }
            }
        }

        private void diam_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void dlina_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void pripusk_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }

        private void struktText_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));

        }
    }
}
