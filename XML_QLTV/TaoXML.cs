using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Web.ApplicationServices;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace XML_QLTV
{
    public class TaoXML
    {
        string strCon = "Data Source=ADMIN-PC;Initial Catalog=WNC_QUANLYTHUVIEN_REAL;Integrated Security=True;Encrypt=False;";

        public void taoXML(string sql, string bang, string fileXML)
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + fileXML))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + fileXML);
            }
            SqlConnection con = new SqlConnection(strCon);
            con.Open();
            SqlDataAdapter ad = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable(bang);
            ad.Fill(dt);
            dt.WriteXml(AppDomain.CurrentDomain.BaseDirectory + fileXML, XmlWriteMode.WriteSchema);
        }

        public DataTable loadDataGridView(string fileXML)
        {
            DataTable dt = new DataTable();
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + fileXML;
            if (File.Exists(FilePath))
            {
                // create thread for processing xml file
                FileStream fsReadXML = new FileStream(FilePath, FileMode.Open);
                // read xml file into data table
                dt.ReadXml(fsReadXML);
                fsReadXML.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("File is not available");
            }
            return dt;
        }

        public void AddXML(string fileXML, string xml)
        {
            try
            {
                XmlTextReader textRead = new XmlTextReader(fileXML);
                XmlDocument doc = new XmlDocument();
                doc.Load(textRead);
                textRead.Close();
                XmlNode currNode;
                XmlDocumentFragment docFrag = doc.CreateDocumentFragment();
                docFrag.InnerXml = xml;
                currNode = doc.DocumentElement;
                currNode.InsertAfter(docFrag, currNode.LastChild);
            }
            catch
            {
                System.Windows.MessageBox.Show("Error");
            }
        }

        public void DeleteXML(string fileXML, string xml)
        {
            try
            {
                string fileName = AppDomain.CurrentDomain.BaseDirectory + fileXML;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode nodeCu = doc.SelectSingleNode(xml);
                doc.DocumentElement.RemoveChild(nodeCu);
                doc.Save(fileName);
            }
            catch
            {
                System.Windows.MessageBox.Show("Error");
            }
        }

        public void UpdateXML(string fileXML, string xml, string sql, string table)
        {
            XmlTextReader reader = new XmlTextReader(fileXML);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            XmlNode oldValue;
            XmlElement root = doc.DocumentElement;
            oldValue = root.SelectSingleNode(sql);
            XmlElement newValue = doc.CreateElement(table);
            newValue.InnerXml = xml;
            root.ReplaceChild(newValue, oldValue);
            doc.Save(fileXML);
        }

        public void Search(string fileXML, string xml, DataGridView dgv)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(AppDomain.CurrentDomain.BaseDirectory + fileXML);
            string xPath = xml;
            XmlNode node = xDoc.SelectSingleNode(xPath);
            DataSet ds = new DataSet();
            XmlNodeReader nr = new XmlNodeReader(node);
            ds.ReadXml(nr);
            dgv.DataSource = ds.Tables[0];
            nr.Close();
        }

        public string RetrieveValue(string path, string colA, string valA, string colB)
        {
            string valB = "";
            DataTable dt = new DataTable();
            int soDong = dt.Rows.Count;
            for (int i = 0; i < soDong; i++)
            {
                if (dt.Rows[i][colA].ToString().Equals(valA))
                {
                    valB = dt.Rows[i][colB].ToString();
                    return valB;
                }
            }
            return valB;
        }

        public bool Check(string fileXML, string colCheck, string valCheck)
        {
            DataTable dt = new DataTable();
            dt = loadDataGridView(fileXML);
            dt.DefaultView.RowFilter = colCheck + " = '" + valCheck + "' ";
            if (dt.DefaultView.Count > 0)
            {
                return true;
            }
            return false;
        }

        public int getID(string fileXML, string colName)
        {
            int id = 0;
            DataTable dt = new DataTable();
            dt = loadDataGridView(fileXML);
            int c = dt.Rows.Count;
            if (c == 0)
            {
                id = 1;
            }
            else
            {
                id = int.Parse(dt.Rows[c - 1][colName].ToString()) + 1;
            }
            return id;
        }

        public bool CheckID(string fileXML, string colID, int id)
        {
            bool check = true;
            DataTable dt = new DataTable();
            dt = loadDataGridView(fileXML);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][colID].ToString().Trim().Equals(id))
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }
            return check;
        }

        public void executeNonQuery(string sql)
        {
            SqlConnection con = new SqlConnection(strCon);
            con.Open();
            SqlCommand com = new SqlCommand(sql, con);
            com.ExecuteNonQuery();
        }

        public void Add_Database(string tableName, string fileXML)
        {
            try
            {
                DataTable table = loadDataGridView(fileXML);

                if (table == null || table.Rows.Count == 0)
                {
                    throw new Exception("No data available to insert.");
                }

                DataRow lastRow = table.Rows[table.Rows.Count - 1];
                string sql = "INSERT INTO " + tableName + " VALUES (";

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    object value = lastRow[j];
                    if (value == DBNull.Value || value == null)
                    {
                        sql += "NULL,";
                    }
                    else if (value is string)
                    {
                        sql += "N'" + value.ToString().Trim().Replace("'", "''") + "',";
                    }
                    else
                    {
                        sql += value.ToString() + ",";
                    }
                }

                sql = sql.TrimEnd(',') + ")";
                executeNonQuery(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public void Update_Database(string tenBang, string _FileXML, string condition)
        {
            try
            {
                DataTable table = loadDataGridView(_FileXML);

                if (table == null || table.Rows.Count == 0)
                {
                    throw new Exception("No data available to update.");
                }

                DataRow lastRow = table.Rows[table.Rows.Count - 1];

                string sql = $"UPDATE {tenBang} SET ";

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    sql += $"{table.Columns[j].ColumnName} = @{table.Columns[j].ColumnName},";
                }

                sql = sql.TrimEnd(',') + $" WHERE {condition}";

                for (int j = 0; j < table.Columns.Count; j++)
                {
                    sql = sql.Replace($"@{table.Columns[j].ColumnName}", lastRow[j]?.ToString().Trim() ?? "NULL");
                }

                executeNonQuery(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void Delete_Database(string tenBang, string condition)
        {
            try
            {
                string sql = $"DELETE FROM {tenBang} WHERE {condition}";

                executeNonQuery(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SearchXSLT(string data, string tenFileXML, string tenfileXSLT)

        {

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load("" + tenfileXSLT + ".xslt");
            XsltArgumentList argList = new XsltArgumentList();
            argList.AddParam("Data", "", data);
            XmlWriter writer = XmlWriter.Create("" + tenFileXML + ".html");
            xslt.Transform(new XPathDocument("" + tenFileXML + ".xml"), argList, writer);
            writer.Close();
            System.Diagnostics.Process.Start("" + tenFileXML + ".html");

        }

    }

}