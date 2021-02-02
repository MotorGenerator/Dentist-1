using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Стамотология.Classes;

namespace Стамотология
{
    public partial class FormБухгалтер : Form
    {
        public FormБухгалтер()
        {
            InitializeComponent();
        }

        private void FormБухгалтер_Load(object sender, EventArgs e)
        {
            //Загрузим данные по врачам
            string query = "select * from ГлавБух";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавВрач");

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_главБух"].Visible = false;
            this.dataGridView1.Columns["ФИО_ГлавБухПадеж"].Visible = false;
            this.dataGridView1.Columns["Должность"].Visible = false;
            this.dataGridView1.Columns["ДолжностьРодПадеж"].Visible = false;
            this.dataGridView1.Columns["Основание"].Visible = false;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_главБух"].Value != DBNull.Value)
            {
                int id = 0;
                try { id=Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_главБух"].Value); }
                catch
                {
                    MessageBox.Show("Нет выбранного поля.", "Ошибка");
                    return;
                }
                string query = "select * from ГлавБух where id_главБух = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавБух").Rows[0];

                this.textBox1.Text = row["ФИО_ГлавБух"].ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
            {
                //InsertEsculap esculap = new InsertEsculap(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                InsertБухгалтер bux = new InsertБухгалтер(this.textBox1.Text);
                Receiver rec = new Receiver();

                rec.Action(bux);

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ГлавБух";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавБух");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_главБух"].Visible = false;
                this.dataGridView1.Columns["ФИО_ГлавБухПадеж"].Visible = false;
                this.dataGridView1.Columns["Должность"].Visible = false;
                this.dataGridView1.Columns["ДолжностьРодПадеж"].Visible = false;
                this.dataGridView1.Columns["Основание"].Visible = false;


                //Обнулим поля редактирования
                this.textBox1.Text = "";

            }
            else
            {
                MessageBox.Show("Поле ФИО обязательно к заполнению");
                this.textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" )
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_главБух"].Value);

                UpdateБухгалтер bux = new UpdateБухгалтер(id, this.textBox1.Text);
                Receiver rec = new Receiver();

                rec.Action(bux);

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ГлавБух";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавБух");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_главБух"].Visible = false;
                this.dataGridView1.Columns["ФИО_ГлавБухПадеж"].Visible = false;
                this.dataGridView1.Columns["Должность"].Visible = false;
                this.dataGridView1.Columns["ДолжностьРодПадеж"].Visible = false;
                this.dataGridView1.Columns["Основание"].Visible = false;


                //Обнулим поля редактирования
                this.textBox1.Text = "";

            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению");
                this.textBox1.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dtCount = (DataTable)this.dataGridView1.DataSource;

            if (dtCount.Rows.Count != 0)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_главБух"].Value);
                string query = "delete from ГлавБух where id_главБух = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string queryUp = "select * from ГлавБух";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "ГлавБух");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_главБух"].Visible = false;
                this.dataGridView1.Columns["ФИО_ГлавБухПадеж"].Visible = false;
                this.dataGridView1.Columns["Должность"].Visible = false;
                this.dataGridView1.Columns["ДолжностьРодПадеж"].Visible = false;
                this.dataGridView1.Columns["Основание"].Visible = false;

                //Обнулим поля редактирования
                this.textBox1.Text = "";
            }
        }
    }
}