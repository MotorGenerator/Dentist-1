using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormSetDate : Form
    {
        private DateTime dt;

        /// <summary>
        /// Хранит выбранную дату
        /// </summary>
        public DateTime DateTimeValue
        {
            get
            {
                return dt;
            }
            set
            {
                dt = value;
            }
        }

        public FormSetDate()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DateTimeValue = this.dateTimePicker1.Value;
        }

        private void FormSetDate_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}