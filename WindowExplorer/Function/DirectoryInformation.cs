using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Diagnostics;

using WindowExplorer.Main;
using WindowExplorer.GUI;
using WindowExplorer.Screen;

namespace WindowExplorer.Function
{
    class DirectoryInformation
    {
        ManageLog manager;
        public Explorer explorer;
        public TaskBar taskBar;
        private static DirectoryInformation setting;
        private string currentPath;

        public static DirectoryInformation GetInstance()
        {
            if (setting == null) setting = new DirectoryInformation();
            return setting;
        }

        public List<SubFileFolderVO> GetCurrentDirectory(string path)
        {
            path = Path.GetFullPath(path); 
            return GetSubEntries(path);
        }

        public List<SubFileFolderVO> GetSubEntries(string path)
        {
            List<SubFileFolderVO> subFolderList = new List<SubFileFolderVO>();
            List<SubFileFolderVO> subFileList = new List<SubFileFolderVO>();
            DirectoryInfo directory;
            FileInfo file;
            string content;

            // 하위 폴더
            string[] folders = Directory.GetDirectories(path);
            foreach (string subFolder in folders)
            {
                content = Path.Combine(path, subFolder);
                directory = new DirectoryInfo(content);
                Icon icon;
                if ((directory.Attributes & FileAttributes.System) != FileAttributes.System)
                {
                    icon = GUI.GettimgFolderIcon.GetIcon(directory.Name, content);
                    if ((directory.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        subFolderList.Add(new SubFileFolderVO { Name = directory.Name, SubIcon = icon, Path = content, IsHidden = true });
                    else
                        subFolderList.Add(new SubFileFolderVO { Name = directory.Name, SubIcon = icon, Path = content, IsHidden = false });
                }
            }
            IOrderedEnumerable<SubFileFolderVO> orderedFolder = subFolderList.OrderBy(x => x.Name);

            // 하위 파일
            string[] files = Directory.GetFiles(path);
            foreach (string subFile in files)
            {
                content = Path.Combine(path, subFile);
                file = new FileInfo(content);
                Icon icon = Icon.ExtractAssociatedIcon(content);
                if ((file.Attributes & FileAttributes.System) != FileAttributes.System)
                {
                    if ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        subFolderList.Add(new SubFileFolderVO { Name = file.Name, SubIcon = icon, Path = content, IsHidden = true });
                    else
                        subFolderList.Add(new SubFileFolderVO { Name = file.Name, SubIcon = icon, Path = content, IsHidden = false });
                }
            }
            IOrderedEnumerable<SubFileFolderVO> orderedFile = subFileList.OrderBy(x => x.Name);

            subFolderList.AddRange(subFileList);

            return subFolderList;
        }

        public void SetButtonToExplorer(string path)
        {
            currentPath = path;

            TitleBar titleBar = TitleBar.GetInstance();
            titleBar.icon_button.Background = GettimgFolderIcon.GetSmallIcon(path);
            titleBar.SetTitle();

            explorer = Explorer.GetInstance();
            taskBar = TaskBar.GetInstance();
            explorer.grid.Children.Clear();

            List<SubFileFolderVO> subEntries = GetCurrentDirectory(path);
            List<ImageButton> buttons = new List<ImageButton>();

            taskBar.SetCount($"{subEntries.Count}개 항목");

            if (subEntries.Count == 0)
            {
                Label label = new Label();
                label.Content = "이 폴더는 비어있습니다.";
                label.HorizontalAlignment = HorizontalAlignment.Center;
                explorer.grid.Children.Add(label);
                Grid.SetColumnSpan(label, 5);
            }

            int index = 0;
            foreach (SubFileFolderVO entry in subEntries)
            {
                buttons.Add(new ImageButton(entry, index));
                index++;
            }

            int columnIndex = 0;
            int rowIndex = 0;

            foreach (ImageButton button in buttons)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.MinWidth = 120;

                RowDefinition row = new RowDefinition();
                row.MinHeight = 170;

                explorer.grid.Children.Add(button);

                button.Click += (object sndr, RoutedEventArgs m_args)
                    => OnClick(sndr, m_args, buttons, (int)button.Tag);
                button.MouseDoubleClick += (object sndr, MouseButtonEventArgs m_args)
                    => DoubleClick(sndr, m_args, button.FullPath);

                if (120 * (columnIndex) < 500)
                {
                    explorer.grid.ColumnDefinitions.Add(column);
                    Grid.SetRow(button, rowIndex);
                    Grid.SetColumn(button, columnIndex);
                    columnIndex++;
                }
                else
                {
                    explorer.grid.RowDefinitions.Add(row);
                    Grid.SetRow(button, rowIndex);
                    Grid.SetColumn(button, columnIndex);
                    rowIndex++;
                    columnIndex = 0;
                }
            }
        }

        private void OnClick(object sender, RoutedEventArgs e, List<ImageButton> buttons, int tag)
        {
            BrushConverter converter = new BrushConverter();

            foreach (ImageButton button in buttons)
            {
                if ((int)button.Tag != tag)
                    button.Background = System.Windows.Media.Brushes.Transparent;
                else
                    button.Background = (System.Windows.Media.Brush)converter.ConvertFromString("#CCF0FF");
            }
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e, string path)
        {
            if (File.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                manager = ManageLog.GetInstance();
                AddressBar addressBar = AddressBar.GetInstance();
                explorer.grid.Children.Clear();
                manager.AddLog(path, 0);
                SetButtonToExplorer(path);
                addressBar.SetPath();
            }
        }

        public string CurrentPath
        {
            get { return currentPath; }
        }
    }
}
