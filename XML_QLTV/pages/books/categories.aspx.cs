﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;

namespace XML_QLTV.pages.books
{
    public partial class categories : System.Web.UI.Page
    {
        private TaoXML xmlHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            xmlHandler = new TaoXML();
            if (!IsPostBack)
            {
                LoadCategories();
            }

        }

        public void Create_category_xml()
        {
            xmlHandler = new TaoXML();

            try
            {
                string sql = "SELECT * FROM Category";
                string bang = "Category";
                string xmlFile = "category.xml";
                xmlHandler.taoXML(sql, bang, xmlFile);
            }
            catch (Exception ex)
            {
                Response.Write("Error initializing page: " + ex.Message);
            }
        }


        private DataTable CreateCategorieschema()
        {
            DataTable dt = new DataTable("Category");
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Columns.Add("CategoryName", typeof(string));
            return dt;
        }

        private void LoadCategories()
        {
            Create_category_xml();
            try
            {
                DataTable dt = CreateCategorieschema();
                string filePath = Server.MapPath("../../category.xml");

                if (System.IO.File.Exists(filePath))
                {
                    dt.ReadXml(filePath);
                }
                else
                {
                    Response.Write("<script>alert('XML file not found.')</script>");
                }

                string htmlContent = string.Empty;
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
                    <tr>
                        <td class='align-middle text-center'>{count}</td>
                        <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["CategoryName"])}</td>
                        <td class='text-center align-middle'>
                            <div class='btn-group align-top'>
                                <button 
                                class='btn btn-sm btn-outline-secondary badge edit-btn' 
                                type='button' 
                                data-authorname='{HttpUtility.HtmlEncode(row["CategoryName"])}'
                                data-authorID='{row["CategoryID"]}'
                                style='margin-right:12px;'>Edit</button>
                                <input type='checkbox' name='deleteCheckbox' value='{row["CategoryID"]}' />
                            </div>
                        </td>
                    </tr>";
                    count++;
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

                string fileXML = Server.MapPath("~/library.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);
                foreach (string authorID in selectedAuthorIDs)
                {
                    XmlNode authorNode = doc.SelectSingleNode($"/Library/Categories/Category[CategoryID='{authorID}']");
                    if (authorNode != null)
                    {
                        XmlNode authorsNode = doc.SelectSingleNode("/Library/Categories");
                        authorsNode.RemoveChild(authorNode);
                        doc.Save(fileXML);
                    }
                    else
                    {

                        throw new Exception("Categories with specified IDs " + authorID + " not found.");
                    }
                }

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
                    Response.Write("<script>alert('No categories selected for deletion.')</script>");
                }

                // Reload the authors table
                LoadCategories();
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
                    Response.Write("<script>alert('Cập nhật thành công thể loại.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('No categories selected for update.')</script>");
                }
                // Load the XML file
                string fileXML = Server.MapPath("~/library.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);

                // Locate the book node by BookID
                XmlNode bookNode = doc.SelectSingleNode($"/Library/Categories/Category[CategoryID='{selectedAuthorID}']");

                if (bookNode != null)
                {
                    // Update the book's fields
                    bookNode["CategoryName"].InnerText = System.Security.SecurityElement.Escape(selectedAuthorName);        

                    // Save the updated XML
                    doc.Save(fileXML);
                }

                else
                {
                    throw new Exception("Book with specified ID not found.");
                }

                // Reload the authors table
                LoadCategories();
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

            try
            {
                DataTable dt = SearchAuthorsInDB(searchName);

                // Generate the HTML content
                string htmlContent = string.Empty;
                if (dt.Rows.Count == 0)
                {
                    Response.Write("<script>alert('No categories found with that name.')</script>");
                    LoadCategories();
                    return;
                }
                int count = 1;
                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
            <tr>
                <td class='align-middle text-center'>{count}</td>
                <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["CategoryName"])}</td>
                <td class='text-center align-middle'>
                    <div class='btn-group align-top'>
                        <button 
                        class='btn btn-sm btn-outline-secondary badge edit-btn' 
                        type='button' 
                        data-authorname='{HttpUtility.HtmlEncode(row["CategoryName"])}'
                        data-authorID='{row["CategoryID"]}'
                        style='margin-right:12px;'>Edit</button>
                        <input type='checkbox' name='deleteCheckbox' value='{row["CategoryID"]}' />
                    </div>
                </td>
            </tr>";
                    count++;
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
                string query = "SELECT * FROM Category WHERE CategoryName LIKE @SearchName";
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
                string query = "UPDATE Category SET CategoryName = @AuthorName WHERE CategoryID = @AuthorID";

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
                string query = "DELETE FROM Category WHERE CategoryID = @AuthorID";

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
                    Response.Write("<script>alert('Vui lòng điền đầy đủ tên thể loại.')</script>");
                    return;
                }

               
                // 2. Add to Database
                AddAuthorToDatabase(authorName);


                // 1. Add to XML File
                // Load the XML file
                string fileXML = Server.MapPath("~/library.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);

                // Find the highest BookID
                XmlNodeList bookNodes = doc.SelectNodes("/Library/Categories/Category/CategoryID");
                int maxBookID = 0;
                foreach (XmlNode bookNode in bookNodes)
                {
                    int currentID = int.Parse(bookNode.InnerText);
                    if (currentID > maxBookID)
                    {
                        maxBookID = currentID;
                    }
                }
                int newBookID = GetNewCategoryID(); // Increment to generate the next ID

                // Construct the new book XML
                XmlElement newBook = doc.CreateElement("Book");
                newBook.InnerXml = $@"
                <CategoryID>{newBookID}</CategoryID>
                <CategoryName>{System.Security.SecurityElement.Escape(authorName)}</CategoryName>";
                // Add the new book to the document
                XmlNode booksNode = doc.SelectSingleNode("/Library/Categories");
                booksNode.AppendChild(newBook);
                // Save the updated XML
                doc.Save(fileXML);

                // Success message
                Response.Write("<script>alert('Thêm thể loại thành công!')</script>");
                Response.Redirect(Request.RawUrl); // Refresh the page
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }

        int GetNewCategoryID()
        {
            int maxAuthorId = 0;

            // Define your database connection string
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;";

            // Query to get the maximum AuthorID
            string query = "SELECT MAX(CategoryID) FROM Category";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        // Check if result is not null and parse the value
                        if (result != DBNull.Value && result != null)
                        {
                            maxAuthorId = Convert.ToInt32(result);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error (use a logging framework in production)
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }
            }

            // Increment the maximum AuthorID to generate the next ID
            return maxAuthorId;
        }



        private void AddAuthorToDatabase(string name)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True"; // Replace with your DB connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Category (CategoryName) VALUES (@Name)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
