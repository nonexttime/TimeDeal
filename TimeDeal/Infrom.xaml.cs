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

namespace TimeDeal
{
    /// <summary>
    /// Infrom.xaml 的交互逻辑
    /// </summary>
    public partial class Infrom : Window
    {

        public Infrom()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            
            label_Title.Content = "该起床了";
      //      textBox_Content.
            textBox_Content.Text = "年代nfo挪威饭呢阿伯封闭式的不符哦圣达菲";
        }

        public Infrom(string Tit,string Con,string mus_path)
        {
            InitializeComponent();
            label_Title.Content = Tit;
            textBox_Content.Text = Con;
            PlayMusic.Playsound(mus_path);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            PlayMusic.Stopsound();
            base.Close();
        }


    }
}
