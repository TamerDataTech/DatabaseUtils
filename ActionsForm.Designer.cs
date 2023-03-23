namespace DatabaseUtils
{
    partial class ActionsForm
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
            this.btnCreateAuditTables = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCreateAuditTables
            // 
            this.btnCreateAuditTables.Location = new System.Drawing.Point(12, 12);
            this.btnCreateAuditTables.Name = "btnCreateAuditTables";
            this.btnCreateAuditTables.Size = new System.Drawing.Size(104, 66);
            this.btnCreateAuditTables.TabIndex = 0;
            this.btnCreateAuditTables.Text = "Create Audit Tables";
            this.btnCreateAuditTables.UseVisualStyleBackColor = true;
            this.btnCreateAuditTables.Click += new System.EventHandler(this.btnCreateAuditTables_Click);
            // 
            // ActionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 89);
            this.Controls.Add(this.btnCreateAuditTables);
            this.MaximizeBox = false;
            this.Name = "ActionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actions";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ActionsForm_FormClosed);
            this.Load += new System.EventHandler(this.ActionsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateAuditTables;
    }
}