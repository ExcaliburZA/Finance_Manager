using finance_manager_classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_1_finances_manager
{
    public partial class frmUser : Form
    {
        public user theUser;
        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Shown(object sender, EventArgs e)
        {

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            theUser = new user(txtName.Text, Double.Parse(txtIncome.Text));
            frmFinances financesForm = new frmFinances(theUser);
            financesForm.Show(); this.Hide();
        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...-------------------------------------------------//

