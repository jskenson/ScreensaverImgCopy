using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ScreensaverImgCopy
{
    public class Log
    {
        /// <summary>
        /// 
        /// </summary>
        private string filePath;

        private DataSet LogData;

        private List<string> existsSFiles;

        public Log()
        {
            filePath = Path.Combine(Environment.CurrentDirectory, "log.xml");
            Init();
        }

        private static DataSet CreateNew()
        {
            DataTable dt = new DataTable("images");
            dt.Columns.Add("s", typeof(string));
            dt.Columns.Add("t", typeof(string));
            dt.Columns.Add("d", typeof(string));
            DataSet dataSet = new DataSet("Log");
            dataSet.Tables.Add(dt);
            return dataSet;
        }

        public void Save()
        {
            LogData.WriteXml(filePath, XmlWriteMode.IgnoreSchema);
        }

        private void Init()
        {
            existsSFiles = new List<string>();
            if (File.Exists(filePath))
            {
                LogData = new DataSet();
                LogData.ReadXml(filePath);
            }
            else
            {
                LogData = CreateNew();
                foreach (DataRow row in LogData.Tables[0].Rows)
                {
                    existsSFiles.Add(row["s"].ToString());
                }
            }
        }

        public bool Exists(string sourceName)
        {
            return existsSFiles.Contains(sourceName);
        }

        public void AddLog(string sFileName, string tFileName)
        {
            LogData.Tables[0].Rows.Add(sFileName, tFileName, DateTime.Now.ToString());
        }
    }
}
