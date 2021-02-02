using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormPassword : Form
    {
        /// <summary>
        /// Прошол проверку или нет.
        /// </summary>
        private bool flag = false;

        public bool FlagValidate
        {
            get
            {
                return flag;
            }
            set
            {
                flag = value;
            }
        }

        public FormPassword()
        {
            InitializeComponent();
        }

        private void FormPassword_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string password = "12A86Asd";

            if (password == this.passWord.Text)
            {
                FlagValidate = true;
            }
        }
    }
}