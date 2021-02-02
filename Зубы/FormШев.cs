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
    public partial class FormШев : Form
    {
        public FormШев()
        {
            InitializeComponent();
        }

        private void FormШев_Load(object sender, EventArgs e)
        {
            //Загрузим данные по врачам
            string query = "select * from ФиоШев";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ФиоШев");

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_шев"].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.textBox4.Text != "" && this.textBox5.Text != "")
            {
                //InsertEsculap esculap = new InsertEsculap(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                //Receiver rec = new Receiver();

                //rec.Action(esculap);

                string queryInsert = "insert into ФиоШев(ФИО_Руководитель,ФИО_РодПадеж,Должность,ДолжностьРодПадеж,Основание)values('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox3.Text + "','" + this.textBox4.Text + "','" + this.textBox5.Text + "')";
                Query.Execute(queryInsert,ConnectionDB.ConnectionString());


                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ФиоШев";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ФиоШев");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_шев"].Visible = false;

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

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_шев"].Value != DBNull.Value)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_шев"].Value);
                string query = "select * from ФиоШев where id_шев = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ФиоШев").Rows[0];

                this.textBox1.Text = row["ФИО_Руководитель"].ToString();
                this.textBox2.Text = row["ФИО_РодПадеж"].ToString();

                this.textBox3.Text = row["Должность"].ToString();
                this.textBox4.Text = row["ДолжностьРодПадеж"].ToString();

                this.textBox5.Text = row["Основание"].ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "" && this.textBox3.Text != "" && this.textBox4.Text != "" && this.textBox5.Text != "")
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_шев"].Value);

                string queryUpdate = "update ФиоШев " +
                                     "set ФИО_Руководитель = '" + this.textBox1.Text + "' " +
                                     ",ФИО_РодПадеж = '" + this.textBox2.Text + "' " +
                                     ",Должность = '" + this.textBox3.Text + "' " +
                                     ",ДолжностьРодПадеж = '" + this.textBox4.Text + "' " +
                                     ",Основание = '" + this.textBox5.Text + "' " +
                                     "where id_шев = "+ id +" ";

                Query.Execute(queryUpdate,ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from ФиоШев";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ФиоШев");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_шев"].Visible = false;

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
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_шев"].Value);
                string query = "delete from ФиоШев where id_шев = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string queryUp = "select * from ФиоШев";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "ФиоШев");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_шев"].Visible = false;

                //Обнулим поля редактирования
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";

            }
        }
    }
}