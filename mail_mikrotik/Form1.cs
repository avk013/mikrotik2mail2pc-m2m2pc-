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
            using (Pop3 pop3 = new Pop3())
            {
                pop3.Connect("pop.i.ua");       // or ConnectSSL for SSL
                pop3.UseBestLogin("udp404@i.ua", "dtnjxrfcbhtyb500");

                foreach (string uid in pop3.GetAll())
                {
                    IMail email = new MailBuilder()
                        .CreateFromEml(pop3.GetMessageByUID(uid));
                    //бесплатная библиотека мусорит сабжект письма
                    // - приговор: Не использовать "Тему"
                    string subj = email.Subject.Replace(@"Please purchase Mail.dll license at https://www.limilabs.com/mail", "");
                    if (subj == "") subj = "subject";
                    textBox1.Text+= subj+Environment.NewLine;
                    // email.Text.Replace("/","-");
                    string filenam;
                    filenam = @email.Text.Replace("/", "-");
                    filenam = email.Text.Replace("\r\n", "");
                    textBox2.Text += filenam;
                    

                    // email.Save(@"e:\1111qa");
                    foreach (MimeData mime in email.Attachments)
                    {
                          mime.Save(path+mime.SafeFileName);
                     //   mime.Save(path + filenam+".txt");
                    }
                }
                //удаляем сообщения с сервера
                foreach (string uid in pop3.GetAll())
            {
                //pop3.DeleteMessageByUID(uid);
            }

                pop3.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
