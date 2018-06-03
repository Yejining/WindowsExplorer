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
using System.Windows.Forms;

namespace WindowExplorer.Screen
{
    /// <summary>
    /// Explorer.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Explorer : System.Windows.Controls.UserControl
    {
        private static Explorer explorer;
        
        public static Explorer GetInstance()
        {
            if (explorer == null) explorer = new Explorer();
            return explorer;
        }

        public Explorer()
        {
            InitializeComponent();
        }
    }
}
