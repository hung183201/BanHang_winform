using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace BTL.Classes
{
    class Functions
    {

        string strConnect = "Data Source=DESKTOP-R3MHMRD;Initial Catalog=QLCH_GiayDanTuong;Integrated Security=True";
        SqlConnection sqlConnect = null;
        //Phương thức mở kết nối 
        void OpenConnect()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
                sqlConnect.Open();
        }
        //Phương thức đóng kết nối 
        void CloseConnect()
        {
            if (sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }
        //Phương thức thực thi câu lệnh Select trả về một DataTable 
        public DataTable DataReader(string sqlSelct)
        {
            DataTable tblData = new DataTable();
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sqlSelct, sqlConnect);
            sqlData.Fill(tblData);
            CloseConnect();
            return tblData;
        }
        //Phương thức thực hiện câu lệnh dạng insert,update,delete 
        public void DataChange(string sql)
        {
            OpenConnect();
            SqlCommand sqlcomma = new SqlCommand();
            sqlcomma.Connection = sqlConnect;
            sqlcomma.CommandText = sql;
            sqlcomma.ExecuteNonQuery();
            CloseConnect();
            
        }
        public bool CheckKey(string sql)
        {
            OpenConnect();
            SqlDataAdapter dap = new SqlDataAdapter(sql, sqlConnect);
            DataTable table = new DataTable();
            dap.Fill(table);
            
            if (table.Rows.Count > 0)
                return true;
            else return false;
            
        }
       public void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            OpenConnect();
            SqlDataAdapter dap = new SqlDataAdapter(sql, sqlConnect);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma; //Trường giá trị
            cbo.DisplayMember = ten; //Trường hiển thị
        }
      
        public string SinhMaTuDong(string TenBang, string MaBatDau, string TruongMa)
        {
            int id = 1;
            bool dung = false;
            string ma = "";
            DataTable dt = new DataTable();
            while (dung == false)
            {
                dt = DataReader("select * from " + TenBang + " where " + TruongMa + " = '" + MaBatDau + id.ToString() + "'");
                if (dt.Rows.Count == 0)
                {
                    dung = true;
                }
                else
                {
                    id++;
                    dung = false;
                }
            }
            ma = MaBatDau + id.ToString();
            return ma;
        }
        public string GetFieldValues(string sql)
        {
            OpenConnect();
            string ma = "";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, sqlConnect);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    ma = reader.GetValue(0).ToString();
                reader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            CloseConnect();
            return ma;
        }
    }
}
