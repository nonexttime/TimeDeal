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
using System.Data;
using System.Xml;

namespace TimeDeal
{
    /// <summary>
    /// Music.xaml 的交互逻辑
    /// </summary>
    public partial class Music : Window
    {
        DataTable dt = new DataTable();
        XmlDocument document = new XmlDocument();
        XmlElement root;
        public Music()
        {
            InitializeComponent();
            document.Load(@"E:\Project\TimeDeal\TimeDeal\TimeDeal\Music.xml");
            root = document.DocumentElement;
            this.ResizeMode = ResizeMode.NoResize;

            dt.Columns.Add("Song", typeof(string));
            dt.Columns.Add("Path", typeof(string));
        }

        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Vista Glass效果
            try
            {
                VistaGlassHelper.ExtendGlass(this, -3, -3, -3, -3);
            }
            catch
            {
                this.Background = Brushes.White;
            }

            //初始化ListView_Music
            ListView_Bind();
        }

        /// <summary>
        /// ListView_Music与Music.xml的数据绑定
        /// </summary>
        private void ListView_Bind()
        {
            dt.Clear();
            FindAllXmlDocument((XmlNode)document.DocumentElement, 0);

            this.listView_Music.ItemsSource = dt.DefaultView;
        }

        /// <summary>
        /// 获取Music.xml的数据，插入DataTable dt
        /// </summary>
        /// <param name="root"></param>
        /// <param name="indent"></param>
        private void FindAllXmlDocument(XmlNode root, int indent)
        {
            if (root == null)
                return;
            if (root is XmlElement)
            {
                root = root.FirstChild;
                for (; root != null; )
                {
                    DataRow dr = dt.NewRow();
                    dr["Song"] = root.FirstChild.InnerText;
                    dr["Path"] = root.FirstChild.NextSibling.InnerText;
                    dt.Rows.Add(dr);
                    root = root.NextSibling;
                }
            }
        }

        /// <summary>
        /// 双击时，播放音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_Music_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listView_Music.SelectedIndex != -1)
            {
                DataRowView c = (DataRowView)listView_Music.SelectedItem;
                string path = c.Row[1].ToString();
      //          myPlayer.Source = new Uri(path);
                PlayMusic.Playsound(path);
            }
        }

        /// <summary>
        /// 窗体跟随鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// 音乐暂停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Pause_Click(object sender, RoutedEventArgs e)
        {
            PlayMusic.Pausesound();
        }

        /// <summary>
        /// 音乐继续
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Resume_Click(object sender, RoutedEventArgs e)
        {
            PlayMusic.Resumesound();
        }

        /// <summary>
        /// 音乐播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Play_Click(object sender, RoutedEventArgs e)
        {
            PlayMusic.Resumesound();
        }

        /// <summary>
        /// 添加音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            int start = filename.LastIndexOf("\\") + 1;
            int end = filename.IndexOf(".");
            string song_name = filename.Substring(start, end - start);

            XmlElement newMusic = document.CreateElement("Music");
            XmlElement newSong = document.CreateElement("Song");
            XmlElement newPath = document.CreateElement("Path");

            XmlText Song = document.CreateTextNode(song_name);
            XmlText Path = document.CreateTextNode(filename);

            newMusic.AppendChild(newSong);
            newMusic.AppendChild(newPath);

            newSong.AppendChild(Song);
            newPath.AppendChild(Path);

            root.InsertAfter(newMusic, root.LastChild);
            document.Save(@"E:\Project\TimeDeal\TimeDeal\TimeDeal\Music.xml");

            ListView_Bind();
        }

        /// <summary>
        /// 删除音乐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (listView_Music.SelectedIndex != -1)
            {
                DataRowView c = (DataRowView)listView_Music.SelectedItem;
                string path = c.Row[1].ToString();
                DeleteSelectedXmlDocument(root, path);
            }
        }

        /// <summary>
        /// 删除所选的XML记录Node
        /// </summary>
        /// <param name="root"></param>
        /// <param name="path"></param>
        private void DeleteSelectedXmlDocument(XmlNode root,string path)
        {
            if (root == null)
                return;
            if (root is XmlElement)
            {
                XmlNode root_child = root.FirstChild;
                for (; root_child!= null; )
                {
                    if (root_child.LastChild.InnerText == path)
                    {
                        root.RemoveChild(root_child);                
                    }                        
                    root_child = root_child.NextSibling;
                }
                document.Save(@"E:\Project\TimeDeal\TimeDeal\TimeDeal\Music.xml");

                ListView_Bind();
            }
        }
    }
}
