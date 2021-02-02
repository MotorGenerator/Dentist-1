using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Стамотология
{
    public partial class LgotnikReply : Form
    {        
        public string splits;
        public LgotnikReply()
        {
            InitializeComponent();
            this.Text = "Ответ по льготнику";
            groupBox1.Visible = false;

            //размер формы
            this.Width = 500;
            this.Height = 100;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LgotnikReply_Load(object sender, EventArgs e)
        {
            splits = label_reply.Text.ToString().Split(';')[0];
            label_reply.Text = "";

            List<Стамотология.Classes.Info> list = new List<Стамотология.Classes.Info>();

            //if (splits.Trim().ToLower() == "Льготник имеет право на получение услуги".Replace(";",string.Empty).Trim().ToLower())   // в конце добавляется пустая строка, поэтому ставим 2)
            if (splits.Trim().ToLower() == "1".Replace(";", string.Empty).Trim().ToLower())   // в конце добавляется пустая строка, поэтому ставим 2)
            {
                label_reply.Text = "Льготник имеет право на получение услуги";
                label_reply.ForeColor = Color.Green;
                label_reply.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                // label_reply.Location = new Point(60, 9);

                label_reply.Location = new Point(20, 20);

                ////записываем строковый массив в коллекцию                
                //for (int i = 1; i < splits.Length - 1; i++)
                //{
                //    Стамотология.Classes.Info unloading = new Стамотология.Classes.Info();
                //    unloading.List = splits[i];
                //    list.Add(unloading);
                //}

                //// формируем dataGridView
                //dataGridView1.DataSource = list;
                //dataGridView1.Columns["List"].HeaderText = "Сводка";
                //dataGridView1.Columns["List"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //dataGridView1.AllowUserToAddRows = false;    //запрет на добавление строк
                //dataGridView1.AllowUserToDeleteRows = false;      //запрет на удаление сторк
                //dataGridView1.ReadOnly = true;  //запрет на редактирование строк
                //// перенос в строке и выравнивание по высоте
                //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            else if (splits.Trim().ToLower() == "2".Replace(";", string.Empty).Trim().ToLower())
            {
                //dataGridView1.Visible = false;
                label_reply.Text = "Льготник требует уточнение"; //Льготник имеет право на получение услугиsplits;

                //цвет фона
                label_reply.ForeColor = Color.Red;

                //положение 
                label_reply.Location = new Point(20, 20);

                //размер формы
                this.Width = 500;
                this.Height = 100;

                //размер шрифта
                label_reply.Font = new Font("Microsoft Sans Serif", 13, FontStyle.Regular);
            }
            else if (splits.Trim().ToLower() == "3".Replace(";", string.Empty).Trim().ToLower())
            {
                //dataGridView1.Visible = false;
                label_reply.Text = "Обнаружена не корректная информация"; //Льготник имеет право на получение услугиsplits;

                //цвет фона
                label_reply.ForeColor = Color.Red;

                //положение 
                label_reply.Location = new Point(20, 20);

                //размер формы
                this.Width = 500;
                this.Height = 100;

                //размер шрифта
                label_reply.Font = new Font("Microsoft Sans Serif", 13, FontStyle.Regular);
            }
        }
    }
}