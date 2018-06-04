using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WindowExplorer.Function;

namespace WindowExplorer.Screen
{
    /// <summary>
    /// AddressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddressBar : System.Windows.Controls.UserControl
    {
        private static AddressBar address;
        DirectoryInformation manager = DirectoryInformation.GetInstance();
        ManageLog log = ManageLog.GetInstance();

        public static AddressBar GetInstance()
        {
            if (address == null) address = new AddressBar();
            return address;
        }

        public AddressBar()
        {
            InitializeComponent();
        }

        private void Back_button_Click(object sender, RoutedEventArgs e)
        {
            string path = log.PreviousLog();

            if (path.Length == 0)
                return;

            log.AddLog(path, 1);
            manager.SetButtonToExplorer(path);
            SetPath();
        }

        private void Forward_button_Click(object sender, RoutedEventArgs e)
        {
            string path = log.LaterLog();

            if (path.Length == 0)
                return;
            
            manager.SetButtonToExplorer(path);
            SetPath();
        }

        private void Up_button_Click(object sender, RoutedEventArgs e)
        {
            string path = manager.CurrentPath;

            if (IsRootDirectory(path))
                return;

            log.AddLog(Directory.GetParent(path).ToString(), 0);
            manager.SetButtonToExplorer(Directory.GetParent(path).ToString());
            SetPath();
        }

        public bool IsRootDirectory(string path)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (path.ToLower() == drive.ToString().ToLower())
                    return true;
            }
            return false;
        }

        private void Refresh_button_Click(object sender, RoutedEventArgs e)
        {
            string path = manager.CurrentPath;
            manager.SetButtonToExplorer(path);
        }

        public void SetPath()
        {
            string title = manager.CurrentPath;

            if (!IsRootDirectory(manager.CurrentPath))
                title = title.Remove(0, title.LastIndexOf('\\') + 1);

            address_bar.Text = manager.CurrentPath;
            search.Content = $"{title} 검색";
        }

        private void Address_bar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            address_bar.SelectAll();
        }

        private void Address_bar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string path = address_bar.Text;
                path = path.Replace('\\', '/');

                if (!Directory.Exists(System.IO.Path.Combine(address_bar.Text)) && !Regex.IsMatch(path, "/"))
                {
                    System.Windows.Forms.MessageBox.Show($"\'{address_bar.Text}\'을(를) 찾을 수 없습니다. 맞춤법을 확인하고 다시 시도하십시오.", 
                        "  파일 탐색기", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    address_bar.SelectAll();
                }
                else if (!Directory.Exists(System.IO.Path.Combine(address_bar.Text)))
                {
                    address_bar.SelectAll();
                }
                else
                {
                    log.AddLog(address_bar.Text, 0);
                    manager.SetButtonToExplorer(address_bar.Text);
                    SetPath();
                }
            }
        }

        private void log_button_Click(object sender, RoutedEventArgs e)
        {
            List<String> logs = log.LogList;
            string a = "";
            foreach (string llog in logs)
            {
                a += $"{llog}\n";
            }
            a += log.Index;

            System.Windows.Forms.MessageBox.Show(a);
        }
    }
}
