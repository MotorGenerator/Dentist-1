using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;

namespace Стамотология
{
    public partial class FormВрач : Form
    {
        // Флаг указывает в каком режиме работает форма.
        private bool flagInsert = true;

        // Переменная для хранения id врача.
        private int Id = 0;

        public FormВрач()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.flagInsert == true)
            {
                if (this.txtФамилия.Text.Length > 0)
                {
                    try
                    {
                        InsertВрач iv = new InsertВрач(this.txtФамилия.Text.Trim());
                        iv.Execute();

                        MessageBox.Show("Данные записаны");

                        ClearTextEditForm();

                        Display();

                        //string query = "select * from Врач";
                        //DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "Врачи");

                        //this.dataGridView1.DataSource = tab;
                        //this.dataGridView1.Columns["id_врач"].Visible = false;
                        //this.dataGridView1.Columns["Фамилия"].Visible = true;
                        //this.dataGridView1.Columns["Имя"].Visible = true;
                        //this.dataGridView1.Columns["Отчество"].Visible = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.Trim(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните Фамилию и Имя врача");
                }
            }
        }

        private void Display()
        {
            string query = "select * from Врач";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "Врачи");

            this.dataGridView1.DataSource = null;

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_врач"].Visible = false;
            this.dataGridView1.Columns["ФИО"].Visible = true;
            //this.dataGridView1.Columns["Имя"].Visible = true;
            //this.dataGridView1.Columns["Отчество"].Visible = true;
        }

        private void ClearTextEditForm()
        {
            this.txtФамилия.Text = string.Empty;
            //this.txtИмя.Text = string.Empty;
            //this.txtОтчество.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flagInsert == false)
            {
                string query = "update Врач " +
                               "set ФИО = '" + txtФамилия.Text.Trim() + "' " +
                               "where id_врач = " + this.Id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                // Отобразим внесённые изменения.
                Display();

                ClearTextEditForm();

                flagInsert = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (flagInsert == false)
            {

                DialogResult rezult = MessageBox.Show("Удалить запись", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                if (rezult == DialogResult.OK)
                {
                    string query = "delete from Врач where id_врач = " + this.Id + " ";

                    Query.Execute(query, ConnectionDB.ConnectionString());

                    Display();

                    ClearTextEditForm();

                    flagInsert = true;
                }


            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_врач"].Value);

            this.txtФамилия.Text = this.dataGridView1.CurrentRow.Cells["ФИО"].Value.ToString().Trim();
            //this.txtИмя.Text = this.dataGridView1.CurrentRow.Cells["Имя"].Value.ToString().Trim();
            //this.txtОтчество.Text = this.dataGridView1.CurrentRow.Cells["Отчество"].Value.ToString().Trim();

            // Переведём работу формы в режим редактирования.
            this.flagInsert = false;
        }

        private void FormВрач_Load(object sender, EventArgs e)
        {
            flagInsert = true;

            Display();
        }

    }
}