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
    public partial class FormКлассификатор : Form
    {
        public FormКлассификатор()
        {
            InitializeComponent();
        }

        private void FormКлассификатор_Load(object sender, EventArgs e)
        {
            //Загрузим данные по врачам
            string query = "select * from КлассификаторУслуги";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_кодУслуги"].Visible = false;
            this.dataGridView1.Columns["КлассификаторУслуги"].Width = 315;

            ////Загрузим данные по врачам
            //string query = "select id_кодУслуги,КлассификаторУслуги as Вид услуг' from КлассификаторУслуги";
            //DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

            //this.dataGridView1.DataSource = tab;
            //this.dataGridView1.Columns["id_кодУслуги"].Visible = false;
            //this.dataGridView1.Columns["Вид услуг"].Width = 315;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_кодУслуги"].Value != DBNull.Value)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_кодУслуги"].Value);
                string query = "select * from КлассификаторУслуги where id_кодУслуги = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги").Rows[0];

                this.textBox1.Text = row["КлассификаторУслуги"].ToString();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "")
            {
                //InsertEsculap esculap = new InsertEsculap(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                //InsertБухгалтер bux = new InsertБухгалтер(this.textBox1.Text);
                //Receiver rec = new Receiver();

                //rec.Action(bux);
                string insertQuery = "insert into КлассификаторУслуги(КлассификаторУслуги) values('"+ this.textBox1.Text +"')";
                Query.Execute(insertQuery, ConnectionDB.ConnectionString());

                //обновим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from КлассификаторУслуги";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_кодУслуги"].Visible = false;
                this.dataGridView1.Columns["КлассификаторУслуги"].Width = 315;

                //Обнулим поля редактирования
                this.textBox1.Text = "";

            }
            else
            {
                MessageBox.Show("Поле ФИО обязательно к заполнению");
                this.textBox1.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "")
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_кодУслуги"].Value);

                string update = "update КлассификаторУслуги " +
                                "set КлассификаторУслуги = '"+ this.textBox1.Text +"' " +
                                "where id_кодУслуги = " + id + " ";
                Query.Execute(update, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from КлассификаторУслуги";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_кодУслуги"].Visible = false;
                this.dataGridView1.Columns["КлассификаторУслуги"].Width = 315;

                //Обнулим поля редактирования
                this.textBox1.Text = "";

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
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_кодУслуги"].Value);
                string query = "delete from КлассификаторУслуги where id_кодУслуги = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string queryUp = "select * from КлассификаторУслуги";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "КлассификаторУслуги");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_кодУслуги"].Visible = false;
                this.dataGridView1.Columns["КлассификаторУслуги"].Width = 315;

                //Обнулим поля редактирования
                this.textBox1.Text = "";
            }
        }
    }
}