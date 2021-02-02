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
    public partial class FormFindContract : Form
    {

        private string prefixHosp = string.Empty;
        private string queryFind = string.Empty;

        public string StringQuery
        {
            get
            {
                return queryFind;
            }
            set
            {
                queryFind = value;
            }
        }


        public FormFindContract()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

            string numContract = prefixHosp + "/" + this.txtNumContract.Text.Trim();
            StringQuery = "select * from Льготник where id_льготник in ( " +
                              "select id_льготник from Договор where НомерДоговора  like '" + numContract + "%' )";
        }

        private void FormFindContract_Load(object sender, EventArgs e)
        {
            // Строка подключения.
            string sConn = ConnectionDB.ConnectionString();

            string queryHosp = "select КодПоликлинники from Поликлинника";
            prefixHosp = Поликлинника.GetПоликлинники(queryHosp, sConn).Rows[0][0].ToString().Trim();

            this.txtHospital.Text = prefixHosp;


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}