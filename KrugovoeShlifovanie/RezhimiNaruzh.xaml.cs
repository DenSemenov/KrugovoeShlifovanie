using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Windows.Threading;
using word = Microsoft.Office.Interop.Word;
using System.Reflection;
using Microsoft.Office.Interop.Word;

namespace KrugovoeShlifovanie
{
    public partial class RezhimiNaruzh : System.Windows.Window
    {
        float n = 0;
        bool Start = false;
        float k = -4;
        bool right = true;
        DispatcherTimer dt = new DispatcherTimer();
        public RezhimiNaruzh()
        {
            InitializeComponent();

            if (Properties.Settings.Default.otchetType == "Наружное круглое шлифование")
            {
                model1.Source = @"Models/mesh.3ds";
                mover.Source = @"Models/mover.3ds";
                wheel.Source = @"Models/wheel.3ds";
                mover.Transform = new TranslateTransform3D(-4, 0, 0);
                mover.MouseEnter += Mover_MouseEnter;
                mover.MouseLeave += Mover_MouseLeave;
                wheel.MouseEnter += Wheel_MouseEnter;
                wheel.MouseLeave += Wheel_MouseLeave;
            }
            else
            {
                model1.Source = @"Models/mesh2.3ds";
                mover.Source = @"Models/mover2.3ds";
                mover.MouseEnter += Mover_MouseEnter1;
                mover.MouseLeave += Mover_MouseLeave1;
            }
            model1.MouseEnter += Model1_MouseEnter;
            model1.MouseLeave += Model1_MouseLeave;
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Tick += Dt_Tick;

        }

        private void Mover_MouseLeave1(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Hidden;
        }

        private void Mover_MouseEnter1(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Visible;
            infoL1.Content = "Шлифовальный круг";
            infoL2.Content = "Скорость круга: " + Properties.Settings.Default.otchetSkorostK + " м/с";
            infoL3.Content = "Характеристика: " + Properties.Settings.Default.otchetCharK;
        }

        private void Model1_MouseLeave(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Hidden;
            info.Height = 106;
        }

        private void Model1_MouseEnter(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Visible;
            infoL1.Content = "Станок "+ Properties.Settings.Default.otchetStanok;
            infoL2.Content = "";
            infoL3.Content = "";
            info.Height = 42;
        }

        private void Wheel_MouseLeave(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Hidden;
        }

        private void Wheel_MouseEnter(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Visible;
            infoL1.Content = "Шлифовальный круг";
            infoL2.Content = "Скорость круга: "+Properties.Settings.Default.otchetSkorostK + " м/с";
            infoL3.Content = "Характеристика: " + Properties.Settings.Default.otchetCharK;
        }

        private void Mover_MouseLeave(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Hidden;
        }

        private void Mover_MouseEnter(object sender, MouseEventArgs e)
        {
            info.Visibility = Visibility.Visible;
            infoL1.Content = "Заготовка";
            infoL2.Content = "Скорость заготовки: " + Properties.Settings.Default.otchetSkorostZ + " м/мин";
            infoL3.Content = "Диаметр: " + Properties.Settings.Default.otchetDiam + " мм, длина: " + Properties.Settings.Default.otchetDlina + " мм";
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.otchetType == "Наружное круглое шлифование")
            {
                if (k < -4)
                {
                    right = false;
                }
                if (k > 1.3)
                {
                    right = true;
                }

                if (right)
                {
                    k = k - 0.01f;
                    mover.Transform = new TranslateTransform3D(k, 0, 0);
                }
                else
                {
                    k = k + 0.01f;
                    mover.Transform = new TranslateTransform3D(k, 0, 0);
                }
            }
            else
            {
                if (k < -14)
                {
                    right = false;
                }
                if (k > 0)
                {
                    right = true;
                }

                if (right)
                {
                    k = k - 0.01f;
                    mover.Transform = new TranslateTransform3D(k+4, 0, 0);
                }
                else
                {
                    k = k + 0.01f;
                    mover.Transform = new TranslateTransform3D(k+4, 0, 0);
                }
            }


            


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            l1.Content = "Продольная подача стола = " + Properties.Settings.Default.ProdolPodach + " мм/об";
            l2.Content = "Частота вращения заготовки = " + Properties.Settings.Default.ChastotaZ + " мин – 1";
            l3.Content = "Частота вращения круга = " + Properties.Settings.Default.ChastotaK + " мин – 1";
            l4.Content = "Скорость движения стола = " + Properties.Settings.Default.SkorostStola + " мм/мин";
            l5.Content = "Шлифовальный круг: " + Properties.Settings.Default.charkText;
            l6.Content = "Индекс зернистости: " + Properties.Settings.Default.indexZText;
            l7.Content = "Структура: " + Properties.Settings.Default.struktText;

            if (Properties.Settings.Default.Nrez <= Properties.Settings.Default.Nshp)
            {
                l8.Content = "Nрез ≤ Nшп (" + Properties.Settings.Default.Nrez + "≤" + Properties.Settings.Default.Nshp + ")";
                l9.Content = "Обработка на выбранных режимах выполнима";
                l9.Foreground = Brushes.Green;
            }
            else
            {
                l8.Content = "Nрез > Nшп (" + Properties.Settings.Default.Nrez + ">" + Properties.Settings.Default.Nshp + ")";
                l9.Content = "Обработка на выбранных режимах невыполнима";
                l8.Foreground = Brushes.Red;
                l9.Foreground = Brushes.Red;
            }
            if (Properties.Settings.Default.Tm1 != 0)
            {
                l10.Content = "Машинное время = " + Properties.Settings.Default.Tm1 + " - " + Properties.Settings.Default.Tm2 + " мин";
            }
            else
            {
                l10.Visibility = Visibility.Hidden;
            }

            
        }

        private void pdf_Click(object sender, RoutedEventArgs e)
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, 42f, iTextSharp.text.Font.NORMAL);
            var font2 = new iTextSharp.text.Font(baseFont, 20f, iTextSharp.text.Font.NORMAL);
            var document = new iTextSharp.text.Document();
            using (var writer = PdfWriter.GetInstance(document, new FileStream("rezhimi.pdf", FileMode.Create)))
            {
                document.Open();
                var t = new PdfPTable(1);
                t.WidthPercentage = 90;
                var c = new PdfPCell();
                c.VerticalAlignment = Element.ALIGN_TOP;
                c.BorderColor = new BaseColor(255, 255, 255, 0);
                c.MinimumHeight = 700;

                var p1 = new iTextSharp.text.Paragraph("Режимы шлифования");
                p1.Font = font;
                p1.Alignment = Element.ALIGN_CENTER;

                var pSpace = new iTextSharp.text.Paragraph("");
                pSpace.Font = font;
                pSpace.Alignment = Element.ALIGN_CENTER;

                var p2 = new iTextSharp.text.Paragraph("Тип шлифования: " + Properties.Settings.Default.otchetType);
                p2.Font = font2;
                p2.Alignment = Element.ALIGN_LEFT;

                var p3 = new iTextSharp.text.Paragraph("Станок: " + Properties.Settings.Default.otchetStanok);
                p3.Font = font2;
                p3.Alignment = Element.ALIGN_LEFT;

                var p4 = new iTextSharp.text.Paragraph("Характеристика процесса шлифования: " + Properties.Settings.Default.otchetCharakter);
                p4.Font = font2;
                p4.Alignment = Element.ALIGN_LEFT;

                var p5 = new iTextSharp.text.Paragraph("Скорость круга: " + Properties.Settings.Default.otchetSkorostK + " м/с");
                p5.Font = font2;
                p5.Alignment = Element.ALIGN_LEFT;

                var p6 = new iTextSharp.text.Paragraph("Скорость заготовки: " + Properties.Settings.Default.otchetSkorostZ + " м/мин");
                p6.Font = font2;
                p6.Alignment = Element.ALIGN_LEFT;

                var p7 = new iTextSharp.text.Paragraph("Глубина шлифования: " + Properties.Settings.Default.otchetGlubina + " мм");
                p7.Font = font2;
                p7.Alignment = Element.ALIGN_LEFT;

                var p8 = new iTextSharp.text.Paragraph("Продольная подача: " + Properties.Settings.Default.otchetProdol);
                p8.Font = font2;
                p8.Alignment = Element.ALIGN_LEFT;

                var p9 = new iTextSharp.text.Paragraph("Диаметр заготовки: " + Properties.Settings.Default.otchetDiam + " мм");
                p9.Font = font2;
                p9.Alignment = Element.ALIGN_LEFT;

                var p10 = new iTextSharp.text.Paragraph("Длина заготовки: " + Properties.Settings.Default.otchetDlina + " мм");
                p10.Font = font2;
                p10.Alignment = Element.ALIGN_LEFT;

                var p11 = new iTextSharp.text.Paragraph("Припуск: " + Properties.Settings.Default.otchetPripusk + " мм");
                p11.Font = font2;
                p11.Alignment = Element.ALIGN_LEFT;

                var p12 = new iTextSharp.text.Paragraph("Характеристика шлифовального круга: " + Properties.Settings.Default.otchetCharK);
                p12.Font = font2;
                p12.Alignment = Element.ALIGN_LEFT;

                var p13 = new iTextSharp.text.Paragraph("Индекс зернистости: " + Properties.Settings.Default.otchetIndex);
                p13.Font = font2;
                p13.Alignment = Element.ALIGN_LEFT;

                var p14 = new iTextSharp.text.Paragraph("Структура: " + Properties.Settings.Default.otchetStruktura);
                p14.Font = font2;
                p14.Alignment = Element.ALIGN_LEFT;

                var p15 = new iTextSharp.text.Paragraph("Продольная подача стола: " + Properties.Settings.Default.ProdolPodach + " мм/об");
                p15.Font = font2;
                p15.Alignment = Element.ALIGN_LEFT;

                var p16 = new iTextSharp.text.Paragraph("Частота вращения заготовки: " + Properties.Settings.Default.ChastotaZ + " мин – 1");
                p16.Font = font2;
                p16.Alignment = Element.ALIGN_LEFT;

                var p17 = new iTextSharp.text.Paragraph("Частота вращения круга: " + Properties.Settings.Default.ChastotaK + " мин – 1");
                p17.Font = font2;
                p17.Alignment = Element.ALIGN_LEFT;

                var p18 = new iTextSharp.text.Paragraph("Скорость движения стола: " + Properties.Settings.Default.SkorostStola + " мм/мин");
                p18.Font = font2;
                p18.Alignment = Element.ALIGN_LEFT;

                c.AddElement(p1);
                c.AddElement(pSpace);
                c.AddElement(p2);
                c.AddElement(p3);
                c.AddElement(p4);
                c.AddElement(p5);
                c.AddElement(p6);
                c.AddElement(p7);
                c.AddElement(p8);
                c.AddElement(p9);
                c.AddElement(p10);
                c.AddElement(p11);
                c.AddElement(p12);
                c.AddElement(p13);
                c.AddElement(p14);
                c.AddElement(p15);
                c.AddElement(p16);
                c.AddElement(p17);
                c.AddElement(p18);

                t.AddCell(c);
                document.Add(t);
                document.Close();
                writer.Close();
            }

            Process.Start("rezhimi.pdf");
        }

        private void word_Click(object sender, RoutedEventArgs e)
        {
            var wordApp = new word.Application();
            var document = wordApp.Documents.Add();
            var table = document.Tables.Add(document.Range(),19,2);

            table.Cell(1, 1).Range.Text = "Режимы шлифования";
            table.Cell(1, 1).Range.Font.Bold = 1;

            table.Cell(3, 1).Range.Text = "Тип шлифования";
            table.Cell(3, 2).Range.Text = Properties.Settings.Default.otchetType;

            table.Cell(4, 1).Range.Text = "Станок";
            table.Cell(4, 2).Range.Text = Properties.Settings.Default.otchetStanok;

            table.Cell(5, 1).Range.Text = "Характеристика процесса шлифования";
            table.Cell(5, 2).Range.Text = Properties.Settings.Default.otchetCharakter;

            table.Cell(6, 1).Range.Text = "Скорость круга";
            table.Cell(6, 2).Range.Text = Properties.Settings.Default.otchetSkorostK + " м/с";

            table.Cell(7, 1).Range.Text = "Скорость заготовки";
            table.Cell(7, 2).Range.Text = Properties.Settings.Default.otchetSkorostZ + " м/мин";

            table.Cell(8, 1).Range.Text = "Глубина шлифования";
            table.Cell(8, 2).Range.Text = Properties.Settings.Default.otchetGlubina + " мм";

            table.Cell(9, 1).Range.Text = "Продольная подача";
            table.Cell(9, 2).Range.Text = Properties.Settings.Default.otchetProdol;

            table.Cell(10, 1).Range.Text = "Диаметр заготовки";
            table.Cell(10, 2).Range.Text = Properties.Settings.Default.otchetDiam + " мм";

            table.Cell(11, 1).Range.Text = "Длина заготовки";
            table.Cell(11, 2).Range.Text = Properties.Settings.Default.otchetDlina + " мм";

            table.Cell(12, 1).Range.Text = "Припуск";
            table.Cell(12, 2).Range.Text = Properties.Settings.Default.otchetPripusk + " мм";

            table.Cell(13, 1).Range.Text = "Характеристика шлифовального круга";
            table.Cell(13, 2).Range.Text = Properties.Settings.Default.otchetCharK;

            table.Cell(14, 1).Range.Text = "Индекс зернистости";
            table.Cell(14, 2).Range.Text = Properties.Settings.Default.otchetIndex;

            table.Cell(15, 1).Range.Text = "Структура";
            table.Cell(15, 2).Range.Text = Properties.Settings.Default.otchetStruktura;

            table.Cell(16, 1).Range.Text = "Продольная подача стола";
            table.Cell(16, 2).Range.Text = Properties.Settings.Default.ProdolPodach + " мм/об";

            table.Cell(17, 1).Range.Text = "Частота вращения заготовки";
            table.Cell(17, 2).Range.Text = Properties.Settings.Default.ChastotaZ + " мин – 1";

            table.Cell(18, 1).Range.Text = "Частота вращения круга";
            table.Cell(18, 2).Range.Text = Properties.Settings.Default.ChastotaK + " мин – 1";

            table.Cell(19, 1).Range.Text = "Скорость движения стола";
            table.Cell(19, 2).Range.Text = Properties.Settings.Default.SkorostStola + " мм/мин";

            for (int i = 3; i <= 19; i++)
            {
                table.Cell(i, 2).Range.Font.Italic = 1;
            }

            document.Activate();
            wordApp.Visible = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Start == false)
            {
                Start = true;
                dt.Start();
                start.Source = new BitmapImage(new Uri(@"Resourses/stop.png", UriKind.Relative));
            }
            else
            {
                Start = false;
                dt.Stop();
                start.Source = new BitmapImage(new Uri(@"Resourses/play.png", UriKind.Relative));
            }

        }
    }
}
