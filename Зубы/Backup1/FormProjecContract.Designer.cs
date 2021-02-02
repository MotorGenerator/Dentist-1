namespace Стамотология
{
    partial class FormProjecContract
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grpЛьготнаяКатегория = new System.Windows.Forms.GroupBox();
            this.cmbЛьготнаяКатегория = new System.Windows.Forms.ComboBox();
            this.grpЛьготнаяКатегория.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(177, 72);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(261, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Печать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpЛьготнаяКатегория
            // 
            this.grpЛьготнаяКатегория.Controls.Add(this.cmbЛьготнаяКатегория);
            this.grpЛьготнаяКатегория.Location = new System.Drawing.Point(12, 12);
            this.grpЛьготнаяКатегория.Name = "grpЛьготнаяКатегория";
            this.grpЛьготнаяКатегория.Size = new System.Drawing.Size(324, 54);
            this.grpЛьготнаяКатегория.TabIndex = 4;
            this.grpЛьготнаяКатегория.TabStop = false;
            this.grpЛьготнаяКатегория.Text = "Льготная категория";
            // 
            // cmbЛьготнаяКатегория
            // 
            this.cmbЛьготнаяКатегория.FormattingEnabled = true;
            this.cmbЛьготнаяКатегория.Location = new System.Drawing.Point(7, 20);
            this.cmbЛьготнаяКатегория.Name = "cmbЛьготнаяКатегория";
            this.cmbЛьготнаяКатегория.Size = new System.Drawing.Size(306, 21);
            this.cmbЛьготнаяКатегория.TabIndex = 0;
            // 
            // FormProjecContract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 109);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpЛьготнаяКатегория);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormProjecContract";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Проекты договоров";
            this.Load += new System.EventHandler(this.FormProjecContract_Load);
            this.grpЛьготнаяКатегория.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox grpЛьготнаяКатегория;
        private System.Windows.Forms.ComboBox cmbЛьготнаяКатегория;
    }
}