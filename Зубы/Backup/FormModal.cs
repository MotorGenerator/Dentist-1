using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormModal : Form
    {
        private string _фио;

        /// <summary>
        /// Хранит имя удаляемого льготника
        /// </summary>
        public string ФИО
        {
            get
            {
                return _фио;
            }
            set
            {
                _фио = value;
            }
        }

        public FormModal()
        {
            InitializeComponent();
        }

        private void FormModal_Load(object sender, EventArgs e)
        {
            this.label1.Text = "Внимание! При удалении льготника " + this.ФИО + " удаляются договора и акты";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}