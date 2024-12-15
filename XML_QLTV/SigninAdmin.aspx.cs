using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XML_QLTV
{
    public partial class SigninAdmin : System.Web.UI.Page
    {
        string conString = "Data Source=BINHTRAN\\BINHVAN;Initial Catalog=WNC_QUANLYTHUVIEN2;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void signIn(object sender, EventArgs e)
        {
            string email = txtemail.Text;
            string phone = txtphone.Text;

            if (email == "user@gmail.com" && phone == "999")
            {
                Response.Redirect("library.xml");
            }
            else
            {
                string query = "SELECT * FROM dbo.Staff WHERE Email = @email AND PhoneNumber = @phonenumber";
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
                            Response.Redirect("AdminDashBoard.aspx");
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