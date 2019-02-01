namespace Server
{
    partial class FormOperator
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
            this.TellersOptions = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnADD = new System.Windows.Forms.Button();
            this.txtTellerPass = new System.Windows.Forms.TextBox();
            this.txtTellerUserName = new System.Windows.Forms.TextBox();
            this.txtTellerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnModify = new System.Windows.Forms.Button();
            this.txtModUName = new System.Windows.Forms.TextBox();
            this.txtModPass = new System.Windows.Forms.TextBox();
            this.txtModName = new System.Windows.Forms.TextBox();
            this.txtModID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.columnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TellersOptions.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // TellersOptions
            // 
            this.TellersOptions.Controls.Add(this.tabPage1);
            this.TellersOptions.Controls.Add(this.tabPage2);
            this.TellersOptions.Controls.Add(this.tabPage3);
            this.TellersOptions.Controls.Add(this.tabPage4);
            this.TellersOptions.Location = new System.Drawing.Point(1, 1);
            this.TellersOptions.Name = "TellersOptions";
            this.TellersOptions.SelectedIndex = 0;
            this.TellersOptions.Size = new System.Drawing.Size(528, 359);
            this.TellersOptions.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnADD);
            this.tabPage1.Controls.Add(this.txtTellerPass);
            this.tabPage1.Controls.Add(this.txtTellerUserName);
            this.tabPage1.Controls.Add(this.txtTellerName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(520, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Add Teller";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Teller\'s Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Teller\'s User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Teller\'s Name";
            // 
            // btnADD
            // 
            this.btnADD.Location = new System.Drawing.Point(362, 153);
            this.btnADD.Name = "btnADD";
            this.btnADD.Size = new System.Drawing.Size(94, 44);
            this.btnADD.TabIndex = 4;
            this.btnADD.Text = "ADD";
            this.btnADD.UseVisualStyleBackColor = true;
            this.btnADD.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTellerPass
            // 
            this.txtTellerPass.Location = new System.Drawing.Point(154, 210);
            this.txtTellerPass.Name = "txtTellerPass";
            this.txtTellerPass.Size = new System.Drawing.Size(150, 20);
            this.txtTellerPass.TabIndex = 3;
            // 
            // txtTellerUserName
            // 
            this.txtTellerUserName.Location = new System.Drawing.Point(154, 166);
            this.txtTellerUserName.Name = "txtTellerUserName";
            this.txtTellerUserName.Size = new System.Drawing.Size(150, 20);
            this.txtTellerUserName.TabIndex = 2;
            // 
            // txtTellerName
            // 
            this.txtTellerName.Location = new System.Drawing.Point(154, 122);
            this.txtTellerName.Name = "txtTellerName";
            this.txtTellerName.Size = new System.Drawing.Size(150, 20);
            this.txtTellerName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 13F);
            this.label1.Location = new System.Drawing.Point(80, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 52);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please Enter the teller\'s name, user name, password and then press Add";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDelete);
            this.tabPage2.Controls.Add(this.txtID);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(520, 333);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Delete Teller";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(140, 174);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 31);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "DELETE";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(140, 126);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 20);
            this.txtID.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label6.Location = new System.Drawing.Point(50, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Teller\'s ID";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 13F);
            this.label5.Location = new System.Drawing.Point(46, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(417, 42);
            this.label5.TabIndex = 1;
            this.label5.Text = "Please Enter the teller\'s ID and then press DELETE";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnModify);
            this.tabPage3.Controls.Add(this.txtModUName);
            this.tabPage3.Controls.Add(this.txtModPass);
            this.tabPage3.Controls.Add(this.txtModName);
            this.tabPage3.Controls.Add(this.txtModID);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(520, 333);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Modify Teller";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(204, 214);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(100, 42);
            this.btnModify.TabIndex = 10;
            this.btnModify.Text = "MODIFY";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // txtModUName
            // 
            this.txtModUName.Location = new System.Drawing.Point(371, 124);
            this.txtModUName.Name = "txtModUName";
            this.txtModUName.Size = new System.Drawing.Size(100, 20);
            this.txtModUName.TabIndex = 9;
            // 
            // txtModPass
            // 
            this.txtModPass.Location = new System.Drawing.Point(371, 157);
            this.txtModPass.Name = "txtModPass";
            this.txtModPass.Size = new System.Drawing.Size(100, 20);
            this.txtModPass.TabIndex = 8;
            // 
            // txtModName
            // 
            this.txtModName.Location = new System.Drawing.Point(115, 157);
            this.txtModName.Name = "txtModName";
            this.txtModName.Size = new System.Drawing.Size(100, 20);
            this.txtModName.TabIndex = 7;
            // 
            // txtModID
            // 
            this.txtModID.Location = new System.Drawing.Point(115, 124);
            this.txtModID.Name = "txtModID";
            this.txtModID.Size = new System.Drawing.Size(100, 20);
            this.txtModID.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(40, 160);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(289, 160);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "User Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(40, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "ID";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 10F);
            this.label7.Location = new System.Drawing.Point(40, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(417, 72);
            this.label7.TabIndex = 1;
            this.label7.Text = "Please Enter the teller\'s ID, name, user name, password and then press MODIFY\r\n*N" +
                "ote that you must enter all of the information even the non-modified.\r\n";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btnRefresh);
            this.tabPage4.Controls.Add(this.dataGridView);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(520, 333);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "View Tellers";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(7, 291);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(507, 35);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "REFRESH";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToOrderColumns = true;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnID,
            this.ColumnName,
            this.ColumnUName,
            this.ColumnPass});
            this.dataGridView.Location = new System.Drawing.Point(-4, 3);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(524, 282);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // columnID
            // 
            this.columnID.HeaderText = "ID";
            this.columnID.Name = "columnID";
            this.columnID.ReadOnly = true;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            this.ColumnName.Width = 125;
            // 
            // ColumnUName
            // 
            this.ColumnUName.HeaderText = "User Name";
            this.ColumnUName.Name = "ColumnUName";
            this.ColumnUName.ReadOnly = true;
            this.ColumnUName.Width = 125;
            // 
            // ColumnPass
            // 
            this.ColumnPass.HeaderText = "Password";
            this.ColumnPass.Name = "ColumnPass";
            this.ColumnPass.ReadOnly = true;
            this.ColumnPass.Width = 125;
            // 
            // FormOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 361);
            this.Controls.Add(this.TellersOptions);
            this.Name = "FormOperator";
            this.Text = "Welcome";
            this.Load += new System.EventHandler(this.FormOperator_Load);
            this.TellersOptions.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TellersOptions;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnADD;
        private System.Windows.Forms.TextBox txtTellerPass;
        private System.Windows.Forms.TextBox txtTellerUserName;
        private System.Windows.Forms.TextBox txtTellerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.TextBox txtModUName;
        private System.Windows.Forms.TextBox txtModPass;
        private System.Windows.Forms.TextBox txtModName;
        private System.Windows.Forms.TextBox txtModID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn columnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPass;

    }
}