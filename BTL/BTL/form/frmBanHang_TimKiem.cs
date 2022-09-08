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

namespace BTL
{
    public partial class frmBanHang_TimKiem : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable tblHoaDonBan;
        string sql = "";
        public frmBanHang_TimKiem()
        {
            InitializeComponent();
        }

        private void frmBanHang_TimKiem_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private bool check()
        {
            if (txttim.TextLength == 0)
            {
                MessageBox.Show("Nhập thông tin cần tìm kiếm");
                return false;
            }
            else if (rdbKhachHang.Checked == true)
            {
                sql = "Select * from tKhachHang as kh, tHoaDonBan as hdb" +
                    " where kh.MaKH = hdb.MaKH and kh.TenKH like N'%" + txttim.Text.Trim() + "%' ";
                return true;

            }
            else if (rdbNhanVien.Checked == true)
            {
                sql = "Select * from tNhanVien as nv, tHoaDonBan as hdb" +
                    " where nv.MaNV = hdb.MaNV and nv.TenNV like N'%" + txttim.Text.Trim() + "%' ";
                return true;

            }


            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                tblHoaDonBan = dtBase.DataReader(sql);
                dataGridView1.DataSource = tblHoaDonBan;
                dataGridView1.Columns[0].HeaderText = "Mã HĐB";
                dataGridView1.Columns[1].HeaderText = "Mã NV";
                dataGridView1.Columns[2].HeaderText = "Ngày Bán";
                dataGridView1.Columns[3].HeaderText = "Mã Khách Hàng";
                dataGridView1.Columns[4].HeaderText = "Thành Tiền";
                dataGridView1.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
                dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically; //Không cho sửa dữ liệu trực tiếp
            }
            else MessageBox.Show("Kiếm tra thông tin tìm kiếm");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string a = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            string b = "select * from ChiTietHDB where MaHDB = N'" + a + "'";
            dataGridView1.DataSource= dtBase.DataReader(b);
        }
    }
}
