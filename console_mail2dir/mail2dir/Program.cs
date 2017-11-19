using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace mail2dir
{
    class Program
    {
        static void Main(string[] args)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = @"E:\!Source\Repos\mail_mikrotik\console_mail2dir\mail2dir\bin\Debug\mail2dir.exe",  // Путь к приложению
                //UseShellExecute = false,
                //CreateNoWindow = true
            };

            System.Diagnostics.Process.Start(startInfo);

        }
    }
}
