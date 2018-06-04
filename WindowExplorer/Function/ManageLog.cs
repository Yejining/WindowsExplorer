using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WindowExplorer.Function
{
    class ManageLog
    {
        enum MODE { NORMAL = 0, BACKWORD, FORWORD };
        private static ManageLog manager;
        private List<string> logList = new List<string>();
        private int index = 0;

        public static ManageLog GetInstance()
        {
            if (manager == null) manager = new ManageLog();
            return manager;
        }

        public string PreviousLog()
        {
            if (index == 0)
                return string.Empty;

            if (index >= 1)
                index--;
            
            return logList[index];
        }

        public string LaterLog()
        {
            if (index != logList.Count - 1)
                index++;

            return logList[index];
        }

        public void AddLog(string path, int mode)
        {
            List<int> indexToDelete = new List<int>();

            if (mode == (int)MODE.NORMAL && (logList.Count == 0 ||index == logList.Count - 1))
            {
                logList.Add(path);
                index = logList.Count - 1;
                return;
            }
            else if (mode == (int)MODE.NORMAL && index != logList.Count - 1)
            {
                logList.Insert(index + 1, path);
                index++;
                logList.RemoveRange(index + 1, logList.Count - index - 1);
                // index - 1이 부모였을 경우
                //if (logList[index - 1] == Directory.GetParent(logList[index + 1]).ToString() && logList[index - 1] == Directory.GetParent(logList[index]).ToString())
                //{
                //    for (int delete = index + 1; delete < logList.Count; delete++)
                //    {
                //        if (logList[index - 1] == Directory.GetParent(logList[delete]).ToString())
                //            indexToDelete.Add(delete);
                //        else
                //            break;
                //    }

                //    for (int delete = indexToDelete.Count - 1; delete >= 0; delete--)
                //        logList.RemoveAt(delete);
                //}
            }

            if (IsRootDirectory(path))
                return;
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

        public List<string> LogList
        {
            get { return logList; }
        }

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    }
}
