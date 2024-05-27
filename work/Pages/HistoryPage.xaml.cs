﻿using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;
using work.Models;

namespace work.Pages
{
    /// <summary>
    /// HistoryPage.xaml 的交互逻辑
    /// </summary>
    public partial class HistoryPage : Page, INotifyPropertyChanged
    {
        private int a;
        private APIService apiService = new APIService();
        public HistoryPage()
        {
            InitializeComponent();
            DataContext = GameService.Instance; // 设置数据上下文为当前窗口实例
        }

        // INotifyPropertyChanged 接口的实现
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //页面跳转
        public void jumpBackToMain(object sender, RoutedEventArgs e)
        {
            MainWindow.window.jumpToTargetPage(MainWindow.WindowsID.main);
        }

        //获取单条历史记录
        public async void getHistoryById(object sender, RoutedEventArgs e)
        {
            var history =  await apiService.getSingleHistory(1);
            MessageBox.Show(history);
                
        }

        //获取所有历史记录
        public async void getHistories(object sender, RoutedEventArgs e)
        {
            var history = await apiService.getHistories(App.user.id);
            string str = "";
            foreach (string item in history) {
                str += "     ";
                str += item;
            }
            MessageBox.Show(str);

        }

        //插入历史记录
        public async void insertHistory(object sender, RoutedEventArgs e) {
            //把这个替换成要插入的历史记录就行了
            string str = "111";
            var isSuccess = await apiService.insertHistory(new History(-1,str),App.user.id);
            MessageBox.Show(isSuccess);
        } 




    }

    }


    public class GameService
    {
        private static GameService _instance;
        public ObservableCollection<string> Result { get; } = new ObservableCollection<string>();

        private GameService() { }

        public static GameService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameService();
                }
                return _instance;
            }
        }
        int turn = 0;
        public static string[] historyStep = new string[42];

        public void getPosition(int x, int y)
        {
            // ... 你的逻辑 ...
            historyStep[turn] = "{x},{y}";
            turn++;
            string moveDescription = (turn % 2 != 0)
                ? $"白子落子位置为{x},{y}"
                : $"黑子落子位置为{x},{y}";
            Result.Add(moveDescription);
        }
    }


