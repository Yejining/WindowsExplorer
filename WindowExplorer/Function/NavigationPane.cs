using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WindowExplorer.Main;

namespace WindowExplorer.Function
{
    class NavigationPane
    {
        private MainScreen screen;

        public NavigationPane(MainScreen screen)
        {
            this.screen = screen;
        }

        public void NavigationPane_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string drive in Directory.GetLogicalDrives())
            {
                // create stack panel
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                // create Image
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(@"\Images\aaa.png", UriKind.Relative));
                image.Width = 16;
                image.Height = 16;

                // Add into stack
                stack.Children.Add(image);

                //// assign stack to header
                //item.Header = stack;
                //return item;

                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(@"\Images\aaa.png", UriKind.Relative));


                TreeViewItem item = new TreeViewItem()
                {
                    Header = $"로컬 디스크 ({drive.TrimEnd('\\')})",
                    Tag = drive
                };
                
                item.Items.Add(null);
                item.Expanded += FolderExpanded;
                screen.FolderView.Items.Add(item);
            }
        }

        public void FolderExpanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            // item이 dummy data를 가지고 있을 경우
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear dummy data
            item.Items.Clear();

            // Get full path
            string fullPath = (string)item.Tag;

            // 하위 디렉토리를 위한 리스트 생성
            List<string> directories = new List<string>();

            string[] subDirectoryPath = Directory.GetDirectories(fullPath);
            if (subDirectoryPath.Length > 0)
                directories.AddRange(subDirectoryPath);

            // 모든 디렉토리마다
            directories.ForEach(directoryPath =>
            {
                DirectoryInfo information = new DirectoryInfo(directoryPath);
                if ((information.Attributes & FileAttributes.System) == FileAttributes.System)
                    return;

                TreeViewItem subItem = new TreeViewItem()
                {
                    // 폴더 이름
                    Header = GetFileFolderName(directoryPath),
                    // 폴더 경로
                    Tag = directoryPath
                };

                // 폴더를 열 수 있도록 subItem에 dummy item 추가
                if (IsHavingSubDirectory(directoryPath))
                {
                    subItem.Items.Add(null);
                    subItem.Expanded += FolderExpanded;
                }

                item.Items.Add(subItem);
            });
        }

        public bool IsHavingSubDirectory(string path)
        {
            if (Directory.GetDirectories(@path).Count() > 0)
                return true;
            else
                return false;
        }

        public string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            string normalizedPath = path.Replace('/', '\\');
            int lastIndex = path.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }
    }
}
