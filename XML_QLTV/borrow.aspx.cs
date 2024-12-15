using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Xml;

namespace XML_QLTV
{
    public partial class borrow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string bookId = Request.QueryString["id"];
            string studentId = "1"; //id sinh viên cứng

            if (!IsPostBack && !string.IsNullOrEmpty(bookId))
            {
                bool isBorrowed = AddBorrowingRecord(bookId, studentId);

                if (isBorrowed)
                {
                    lblMessage.Text = "Mượn thành công";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Mượn thất bại, có vài lỗi =(";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                LoadBorrowingRecords();
            }
        }

        private bool AddBorrowingRecord(string bookId, string studentId)
        {
            try
            {
                AddBorrowingRecordToXml(bookId, studentId);

                AddBorrowingRecordToDatabase(bookId, studentId);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void AddBorrowingRecordToXml(string bookId, string studentId)
        {
            string xmlFilePath = Server.MapPath("~/library.xml");

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlElement root = xmlDoc.DocumentElement;
            XmlElement borrowingRecords = root.SelectSingleNode("BorrowingRecords") as XmlElement;

            if (borrowingRecords == null)
            {
                borrowingRecords = xmlDoc.CreateElement("BorrowingRecords");
                root.AppendChild(borrowingRecords);
            }

            XmlElement borrowingRecord = xmlDoc.CreateElement("BorrowingRecord");

            XmlElement borrowId = xmlDoc.CreateElement("BorrowID");
            borrowId.InnerText = Guid.NewGuid().ToString();
            borrowingRecord.AppendChild(borrowId);

            XmlElement studentID = xmlDoc.CreateElement("StudentID");
            studentID.InnerText = studentId;
            borrowingRecord.AppendChild(studentID);

            XmlElement bookID = xmlDoc.CreateElement("BookID");
            bookID.InnerText = bookId;
            borrowingRecord.AppendChild(bookID);

            XmlElement borrowDate = xmlDoc.CreateElement("BorrowDate");
            borrowDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            borrowingRecord.AppendChild(borrowDate);

            XmlElement dueDate = xmlDoc.CreateElement("DueDate");
            dueDate.InnerText = DateTime.Now.AddDays(14).ToString("yyyy-MM-dd"); // Hết hạn mượn, ví dụ 2 tuần từ ngày mượn (14 ngày)
            borrowingRecord.AppendChild(dueDate);

            XmlElement returnDate = xmlDoc.CreateElement("ReturnDate");
            returnDate.InnerText = "";
            borrowingRecord.AppendChild(returnDate);

            borrowingRecords.AppendChild(borrowingRecord);

            xmlDoc.Save(xmlFilePath);
        }

        private void AddBorrowingRecordToDatabase(string bookId, string studentId)
        {
            string connectionString = "Data Source=DESKTOP-IA0NH5J;Initial Catalog=WNC_QUANLYTHIVIEN_REAL;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Borrowing_Record (StudentID, BookID, BorrowDate, DueDate, ReturnDate)
                                 VALUES (@StudentID, @BookID, @BorrowDate, @DueDate, NULL)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@BookID", bookId);
                    cmd.Parameters.AddWithValue("@BorrowDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DueDate", DateTime.Now.AddDays(14));// Hết hạn mượn, ví dụ 2 tuần từ ngày mượn (14 ngày)

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadBorrowingRecords()
        {
            string connectionString = "Data Source=DESKTOP-IA0NH5J;Initial Catalog=WNC_QUANLYTHIVIEN_REAL;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT BorrowID, StudentID, BookID, BorrowDate, DueDate, ReturnDate FROM Borrowing_Record";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    gvBorrowingRecords.DataSource = dt;
                    gvBorrowingRecords.DataBind();
                }
            }
        }
    }
}
