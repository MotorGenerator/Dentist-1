using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormЛьготник льготник = new FormЛьготник();
            //льготник.MdiParent = this;
            льготник.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormДоговор договор = new FormДоговор();
            договор.Show();
            this.Close();
        }
    }
}