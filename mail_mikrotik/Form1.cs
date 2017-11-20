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
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
            // ищем уникальніе имена
            string name_u=dt.Rows[0][0].ToString(),name_old = "";
            for(int i=0;i<dt.Rows.Count;i++)
            {
                name_u = dt.Rows[i][0].ToString();
                if (name_u != name_old)
                { name_old = name_u; label3.Text += name_old;}
            }
            // label1.Text = files[0].Name;
        }

    }
}
