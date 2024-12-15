using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace XML_QLTV.pages.books
{
    public partial class books : System.Web.UI.Page
    {
        private TaoXML xmlHandler;

        protected void Page_Load(object sender, EventArgs e)
        {
            xmlHandler = new TaoXML();
            if (!IsPostBack)
            {
                LoadBooks();
            }

        }

        public void Create_Book_XML()
        {
            xmlHandler = new TaoXML();

            try
            {
                string sql = "SELECT * FROM Book";
                string bang = "Book";
                string xmlFile = "book.xml";
                xmlHandler.taoXML(sql, bang, xmlFile);
            }
            catch (Exception ex)
            {
                Response.Write("Error initializing page: " + ex.Message);
            }
        }


        private DataTable CreateAuthorSchema()
        {
            DataTable dt = new DataTable("Book");
            dt.Columns.Add("BookID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("PublisherID", typeof(int));
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Columns.Add("PublishedYear", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("ImageURL", typeof(string));
            return dt;
        }

        private void LoadBooks()
        {
            Create_Book_XML();
            Random random = new Random();
            try
            {
                DataTable dt = CreateAuthorSchema();
                string filePath = Server.MapPath("../../book.xml");

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
                        <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["Title"])}</td>
                        <td class='align-middle text-center'>{row["PublisherID"]}</td>
                        <td class='align-middle text-center'>{row["CategoryID"]}</td>
                        <td class='align-middle text-center'>{row["PublishedYear"]}</td>
                        <td class='align-middle text-center'>{row["Quantity"]}</td>
                        <td class='align-middle text-center'>{row["Description"]}</td>
                        <td class='align-middle text-center'><a href={row["ImageURL"]} target=""_blank"">Ảnh</a></td>
                        <td class='text-center align-middle'>
                           <div class='btn-group align-top'>
                               <button 
                                class='btn btn-sm btn-outline-secondary badge edit-btn' 
                                type='button' 
                                data-title='{HttpUtility.HtmlEncode(row["Title"])}'
                                data-bookID='{row["BookID"]}' 
                                data-publisherID='{row["PublisherID"]}' 
                                data-categoryID='{row["CategoryID"]}' 
                                data-year='{row["PublishedYear"]}' 
                                data-quantity='{row["Quantity"]}' 
                                data-description='{row["Description"]}' 
                                data-imageURL='{row["ImageURL"]}' 
                                style='margin-right:12px;'>Edit</button>
                                <input type='checkbox' name='deleteCheckbox' value='{row["BookID"]}' />
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
                    Response.Write("<script>alert('No books selected for deletion.')</script>");
                }

                // Delete book in library.xml
                string fileXML = Server.MapPath("~/library.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);

                // Locate the book node using BookID
                XmlNode bookNode = doc.SelectSingleNode($"/Library/Books/Book[BookID='{selectedAuthorIDs}']");
                if (bookNode != null)
                {
                    XmlNode booksNode = doc.SelectSingleNode("/Library/Books");
                    booksNode.RemoveChild(bookNode);
                    doc.Save(fileXML);
                }
                else
                {
                    throw new Exception("Book with specified ID not found.");
                }

                // Reload the authors table
                LoadBooks();
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
                string selectedBookID = Request.Form.Get("bookID");
                string selectedBookTitle = Request.Form.Get("title");
                string selectedBookpublisherID = Request.Form.Get("publisherID")?.Replace(",", "").Trim();
                string selectedBookcategoryID = Request.Form.Get("categoryID")?.Replace(",", "").Trim();
                string selectedBookYear = Request.Form.Get("publishedYear")?.Replace(",", "").Trim();
                string selectedBookQuantity = Request.Form.Get("quantity")?.Replace(",", "").Trim();
                string selectedBookDescription = Request.Form.Get("description");
                string selectedBookImageURL = Request.Form.Get("imageURL");

                if (selectedBookID != null && selectedBookID.Length > 0)
                {
                    UpdateBookInDB(selectedBookID, selectedBookTitle, selectedBookpublisherID, selectedBookcategoryID, selectedBookYear, selectedBookQuantity, selectedBookDescription, selectedBookImageURL);
                    Response.Write("<script>alert('Cập nhật thành công sách.')</script>");
                }
                else
                {
                    Response.Write("<script>alert('No books selected for update.')</script>");
                }

                // Load the XML file
                string fileXML = Server.MapPath("~/library.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);

                // Locate the book node by BookID
                XmlNode bookNode = doc.SelectSingleNode($"/Library/Books/Book[BookID='{selectedBookID}']");

                if (bookNode != null)
                {
                    // Update the book's fields
                    bookNode["Title"].InnerText = System.Security.SecurityElement.Escape(selectedBookTitle);
                    bookNode["PublisherID"].InnerText = selectedBookID;
                    bookNode["CategoryID"].InnerText = selectedBookcategoryID;
                    bookNode["PublishedYear"].InnerText = selectedBookYear;
                    bookNode["Quantity"].InnerText = selectedBookQuantity;
                    bookNode["Description"].InnerText = System.Security.SecurityElement.Escape(selectedBookDescription);
                    bookNode["ImageURL"].InnerText = System.Security.SecurityElement.Escape(selectedBookImageURL);

                    // Save the updated XML
                    doc.Save(fileXML);
                }

                else
                {
                    throw new Exception("Book with specified ID not found.");
                }


                // Reload the authors table
                LoadBooks();
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
                    Response.Write("<script>alert('No books found with that name.')</script>");
                    LoadBooks();
                    return;
                }
                int counter = 1;
                foreach (DataRow row in dt.Rows)
                {
                    htmlContent += $@"
            <tr>
                <td class='align-middle text-center'>{counter}</td>
                <td class='text-nowrap align-middle'>{HttpUtility.HtmlEncode(row["Title"])}</td>
                <td class='align-middle text-center'>{row["PublisherID"]}</td>
                <td class='align-middle text-center'>{row["CategoryID"]}</td>
                <td class='align-middle text-center'>{row["PublishedYear"]}</td>
                <td class='align-middle text-center'>{row["Quantity"]}</td>
                <td class='align-middle text-center'>{row["Description"]}</td>
                <td class='align-middle text-center'><a href={row["ImageURL"]} target=""_blank"">Ảnh</a></td>
                <td class='text-center align-middle'>
                    <div class='btn-group align-top'>
                       <button 
                        class='btn btn-sm btn-outline-secondary badge edit-btn' 
                        type='button' 
                        data-title='{HttpUtility.HtmlEncode(row["Title"])}'
                        data-bookID='{row["BookID"]}' 
                        data-publisherID='{row["PublisherID"]}' 
                        data-categoryID='{row["CategoryID"]}' 
                        data-year='{row["PublishedYear"]}' 
                        data-quantity='{row["Quantity"]}' 
                        data-description='{row["Description"]}' 
                        data-imageURL='{row["ImageURL"]}' 
                        style='margin-right:12px;'>Edit</button>
                        <input type='checkbox' name='deleteCheckbox' value='{row["BookID"]}' />
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
                string query = "SELECT * FROM Book WHERE Title LIKE @SearchName";
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



        private void UpdateBookInDB(string bookID, string title, string publisherID, string categoryID, string year, string quantity, string description, string imageURL)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Book SET Title = @Title, PublisherID = @publisherID, CategoryID = @categoryID, Quantity = @quantity, " +
                    " PublishedYear = @year, Description = @description, ImageURL = @imageURL " +
                    " WHERE BookID = @BookID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@BookID", SqlDbType.Int).Value = Convert.ToInt32(bookID);
                    command.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                    command.Parameters.Add("@publisherID", SqlDbType.Int).Value = Convert.ToInt32(publisherID);
                    command.Parameters.Add("@categoryID", SqlDbType.Int).Value = Convert.ToInt32(categoryID);
                    command.Parameters.Add("@year", SqlDbType.Int).Value = Convert.ToInt32(year);
                    command.Parameters.Add("@quantity", SqlDbType.Int).Value = Convert.ToInt32(quantity);
                    command.Parameters.Add("@description", SqlDbType.NVarChar).Value = description;
                    command.Parameters.Add("@imageURL", SqlDbType.Text).Value = imageURL;
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
                string query = "DELETE FROM Book WHERE BookID = @AuthorID";

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
                string bookName = Request.Form["name"];
                string PublisherID = Request.Form["publisherID"]?.Replace(",", "").Trim();
                string CategoryID = Request.Form["categoryID"]?.Replace(",", "").Trim();
                string PublishedYear = Request.Form["publishedYear"]?.Replace(",", "").Trim();
                string Quantity = Request.Form["quantity"]?.Replace(",", "").Trim();
                string Description = Request.Form["description"];
                string ImageURL = Request.Form["imageURL"];

                // Validate inputs
                if (string.IsNullOrWhiteSpace(bookName) ||
                    string.IsNullOrWhiteSpace(PublisherID) ||
                    string.IsNullOrWhiteSpace(CategoryID) ||
                    string.IsNullOrWhiteSpace(PublishedYear) ||
                    string.IsNullOrWhiteSpace(Quantity))
                {
                    Response.Write("<script>alert('Vui lòng điền đầy đủ thông tin sách.')</script>");
                    return;
                }

                if (!int.TryParse(PublisherID, out int publisherID) ||
                    !int.TryParse(CategoryID, out int categoryID) ||
                    !int.TryParse(PublishedYear, out int publishedYear) ||
                    !int.TryParse(Quantity, out int quantity))
                {
                    //Response.Write("<script>alert('PublisherID, CategoryID, PublishedYear, and Quantity must be valid numbers.')</script>");
                    Response.Write($"<script>alert('{PublisherID}, {CategoryID}, {PublishedYear}, and {Quantity} must be valid numbers.')</script>");
                    return;
                }

                // 1. Add to XML File library.xml
                // Load the XML file
                string fileXML = Server.MapPath("~/library.xml");
                XmlDocument doc = new XmlDocument();
                doc.Load(fileXML);

                // Find the highest BookID
                XmlNodeList bookNodes = doc.SelectNodes("/Library/Books/Book/BookID");
                int maxBookID = 0;
                foreach (XmlNode bookNode in bookNodes)
                {
                    int currentID = int.Parse(bookNode.InnerText);
                    if (currentID > maxBookID)
                    {
                        maxBookID = currentID;
                    }
                }
                int newBookID = maxBookID + 1; // Increment to generate the next ID

                // Construct the new book XML
                XmlElement newBook = doc.CreateElement("Book");
                newBook.InnerXml = $@"
                <BookID>{newBookID}</BookID>
                <Title>{System.Security.SecurityElement.Escape(bookName)}</Title>
                <PublisherID>{publisherID}</PublisherID>
                <CategoryID>{categoryID}</CategoryID>
                <PublishedYear>{publishedYear}</PublishedYear>
                <Quantity>{quantity}</Quantity>
                <Description>{System.Security.SecurityElement.Escape(Description)}</Description>
                <ImageURL>{System.Security.SecurityElement.Escape(ImageURL)}</ImageURL>";
                // Add the new book to the document
                XmlNode booksNode = doc.SelectSingleNode("/Library/Books");
                booksNode.AppendChild(newBook);
                // Save the updated XML
                doc.Save(fileXML);

                // 2. Add to Database
                AddBookToDatabase(bookName, PublisherID, CategoryID, PublishedYear, Quantity, Description, ImageURL);

                // Success message
                Response.Write("<script>alert('Thêm sách thành công!')</script>");
                Response.Redirect(Request.RawUrl); // Refresh the page
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {HttpUtility.HtmlEncode(ex.Message)}')</script>");
            }
        }



        private void AddBookToDatabase(string name, string PublisherID, string CategoryID, string PublishedYear, string Quantity, string Description, string ImageURL)
        {
            string connectionString = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;trustservercertificate=True"; // Replace with your DB connection string

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Book (Title, PublisherID, CategoryID, PublishedYear, Quantity, Description, ImageURL) VALUES (@Name, @PublisherID, @CategoryID, @PublishedYear, @Quantity, @Description, @ImageURL)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                    command.Parameters.Add("@PublisherID", SqlDbType.Int).Value = Convert.ToInt32(PublisherID);
                    command.Parameters.Add("@CategoryID", SqlDbType.Int).Value = Convert.ToInt32(CategoryID);
                    command.Parameters.Add("@PublishedYear", SqlDbType.Int).Value = Convert.ToInt32(PublishedYear);
                    command.Parameters.Add("@Quantity", SqlDbType.Int).Value = Convert.ToInt32(Quantity);
                    command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = Description;
                    command.Parameters.Add("@ImageURL", SqlDbType.Text).Value = ImageURL;
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
