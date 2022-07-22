using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RegisterUser
{
    public partial class Home : Form
    {
        string constring = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lazarus\Downloads\RegisterUser\RegisterUser\RegisterUser\RegisterUser\Database1.mdf;Integrated Security=True");
        public Home()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogeResult = MessageBox.Show("Are you sure that you want to Log-out?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
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



        private void btnSubmit_Click(object sender, EventArgs e)

        {
               if (rtbDescription.Text == "")
            {
                MessageBox.Show("Please Fill in the Discription.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                if (txtQTY.Text == "")
                {
                    MessageBox.Show("Please Fill in the Quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                }
                else
                {
                    if (txtAmount.Text == "")
                    {
                        MessageBox.Show("Please Fill in the Amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    else
                    {
                        //sending the data to database
                        using (SqlConnection SqlCon = new SqlConnection(constring))
                        {
                            SqlCon.Open();
                            SqlCommand sqlcmd = new SqlCommand("Maindataadd", SqlCon);
                            sqlcmd.CommandType = CommandType.StoredProcedure;
                            sqlcmd.Parameters.AddWithValue("@Date", txtDate.Text.ToString().Trim());
                            sqlcmd.Parameters.AddWithValue("@Description", rtbDescription.Text.Trim());
                            sqlcmd.Parameters.AddWithValue("@QTY", txtQTY.Text.Trim());
                            sqlcmd.Parameters.AddWithValue("@Amount", txtAmount.Text.Trim());
                            sqlcmd.ExecuteNonQuery();
                            MessageBox.Show("data has been submitted");
                            Clear();
                        }
                        //displaying data on gridview
                        DataTable dt = new DataTable();
                        SqlConnection conn = new SqlConnection(constring);

                        conn.Open();
                        string query = "SELECT * FROM Maindata ";
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }




                };




            }





        }
    
        void Clear()
        {
            txtAmount.Text = txtQTY.Text = rtbDescription.Text = txtDate.Text = "";
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact ... \n Contact: ...");
        }

        private void btnReport_Click(object sender, EventArgs e)
        {

        }
        //the code below sends the datagrid data to pdf.  Issues to be resolved are the design of the pdf
        private void btnPrint_Click(object sender, EventArgs e)
        {

             
                 string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lazarus\Downloads\RegisterUser\RegisterUser\RegisterUser\RegisterUser\Database1.mdf;Integrated Security=True";
                 SqlConnection connection;
                 SqlCommand command;
                 SqlDataAdapter adapter = new SqlDataAdapter();
                 DataSet ds = new DataSet();
                 int i = 0;
                 string sql = "select * from Maindata";
                 int yPoint = 0;
                 string Date = null;
                 string Description = null;
                 string QTY = null;
                 string Amount = null;

                 connection = new SqlConnection(connetionString);
                 connection.Open();
                 command = new SqlCommand(sql, connection);
                 adapter.SelectCommand = command;
                 adapter.Fill(ds);
                 connection.Close();

                 PdfDocument pdf = new PdfDocument();
                 pdf.Info.Title = "Main Data";
                 PdfPage pdfPage = pdf.AddPage();
                 XGraphics graph = XGraphics.FromPdfPage(pdfPage);
                 XFont font = new XFont("Verdana", 12, XFontStyle.Regular);
         


                 yPoint = yPoint + 100;

                 for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                 {
                     Date = ds.Tables[0].Rows[i].ItemArray[0].ToString();
                     Description = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                     QTY = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                     Amount = ds.Tables[0].Rows[i].ItemArray[3].ToString();


                     graph.DrawString(Date, font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                     graph.DrawString(Description, font, XBrushes.Black, new XRect(110, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                     graph.DrawString(QTY, font, XBrushes.Black, new XRect(180, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                     graph.DrawString(Amount, font, XBrushes.Black, new XRect(230, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                     yPoint = yPoint + 40;
                 }


                 string pdfFilename = "OrderData.pdf";
                 pdf.Save(pdfFilename);
            
            //Load PDF File for viewing
            Process.Start(pdfFilename);



        }

        private void btnData_Click(object sender, EventArgs e)
        {
            //displaying data on gridview
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(constring);

            conn.Open();
            string query = "SELECT * FROM Maindata";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Home_Load(object sender, EventArgs e)
        {
            //displaying data on gridview
            
           try { SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lazarus\Downloads\RegisterUser\RegisterUser\RegisterUser\RegisterUser\Database1.mdf;Integrated Security=True";
                using (con)
                { 
                    con.Open(); 
                    string qry = "select Name from Maindata where Name='" + lblName.Text + "' "; 
                    SqlCommand cmd = new SqlCommand(qry, con);
                    string strUsrNm = Convert.ToString(cmd.ExecuteScalar());
                    lblName.Text = strUsrNm; 
                    con.Close();
                } 
            } catch { 
            } finally {
            } 
        }

        }
    }

