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
        public void move2path(string name_old,string path)
        {    //откуда копируем
            string Dir1 = path_log;
            //куда копируем
            string Dir2 = path_log + @"\" + name_old;
            if (!Directory.Exists(Dir2)) Directory.CreateDirectory(Dir2);
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Dir1);
                foreach (FileInfo file in dirInfo.GetFiles(name_old + "*.txt"))
                {
                    File.Move(file.FullName, Dir2 + "\\" + file.Name);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
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
            //создание таблицы с именами всех фыйлов, разделенных на столбцы по знаку "_"
            // name_date_time_uptimeHour_uptimeMinutes_uptimeSecund
            DataTable dt = new DataTable("tab0");
            int st = 0;
            //dt.Clear();
            //dt = new DataTable("tab0");
            DataColumn a0 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a1 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a2 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a3 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a4 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a5 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a6 = new DataColumn(st++.ToString(), typeof(String));
            // download_upload
            DataColumn a7 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a8 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a9 = new DataColumn(st++.ToString(), typeof(String));
            DataColumn a10 = new DataColumn(st++.ToString(), typeof(String));
            dt.Columns.AddRange(new DataColumn[] { a0, a1, a2, a3, a4, a5, a6, a7, a8, a9, a10 });

            string[] tab0Values = null;
            DataRow dr = null;
            /////////////////////////////////


            for (int i = 0; i < files.Length; i++)
            {
                textBox2.Text += files[i].Name + Environment.NewLine;
                tab0Values = files[i].Name.Split('_');
                dr = dt.NewRow();
                for (int ii = 0; ii < 6; ii++) { dr[ii] = tab0Values[ii]; }
                //считываем содержимое файла
                // name: 
                // driver-rx-byte:
                // tx-bytes:
                string[] readText = File.ReadAllLines(path_log+ files[i].Name);
                for(int ii=0;ii<readText.Length;ii++)
                { if(readText[ii].IndexOf("name:") >0) dr[6] = readText[ii];
                  if (readText[ii].IndexOf("driver-rx-byte:") > 0) dr[7] = readText[ii];
                  if (readText[ii].IndexOf("tx-bytes:") > 0) dr[8] = readText[ii];
                }


                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            // подумать если пусто
            if(dt.Rows.Count>0)
{//записываем текущие данные
string dates = DateTime.Now.ToString();
dates = dates.Replace(" ", "_");dates = dates.Replace(":", "-");dates = dates.Replace(".", "_");
label4.Text = dates; dt.WriteXml(path_log + @"out\" + dates + @"out.xml");
                // ищем уникальніе имена
                string name_u =dt.Rows[0][0].ToString(),name_old = "";
            for(int i=0;i<dt.Rows.Count;i++)
            {
                name_u = dt.Rows[i][0].ToString();
                if (name_u != name_old)
                { name_old = name_u; label3.Text += name_old;
                    //кидаем в соотвующую дирректорию
                    /////////////////////
                    //откуда копируем
                    string Dir1 = path_log;
                    //куда копируем
                    string Dir2 = path_log+@"\"+name_old;
                    if (!Directory.Exists(Dir2)) Directory.CreateDirectory(Dir2);
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Dir1);
                        foreach (FileInfo file in dirInfo.GetFiles(name_old+"*.txt"))
                        {
                            File.Move(file.FullName, Dir2 + "\\" + file.Name);
                        }
                    }
                    catch (Exception ex)
                    {MessageBox.Show(ex.Message);}
                    }

                    /////////////
                }
            }
            // label1.Text = files[0].Name;
        }

        private void button5_Click(object sender, EventArgs e)
        {//animation
            timer1.Enabled = true;
            // wait file "mail_OK"
            timer2.Enabled = true;
        }
        string zagruz = "загрузка................загрузка............загрузка......",za;
        int i = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            za = zagruz.Substring(i++, 20);
            //if (flag == 1) i++; else i--;
            if (i == 24) i=0;
            //if (i == 0) flag = 1;
            label3.Text= za;
            //if (label3.Text.Length > 32) label3.Text = "загрузка";
        }
    }
}
