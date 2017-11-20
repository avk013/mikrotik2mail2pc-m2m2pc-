using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace mail_mikrotik
{
    public partial class Form1 : Form
    {
        string path_log = @"E:\!email-mikrotik\";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {         
            string path_m = @"E:\!Source\Repos\mail_mikrotik\console_mail2dir\mail2dirr\mail2dirr\bin\Debug\mail2dirr.exe";
            if(File.Exists(path_m))
            { var startInfo = new System.Diagnostics.ProcessStartInfo
            { FileName = path_m,// + @" /dir "+@path_log,  // Путь к приложению
            Arguments= @" /dir " + @path_log,
                UseShellExecute = false,
                CreateNoWindow = true};
            System.Diagnostics.Process.Start(startInfo);
                label2.Text = DateTime.Now.ToString()+"_OK";
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(@path_log);
            System.IO.DirectoryInfo[] dirs = info.GetDirectories();
            System.IO.FileInfo[] files = info.GetFiles();
             //foreach (string s in files.name) label1.Text += s;
             for(int i=0; i<files.Length;i++) textBox2.Text+=files[i].Name+Environment.NewLine;
            // label1.Text = files[0].Name;
        }

    }
}
