using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Limilabs.Client.IMAP;
using Limilabs.Client.POP3;
using Limilabs.Client.SMTP;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using Limilabs.Mail.Fluent;
using Limilabs.Mail.Headers;
using System.IO;

namespace mail2dirr
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arg;
            arg = System.Environment.GetCommandLineArgs();
            string path = @"e:\!email-mikrotik\";
            for (int i = 0; i < arg.Length; i++)
            {
                if (arg[i] == "/dir")
                    if (!String.IsNullOrEmpty(arg[i + 1]))
                        path = arg[i + 1];

                if (arg[i] == "/?")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Использование: mail2dirr.exe /dir \"папка для вложений\" ");

                    return;
                }
            }

            //string path_base = @"e:\!email-mikrotik\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (path.Substring(path.Length - 1) != @"\") path += @"\";
            using (Pop3 pop3 = new Pop3())
            {
                pop3.Connect("pop.i.ua");       // or ConnectSSL for SSL
                pop3.UseBestLogin("udp404@i.ua", "dtnjxrfcbhtyb500");

                foreach (string uid in pop3.GetAll())
                {
                    IMail email = new MailBuilder()
                        .CreateFromEml(pop3.GetMessageByUID(uid));
                    //бесплатная библиотека мусорит сабжект письма
                    string filenam;
                    filenam = @email.Text.Replace("/", "-");
                    filenam = email.Text.Replace("\r\n", "");
                    // email.Save(@"e:\1111qa");
                    foreach (MimeData mime in email.Attachments)
                    {
                        mime.Save(path + mime.SafeFileName);
                        //   mime.Save(path + filenam+".txt");
                    }
                }
                //удаляем сообщения с сервера
                foreach (string uid in pop3.GetAll())
                {
                    pop3.DeleteMessageByUID(uid);
                }

                pop3.Close();
            }
            //ставим знак окончания приема писем
            path += "ok.ok";
            if (!File.Exists(path)) File.Create(path);
        }
    }
}
