using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;

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
            Create_authors_xml();
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
                                <button 
                                class='btn btn-sm btn-outline-secondary badge edit-btn' 
                                type='button' 
                                data-authorname='{HttpUtility.HtmlEncode(row["AuthorName"])}'
                                data-authorID='{row["AuthorID"]}'
                                style='margin-right:12px;'>Edit</button>
                                <input type='checkbox' name='deleteCheckbox' value='{row["AuthorID"]}' />
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

        protected void DeleteSelectedAuthors_Click(object sender, EventArgs e)
        {
            try
            {
                // Get selected AuthorIDs from Request.Form
                string[] selectedAuthorIDs = Request.Form.GetValues("deleteCheckbox");

                if (selectedAuthorIDs != null && selectedAuthorIDs.Length > 0)
                {
                    foreach (string authorId in selectedAuthorIDs)
                    {
                        // Delete each selected author from the database
                        DeleteAuthorFromDatabase(authorId);
                    }
                }
                else
                {
                    Response.Write("<script>alert('No authors selected for deletion.')</script>");
                }

                // Reload the authors table
                LoadAuthors();
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write($"<script>alert('Error: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }

        protected void UpdateAuthorById(object sender, EventArgs e)
        {
            try
            {
                string selectedAuthorID = Request.Form.Get("authorID");
                string selectedAuthorName = Request.Form.Get("authorname");

                if (selectedAuthorID != null && selectedAuthorID.Length > 0)
                {
                    UpdateAuthorInDB(selectedAuthorID, selectedAuthorName);
                    Response.Write("<script>alert('Cập nhật thành công tác giả.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('No authors selected for update.')</script>");
                }

                // Reload the authors table
                LoadAuthors();
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write($"<script>alert('Error: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }



        protected void SearchAuthorByName(object sender, EventArgs e)
        {
            string searchName = Request.Form.Get("searchname");
            Random random = new Random();

            try
            {
                DataTable dt = SearchAuthorsInDB(searchName);

                // Generate the HTML content
                string htmlContent = string.Empty;
                if (dt.Rows.Count == 0)
                {
                    Response.Write("<script>alert('No authors found with that name.')</script>");
                    LoadAuthors();
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
            <tr>
                <td class='align-middle text-center'>{row["AuthorID"]}</td>
                <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["AuthorName"])}</td>
                <td class='text-nowrap align-middle'>{randomDate(random)}</td>
                <td class='text-center align-middle'>
                    <div class='btn-group align-top'>
                        <button 
                        class='btn btn-sm btn-outline-secondary badge edit-btn' 
                        type='button' 
                        data-authorname='{HttpUtility.HtmlEncode(row["AuthorName"])}'
                        data-authorID='{row["AuthorID"]}'
                        style='margin-right:12px;'>Edit</button>
                        <input type='checkbox' name='deleteCheckbox' value='{row["AuthorID"]}' />
                    </div>
                </td>
            </tr>";
                }

                // Assign the filtered HTML to the table content
                tableContent.InnerHtml = htmlContent;


            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }

        private DataTable SearchAuthorsInDB(string searchName)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT AuthorID, AuthorName FROM Author WHERE AuthorName LIKE @SearchName";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchName", "%" + searchName + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable authorsTable = new DataTable();
                    adapter.Fill(authorsTable);

                    return authorsTable;
                }
            }
        }



        private void UpdateAuthorInDB(string authorID, string authorName)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Author SET AuthorName = @AuthorName WHERE AuthorID = @AuthorID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorID", authorID);
                    command.Parameters.AddWithValue("@AuthorName", authorName);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void DeleteAuthorFromDatabase(string authorId)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Author WHERE AuthorID = @AuthorID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorID", authorId);
                    command.ExecuteNonQuery();
                }
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
