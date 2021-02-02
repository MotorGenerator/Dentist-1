using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.Data.OleDb;

namespace Стамотология
{
    public partial class FormEditEsculap : Form
    {
        public FormEditEsculap()
        {
            InitializeComponent();
        }

        private void FormEditEsculap_Load(object sender, EventArgs e)
        {
            //Загрузим данные по врачам
            string query = "select * from ГлавВрач";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавВрач");

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_главВрач"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.textBox4.Text != "" && this.textBox5.Text != "")
            {
                InsertEsculap esculap = new InsertEsculap(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                Receiver rec = new Receiver();

                rec.Action(esculap);

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ГлавВрач";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавВрач");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_главВрач"].Visible = false;

                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";

            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению");
                this.textBox1.Focus();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DataTable dtCount = (DataTable)this.dataGridView1.DataSource;

            if (dtCount.Rows.Count != 0)
            {
                int id = 0;
                try
                {
                    id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_главВрач"].Value);
                }
                catch
                {
                    MessageBox.Show("Нет выбранного поля.", "Ошибка");
                    return;
                }
                string query = "delete from ГлавВрач where id_главВрач = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string queryUp = "select * from ГлавВрач";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "ГлавВрач");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_главВрач"].Visible = false;

                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";

            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.textBox4.Text != "" && this.textBox5.Text != "")
            {
               int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_главВрач"].Value);

                UpdateEsculap esculap = new UpdateEsculap(id,this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                Receiver rec = new Receiver();

                rec.Action(esculap);

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ГлавВрач";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавВрач");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_главВрач"].Visible = false;

                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";

            }
            else
            {
                MessageBox.Show("Все поля обязательны к заполнению");
                this.textBox1.Focus();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_главВрач"].Value != DBNull.Value)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_главВрач"].Value);
                string query = "select * from ГлавВрач where id_главВрач = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавВрач").Rows[0];

                this.textBox1.Text = row["ФИО_ГлавВрач"].ToString();
                this.textBox2.Text = row["ФИО_РодительныйПадеж"].ToString();

                this.textBox3.Text = row["Должность"].ToString();
                this.textBox4.Text = row["ДолжностьРодПадеж"].ToString();

                this.textBox5.Text = row["Основание"].ToString();
            }
        }
    }
}