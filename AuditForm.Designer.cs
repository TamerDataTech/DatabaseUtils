namespace DatabaseUtils
{
    partial class AuditForm
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
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.cboDbNames = new System.Windows.Forms.ComboBox();
            this.btnDBCheckNoTables = new System.Windows.Forms.Button();
            this.btnDBCheckAllTables = new System.Windows.Forms.Button();
            this.clbDbTables = new System.Windows.Forms.CheckedListBox();
            this.btnDbGetTables = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboTargetDb = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSuffix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.btnCreateAudit = new System.Windows.Forms.Button();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.lblDbName = new System.Windows.Forms.Label();
            this.bwLogs = new System.ComponentModel.BackgroundWorker();
            this.groupBoxConnection.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.cboDbNames);
            this.groupBoxConnection.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(267, 48);
            this.groupBoxConnection.TabIndex = 1;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Database";
            // 
            // cboDbNames
            // 
            this.cboDbNames.DisplayMember = "Name";
            this.cboDbNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDbNames.FormattingEnabled = true;
            this.cboDbNames.Location = new System.Drawing.Point(6, 19);
            this.cboDbNames.Name = "cboDbNames";
            this.cboDbNames.Size = new System.Drawing.Size(255, 21);
            this.cboDbNames.TabIndex = 7;
            this.cboDbNames.ValueMember = "Name";
            this.cboDbNames.SelectedIndexChanged += new System.EventHandler(this.cboDbNames_SelectedIndexChanged);
            // 
            // btnDBCheckNoTables
            // 
            this.btnDBCheckNoTables.Location = new System.Drawing.Point(200, 71);
            this.btnDBCheckNoTables.Name = "btnDBCheckNoTables";
            this.btnDBCheckNoTables.Size = new System.Drawing.Size(80, 23);
            this.btnDBCheckNoTables.TabIndex = 16;
            this.btnDBCheckNoTables.Text = "Check None";
            this.btnDBCheckNoTables.UseVisualStyleBackColor = true;
            this.btnDBCheckNoTables.Click += new System.EventHandler(this.btnDBCheckNoTables_Click);
            // 
            // btnDBCheckAllTables
            // 
            this.btnDBCheckAllTables.Location = new System.Drawing.Point(114, 71);
            this.btnDBCheckAllTables.Name = "btnDBCheckAllTables";
            this.btnDBCheckAllTables.Size = new System.Drawing.Size(80, 23);
            this.btnDBCheckAllTables.TabIndex = 15;
            this.btnDBCheckAllTables.Text = "Check All";
            this.btnDBCheckAllTables.UseVisualStyleBackColor = true;
            this.btnDBCheckAllTables.Click += new System.EventHandler(this.btnDBCheckAllTables_Click);
            // 
            // clbDbTables
            // 
            this.clbDbTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clbDbTables.FormattingEnabled = true;
            this.clbDbTables.Location = new System.Drawing.Point(12, 101);
            this.clbDbTables.Name = "clbDbTables";
            this.clbDbTables.Size = new System.Drawing.Size(267, 334);
            this.clbDbTables.TabIndex = 14;
            // 
            // btnDbGetTables
            // 
            this.btnDbGetTables.Location = new System.Drawing.Point(11, 71);
            this.btnDbGetTables.Name = "btnDbGetTables";
            this.btnDbGetTables.Size = new System.Drawing.Size(97, 23);
            this.btnDbGetTables.TabIndex = 13;
            this.btnDbGetTables.Text = "Get Tables";
            this.btnDbGetTables.UseVisualStyleBackColor = true;
            this.btnDbGetTables.Click += new System.EventHandler(this.btnDbGetTables_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboTargetDb);
            this.groupBox1.Location = new System.Drawing.Point(296, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(267, 48);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Database";
            // 
            // cboTargetDb
            // 
            this.cboTargetDb.DisplayMember = "Name";
            this.cboTargetDb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTargetDb.FormattingEnabled = true;
            this.cboTargetDb.Location = new System.Drawing.Point(6, 19);
            this.cboTargetDb.Name = "cboTargetDb";
            this.cboTargetDb.Size = new System.Drawing.Size(255, 21);
            this.cboTargetDb.TabIndex = 7;
            this.cboTargetDb.ValueMember = "Name";
            this.cboTargetDb.SelectedValueChanged += new System.EventHandler(this.cboTargetDb_SelectedValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDbName);
            this.groupBox2.Controls.Add(this.lblDbName);
            this.groupBox2.Controls.Add(this.txtSuffix);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(579, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 48);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameters";
            // 
            // txtSuffix
            // 
            this.txtSuffix.Location = new System.Drawing.Point(55, 20);
            this.txtSuffix.Name = "txtSuffix";
            this.txtSuffix.Size = new System.Drawing.Size(96, 20);
            this.txtSuffix.TabIndex = 1;
            this.txtSuffix.Text = "Audit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Suffix";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtLogs);
            this.groupBox3.Location = new System.Drawing.Point(296, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(776, 367);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Log";
            // 
            // txtLogs
            // 
            this.txtLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLogs.Location = new System.Drawing.Point(7, 20);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.Size = new System.Drawing.Size(753, 341);
            this.txtLogs.TabIndex = 0;
            // 
            // btnCreateAudit
            // 
            this.btnCreateAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateAudit.Location = new System.Drawing.Point(947, 12);
            this.btnCreateAudit.Name = "btnCreateAudit";
            this.btnCreateAudit.Size = new System.Drawing.Size(125, 48);
            this.btnCreateAudit.TabIndex = 19;
            this.btnCreateAudit.Text = "Create Audit Tables";
            this.btnCreateAudit.UseVisualStyleBackColor = true;
            this.btnCreateAudit.Click += new System.EventHandler(this.btnCreateAudit_Click);
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(220, 20);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(96, 20);
            this.txtDbName.TabIndex = 3;
            this.txtDbName.Visible = false;
            // 
            // lblDbName
            // 
            this.lblDbName.AutoSize = true;
            this.lblDbName.Location = new System.Drawing.Point(170, 22);
            this.lblDbName.Name = "lblDbName";
            this.lblDbName.Size = new System.Drawing.Size(49, 13);
            this.lblDbName.TabIndex = 2;
            this.lblDbName.Text = "DbName";
            this.lblDbName.Visible = false;
            // 
            // bwLogs
            // 
            this.bwLogs.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLogs_DoWork);
            this.bwLogs.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwLogs_ProgressChanged);
            // 
            // AuditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 450);
            this.Controls.Add(this.btnCreateAudit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDBCheckNoTables);
            this.Controls.Add(this.btnDBCheckAllTables);
            this.Controls.Add(this.clbDbTables);
            this.Controls.Add(this.btnDbGetTables);
            this.Controls.Add(this.groupBoxConnection);
            this.Name = "AuditForm";
            this.Text = "Audit";
            this.Activated += new System.EventHandler(this.AuditForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AuditForm_FormClosed);
            this.Load += new System.EventHandler(this.AuditForm_Load);
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.ComboBox cboDbNames;
        private System.Windows.Forms.Button btnDBCheckNoTables;
        private System.Windows.Forms.Button btnDBCheckAllTables;
        private System.Windows.Forms.CheckedListBox clbDbTables;
        private System.Windows.Forms.Button btnDbGetTables;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboTargetDb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLogs;
        private System.Windows.Forms.TextBox txtSuffix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateAudit;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label lblDbName;
        private System.ComponentModel.BackgroundWorker bwLogs;
    }
}