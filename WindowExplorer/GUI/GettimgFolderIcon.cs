using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WindowExplorer.GUI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public IntPtr iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    class GettimgFolderIcon
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0;    // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1;    // 'Small icon

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath,
                                    uint dwFileAttributes,
                                    ref SHFILEINFO psfi,
                                    uint cbSizeFileInfo,
                                    uint uFlags);

        public static Icon GetIcon(string fName, string path)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgLarge;

            hImgLarge = SHGetFileInfo(path, 0,
                ref shinfo, (uint)Marshal.SizeOf(shinfo),
                GettimgFolderIcon.SHGFI_ICON | GettimgFolderIcon.SHGFI_LARGEICON);

            Icon myIcon = Icon.FromHandle(shinfo.hIcon);
            return myIcon;
        }

        public static ImageBrush GetSmallIcon(string name)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            IntPtr hImgSmall;

            hImgSmall = SHGetFileInfo(name, 0, ref shinfo,
                                  (uint)Marshal.SizeOf(shinfo),
                                   SHGFI_ICON |
                                   SHGFI_SMALLICON);

            // icon
            Icon myIcon = Icon.FromHandle(shinfo.hIcon);
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(myIcon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

            // image
            System.Windows.Controls.Image convertedImage = new System.Windows.Controls.Image();
            convertedImage.Source = imageSource;
            convertedImage.Height = 15;
            convertedImage.Width = 15;

            // image sourc to brush
            ImageBrush imageBrush = new ImageBrush(imageSource);

            return imageBrush;
        }

        public static ImageBrush IconToImageBrush(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            ImageBrush imageBrush = new ImageBrush(imageSource);

            return imageBrush;
        }
    }
}
