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
    public partial class FormНаселПункт : Form
    {
        public FormНаселПункт()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtНП.Text != "")
            {
                //InsertEsculap esculap = new InsertEsculap(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                string queryIns = "insert into НаселенныйПункт (Наименование)values('" + this.txtНП.Text.Trim() + "')";
                Query.Execute(queryIns, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from НаселенныйПункт";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_насПункт"].Visible = false;

                this.dataGridView1.Columns["Наименование"].Width = 368;

                //Обнулим поля редактирования
                this.txtНП.Text = "";

            }
            else
            {
                MessageBox.Show("Поле населённый пункт обязательно к заполнению");
                this.txtНП.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_насПункт"].Value != DBNull.Value)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_насПункт"].Value);
                string query = "select * from НаселенныйПункт where id_насПункт = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавБух").Rows[0];

                this.txtНП.Text = row["Наименование"].ToString();
            }
        }

        private void FormНаселПункт_Load(object sender, EventArgs e)
        {
            string query = "select * from НаселенныйПункт";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаселенныйПункт");

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_насПункт"].Visible = false;
            this.dataGridView1.Columns["Наименование"].Width = 368;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtНП.Text != "")
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_насПункт"].Value);

                string update = "update НаселенныйПункт " +
                               "set Наименование = '" + this.txtНП.Text + "' " +
                               "where id_насПункт = " + id + " ";
                Query.Execute(update, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from НаселенныйПункт";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_насПункт"].Visible = false;
                this.dataGridView1.Columns["Наименование"].Width = 368;

                //Обнулим поля редактирования
                this.txtНП.Text = "";

            }
            else
            {
                MessageBox.Show("Поле населённый пункт обязательно к заполнению");
                this.txtНП.Focus();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DataTable dtCount = (DataTable)this.dataGridView1.DataSource;

            if (dtCount.Rows.Count != 0)
            {
                //int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_насПункт"].Value);

                int id = 0;
                try
                {
                    id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_насПункт"].Value);
                }
                catch
                {
                    MessageBox.Show("Нет выбранного поля.", "Ошибка");
                    return;
                }
                string query = "delete from НаселенныйПункт where id_насПункт = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string queryUp = "select * from НаселенныйПункт";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "НаселенныйПункт");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_насПункт"].Visible = false;
                this.dataGridView1.Columns["Наименование"].Width = 368;

                //Обнулим поля редактирования
                this.txtНП.Text = "";
            }
        }
    }
}