using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using ClassLibrary1;

namespace Стамотология
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void льготнаяКатегорияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormЛьготнаяКатегория fлк = new FormЛьготнаяКатегория();
            fлк.MdiParent = this;
            fлк.Show();
        }

        private void поликлинникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormПоликлинника fHospital = new FormПоликлинника();
            fHospital.MdiParent = this;
            fHospital.Show();
        }

        private void льготникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FormЛьготник льготник = new FormЛьготник();
            //льготник.MdiParent = this;
            //льготник.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //FormMenu menu = new FormMenu();
            //menu.MdiParent = this;
            //menu.Show();

            MainForm main = new MainForm();
            main.MdiParent = this;
            main.Show();
        }

        private void менюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu();
            menu.MdiParent = this;
            menu.Show();
        }

        private void asdasdasdToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                KeyHospital key = new KeyHospital();
                key.Flag = true;

                string fileBinaryName = "KeyDantist.k";
                FileStream fs = new FileStream(fileBinaryName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                BinaryFormatter bf = new BinaryFormatter();

                //сериализация
                bf.Serialize(fs, key);

                //Освободим в потоке все ресурсы
                fs.Dispose();
                fs.Close();
            }
            catch
            {

            }
        }

   
    }
}