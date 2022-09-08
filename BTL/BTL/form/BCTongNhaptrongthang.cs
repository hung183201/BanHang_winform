using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;

namespace BTL.form
{
    public partial class BCTongNhaptrongthang : Form
    {
        public BCTongNhaptrongthang()
        {
            InitializeComponent();
        }

        private void BCTongNhaptrongthang_Load(object sender, EventArgs e)
        {
            cbThang.SelectedIndex = 1;
            cbNam_NhapHang.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM ufn_HoaDonNhap_TimKiemHoaDonNhapTheoThang(" + cbThang.SelectedItem.ToString() + ","+cbNam_NhapHang.SelectedItem.ToString()+")";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.QLCH_GiayDanTuongConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            DataSet ds = new DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(ds);


            rpvtongnhap.ProcessingMode = ProcessingMode.Local;
            rpvtongnhap.LocalReport.ReportPath = "rptongnhap.rdlc";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "TongNhap";
                rds.Value = ds.Tables[0];

                rpvtongnhap.LocalReport.DataSources.Clear();
                rpvtongnhap.LocalReport.DataSources.Add(rds);
                rpvtongnhap.RefreshReport();
            }
        }
    }
}
