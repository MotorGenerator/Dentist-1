namespace Стамотология
{
    partial class FormДоговор
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
            this.components = new System.ComponentModel.Container();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDate = new System.Windows.Forms.Button();
            this.gvДоговор = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.привязатьВрачаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gvВидУслуг = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnДатаДоп = new System.Windows.Forms.Button();
            this.gvДопСоглашение = new System.Windows.Forms.DataGridView();
            this.btnДопСоглашение = new System.Windows.Forms.Button();
            this.lblFIO = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnАкт = new System.Windows.Forms.Button();
            this.btnТехническийЛист = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtTechSheet = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbEsculap = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvДоговор)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvВидУслуг)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvДопСоглашение)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDate);
            this.groupBox2.Controls.Add(this.gvДоговор);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Location = new System.Drawing.Point(20, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(311, 171);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Договоры с льготником";
            // 
            // btnDate
            // 
            this.btnDate.Enabled = false;
            this.btnDate.Location = new System.Drawing.Point(230, 144);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(75, 23);
            this.btnDate.TabIndex = 1;
            this.btnDate.Text = "Дата договора";
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.button5_Click);
            // 
            // gvДоговор
            // 
            this.gvДоговор.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.gvДоговор.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvДоговор.ContextMenuStrip = this.contextMenuStrip1;
            this.gvДоговор.Location = new System.Drawing.Point(10, 19);
            this.gvДоговор.Name = "gvДоговор";
            this.gvДоговор.ReadOnly = true;
            this.gvДоговор.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvДоговор.Size = new System.Drawing.Size(295, 119);
            this.gvДоговор.TabIndex = 0;
            this.gvДоговор.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvДоговор_CellClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.привязатьВрачаToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 26);
            // 
            // привязатьВрачаToolStripMenuItem
            // 
            this.привязатьВрачаToolStripMenuItem.Name = "привязатьВрачаToolStripMenuItem";
            this.привязатьВрачаToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.привязатьВрачаToolStripMenuItem.Text = "Привязать врача";
            this.привязатьВрачаToolStripMenuItem.Click += new System.EventHandler(this.привязатьВрачаToolStripMenuItem_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(10, 144);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(119, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Заключить договор";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gvВидУслуг);
            this.groupBox3.Location = new System.Drawing.Point(17, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(622, 201);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Услуги";
            // 
            // gvВидУслуг
            // 
            this.gvВидУслуг.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvВидУслуг.Location = new System.Drawing.Point(7, 20);
            this.gvВидУслуг.Name = "gvВидУслуг";
            this.gvВидУслуг.ReadOnly = true;
            this.gvВидУслуг.Size = new System.Drawing.Size(609, 175);
            this.gvВидУслуг.TabIndex = 0;
            this.gvВидУслуг.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView2_CellBeginEdit);
            this.gvВидУслуг.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.gvВидУслуг.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvВидУслуг_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(21, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "следующий договор №:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(558, 508);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Закрыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 508);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Печать договора";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(234, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnДатаДоп);
            this.groupBox1.Controls.Add(this.gvДопСоглашение);
            this.groupBox1.Controls.Add(this.btnДопСоглашение);
            this.groupBox1.Location = new System.Drawing.Point(337, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(296, 189);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дополнительные соглашения";
            // 
            // btnДатаДоп
            // 
            this.btnДатаДоп.Enabled = false;
            this.btnДатаДоп.Location = new System.Drawing.Point(215, 160);
            this.btnДатаДоп.Name = "btnДатаДоп";
            this.btnДатаДоп.Size = new System.Drawing.Size(75, 23);
            this.btnДатаДоп.TabIndex = 1;
            this.btnДатаДоп.Text = "Дата";
            this.btnДатаДоп.UseVisualStyleBackColor = true;
            this.btnДатаДоп.Click += new System.EventHandler(this.btnДатаДоп_Click);
            // 
            // gvДопСоглашение
            // 
            this.gvДопСоглашение.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvДопСоглашение.Location = new System.Drawing.Point(7, 19);
            this.gvДопСоглашение.Name = "gvДопСоглашение";
            this.gvДопСоглашение.Size = new System.Drawing.Size(283, 106);
            this.gvДопСоглашение.TabIndex = 0;
            this.gvДопСоглашение.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvДопСоглашение_CellClick_1);
            this.gvДопСоглашение.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvДопСоглашение_CellContentClick);
            // 
            // btnДопСоглашение
            // 
            this.btnДопСоглашение.Location = new System.Drawing.Point(50, 131);
            this.btnДопСоглашение.Name = "btnДопСоглашение";
            this.btnДопСоглашение.Size = new System.Drawing.Size(240, 23);
            this.btnДопСоглашение.TabIndex = 10;
            this.btnДопСоглашение.Text = "Заключить дополнительное соглашение";
            this.btnДопСоглашение.UseVisualStyleBackColor = true;
            this.btnДопСоглашение.Click += new System.EventHandler(this.btnДопСоглашение_Click);
            // 
            // lblFIO
            // 
            this.lblFIO.AutoSize = true;
            this.lblFIO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFIO.ForeColor = System.Drawing.Color.Green;
            this.lblFIO.Location = new System.Drawing.Point(15, 13);
            this.lblFIO.Name = "lblFIO";
            this.lblFIO.Size = new System.Drawing.Size(47, 15);
            this.lblFIO.TabIndex = 11;
            this.lblFIO.Text = "label1";
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(133, 508);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Удалить услугу";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(234, 508);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(114, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Добавить услугу";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnАкт
            // 
            this.btnАкт.Enabled = false;
            this.btnАкт.Location = new System.Drawing.Point(354, 508);
            this.btnАкт.Name = "btnАкт";
            this.btnАкт.Size = new System.Drawing.Size(75, 23);
            this.btnАкт.TabIndex = 14;
            this.btnАкт.Text = "Акт работ";
            this.btnАкт.UseVisualStyleBackColor = true;
            this.btnАкт.Click += new System.EventHandler(this.btnАкт_Click);
            // 
            // btnТехническийЛист
            // 
            this.btnТехническийЛист.Location = new System.Drawing.Point(506, 50);
            this.btnТехническийЛист.Name = "btnТехническийЛист";
            this.btnТехническийЛист.Size = new System.Drawing.Size(116, 23);
            this.btnТехническийЛист.TabIndex = 15;
            this.btnТехническийЛист.Text = "Технический лист";
            this.btnТехническийЛист.UseVisualStyleBackColor = true;
            this.btnТехническийЛист.Click += new System.EventHandler(this.btnТехническийЛист_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(30, 279);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(301, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "Изменить акт(Распчатать акт)";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtTechSheet
            // 
            this.txtTechSheet.Location = new System.Drawing.Point(478, 79);
            this.txtTechSheet.Name = "txtTechSheet";
            this.txtTechSheet.Size = new System.Drawing.Size(100, 20);
            this.txtTechSheet.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(400, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "№ тех-листа:";
            // 
            // cmbEsculap
            // 
            this.cmbEsculap.FormattingEnabled = true;
            this.cmbEsculap.Location = new System.Drawing.Point(112, 79);
            this.cmbEsculap.Name = "cmbEsculap";
            this.cmbEsculap.Size = new System.Drawing.Size(279, 21);
            this.cmbEsculap.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Врач протезист:";
            // 
            // FormДоговор
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 539);
            this.Controls.Add(this.txtTechSheet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbEsculap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnТехническийЛист);
            this.Controls.Add(this.btnАкт);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblFIO);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormДоговор";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма заключения договора";
            this.Load += new System.EventHandler(this.FormДоговор_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvДоговор)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvВидУслуг)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvДопСоглашение)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gvДоговор;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gvВидУслуг;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView gvДопСоглашение;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.Button btnДатаДоп;
        private System.Windows.Forms.Button btnДопСоглашение;
        private System.Windows.Forms.Label lblFIO;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnАкт;
        private System.Windows.Forms.Button btnТехническийЛист;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtTechSheet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbEsculap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem привязатьВрачаToolStripMenuItem;
    }
}