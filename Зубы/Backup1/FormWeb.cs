using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormWeb : Form
    {
        public FormWeb()
        {
            InitializeComponent();
        }

        private void FormWeb_Load(object sender, EventArgs e)
        {
            // Сохраним файл на диск.
            //SaveFileDialog sfd = new SaveFileDialog();
            //if (sfd.ShowDialog() == DialogResult.OK)
            //{
                //string file = sfd.FileName + ".html";

                StreamWriter streamwriter = new StreamWriter(@"C:\index.html");
                streamwriter.WriteLine("<html>");
                streamwriter.WriteLine("<head>");
                streamwriter.WriteLine("  <title>HTML-Document</title>");
                streamwriter.WriteLine("  <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
                streamwriter.WriteLine("</head>");
                streamwriter.WriteLine("<body>");
                streamwriter.WriteLine("Содержимое документа Word");
                streamwriter.WriteLine("</body>");
                streamwriter.WriteLine("</html>");
                streamwriter.Close();
            //}

            FileStream fs = new FileStream(@"C:\index.html", FileMode.OpenOrCreate);
            webBrowser1.DocumentStream = fs;
        }
    }
}