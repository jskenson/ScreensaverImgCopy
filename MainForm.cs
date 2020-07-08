using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreensaverImgCopy
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// 屏保图片文件夹
        /// </summary>
        private string sourceDirectory;
        /// <summary>
        /// 复制的目标文件夹
        /// </summary>
        private string targetDirectory;

        private Log log;

        private int CopyIndex = 1;

        private string GetSourceDirectory()
        {
            string directoryName1 = $@"C:\Users\{Environment.UserName}\AppData\Local\Packages\";
            string directoryName2 = Directory.GetDirectories(directoryName1, "Microsoft.Windows.ContentDeliveryManager_*")[0];
            return directoryName2 + @"\LocalState\Assets";
        }

        private string GetTargetDirectory()
        {
            return Environment.CurrentDirectory;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            sourceDirectory = GetSourceDirectory();
            targetDirectory = GetTargetDirectory();
            log = new Log();
            Copy();
        }

        private void Copy()
        {
            string[] sFiles = Directory.GetFiles(sourceDirectory);
            foreach (string s in sFiles)
            {
                string sFileName = Path.GetFileName(s);
                if (!log.Exists(sFileName))
                {
                    string newFileName = $"{DateTime.Now.ToString("yyyyMMdd")}-{CopyIndex++}.jpg";
                    Copy(s, Path.Combine(targetDirectory, newFileName));
                    log.AddLog(sFileName, newFileName);
                }
            }
            log.Save();
            label1.Text = $"完成：共拷贝{CopyIndex-1}张图片";
        }

        private void Copy(string sourceFile, string targetFile)
        {
            File.Copy(sourceFile, targetFile);
        }
    }
}
