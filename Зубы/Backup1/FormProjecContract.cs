using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Стамотология.Classes;
using System.Windows;


namespace Стамотология
{
    public partial class FormProjecContract : Form
    {
        private int idLK;

        public int IdЛьготнаяКатегория
        {
            get
            {
                return idLK;
            }
            set
            {
                idLK = value;
            }
        }

        public FormProjecContract()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.IdЛьготнаяКатегория = Convert.ToInt32(this.cmbЛьготнаяКатегория.SelectedValue.ToString());
        }

        private void FormProjecContract_Load(object sender, EventArgs e)
        {

            DataTable tbLK = TableЛьготнаяКатегория.GetDateTable();

            this.cmbЛьготнаяКатегория.DataSource = tbLK;
            this.cmbЛьготнаяКатегория.DisplayMember = "ЛьготнаяКатегория";
            this.cmbЛьготнаяКатегория.ValueMember = "id_льготнойКатегории";

            ////string sCon = Стамотология.Classes.ConnectionDB.ConnectionString();
            //using (OleDbConnection con = new OleDbConnection(ConnectionDB.ConnectionString()))
            //{
            //    con.Open();
            //     string 

            //    OleDbCommand com = new OleDbCommand(Query, con);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}