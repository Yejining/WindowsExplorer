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

using WindowExplorer.Function;
using WindowExplorer.Screen;

namespace WindowExplorer.Main
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MainScreen screen;
        DirectoryInformation directoryManager;
        AddressBar addressBar;
        ManageLog manager;
        TitleBar titleBar;

        public MainWindow()
        {
            InitializeComponent();

            // screen
            screen = new MainScreen(this);
            MainGrid.Children.Add(screen);

            // log
            manager = ManageLog.GetInstance();
            manager.AddLog("C:\\", 0);

            // directory information class for explorer
            directoryManager = DirectoryInformation.GetInstance();
            directoryManager.SetButtonToExplorer("C:\\");

            // addressBar
            addressBar = AddressBar.GetInstance();
            addressBar.SetPath();

            // titleBar
            titleBar = TitleBar.GetInstance();
            titleBar.PassMain(this);
        }
    }
}
