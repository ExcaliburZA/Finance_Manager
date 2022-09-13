using finance_manager_classes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task_1_finances_manager
{
    public partial class frmFinances : Form
    {
        public user theUser;
        public Dictionary<string, expense> expenses = new Dictionary<string, expense>();

        /// <summary>
        /// constructor. All components on the form are also hidden using their Hide() methods when the form is created
        /// when the form is created
        /// </summary>
        /// <param name="theUser"></param>
        public frmFinances(user theUser)
        {
            InitializeComponent();
            this.theUser = theUser;
            pnlLivingCosts.Hide(); pnlPurchase.Hide();
            pnlRent.Hide(); pnlVehicle.Hide();
            pctFrame.Hide(); pnlIncomeSurplus.Hide();
        }

        /// <summary>
        /// Displays a heading with the user's entered name when the form is shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmFinances_Shown(object sender, EventArgs e)
        {
            lblUserWelcome.Text = theUser.name + "\'s Finances Manager";          
        }
 
        /// <summary>
        /// adding a new living cost to the user's expenses 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = cmbLivingCosts.SelectedItem.ToString().ToLower();
            double cost = Double.Parse(txtLivingCost.Text.Trim());

            if ((cmbLivingCosts.SelectedIndex) == 5)
            {
                name = (Interaction.InputBox("Please enter the expense name")).ToLower();
            }

            expenses.Add(name, new livingCost(cost, name));
            txtTest.Text += name + " " + expenses[name].amt + Environment.NewLine;
        }

        /// <summary>
        /// adding the total monthly cost of buying a vehicle to the user's expenses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVehicleAdd_Click(object sender, EventArgs e)
        {
            vehicle theVehicle = new vehicle(Double.Parse(txtVehiclePrice.Text.Trim()), txtModel.Text.Trim(), 
                txtMake.Text.Trim(), Double.Parse(txtVehicleDeposit.Text.Trim()),  (Double.Parse(txtVehicleInterestRate.Text.Trim()) / 100),
                Double.Parse(txtInsurancePremiums.Text.Trim()));

            vehicleLoan theVehicleLoan = new vehicleLoan(theVehicle);
            theVehicleLoan.calcMonthlyLoanAmt(); theVehicleLoan.calcMonthlyAmt();

            expenses.Add("vehicle", theVehicleLoan);
            txtTest.Text += "vehicle " + theVehicleLoan.amt + Environment.NewLine;
        }

        /// <summary>
        /// adding the total monthly cost of buying a property to the user's expenses
        /// this method also informs the user if the likelihood of their loan being approved is low
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPropertyAdd_Click(object sender, EventArgs e)
        {
            property theProperty = new property(Double.Parse(txtPropertyPrice.Text.Trim()), Double.Parse(txtDeposit.Text.Trim()), 
                (Double.Parse(txtPropertyInterestRate.Text.Trim()) / 100), Int32.Parse(txtRepaymentMonths.Text.Trim()));

            homeLoan theHomeLoan = new homeLoan(theProperty);
            theHomeLoan.calcMonthlyAmt();

            if (theHomeLoan.amt > (theUser.income / 3))
            {
                MessageBox.Show("Monthly loan repayments are greater than one third of your income"+ Environment.NewLine +"It is unlikely this loan will be approved");
            }

            expenses.Add("home loan", theHomeLoan);
            txtTest.Text += "home loan " + theHomeLoan.amt + Environment.NewLine; 
        }

        /// <summary>
        /// adding the monthly cost of rent to the user's expenses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRentAdd_Click(object sender, EventArgs e)
        {
            livingCost rent = new livingCost(Double.Parse(txtRent.Text.Trim()), "rent");
            expenses.Add(rent.name, rent);
            txtTest.Text += rent.name + " " + rent.amt + Environment.NewLine;
        }

        /// <summary>
        /// displaying the appropriate components based on the user's selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vehicleExpensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlPurchase.Hide();
            pnlRent.Hide();
            pnlLivingCosts.Hide();
            pnlRent.Hide();
            pnlIncomeSurplus.Hide();

            pnlVehicle.Show();
            pctFrame.Show();
            pctFrame.Image = task_1_finances_manager.Properties.Resources.car;

        }

        /// <summary>
        /// displaying the appropriate components based on the user's selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rentAccomodationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlPurchase.Hide();
            pnlLivingCosts.Hide();
            pnlVehicle.Hide();
            pnlRent.Hide();
            pnlIncomeSurplus.Hide();

            pnlRent.Show();
            pctFrame.Show();
            pctFrame.Image = task_1_finances_manager.Properties.Resources.apartment;

        }

        /// <summary>
        /// displaying the appropriate components based on the user's selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void purchasePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlLivingCosts.Hide();
            pnlRent.Hide();
            pnlVehicle.Hide();
            pnlIncomeSurplus.Hide();

            pnlPurchase.Show();
            pctFrame.Show();
            pctFrame.Image = task_1_finances_manager.Properties.Resources.home;

        }

        /// <summary>
        /// displaying the appropriate components based on the user's selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateIncomeSurplusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlPurchase.Hide();
            pnlRent.Hide();
            pnlVehicle.Hide();
            pnlLivingCosts.Hide();

            pnlIncomeSurplus.Show();
            pctFrame.Show();
            pctFrame.Image = task_1_finances_manager.Properties.Resources.moneycounting;

            theUser.calcRemainingIncome(expenses);
            txtIncomeSurplus.Text = theUser.incomeSurplus.ToString();

        }

        /// <summary>
        /// displaying the appropriate components based on the user's selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void livingCostsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlPurchase.Hide();
            pnlRent.Hide();
            pnlVehicle.Hide();
            pnlIncomeSurplus.Hide();

            pnlLivingCosts.Show();
            pctFrame.Show();
            pctFrame.Image = task_1_finances_manager.Properties.Resources.groceries;

        }

        private void propertyManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
//------------------------------------------...ooo000 END OF FILE 000ooo...-------------------------------------------------//

