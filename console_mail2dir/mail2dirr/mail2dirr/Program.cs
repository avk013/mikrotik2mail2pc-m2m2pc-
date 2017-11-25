using System;
using Limilabs.Client.POP3;
using Limilabs.Mail;
using Limilabs.Mail.MIME;
using System.IO;
//получение писем логов микротика с почтового сервера по попу
namespace mail2dirr
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arg;
            arg = System.Environment.GetCommandLineArgs();
            //если не будет аргумента то запись вложений в папку...
            string path=@"e:\!email-mikrotik\";
            for (int i = 0; i < arg.Length; i++)
            {
                if (arg[i] == "/dir")
                    if (!String.IsNullOrEmpty(arg[i + 1]))
                        path = arg[i + 1];
//если запускают с вопросом утилиту....
if (arg[i] == "/?")              
{Console.WriteLine("");              
Console.WriteLine("Использование: mail2dirr.exe /dir \"папка для вложений\" ");
                return;}}
            
            //string path_base = @"e:\!email-mikrotik\";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            //если каталог указан без слеша, дописываем его
            if (path.Substring(path.Length - 1) != @"\") path += @"\";
                using (Pop3 pop3 = new Pop3())
            {
                pop3.Connect("pop.i.ua");       // or ConnectSSL for SSL
                pop3.UseBestLogin("udp@i.ua", "dtnjxrfcbhtyb");

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
            path+= "ok.ok";
            if (!File.Exists(path)) File.Create(path);
        }
    }
}
