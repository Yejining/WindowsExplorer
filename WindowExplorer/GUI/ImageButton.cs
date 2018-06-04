using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WindowExplorer.Function;

namespace WindowExplorer.GUI
{
    class ImageButton : Button
    {
        private SubFileFolderVO subEntry;
        private System.Windows.Controls.Image image;
        private TextBox name;
        private string fullPath;

        public ImageButton(SubFileFolderVO subEntry, int index)
        {
            this.subEntry = subEntry;
            Init();
            this.AddChild(CreateStackPanel());
            this.Tag = index;
        }

        private void Init() 
        {
            BrushConverter converter = new BrushConverter();

            image = GetImageFromIcon(subEntry.SubIcon); // 이미지 적용
            name = GetTextBox(subEntry.Name); // 파일/폴더명 적용
            fullPath = subEntry.Path;
            this.Background = System.Windows.Media.Brushes.Transparent;
            this.BorderThickness = new Thickness(0);
            this.BorderBrush = (System.Windows.Media.Brush)converter.ConvertFromString("#99D1FF");
            this.Height = 170;
            this.Width = 100;
        }

        private StackPanel CreateStackPanel()
        {
            StackPanel stack = new StackPanel();
            stack.Height = 170;
            stack.Width = 100;
            stack.Children.Add(image);
            stack.Children.Add(name);
            return stack;
        }

        private System.Windows.Controls.Image GetImageFromIcon(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            System.Windows.Controls.Image convertedImage = new System.Windows.Controls.Image();
            convertedImage.Source= imageSource;
            convertedImage.Height = 90;
            convertedImage.Width = 70;

            if (subEntry.IsHidden)
                convertedImage.Opacity = 0.5;

            return convertedImage;
        }

        private TextBox GetTextBox(string name)
        {
            TextBox box = new TextBox();
            
            box.MaxWidth = 85;
            box.MaxHeight = 70;
            box.Text = name;

            Typeface typeface = new Typeface(box.FontFamily, box.FontStyle, box.FontWeight, box.FontStretch);
            FormattedText ft = new FormattedText(box.Text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, box.FontSize, System.Windows.Media.Brushes.Black);

            while (ft.Width > 300)
            {
                box.Text = box.Text.Remove(box.Text.Length - 6);
                box.Text = box.Text.Insert(box.Text.Length, "...");
                ft = new FormattedText(box.Text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface, box.FontSize, System.Windows.Media.Brushes.Black);
            }

            if (ft.Width < 77)
                box.Height = 20;
            else if (ft.Width < 160)
                box.Height = 35;
            else if (ft.Width < 250)
                box.Height = 60;
            else
                box.Height = box.MaxHeight;

            box.BorderThickness = new Thickness(0);
            box.HorizontalContentAlignment = HorizontalAlignment.Center;
            box.Focusable = false;
            box.TextWrapping = TextWrapping.Wrap;
            box.Background = System.Windows.Media.Brushes.Transparent;

            return box;
        }

        public string FullPath
        {
            get { return fullPath; }
        }
    }
}
