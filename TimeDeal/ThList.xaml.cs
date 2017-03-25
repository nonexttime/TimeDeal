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
using System.Data;

namespace TimeDeal
{
    /// <summary>
    /// ThList.xaml 的交互逻辑
    /// </summary>
    public partial class ThList : Window
    {
        XmlDocument document = new XmlDocument();
        DataTable dt = new DataTable();
        XmlNode root;
        public ThList()
        {
            InitializeComponent();

/*
            struct Th
            {
                int ID;
                string  Name;
                int Age;
            }

            //准备数据源
            List<Th> ThList = new List<Th>();
            {
        new Th(){ID=0,Name="abc",Age=29},
            };
 * */
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                VistaGlassHelper.ExtendGlass(this, -3, -3, -3, -3);
            }
            catch
            {
                this.Background = Brushes.White;
            }

            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Content",typeof(string));
            dt.Columns.Add("Time", typeof(string));
            dt.Columns.Add("Music", typeof(string));


            document.Load(@"E:\Project\TimeDeal\TimeDeal\TimeDeal\Th.xml");
            root = document.DocumentElement;

            listView1_Bind();
        }

        private void listView1_Bind()
        {
            dt.Clear();
            FindAllXmlDocument((XmlNode)document.DocumentElement, 0);

            this.listView1.ItemsSource = dt.DefaultView;

        }


        /// <summary>
        /// Find all nodes information
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
                    dr["Title"] = root.FirstChild.InnerText;
                    dr["Content"] = root.FirstChild.NextSibling.InnerText;
                    dr["Time"] = root.FirstChild.NextSibling.NextSibling.FirstChild.InnerText + "/" + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.InnerText + "/" + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.NextSibling.InnerText + " " + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.NextSibling.NextSibling.InnerText + ":" + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
                    dr["Music"] = root.FirstChild.NextSibling.NextSibling.NextSibling.InnerText;
                    dt.Rows.Add(dr);
                    root = root.NextSibling;
                }
            }
        }

        private void button_Delete_Click(object sender, RoutedEventArgs e)
        {
            int i_count = listView1.SelectedIndex;
            if ( i_count != -1)
            {
                DeleteSelectedXmlDocument(i_count);
            }
        }

        /// <summary>
        /// 删除选定的Thing的XML文件中对应的节点，根据排序
        /// </summary>
        /// <param name="root"></param>
        /// <param name="path"></param>
        private void DeleteSelectedXmlDocument(int Num)
        {
            int count = 0;
            if (root == null)
                return;
            if (root is XmlElement)
            {
                XmlNode root_child = root.FirstChild;
                for (; root_child != null; )
                {
                    if (count == Num)
                        root.RemoveChild(root_child);
                    count++;
                    root_child = root_child.NextSibling;
                }
                document.Save(@"E:\Project\TimeDeal\TimeDeal\TimeDeal\Th.xml");

                listView1_Bind();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        /*
        private void RecurseXmlDocument(XmlNode root, int indent)
        {
            string ar;
            if (root == null)
                return;
            if (root is XmlElement)
            {
        //        listBox1.Items.Add(root.Name.PadLeft(root.Name.Length + indent));
                if (root.HasChildNodes)
                    RecurseXmlDocument(root.FirstChild, indent + 2);
                if (root.NextSibling != null)
                    RecurseXmlDocument(root.NextSibling, indent);
            }
            else if (root is XmlText)
            {
                string text = ((XmlText)root).Value;
       //         listBox1.Items.Add(text.PadLeft(text.Length + indent));
            }
        }
         */
    }
}
