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
    /// Логика взаимодействия для AddStanokForm2.xaml
    /// </summary>
    public partial class AddStanokForm2 : Window
    {
        public SqlConnection constr = new SqlConnection(@"Data Source = .\SQLEXPRESS; Integrated Security = true; Initial Catalog = Shlifovanie");
        public AddStanokForm2()
        {
            InitializeComponent();
        }

        private void naibDiametr_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));
        }

        private void naibDlina_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));
        }

        private void moshnostN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));
        }

        private void KPD_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));
        }

        private void Diam_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));
        }

        private void Shirina_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0) && !((thisTextBox.Text.IndexOf("-") == 0) && thisTextBox.SelectionStart == 0))) &&
               ((e.Text.Substring(0, 1) != ".") || (thisTextBox.Text.IndexOf(".") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(",") != -1))) &&
               ((e.Text.Substring(0, 1) != ",") || (thisTextBox.Text.IndexOf(",") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf(".") != -1)));
        }

        private void ChastotaZ_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0))) && (e.Text.Substring(0, 1) != ";") && (e.Text.Substring(0, 1) != ",") && (e.Text.Substring(0, 1) != ".") &&
            ((e.Text.Substring(0, 1) != "-") || (thisTextBox.Text.IndexOf("-") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf("-") != -1)));
        }

        private void ChastotaK_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0))) && (e.Text.Substring(0, 1) != ";") && (e.Text.Substring(0, 1) != ",") && (e.Text.Substring(0, 1) != ".") &&
            ((e.Text.Substring(0, 1) != "-") || (thisTextBox.Text.IndexOf("-") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf("-") != -1)));
        }

        private void SkorostStola_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0))) && (e.Text.Substring(0, 1) != ";") && (e.Text.Substring(0, 1) != ",") && (e.Text.Substring(0, 1) != ".") &&
            ((e.Text.Substring(0, 1) != "-") || (thisTextBox.Text.IndexOf("-") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf("-") != -1)));
        }

        private void PeriodPoperechPodacha_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0))) && (e.Text.Substring(0, 1) != ";") && (e.Text.Substring(0, 1) != ",") && (e.Text.Substring(0, 1) != ".") &&
            ((e.Text.Substring(0, 1) != "-") || (thisTextBox.Text.IndexOf("-") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf("-") != -1)));
        }

        private void NepreprivPodachaVrez_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox thisTextBox = (sender as TextBox);
            e.Handled = (!(Char.IsDigit(e.Text, 0))) && (e.Text.Substring(0, 1) != ";") && (e.Text.Substring(0, 1) != ",") && (e.Text.Substring(0, 1) != ".") &&
            ((e.Text.Substring(0, 1) != "-") || (thisTextBox.Text.IndexOf("-") != -1) || (thisTextBox.SelectionStart == 0) || (!Char.IsDigit(thisTextBox.Text.Substring(thisTextBox.SelectionStart - 1, 1), 0)) || ((thisTextBox.Text.IndexOf("-") != -1)));
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            DataTable data1 = new DataTable();
            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cmd = new SQLiteCommand("SELECT *, CASE WHEN count(id)>0 THEN max(id)+1 ELSE 1 END AS PasswordPresent FROM naruzhniyStanok", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(data1);
            }

            DataTable data = new DataTable();
            if (conn.State == ConnectionState.Open)
            {
                SQLiteCommand cmd = new SQLiteCommand("insert into naruzhniyStanok values('" + data1.Rows[0][0].ToString() + "', '" + naibDiametr.Text.Replace(",", ".") + "','" + naibDlina.Text.Replace(",", ".") + "', '" + moshnostN.Text.Replace(",", ".") + "', '" + KPD.Text.Replace(",", ".") + "', '" + Diam.Text.Replace(",", ".") + "', '" + Shirina.Text.Replace(",", ".") + "', '" + ChastotaZ.Text + "', '" + ChastotaK.Text + "', '" + SkorostStola.Text + "','" + PeriodPoperechPodacha.Text + "', '" + NepreprivPodachaVrez.Text + "','" + name.Text + "')", conn);
                SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
                ad.Fill(data);
            }
            conn.Dispose();

            MessageBox.Show("Станок успешно добавлен");
            this.Close();
        }
    }
}
