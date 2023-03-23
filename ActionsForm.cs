using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseUtils
{
    public partial class ActionsForm : Form
    {
        public ActionsForm()
        {
            InitializeComponent();
        }

        private void ActionsForm_Load(object sender, EventArgs e)
        {

        }

        private void ActionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCreateAuditTables_Click(object sender, EventArgs e)
        {
            AuditForm auditForm = new AuditForm();
            auditForm.Show();
            this.Hide();
        }
    }
}
