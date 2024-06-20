﻿using System;
using System.Collections.Generic;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace work.Pages
{
    /// <summary>
    /// Home.xaml 的交互逻辑
    /// </summary>
    public partial class Home : Page
    {
        public HistoryPage HistoryPage { get; set; }
        public Home()
        {
            App.HomeInstance = this;
            InitializeComponent();
            EnsureSaveDirectoryExists();
            LoadLastImage();
            HistoryPage = new HistoryPage();
            this.DataContext = HistoryPage.combine;

        }
        private void CommonBtnClickHandler(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            // MessageBox.Show(btn.Name);


        }

        //模糊效果
        private void InteractiveGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button1.Visibility = Visibility.Visible;
            Button2.Visibility = Visibility.Visible;
            ApplyBlurEffect(InteractiveGrid, true);
        }

        private void InteractiveGrid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button1.Visibility = Visibility.Collapsed;
            Button2.Visibility = Visibility.Collapsed;
            ApplyBlurEffect(InteractiveGrid, false);
        }

        private void ApplyBlurEffect(Panel panel, bool applyBlur)
        {
            foreach (UIElement element in panel.Children)
            {
                if (!(element is Button))
                {
                    if (applyBlur)
                    {
                        BlurEffect blur = new BlurEffect
                        {
                            Radius = 6
                        };
                        element.Effect = blur;
                    }
                    else
                    {
                        element.Effect = null;
                    }
                }
            }
        }





        //退出登录页面
        public void logout(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            mainpage.window.Close();
        }
        //跳转到设置页面
        public void setting(object sender, RoutedEventArgs e)
        {
            mainpage.window.jumpToTargetPage(mainpage.WindowsID.set);
        }


        //跳转到pvp页面
        public void jumpToPvp(object sender, RoutedEventArgs e)
        {

            mainpage.window.jumpToTargetPage(mainpage.WindowsID.websocketpvp);
        }
        //跳转到local页面
        public void jumpToLocal(object sender, RoutedEventArgs e)
        {

            mainpage.window.jumpToTargetPage(mainpage.WindowsID.local);
        }
        //跳转到history页面
        //public void jumpToHistory(object sender, RoutedEventArgs e)
        //{

        //	mainpage.window.jumpToTargetPage(mainpage.WindowsID.history);
        //}
        //ai难度选择
        public void easyAi(object sender, RoutedEventArgs e)
        {
            AI.difficulty = -1;
            mainpage.window.jumpToTargetPage(mainpage.WindowsID.ai);
        }
        public void difficultAi(object sender, RoutedEventArgs e)
        {
            AI.difficulty = 1;
            mainpage.window.jumpToTargetPage(mainpage.WindowsID.ai);
        }


        //从本地选择图片并保存
        private const string SaveDirectory = "../Images";
        private const string ConfigFilePath = "../Settings/config.txt";
        private void EnsureSaveDirectoryExists()
        {
            // 创建保存目录如果不存在
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }
        private void LoadLastImage()
        {
            if (File.Exists(ConfigFilePath))
            {
                string savedImagePath = File.ReadAllText(ConfigFilePath);
                if (File.Exists(savedImagePath))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(savedImagePath, UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();

                    UserImageBrush.Source = bitmap;
                }
            }
        }

    }
}
