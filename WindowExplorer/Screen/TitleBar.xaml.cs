using System;
using System.Collections.Generic;
using System.IO;
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
using WindowExplorer.Main;
using WindowExplorer.GUI;
using System.Windows.Interop;
using System.Drawing;

namespace WindowExplorer.Screen
{
    /// <summary>
    /// TitleBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TitleBar : UserControl
    {
        private static TitleBar title;
        private MainWindow main;
        DirectoryInformation manager;

        public static TitleBar GetInstance()
        {
            if (title == null) title = new TitleBar();
            return title;
        }

        public TitleBar()
        {
            ImageBrush brush;

            InitializeComponent();

            brush = GetImageBrush(@"\..\..\Images\kill_normal.png");
            minimize_button.Background = brush;

            brush = GetImageBrush(@"\..\..\Images\small_normal.png");
            size_button.Background = brush;

            brush = GetImageBrush(@"\..\..\Images\close_normal.png");
            close_button.Background = brush;
        }

        public void SetTitle()
        {
            manager = DirectoryInformation.GetInstance();
            string title = manager.CurrentPath;

            if (!IsRootDirectory(manager.CurrentPath))
                title = title.Remove(0, title.LastIndexOf('\\') + 1);

            folder_label.Content = title;
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

        private void Icon_button_MouseEnter(object sender, MouseEventArgs e)
        {
            icon_button.Background = GettimgFolderIcon.GetSmallIcon(DirectoryInformation.GetInstance().CurrentPath);
        }

        private void Icon_button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            main.Close();
        }

        public void PassMain(MainWindow main)
        {
            this.main = main;
        }

        private void Minimize_button_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\kill_hover.png");
            minimize_button.Background = brush;
        }

        private void Minimize_button_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\kill_normal.png");
            minimize_button.Background = brush;
        }

        private void Minimize_button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\kill_down.png");
            minimize_button.Background = brush;
        }

        private void Minimize_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\kill_hover.png");
            minimize_button.Background = brush;

            // 기능
        }

        private void Size_button_MouseEnter(object sender, MouseEventArgs e)
        {
            if ((int)size_button.Tag == 0)
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\small_hover.png");
                size_button.Background = brush;
            }
            else
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\big_hover.png");
                size_button.Background = brush;
            }
        }

        private void Size_button_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((int)size_button.Tag == 0)
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\small_normal.png");
                size_button.Background = brush;
            }
            else
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\big_normal.png");
                size_button.Background = brush;
            }
        }

        private void Size_button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if ((int)size_button.Tag == 0)
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\small_down.png");
                size_button.Background = brush;
            }
            else
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\big_down.png");
                size_button.Background = brush;
            }
        }

        private void Size_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((int)size_button.Tag == 0)
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\big_normal.png");
                size_button.Background = brush;
                size_button.Tag = 1;
            }
            else
            {
                ImageBrush brush = GetImageBrush(@"\..\..\Images\small_normal.png");
                size_button.Background = brush;
                size_button.Tag = 0;
            }
        }

        private void Close_button_MouseEnter(object sender, MouseEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\close_hover.png");
            close_button.Background = brush;
        }

        private void Close_button_MouseLeave(object sender, MouseEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\close_normal.png");
            close_button.Background = brush;
        }

        private void Close_button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ImageBrush brush = GetImageBrush(@"\..\..\Images\close_down.png");
            close_button.Background = brush;
        }

        private void Close_button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            main.Close();
        }

        private ImageBrush GetImageBrush(string path)
        {
            path = System.Windows.Forms.Application.StartupPath + path;
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            Bitmap bitmap = new Bitmap(image);
            BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageBrush brush = new ImageBrush(source);

            return brush;
        }
    }
}
