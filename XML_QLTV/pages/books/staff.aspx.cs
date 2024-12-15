using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace XML_QLTV.pages.books
{
    public partial class staff : System.Web.UI.Page
    {
        private TaoXML xmlHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            xmlHandler = new TaoXML();
            if (!IsPostBack)
            {
                LoadStaff();
            }

        }

        public void Create_Staff_XML()
        {
            xmlHandler = new TaoXML();

            try
            {
                string sql = "SELECT * FROM Staff";
                string bang = "Staff";
                string xmlFile = "staff.xml";
                xmlHandler.taoXML(sql, bang, xmlFile);
            }
            catch (Exception ex)
            {
                Response.Write("Error initializing page: " + ex.Message);
            }
        }


        private DataTable CreatStaffSchema()
        {
            DataTable dt = new DataTable("Staff");
            dt.Columns.Add("StaffID", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("Role", typeof(string));
            return dt;
        }

        private void LoadStaff()
        {
            Create_Staff_XML();
            Random random = new Random();
            try
            {
                DataTable dt = CreatStaffSchema();
                string filePath = Server.MapPath("../../staff.xml");

                if (System.IO.File.Exists(filePath))
                {
                    dt.ReadXml(filePath);
                }
                else
                {
                    Response.Write("<script>alert('XML file not found.')</script>");
                }

                string htmlContent = string.Empty;
                int counter = 1;
                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
                    <tr>
                        <td class='align-middle text-center'>{counter}</td>
                        <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["LastName"])}</td>
                        <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["FirstName"])}</td>
                        <td class='align-middle text-center'>{row["Email"]}</td>
                        <td class='align-middle text-center'>{row["PhoneNumber"]}</td>
                        <td class='align-middle text-center'>{row["Role"]}</td>
                        <td class='text-center align-middle'>
                           <div class='btn-group align-top'>
                               <button 
                                class='btn btn-sm btn-outline-secondary badge edit-btn' 
                                type='button'
                                data-staffID='{row["StaffID"]}' 
                                data-lastname='{row["LastName"]}' 
                                data-firstname='{row["FirstName"]}' 
                                data-email='{row["Email"]}' 
                                data-phonenumber='{row["PhoneNumber"]}' 
                                data-role='{row["Role"]}' 
                                style='margin-right:12px;'>Edit</button>
                                <input type='checkbox' name='deleteCheckbox' value='{row["StaffID"]}' />
                            </div>
                        </td>
                    </tr>";
                    counter++;
                }

                // Assign the generated HTML to the table content
                tableContent.InnerHtml = htmlContent;
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }

        protected void DeleteSelectedBooks_Click(object sender, EventArgs e)
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
                    Response.Write("<script>alert('No staffs selected for deletion.')</script>");
                }

                // Reload the authors table
                LoadStaff();
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write($"<script>alert('Error: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }

        protected void UpdateBookById(object sender, EventArgs e)
        {
            try
            {
                string lastName = Request.Form["lastName"].TrimStart(',');
                string firstName = Request.Form["firstName"].TrimStart(',');
                string email = Request.Form["email"].TrimStart(',');
                string phoneNumber = Request.Form["phoneNumber"].TrimStart(',');
                string role = Request.Form["role"].TrimStart(',');
                string staffID = Request.Form["staffID"];

                if (staffID != null && staffID.Length > 0)
                {   
                    if (phoneNumber.Length > 10)
                    {
                        phoneNumber = phoneNumber.Length > 10 ? phoneNumber.Substring(0, 10) : phoneNumber;
                    }
                    UpdateBookInDB(firstName, lastName, email, phoneNumber, role, staffID);
                    Response.Write("<script>alert('Cập nhật thành công cán bộ.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('No staffs selected for update.')</script>");
                }

                // Reload the authors table
                LoadStaff();
            }
            catch (Exception ex)
            {
                // Handle errors
                Response.Write($"<script>alert('Error: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }



        protected void SearchBookByName(object sender, EventArgs e)
        {
            string searchName = Request.Form.Get("searchname");
            Random random = new Random();

            try
            {
                DataTable dt = SearchBooksInDB(searchName);

                // Generate the HTML content
                string htmlContent = string.Empty;
                if (dt.Rows.Count == 0)
                {
                    Response.Write("<script>alert('No staffs found with that name.')</script>");
                    LoadStaff();
                    return;
                }
                int counter = 1;
                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
            <tr>
                <td class='align-middle text-center'>{counter}</td>
               <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["LastName"])}</td>
                <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["FirstName"])}</td>
                <td class='align-middle text-center'>{row["Email"]}</td>
                <td class='align-middle text-center'>{row["PhoneNumber"]}</td>
                <td class='align-middle text-center'>{row["Role"]}</td>
                <td class='text-center align-middle'>
                    <div class='btn-group align-top'>
                        <button 
                                class='btn btn-sm btn-outline-secondary badge edit-btn' 
                                type='button'
                                data-staffID='{row["StaffID"]}' 
                                data-lastname='{row["LastName"]}' 
                                data-firstname='{row["FirstName"]}' 
                                data-email='{row["Email"]}' 
                                data-phonenumber='{row["PhoneNumber"]}' 
                                data-role='{row["Role"]}' 
                                style='margin-right:12px;'>Edit</button>
                        <input type='checkbox' name='deleteCheckbox' value='{row["StaffID"]}' />
                    </div>
                </td>
            </tr>";
                    counter++;
                }

                // Assign the filtered HTML to the table content
                tableContent.InnerHtml = htmlContent;


            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }

        private DataTable SearchBooksInDB(string searchName)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Staff WHERE FirstName LIKE @SearchName OR LastName LIKE @SearchName";
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



        private void UpdateBookInDB(string FirstName, string LastName, string Email, string PhoneNumber, string Role, string StaffID)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Staff SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, Role = @Role " +
                    " WHERE StaffID = @StaffID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@Role", Role);
                    command.Parameters.AddWithValue("@StaffID", StaffID);
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
                string query = "DELETE FROM Staff WHERE StaffID = @AuthorID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AuthorID", authorId);
                    command.ExecuteNonQuery();
                }
            }
        }



        protected void AddBook_Click(object sender, EventArgs e)
        {
            try
            {
                // Get form data
                string lastName = Request.Form["lastName"].TrimStart(',');
                string firstName = Request.Form["firstName"].TrimStart(',');
                string email = Request.Form["email"].TrimStart(',');
                string phoneNumber = Request.Form["phoneNumber"].TrimStart(',');
                string role = Request.Form["role"].TrimStart(',');



                // Validate inputs
                if (string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(phoneNumber) ||
                    string.IsNullOrWhiteSpace(role))
                {
                    Response.Write("<script>alert('Vui lòng điền đầy đủ thông tin cán bộ.')</script>");
                    return;
                }


                // 1. Add to XML File

                // 2. Add to Database
                AddBookToDatabase(firstName, lastName, email, phoneNumber, role);

                // Success message
                Response.Write("<script>alert('Thêm cán bộ thành công!')</script>");
                Response.Redirect(Request.RawUrl); // Refresh the page
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }



        private void AddBookToDatabase(string FirstName, string LastName, string Email, string PhoneNumber, string Role)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True"; // Replace with your DB connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Staff (FirstName, LastName, Email, PhoneNumber, Role) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Role)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                    command.Parameters.AddWithValue("@Role", Role);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
