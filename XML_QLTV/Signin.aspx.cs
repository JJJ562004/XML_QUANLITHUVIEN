using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace XML_QLTV
{
    public partial class Signin : System.Web.UI.Page
    {
        string conString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signIn(object sender, EventArgs e)
        {
            string email = txtemail.Text;
            string phone = txtphone.Text;

            if (email == "admin@gmail.com" && phone == "000")
            {
                Response.Redirect("AdminDashBoard.aspx");
            }
            else
            {
                string query = "SELECT * FROM dbo.Student WHERE Email = @email AND PhoneNumber = @phonenumber";
                using (SqlConnection conn = new SqlConnection(conString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phonenumber", phone);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            Response.Redirect("library.xml");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đăng nhập thất bại');", true);
                        }
                    }
                }

            }
        }
    }

}