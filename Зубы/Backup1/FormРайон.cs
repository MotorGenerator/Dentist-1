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
    public partial class FormРайон : Form
    {
        public FormРайон()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtРайон.Text != "")
            {
                //InsertEsculap esculap = new InsertEsculap(this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.textBox5.Text.Trim());
                string queryIns = "insert into НаименованиеРайона (РайонОбласти)values('"+ this.txtРайон.Text.Trim() +"')";
                Query.Execute(queryIns, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from НаименованиеРайона";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавБух");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_район"].Visible = false;

                this.dataGridView1.Columns["РайонОбласти"].Width = 368;

                //Обнулим поля редактирования
                this.txtРайон.Text = "";

            }
            else
            {
                MessageBox.Show("Поле наименование района обязательно к заполнению");
                this.txtРайон.Focus();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells["id_район"].Value != DBNull.Value)
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_район"].Value);
                string query = "select * from НаименованиеРайона where id_район = " + id + " ";

                DataRow row = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаименованиеРайона").Rows[0];

                this.txtРайон.Text = row["РайонОбласти"].ToString();
            }

        }

        private void FormРайон_Load(object sender, EventArgs e)
        {
            //Загрузим данные по врачам
            string query = "select * from НаименованиеРайона";
            DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "НаименованиеРайона");

            this.dataGridView1.DataSource = tab;
            this.dataGridView1.Columns["id_район"].Visible = false;

            this.dataGridView1.Columns["РайонОбласти"].Width = 368;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (this.txtРайон.Text != "")
            {
                int id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_район"].Value);

                string update = "update НаименованиеРайона " +
                                "set РайонОбласти = '"+ this.txtРайон.Text +"' " +
                                "where id_район = "+ id +" ";
                Query.Execute(update, ConnectionDB.ConnectionString());
                
                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string query = "select * from НаименованиеРайона";
                DataTable tab = ТаблицаБД.GetTable(query, ConnectionDB.ConnectionString(), "ГлавБух");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_район"].Visible = false;
                this.dataGridView1.Columns["РайонОбласти"].Width = 368;

                //Обнулим поля редактирования
                this.txtРайон.Text = "";

            }
            else
            {
                MessageBox.Show("Все поле наименование района обязательно к заполнению");
                this.txtРайон.Focus();
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
                    id = Convert.ToInt32(this.dataGridView1.CurrentRow.Cells["id_район"].Value);
                }
                catch
                {
                    MessageBox.Show("Нет выбранного поля.", "Ошибка");
                    return;
                }
                string query = "delete from НаименованиеРайона where id_район = " + id + " ";

                Query.Execute(query, ConnectionDB.ConnectionString());

                //обнговим dataGridView
                this.dataGridView1.DataSource = null;

                //Загрузим данные по врачам
                string queryUp = "select * from НаименованиеРайона";
                DataTable tab = ТаблицаБД.GetTable(queryUp, ConnectionDB.ConnectionString(), "НаименованиеРайона");

                this.dataGridView1.DataSource = tab;
                this.dataGridView1.Columns["id_район"].Visible = false;
                this.dataGridView1.Columns["РайонОбласти"].Width = 368;

                //Обнулим поля редактирования
                this.txtРайон.Text = "";
            }
        }

        private void txtРайон_MouseHover(object sender, EventArgs e)
        {
            this.toolTip1.SetToolTip(txtРайон, "Формат ввода Балаковский");
        }
    }
}