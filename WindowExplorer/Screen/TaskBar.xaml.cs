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

namespace WindowExplorer.Screen
{
    /// <summary>
    /// TaskBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TaskBar : UserControl
    {
        private static TaskBar task;

        public static TaskBar GetInstance()
        {
            if (task == null) task = new TaskBar();
            return task;
        }

        public TaskBar()
        {
            InitializeComponent();
        }
    }
}
