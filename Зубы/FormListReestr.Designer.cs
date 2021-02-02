namespace Стамотология
{
    partial class FormListReestr
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnChekClear = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtНомерСчётФактуры = new System.Windows.Forms.TextBox();
            this.chkBoxНомерСФ = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_search_fio = new System.Windows.Forms.TextBox();
            this.tb_search_dogovor = new System.Windows.Forms.TextBox();
            this.label_sovpad = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-1, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(982, 542);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(893, 608);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(812, 608);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Печать";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnChekClear
            // 
            this.btnChekClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChekClear.Location = new System.Drawing.Point(731, 608);
            this.btnChekClear.Name = "btnChekClear";
            this.btnChekClear.Size = new System.Drawing.Size(75, 23);
            this.btnChekClear.TabIndex = 3;
            this.btnChekClear.Text = "Сбросить отметки договоров";
            this.btnChekClear.UseVisualStyleBackColor = true;
            this.btnChekClear.Click += new System.EventHandler(this.button2_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePicker1.Location = new System.Drawing.Point(435, 605);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(133, 20);
            this.dateTimePicker1.TabIndex = 10;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 609);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Дата счёт-фактуры";
            // 
            // txtНомерСчётФактуры
            // 
            this.txtНомерСчётФактуры.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtНомерСчётФактуры.Location = new System.Drawing.Point(199, 606);
            this.txtНомерСчётФактуры.Name = "txtНомерСчётФактуры";
            this.txtНомерСчётФактуры.Size = new System.Drawing.Size(118, 20);
            this.txtНомерСчётФактуры.TabIndex = 8;
            // 
            // chkBoxНомерСФ
            // 
            this.chkBoxНомерСФ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkBoxНомерСФ.AutoSize = true;
            this.chkBoxНомерСФ.Location = new System.Drawing.Point(12, 608);
            this.chkBoxНомерСФ.Name = "chkBoxНомерСФ";
            this.chkBoxНомерСФ.Size = new System.Drawing.Size(181, 17);
            this.chkBoxНомерСФ.TabIndex = 7;
            this.chkBoxНомерСФ.Text = "Записать номер счёт-фактуры";
            this.chkBoxНомерСФ.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 555);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Поиск по ФИО -";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 579);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Поиск по договору -";
            // 
            // tb_search_fio
            // 
            this.tb_search_fio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_search_fio.Location = new System.Drawing.Point(114, 548);
            this.tb_search_fio.Name = "tb_search_fio";
            this.tb_search_fio.Size = new System.Drawing.Size(262, 20);
            this.tb_search_fio.TabIndex = 13;
            this.tb_search_fio.TextChanged += new System.EventHandler(this.tb_search_fio_TextChanged);
            this.tb_search_fio.Click += new System.EventHandler(this.tb_search_fio_Click);
            this.tb_search_fio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_search_fio_KeyUp);
            // 
            // tb_search_dogovor
            // 
            this.tb_search_dogovor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_search_dogovor.Location = new System.Drawing.Point(114, 576);
            this.tb_search_dogovor.Name = "tb_search_dogovor";
            this.tb_search_dogovor.Size = new System.Drawing.Size(262, 20);
            this.tb_search_dogovor.TabIndex = 14;
            this.tb_search_dogovor.TextChanged += new System.EventHandler(this.tb_search_dogovor_TextChanged);
            this.tb_search_dogovor.Click += new System.EventHandler(this.tb_search_dogovor_Click);
            this.tb_search_dogovor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_search_dogovor_KeyUp);
            // 
            // label_sovpad
            // 
            this.label_sovpad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_sovpad.AutoSize = true;
            this.label_sovpad.Location = new System.Drawing.Point(392, 564);
            this.label_sovpad.Name = "label_sovpad";
            this.label_sovpad.Size = new System.Drawing.Size(113, 13);
            this.label_sovpad.TabIndex = 15;
            this.label_sovpad.Text = "Кол-во совпадений - ";
            // 
            // FormListReestr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 643);
            this.Controls.Add(this.label_sovpad);
            this.Controls.Add(this.tb_search_dogovor);
            this.Controls.Add(this.tb_search_fio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtНомерСчётФактуры);
            this.Controls.Add(this.chkBoxНомерСФ);
            this.Controls.Add(this.btnChekClear);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormListReestr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выгрузка выполненных договоров";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormListReestr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnChekClear;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtНомерСчётФактуры;
        private System.Windows.Forms.CheckBox chkBoxНомерСФ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_search_fio;
        private System.Windows.Forms.TextBox tb_search_dogovor;
        private System.Windows.Forms.Label label_sovpad;
    }
}