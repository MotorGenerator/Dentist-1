using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Стамотология
{
    public partial class FormТеррОргн : Form
    {
        private string город = string.Empty;

        /// <summary>
        /// Свойство хранит наименование города
        /// </summary>
        public string Город
        {
            get
            {
                return город;
            }
            set
            {
                город = value;
            }
        }

        public FormТеррОргн()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
            {
                //Сохраним в свойстве название города
                this.Город = this.textBox1.Text.Trim();
            }
            else
            {
                MessageBox.Show("Введите название населённого пункта где расположен территориальный орган");
                return;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
            {
                this.button2.Enabled = true;
            }
            else
            {
                this.button2.Enabled = false;
            }
        }
    }
}