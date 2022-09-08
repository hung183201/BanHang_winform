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
    public partial class BCTongBantrongthang : Form
    {
        public BCTongBantrongthang()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM ufn_HoaDonBan_TimKiemHoaDonBanTheoThangnam(" + cbThang.SelectedItem.ToString() + "," + cbNam_NhapHang.SelectedItem.ToString() + ")";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = Properties.Settings.Default.QLCH_GiayDanTuongConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = con;

            DataSet ds = new DataSet();
            SqlDataAdapter dap = new SqlDataAdapter(cmd);
            dap.Fill(ds);

            rpvtongban.ProcessingMode = ProcessingMode.Local;
            rpvtongban.LocalReport.ReportPath = "rptongxuat.rdlc";
            if (ds.Tables[0].Rows.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "TongBan";
                rds.Value = ds.Tables[0];

                rpvtongban.LocalReport.DataSources.Clear();
                rpvtongban.LocalReport.DataSources.Add(rds);
                rpvtongban.RefreshReport();
            }
        }

        private void cbThang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BCTongBantrongthang_Load(object sender, EventArgs e)
        {
            cbThang.SelectedIndex = 1;
            cbNam_NhapHang.SelectedIndex = 1;
        }
    }
}
