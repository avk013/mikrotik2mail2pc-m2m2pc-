using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Limilabs.Client.IMAP;
using Limilabs.Client.POP3;
using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Limilabs.Mail.Fluent;
using Limilabs.Mail.Headers;

namespace mail_mikrotik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"e:\!email-mikrotik\";
            string path_m = @"E:\!Source\Repos\mail_mikrotik\console_mail2dir\mail2dir\bin\Debug\mail2dir.exe";
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = path_m,  // Путь к приложению
                UseShellExecute = false,
                CreateNoWindow = true
            };
            System.Diagnostics.Process.Start(startInfo);


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
