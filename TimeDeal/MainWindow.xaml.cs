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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Windows.Media.Animation;

using System.Drawing;
using System.Windows.Forms;

namespace TimeDeal
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon notifyIcon;
 //     System.String str_path = AppDomain.CurrentDomain.BaseDirectory;
        DispatcherTimer tm = new DispatcherTimer();
        DispatcherTimer action = new DispatcherTimer();
        XmlDocument document = new XmlDocument();

        public MainWindow()
        {
            InitializeComponent();
            tm.Tick += new EventHandler(tm_Tick);
            tm.Interval = TimeSpan.FromSeconds(20); //20秒激发一次
            tm.Start();

            InitialNotifyIcon();
        }

        private void InitialNotifyIcon()
        {
            this.notifyIcon = new NotifyIcon();
            this.notifyIcon.BalloonTipText = "TimeDeal";
            this.notifyIcon.Text = "TimeDeal";
            this.notifyIcon.Icon = new System.Drawing.Icon("clock.ico");
            this.notifyIcon.Visible = true;
            this.Visibility = Visibility.Hidden;
            this.notifyIcon.ShowBalloonTip(1000);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);


            //设置菜单项，依次为添加提醒、事务清单、音乐库、退出
            System.Windows.Forms.MenuItem menu_add = new System.Windows.Forms.MenuItem("添加提醒");
            System.Windows.Forms.MenuItem menu_th = new System.Windows.Forms.MenuItem("事务清单");
            System.Windows.Forms.MenuItem menu_music = new System.Windows.Forms.MenuItem("音乐库");
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("exit");
            //事件触发：添加提醒
            menu_add.Click += new EventHandler(menu_add_Click);

            //事件触发：事务清单
            menu_th.Click += new EventHandler(menu_th_Click);

            //事件触发：音乐库
            menu_music.Click += new EventHandler(menu_music_Click);
            
            //事件触发：退出菜单项
            exit.Click += new EventHandler(notifyIcon_exit_Click);

            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { menu_add,menu_th,menu_music , exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

        }
        //事件响应：增加提醒
        private void menu_add_Click(object sender, EventArgs e)
        {
            AddTh Thing_add = new AddTh();
            Thing_add.Show();
        }
        //事件响应：事务清单
        private void menu_th_Click(object sender, EventArgs e)
        {
            ThList Th_list = new ThList();
            Th_list.Show();
        }
        //事件响应：音乐库
        private void menu_music_Click(object sender, EventArgs e)
        {
            Music window_music = new Music();
            window_music.Show();
        }

        //事件响应：显示主窗体
        private void notifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (this.Visibility == Visibility.Visible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.Visibility = Visibility.Visible;
                    this.Activate();
                }
            }
        }

        private void notifyIcon_exit_Click(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            System.Windows.Application.Current.Shutdown();
        }



        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                VistaGlassHelper.ExtendGlass(this, 2, 2, 2, 2);
            }
            catch
            {
                this.Background = System.Windows.Media.Brushes.White;
            }

            document.Load(@"E:\Project\TimeDeal\TimeDeal\TimeDeal\Th.xml");

        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {
            AddTh Thing_add = new AddTh();
            Thing_add.Show();
        }

        private void Window_MouseEnter(object sender,System.Windows.Input.MouseEventArgs e)
        {/*
            button_Add.Visibility = System.Windows.Visibility.Visible;
            button3_Copy1.Visibility = System.Windows.Visibility.Visible;
            button3_Copy2.Visibility = System.Windows.Visibility.Visible;
            button3_Copy3.Visibility = System.Windows.Visibility.Visible;
          * */
        }

        private void Window_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            action.Start();
            


            DoubleAnimation da = new DoubleAnimation();
            DoubleAnimation db = new DoubleAnimation();
            da.From = 0;
            da.To = 60;
            da.Duration = TimeSpan.FromSeconds(60);
            this.button_Add.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, da);
            this.button_Add.Visibility = System.Windows.Visibility.Hidden;

            this.button_Music.BeginAnimation(System.Windows.Controls.Button.OpacityProperty, da);
            button_Music.Visibility = System.Windows.Visibility.Hidden;


//            
            button_Music.Visibility = System.Windows.Visibility.Hidden;
            button_All.Visibility = System.Windows.Visibility.Hidden;
            button3_Copy3.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            button_Add.Visibility = System.Windows.Visibility.Visible;
            button_Music.Visibility = System.Windows.Visibility.Visible;
            button_All.Visibility = System.Windows.Visibility.Visible;
            button3_Copy3.Visibility = System.Windows.Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {/*
            button_Add.Visibility = System.Windows.Visibility.Hidden;
            button3_Copy1.Visibility = System.Windows.Visibility.Hidden;
            button3_Copy2.Visibility = System.Windows.Visibility.Hidden;
            button3_Copy3.Visibility = System.Windows.Visibility.Hidden;
          * */
        }

        private void button_All_Click(object sender, RoutedEventArgs e)
        {
            button_All.Visibility = System.Windows.Visibility.Visible;
            ThList Th_list = new ThList();
            Th_list.Show();
        }

        private void tm_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            string time2 = now.Year +"-"+ now.Month + "-" + now.Day + " " +now.Hour + ":" + now.Minute;
    //        string time2 = now.ToString("yyyy-MM-dd HH:mm");
            string music_path, title, content;
            XmlNode root = document.DocumentElement;
            if (root == null)
                return;
            if (root is XmlElement)          //根据时间判断时间到了没有
            {
                root = root.FirstChild;
                for (; root != null; )
                {
                    string time = root.FirstChild.NextSibling.NextSibling.FirstChild.InnerText + "-" + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.InnerText + "-" + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.NextSibling.InnerText + " " + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.NextSibling.NextSibling.InnerText + ":" + root.FirstChild.NextSibling.NextSibling.FirstChild.NextSibling.NextSibling.NextSibling.NextSibling.InnerText;
               //     bool result = string.Compare(time, time2);
                    if(time == time2)
                    {
                        music_path = root.LastChild.InnerText;
                        title = root.FirstChild.InnerText;
                        content = root.FirstChild.NextSibling.InnerText;

                        Infrom inf = new Infrom(title, content, music_path);
                        inf.Show();
                    }
                    root = root.NextSibling;
                }
            }
        }

        private void button_Music_Click(object sender, RoutedEventArgs e)
        {
            Music window_music = new Music();
            window_music.Show();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            this.notifyIcon.Visible = true;
        }
    }
}
