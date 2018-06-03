using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using WindowExplorer.Main;
using WindowExplorer.GUI;
using WindowExplorer.Screen;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace WindowExplorer.Function
{
    class DirectoryInformation
    {
        MainScreen screen;
        public Explorer explorer;

        public DirectoryInformation(MainScreen screen)
        {
            this.screen = screen;
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
                    subFolderList.Add(new SubFileFolderVO { Name = directory.Name, SubIcon = icon, Path = content });
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
                    subFileList.Add(new SubFileFolderVO { Name = file.Name, SubIcon = icon, Path = content });
            }
            IOrderedEnumerable<SubFileFolderVO> orderedFile = subFileList.OrderBy(x => x.Name);

            subFolderList.AddRange(subFileList);

            return subFolderList;
        }

        public void SetButtonToExplorer(string path)
        {
            explorer = Explorer.GetInstance();

            List<SubFileFolderVO> subEntries = GetCurrentDirectory(path);
            List<ImageButton> buttons = new List<ImageButton>();

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
                row.MinHeight = 150;

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
            explorer.grid.Children.Clear();
            SetButtonToExplorer(path);
        }
    }
}
