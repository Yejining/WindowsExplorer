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

using WindowExplorer.GUI;

namespace WindowExplorer.Screen
{
    /// <summary>
    /// AddressBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddressBar : UserControl
    {
        private static AddressBar address;

        public static AddressBar GetInstance()
        {
            if (address == null) address = new AddressBar();
            return address;
        }

        public AddressBar()
        {
            InitializeComponent();
        }
    }
}
