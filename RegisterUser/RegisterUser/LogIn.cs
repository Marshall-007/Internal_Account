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
    public partial class LogIn : Form
    {
        public static string settext = "";
        string constring = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lazarus\Downloads\RegisterUser\RegisterUser\RegisterUser\RegisterUser\Database1.mdf;Integrated Security=True");
        public LogIn()
        {
            InitializeComponent();
            
        }

        //String to validate the users Email
        String Valid_Email = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";


        private void btnLogin_Click(object sender, EventArgs e)

        {
            if (txtUsername.Text == "")
            {
                MessageBox.Show("Please check your Email Address.", "Error");

            }
            else
            {
                if (txtPassword.Text == "")
                {

                    MessageBox.Show("Please Enter a password.", "Error");

                }
                else
                {
                    if (Regex.IsMatch(txtUsername.Text, Valid_Email))
                    {
                        errorProviderEmail.SetError(txtUsername, "");
                        try
                        {
                            SqlConnection sqlcon = new SqlConnection(constring);

                            sqlcon.Open();
                            string newcom = "select Email from Users where Email='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'";

                            SqlDataAdapter adpt = new SqlDataAdapter(newcom, sqlcon);
                            DataSet ds = new DataSet();

                            adpt.Fill(ds);
                            DataTable dt = ds.Tables[0];
                             
                            if (dt.Rows.Count >= 1)
                            {
                                settext = txtUsername.Text;
                                Home home = new Home();
                                home.Show();
                               
                                this.Hide();

                            }
                            else
                            {
                                MessageBox.Show("Invalid Login details. If you are a new user please Register.");
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Please use example@gmail.com");
                    }
                }
            }
        }

        private void lblReg_Click(object sender, EventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Hide();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.PasswordChar = '\0';
                

            }
            else
            {
                txtPassword.PasswordChar = '•';
               
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
