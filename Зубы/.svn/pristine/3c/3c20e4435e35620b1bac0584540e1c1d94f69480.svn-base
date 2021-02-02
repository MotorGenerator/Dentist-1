using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormКалендарь : Form
    {
        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public FormКалендарь()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Date = this.dateTimePicker1.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}