using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowExplorer.Screen
{
    class ScreenShare
    {
        public Explorer explorer;
        public TitleBar title;
        public SettingBar setting;
        public AddressBar address;
        public TaskBar task;

        public ScreenShare()
        {
            explorer = Explorer.GetInstance();
            title = TitleBar.GetInstance();
            setting = SettingBar.GetInstance();
            address = AddressBar.GetInstance();
            task = TaskBar.GetInstance();
        }
    }
}
