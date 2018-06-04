using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WindowExplorer.GUI;

namespace WindowExplorer.Screen
{
    /// <summary>
    /// SettingBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingBar : System.Windows.Controls.UserControl
    {
        private static SettingBar setting;

        public static SettingBar GetInstance()
        {
            if (setting == null) setting = new SettingBar();
            return setting;
        }

        public SettingBar()
        {
            InitializeComponent();
            string path = System.Windows.Forms.Application.StartupPath + @"\..\..\Images\ico24.ico";
            Icon icon = Icon.ExtractAssociatedIcon(path);
            help_button.Background = GettimgFolderIcon.IconToImageBrush(icon);
        }

        private void Help_button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.co.kr/search?q=windows+10%EC%9D%98+%ED%8C%8C%EC%9D%BC+%ED%83%90%EC%83%89%EA%B8%B0%EC%97%90+%EB%8C%80%ED%95%9C+%EB%8F%84%EC%9B%80%EB%A7%90+%EB%B3%B4%EA%B8%B0&oq=windows+10%EC%9D%98+%ED%8C%8C%EC%9D%BC+%ED%83%90%EC%83%89%EA%B8%B0%EC%97%90+%EB%8C%80%ED%95%9C+%EB%8F%84%EC%9B%80%EB%A7%90+%EB%B3%B4%EA%B8%B0&aqs=chrome..69i57.820j0j7&sourceid=chrome&ie=UTF-8");
        }
    }
}
