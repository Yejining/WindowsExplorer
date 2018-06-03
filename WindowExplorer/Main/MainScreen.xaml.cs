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
using System.IO;
using System.Text.RegularExpressions;

using WindowExplorer.Screen;
using WindowExplorer.Function;

namespace WindowExplorer.Main
{
    /// <summary>
    /// MainScreen.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainScreen : UserControl
    {
        MainWindow main;
        ScreenShare screen = new ScreenShare();

        public MainScreen(MainWindow main)
        {
            InitializeComponent();
            InitializeScreen();
            this.main = main;
        }

        public void InitializeScreen()
        {
            SetGrid(screen.title, 0, 0, 2);
            SetGrid(screen.setting, 1, 0, 2);
            SetGrid(screen.address, 2, 0, 2);
            SetGrid(screen.explorer, 3, 2, 1);
            SetGrid(screen.task, 4, 0, 2);
        }

        public void SetGrid(UIElement grid, int row, int column, int span)
        {
            ScreenGrid.Children.Add(grid);
            Grid.SetRow(grid, row);
            Grid.SetColumn(grid, column);
            Grid.SetColumnSpan(grid, span);
        }

        public void NavigationPane_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationPane navigation = new NavigationPane(this);
            navigation.NavigationPane_Loaded(sender, e);
        }

        public double ExplorerWidth()
        {
            return ScreenGrid.ColumnDefinitions[1].Width.Value;
        }
    }
}
