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
    /// Логика взаимодействия для RegisterNewUser.xaml
    /// </summary>
    public partial class RegisterNewUser : Window
    {
        public RegisterNewUser()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool isExist = false;
            
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            
            SQLiteCommand cmd = new SQLiteCommand("select name from users", conn);
            SQLiteDataAdapter a = new SQLiteDataAdapter(cmd);
            DataTable data = new DataTable();
            a.Fill(data);

            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (name.Text == data.Rows[i][0].ToString())
                {
                    isExist = true;
                }
            }

            if (isExist == false)
            {
                if (password.Password.Length > 5)
                {
                    SQLiteCommand cmd1 = new SQLiteCommand("insert into users values('" + name.Text + "', '" + password.Password + "')", conn);
                    cmd1.ExecuteNonQuery();
                    conn.Dispose();
                    MessageBox.Show("Пользователь успешно добавлен");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пароль меньше 6-ти символов");
                }
            }
            else
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
            }
        }
    }
}
