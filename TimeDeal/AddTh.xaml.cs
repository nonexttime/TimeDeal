using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace TimeDeal
{
    /// <summary>
    /// AddTh.xaml 的交互逻辑
    /// </summary>
    public partial class AddTh : Window
    {
        XmlDocument document = new XmlDocument();
        XmlElement root;
        public AddTh()
        {
            InitializeComponent();
            document.Load(@"S:\Project\TimeDeal\TimeDeal\TimeDeal\Th.xml");
            root = document.DocumentElement;
            this.ResizeMode = ResizeMode.NoResize;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                VistaGlassHelper.ExtendGlass(this, -20, -20, -20, -20);
            }
            catch 
            {
                this.Background = Brushes.White;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.ShowDialog();
         
            textBox_music.Text = openFileDialog1.FileName;
        }

        private void combo_Min_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            combo_Min.SelectedIndex = (combo_Min.SelectedIndex + 1);
        }

        private void combo_Hour_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            combo_Hour.SelectedIndex = (combo_Hour.SelectedIndex + 1);
        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            XmlElement newTh = document.CreateElement("Th");
            XmlElement newTitle = document.CreateElement("Title");
            XmlElement newContent = document.CreateElement("Content"); 
            XmlElement newTime = document.CreateElement("Time"); 
            XmlElement newYear = document.CreateElement("Year"); 
            XmlElement newMonth = document.CreateElement("Month");
            XmlElement newDay = document.CreateElement("Day");
            XmlElement newHour = document.CreateElement("Hour");
            XmlElement newMin = document.CreateElement("Min");
            XmlElement newMusic = document.CreateElement("Music");

            XmlText title = document.CreateTextNode(textBox_Title.Text);
            XmlText content = document.CreateTextNode(textBox_Content.Text);

            DateTime date = DateTime.Parse(textBox_date.Text);

            XmlText year = document.CreateTextNode(date.Year.ToString());
            XmlText month = document.CreateTextNode(date.Month.ToString());
            XmlText day = document.CreateTextNode(date.Day.ToString());
            XmlText hour = document.CreateTextNode(combo_Hour.Text);
            XmlText min = document.CreateTextNode(combo_Min.Text);
            XmlText music = document.CreateTextNode(textBox_music.Text);

            newTh.AppendChild(newTitle);
            newTh.AppendChild(newContent);
            newTh.AppendChild(newTime);
            newTh.AppendChild(newMusic);

            newTitle.AppendChild(title);
            newContent.AppendChild(content);
            newTime.AppendChild(newYear);
            newTime.AppendChild(newMonth);
            newTime.AppendChild(newDay);
            newTime.AppendChild(newHour);
            newTime.AppendChild(newMin);

            newYear.AppendChild(year);
            newMonth.AppendChild(month);
            newDay.AppendChild(day);
            newHour.AppendChild(hour);
            newMin.AppendChild(min);

            newMusic.AppendChild(music);

            root.InsertAfter(newTh, root.LastChild);
            document.Save(@"S:\Project\TimeDeal\TimeDeal\TimeDeal\Th.xml");

            base.Close();
           
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button_date_Click(object sender, RoutedEventArgs e)
        {
            this.calendar1.Visibility = Visibility.Visible;
        }

        private void calendar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            string date_day = this.calendar1.SelectedDate.ToString().Substring(0,10);
            this.textBox_date.Text = date_day;
        }

        private void calendar1_MouseLeave(object sender, MouseEventArgs e)
        {
            calendar1.Visibility = Visibility.Hidden;
        }

    }
}
