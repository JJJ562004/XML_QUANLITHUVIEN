using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace XML_QLTV
{
    public partial class book_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string bookId = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(bookId))
            {
                GetBookDetails(bookId);
            }
            else
            {
                Response.Write("Book ID is missing.");
            }
        }

        private void GetBookDetails(string bookId)
        {
            string connectionString = "Data Source=BINHTRAN\\BINHVAN;Initial Catalog=WNC_QUANLYTHUVIEN2;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Book.Title, Publisher.PublisherName, Category.CategoryName, Book.PublishedYear, " +
                               "Book.Quantity, Book.Description, Book.ImageURL " +
                               "FROM Book " +
                               "INNER JOIN Category ON Book.CategoryID = Category.CategoryID " +
                               "INNER JOIN Publisher ON Book.PublisherID = Publisher.PublisherID " +
                               "WHERE Book.BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookID", bookId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    bookTitle.InnerText = reader["Title"].ToString();
                    publisherId.InnerText = reader["PublisherName"].ToString(); 
                    categoryId.InnerText = reader["CategoryName"].ToString(); 
                    publishedYear.InnerText = reader["PublishedYear"].ToString();
                    quantity.InnerText = reader["Quantity"].ToString();
                    description.InnerText = reader["Description"].ToString();

                    string imageUrl = reader["ImageURL"].ToString();
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        bookImage.Src = imageUrl;  
                    }
                    else
                    {
                        bookImage.Src = "../assets/images/portfolio-03.jpg"; 
                    }
                }
                else
                {
                    Response.Write("Book not found.");
                }

            }
        }
    }
}
