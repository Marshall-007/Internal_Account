using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegisterUser
{
    public partial class Register : Form
    {
        string constring = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lazarus\Downloads\RegisterUser\RegisterUser\RegisterUser\RegisterUser\Database1.mdf;Integrated Security=True");
        public Register()
        {
            InitializeComponent();
            
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            {




                String Valid_Email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


                //Validating the users Email


                // Name
                if (txtName.Text == "")
                {

                    errorProviderName.SetError(txtName, "Please Enter your name.");
                    txtName.Focus();

                }
                else
                {

                    errorProviderName.SetError(txtName, "");

                }

                //Surname 
                if (txtSurname.Text == "")
                {


                    errorProviderSurname.SetError(txtSurname, "Please Enter your surname.");
                    txtSurname.Focus();

                }
                else
                {


                    errorProviderSurname.SetError(txtSurname, "");

                }
                //Email
                if (txtEmail.Text == "")
                {

                    errorProviderEmail.SetError(txtEmail, "Please Enter your email address.");
                    txtEmail.Focus();

                }
                else
                {


                    errorProviderEmail.SetError(txtEmail, "");

                }
                if (txtContact.Text == "")
                {

                    errorProviderContact.SetError(txtContact, "Please Enter your Contact Number.");
                    txtContact.Focus();


                }
                else
                {

                    errorProviderContact.SetError(txtContact, "");

                }
                if (txtCompany.Text == "")
                {

                    errorProviderCompany.SetError(txtCompany, "Please Enter the name of your company.");
                    txtCompany.Focus();

                }
                else
                {

                    errorProviderCompany.SetError(txtCompany, "");

                }
                if (txtAddress.Text == "")
                {

                    errorProviderAddress.SetError(txtAddress, "Please Enter your Physical address.");
                    txtAddress.Focus();

                }
                else
                {

                    errorProviderAddress.SetError(txtAddress, "");

                }
                if (txtPassword.Text == "")
                {

                    errorProviderPassword.SetError(txtPassword, "Please Enter a Password.");
                    txtPassword.Focus();

                }
                else
                {


                    errorProviderPassword.SetError(txtPassword, "");

                }


                if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" || txtContact.Text == "" || txtAddress.Text == "" || txtCompany.Text == "" || txtPassword.Text == "" || txtConfirm.Text == "")
                {
                    MessageBox.Show("Please make sure that you fill in all required feilds.");


                }
                else
                {
                    if (txtContact.Text.Length != 10)
                    {

                        MessageBox.Show("Your contact number must have 10 digits");

                    }
                    else
                    {
                        if (!txtConfirm.Text.Equals(txtPassword.Text))
                        {

                            errorProviderConfirm.SetError(txtConfirm, "The Passwords dont match.");
                            txtEmail.Focus();

                        }
                        else
                        {

                            errorProviderConfirm.SetError(txtConfirm, "");

                            if (txtPassword.Text.Length <= 7)
                            {

                                MessageBox.Show("Your Password must be 8 or more characters long.");
                                txtPassword.Focus();

                            }
                            else
                            {

 
                                if (Regex.IsMatch(txtEmail.Text, Valid_Email))
                                {
                                    errorProviderEmail.SetError(txtEmail, "");


                                   
                                        //storing the data on the table
                                        using (SqlConnection SqlCon = new SqlConnection(constring))
                                        {
                                            SqlCon.Open();
                                            SqlCommand sqlcmd = new SqlCommand("UserAdd", SqlCon);
                                            sqlcmd.CommandType = CommandType.StoredProcedure;
                                            sqlcmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                                            sqlcmd.Parameters.AddWithValue("@Surname", txtSurname.Text.Trim());
                                            sqlcmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                                            sqlcmd.Parameters.AddWithValue("@Company", txtCompany.Text.Trim());
                                            sqlcmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                                            sqlcmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                                            sqlcmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                                            sqlcmd.ExecuteNonQuery();
                                            MessageBox.Show("Thank you have been successfully registerd.");
                                            Clear();
                                        }
                                    
                                   
                                    LogIn log = new LogIn();
                                    log.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    errorProviderEmail.SetError(txtEmail, "Please use example@gmail.com");
                                    txtEmail.Focus();
                                }




                            }

                        }

                    }
                }
            }
        }

            
           
        //clears data on the textboxes
        void Clear()
        {
            txtName.Text = txtSurname.Text = txtEmail.Text = txtCompany.Text = txtPassword.Text = txtConfirm.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogeResult = MessageBox.Show("Are you sure that you want to exit registration?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogeResult == DialogResult.Yes)
            {
                LogIn log = new LogIn();
                log.Show();
                this.Close();
            }
            else if (dialogeResult == DialogResult.No)
            {

            }
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkbxPas_CheckedChanged(object sender, EventArgs e)
        {
            if (checkbxPas.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtConfirm.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '•';
                txtConfirm.PasswordChar = '•';
            }
        }
    }
}

