using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace XML_QLTV.pages.books
{
    public partial class authors : System.Web.UI.Page
    {
        private TaoXML xmlHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            xmlHandler = new TaoXML();
            if (!IsPostBack)
            {
                Create_authors_xml();
                LoadAuthors();
            }

        }

        public void Create_authors_xml()
        {
            xmlHandler = new TaoXML();

            try
            {
                string sql = "SELECT * FROM Author";
                string bang = "Author";
                string xmlFile = "author.xml";
                xmlHandler.taoXML(sql, bang, xmlFile);
            }
            catch (Exception ex)
            {
                Response.Write("Error initializing page: " + ex.Message);
            }
        }

        public string randomDate(Random random)
        {
            DateTime startDate = new DateTime(1980, 1, 1);
            DateTime endDate = new DateTime(1999, 12, 31);

            // Create a random generator


            // Calculate the range in days
            int range = (endDate - startDate).Days;

            // Generate a random date
            DateTime randomDate = startDate.AddDays(random.Next(range + 1));

            // Output the result
            return randomDate.ToShortDateString();
        }

        private DataTable CreateAuthorSchema()
        {
            DataTable dt = new DataTable("Author");
            dt.Columns.Add("AuthorID", typeof(int));
            dt.Columns.Add("AuthorName", typeof(string));
            return dt;
        }

        private void LoadAuthors()
        {
            Random random = new Random();
            try
            {
                DataTable dt = CreateAuthorSchema();
                string filePath = Server.MapPath("../../author.xml");

                if (System.IO.File.Exists(filePath))
                {
                    dt.ReadXml(filePath);
                }
                else
                {
                    Response.Write("<script>alert('XML file not found.')</script>");
                }

                string htmlContent = string.Empty;

                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
                    <tr>
                        <td class='align-middle text-center'>{row["AuthorID"]}</td>
                        <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["AuthorName"])}</td>
                        <td class='text-nowrap align-middle'>{randomDate(random)}</td>
                        <td class='text-center align-middle'>
                            <div class='btn-group align-top'>
                                <button class='btn btn-sm btn-outline-secondary badge' type='button' data-toggle='modal' data-target='#user-form-modal'>Edit</button>
                                <button class='btn btn-sm btn-outline-secondary badge' type='button'><i class='fa fa-trash'></i></button>
                            </div>
                        </td>
                    </tr>";
                }

                // Assign the generated HTML to the table content
                tableContent.InnerHtml = htmlContent;
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }


        protected void AddAuthor_Click(object sender, EventArgs e)
        {
            try
            {
                // Get form data
                string authorName = Request.Form["name"];

                // Validate inputs
                if (string.IsNullOrWhiteSpace(authorName))
                {
                    Response.Write("<script>alert('Vui lòng điền đầy đủ thông tin tác giả.')</script>");
                    return;
                }

                // 1. Add to XML File

                // 2. Add to Database
                AddAuthorToDatabase(authorName);

                // Success message
                Response.Write("<script>alert('Thêm tác giả thành công!')</script>");
                Response.Redirect(Request.RawUrl); // Refresh the page
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }



        private void AddAuthorToDatabase(string name)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True"; // Replace with your DB connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Author (AuthorName) VALUES (@Name)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
