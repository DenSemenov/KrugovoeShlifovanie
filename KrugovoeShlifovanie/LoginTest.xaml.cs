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
    /// Логика взаимодействия для LoginTest.xaml
    /// </summary>
    public partial class LoginTest : Window
    {
        public SqlConnection constr = new SqlConnection(@"Data Source = .\SQLEXPRESS; Integrated Security = true; Initial Catalog = Shlifovanie");
        public bool hidden = false;
        DataTable data = new DataTable();

        public LoginTest()
        {
            InitializeComponent();

            update();
        }

        private void update()
        {
            data.Reset();
            loginCombo.Items.Clear();

            Button reg = new Button();
            reg.Content = "Регистрация";
            reg.FontSize = 24;
            reg.Height = 42;
            reg.Click += Reg_Click;

            loginCombo.Items.Add(reg);

            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cmd = new SQLiteCommand("select * from users", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(data);
            }

            for (int i = 0; i < data.Rows.Count; i++)
            {
                loginCombo.Items.Add(data.Rows[i][0].ToString());
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            RegisterNewUser rnu = new RegisterNewUser();
            rnu.ShowDialog();

            update();
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

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RegisterNewUser rnu = new RegisterNewUser();
            rnu.ShowDialog();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            bool isLogin = false;
            if (password.Password.Length != 0)
            {
                if (loginCombo.SelectedIndex != -1)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        if ((data.Rows[i][0].ToString() == loginCombo.Text) && (data.Rows[i][1].ToString() == password.Password))
                        {
                            isLogin = true;
                            Properties.Settings.Default.userName = data.Rows[i][0].ToString();
                        }
                    }

                    if (isLogin)
                    {
                        UserPage up = new UserPage();
                        up.Show();
                        hidden = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите логин или зарегистируйтесь");
                }
            }
            else
            {
                MessageBox.Show("Введите пароль");
            }
        }

        private void loginCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loginCombo.SelectedIndex == 0)
            {
                loginCombo.SelectedIndex = -1;
            }
        }
    }
}
