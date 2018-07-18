using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace KrugovoeShlifovanie
{
    /// <summary>
    /// Логика взаимодействия для Results.xaml
    /// </summary>
    public partial class Results : Window
    {
        public Results()
        {
            InitializeComponent();
        }

        private void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from resTest where id = '"+Properties.Settings.Default.testID+"'", conn);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            DataTable data = new DataTable();
            ad.Fill(data);
            conn.Dispose();

            SQLiteConnection conn1 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn1.Open();
            SQLiteCommand cmd1 = new SQLiteCommand("select answer, right_variant from answers", conn1);
            SQLiteDataAdapter ad1 = new SQLiteDataAdapter(cmd1);
            DataTable data1 = new DataTable();
            ad1.Fill(data1);
            conn1.Dispose();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                Label l1 = new Label();
                l1.Content = "Вопрос №" + (i + 1).ToString() + ": " + data1.Rows[i][0].ToString();
                l1.Margin = new Thickness(0, 60 * i, 0, 0);
                l1.FontSize = 24;
                results.Children.Add(l1);

                Label l2 = new Label();
                l2.Margin = new Thickness(0, 60 * i+30, 0, 0);
                if (data1.Rows[i][1].ToString() == data.Rows[i][3].ToString()) {
                    l2.Content = "Ваш ответ: '" + data.Rows[i][3].ToString() + "' верный";
                    l2.Foreground = Brushes.Green;
                }
                else if (data.Rows[i][3].ToString() == "")
                {
                    l2.Content = "Нет ответа, правильный ответ '" + data1.Rows[i][1].ToString()+"'";
                    l2.Foreground = Brushes.Red;
                }
                else
                {
                    l2.Content = "Ваш ответ: '" + data.Rows[i][3].ToString() + "' неверный, правильный ответ '"+ data1.Rows[i][1].ToString() + "'";
                    l2.Foreground = Brushes.Red;
                }

                results.Children.Add(l2);
            }

            SQLiteConnection conn2 = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn2.Open();
            SQLiteCommand cmd2 = new SQLiteCommand("select * from result where id = '" + Properties.Settings.Default.testID + "'", conn2);
            SQLiteDataAdapter ad2 = new SQLiteDataAdapter(cmd2);
            DataTable data2 = new DataTable();
            ad2.Fill(data2);
            conn2.Dispose();

            if (float.Parse(data2.Rows[0][2].ToString()) >= 70)
            {
                sertf.IsEnabled = true;
            }
            else
            {
                sertf.IsEnabled = false;
            }
        }

        private void sertf_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection conn = new SQLiteConnection(@"Data Source=" + Environment.CurrentDirectory + @"\data.db; Version=3;");
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("select result from result where id = '" + Properties.Settings.Default.testID + "'", conn);
            SQLiteDataAdapter ad = new SQLiteDataAdapter(cmd);
            DataTable data = new DataTable();
            ad.Fill(data);
            conn.Dispose();

            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, 42f, iTextSharp.text.Font.NORMAL);
            var font2 = new Font(baseFont, 20f, iTextSharp.text.Font.NORMAL);
            var document = new iTextSharp.text.Document();
            using (var writer = PdfWriter.GetInstance(document, new FileStream("result.pdf", FileMode.Create)))
            {
                document.Open();

                var logo = iTextSharp.text.Image.GetInstance(new FileStream(Environment.CurrentDirectory+@"\sertificate.jpg", FileMode.Open));
                logo.SetAbsolutePosition(0, 0);
                writer.DirectContent.AddImage(logo);
                writer.DirectContent.BeginText();

                writer.DirectContent.SetFontAndSize(baseFont, 46f);
                writer.DirectContent.ShowTextAligned(iTextSharp.text.Element.ALIGN_MIDDLE, "СЕРТИФИКАТ", 150, 650, 0);

                writer.DirectContent.EndText();
                var t = new PdfPTable(1);
                t.WidthPercentage = 87;
                var c = new PdfPCell();
                c.VerticalAlignment = Element.ALIGN_MIDDLE;
                c.BorderColor = new BaseColor(255,255,255,0);
                c.PaddingTop = 280;
                c.MinimumHeight = 500;

                var p3 = new iTextSharp.text.Paragraph("Настроящий сертификат подтверждает, что");
                p3.Font = font2;
                p3.Alignment = Element.ALIGN_CENTER;

                var p = new iTextSharp.text.Paragraph(Properties.Settings.Default.userName);
                p.Font = font;
                p.Alignment = Element.ALIGN_CENTER;

                var p1 = new iTextSharp.text.Paragraph("прошел тестрирование с результатом");
                p1.Font = font2;
                p1.Alignment = Element.ALIGN_CENTER;
                var p2 = new iTextSharp.text.Paragraph(data.Rows[0][0].ToString()+ " баллов");
                p2.Font = font;
                p2.Alignment = Element.ALIGN_CENTER;

                var p4 = new iTextSharp.text.Paragraph("АИС 'Расчет режимов резания при круглом шлифовании'");
                p4.Font = font2;
                p4.Alignment = Element.ALIGN_CENTER;

                c.AddElement(p3);
                c.AddElement(p);
                c.AddElement(p1);
                c.AddElement(p2);

                var c1 = new PdfPCell();
                c1.VerticalAlignment = Element.ALIGN_MIDDLE;
                c1.BorderColor = new BaseColor(255, 255, 255, 0);
                c1.PaddingTop = 100;
                c1.MinimumHeight = 100;
                c1.BorderColorBottom = new BaseColor(0, 0, 0);

                c1.AddElement(p4);
                t.AddCell(c);
                t.AddCell(c1);

                document.Add(t);


                document.Close();
                writer.Close();
            }

            Process.Start("result.pdf");

        }
    }
}
