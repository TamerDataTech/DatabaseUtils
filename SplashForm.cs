using DatabaseUtils.Model;
using DatabaseUtils.Model.Database;
using DatabaseUtils.Model.General;
using DatabaseUtils.Service;
using System;
using System.Windows.Forms;

namespace DatabaseUtils
{
    public partial class SplashForm : Form
    {
        public SplashForm()
        {
            InitializeComponent();
        }

        private void btnCheckConnection_Click(object sender, EventArgs e)
        {
            DbConnectionString dbConnectionString = new DbConnectionString(txtConnectionString.Text); 
            var result = DatabaseService.GetDataBases(new Query<Database>
            {
                Conn = dbConnectionString
            });

            if (result.Result)
            {
                MessageBox.Show("Connection Succeeded", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Connection failed! " + result.ErrorMessage, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            DbConnectionString dbConnectionString = new DbConnectionString(txtConnectionString.Text);
            var result = DatabaseService.GetDataBases(new Query<Database>
            {
                Conn = dbConnectionString
            });

            if (result.Result)
            {
                GlobalValues.ConnectionString = dbConnectionString.ConnectionString;
                ActionsForm actionsForm = new ActionsForm();
                actionsForm.Show();
                this.Hide(); 
            }
            else
            {
                MessageBox.Show("Connection failed! " + result.ErrorMessage, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
