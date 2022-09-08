using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL.form
{
    public partial class frmQLKH : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmQLKH()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tKhachHang";
            dtTable = dtBase.DataReader(sql);
            dgvKhachHang.DataSource = dtTable;
            dgvKhachHang.Columns[0].HeaderText = "Mã Khách Hàng";
            dgvKhachHang.Columns[1].HeaderText = "Tên Khách Hàng";
            dgvKhachHang.Columns[2].HeaderText = "Dịa Chỉ";
            dgvKhachHang.Columns[3].HeaderText = "Diện Thoại";
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frmQLKH_Load(object sender, EventArgs e)
        {
            txtMKH.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMKH.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMKH.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
            txtTenKH.Text = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMKH.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMKH.Text = dtBase.SinhMaTuDong("tKhachHang", "KH0", "MaKH");
            txtTenKH.Focus();// đặt con trỏ vào textbox đó
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMKH.Focus();
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            //Kiểm tra đã tồn tại mã khách chưa
            sql = "SELECT * FROM tKhachHang WHERE MaKH=N'" + txtMKH.Text.Trim() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMKH.Focus();
                return;
            }
            //Chèn thêm
            sql = "INSERT INTO tKhachHang VALUES (N'" + txtMKH.Text.Trim() +
                "',N'" + txtTenKH.Text.Trim() + "',N'" + txtDiaChi.Text.Trim() + "','" + txtDienThoai.Text + "')";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMKH.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMKH.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKH.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtDienThoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienThoai.Focus();
                return;
            }
            sql = "UPDATE tKHachHang SET TenKH =N'" + txtTenKH.Text.Trim().ToString() + "',DiaChi=N'" +
                txtDiaChi.Text.Trim().ToString() + "',DienThoai='" + txtDienThoai.Text.Trim().ToString() +
                "' WHERE MaKH=N'" + txtMKH.Text + "'";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMKH.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tChiTietHDB WHERE MaHDB=(select hdb.MaHDB from tKhachHang as kh, tHoaDonBan as hdb where kh.MaKH = hdb.MaKH and kh.MaKH=N'" + txtMKH.Text + "')";
                dtBase.DataChange(sql);
                sql = "DELETE tHoaDonBan WHERE MaKH=N'" + txtMKH.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tKhachHang WHERE MaKH=N'" + txtMKH.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) < Convert.ToInt32('0') || Convert.ToInt32(e.KeyChar)
                > Convert.ToInt32(Convert.ToInt32('9')) && Convert.ToInt16(e.KeyChar) != 8)
            {
                MessageBox.Show("chi được phép nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sqlSelect;
            sqlSelect = "SELECT * FROM tKhachHang";
            if (txtTim.Text.Trim() != "")
                sqlSelect = sqlSelect + " Where( MaKH like '%" + txtTim.Text + "%' or TenKH like N'%" + txtTim.Text + "%' " +
                    "or DiaChi like N'%" + txtTim.Text + "%' )";
                
            dgvKhachHang.DataSource = dtBase.DataReader(sqlSelect);
        }
    }
}
