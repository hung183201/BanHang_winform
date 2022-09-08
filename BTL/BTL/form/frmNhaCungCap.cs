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
    public partial class frmNhaCungCap : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tNhaCungCap";
            dtTable = dtBase.DataReader(sql);
            dgvNCC.DataSource = dtTable;
            dgvNCC.Columns[0].HeaderText = "Mã Mã NCC";
            dgvNCC.Columns[1].HeaderText = "Tên Mã NCC";
            dgvNCC.Columns[2].HeaderText = "Diện Thoại";
            dgvNCC.Columns[3].HeaderText = "Dịa Chỉ";
            dgvNCC.AllowUserToAddRows = false;
            dgvNCC.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            txtMaNCC.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaNCC.Text = dgvNCC.CurrentRow.Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNCC.CurrentRow.Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNCC.CurrentRow.Cells[3].Value.ToString();
            txtDienThoai.Text = dgvNCC.CurrentRow.Cells[2].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
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
            txtMaNCC.Text = dtBase.SinhMaTuDong("tNhaCungCap", "NCC0", "MaNCC");
            txtTenNCC.Focus();// đặt con trỏ vào textbox đó
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCC.Focus();
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
            sql = "SELECT * FROM tNhaCungCap WHERE MaNCC = N'" + txtMaNCC.Text.Trim() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCC.Focus();
                return;
            }
            //Chèn thêm
            sql = "INSERT INTO tNhaCungCap VALUES (N'" + txtMaNCC.Text.Trim() +
                "',N'" + txtTenNCC.Text.Trim() + "',N'" + txtDienThoai.Text.Trim() + "','" + txtDiaChi.Text + "')";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaNCC.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCC.Focus();
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
            sql = "UPDATE tNhaCungCap SET TenNCC =N'" + txtTenNCC.Text.Trim().ToString() + "',DienThoai='" +
                txtDienThoai.Text + "',DiaChi =N'" + txtDiaChi.Text.Trim().ToString() +
                "' WHERE MaNCC=N'" + txtMaNCC.Text + "'";
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
            if (txtMaNCC.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
         
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                

                sql = "DELETE tChiTietHDN WHERE MaHDN=(select hdn.MaHDN from tNhaCungCap as ncc, tHoaDonNhap as hdn where ncc.MaNCC = hdn.MaNCC and ncc.MaNCC=N'" + txtMaNCC.Text.Trim() + "')";
                dtBase.DataChange(sql);
                sql = "DELETE tHoaDonNhap WHERE MaNCC=N'" + txtMaNCC.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tNhaCungCap WHERE MaNCC=N'" + txtMaNCC.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sqlSelect;
            sqlSelect = "SELECT * FROM tNhaCungCap";
            if (txtTim.Text.Trim() != "")
                sqlSelect = sqlSelect + " Where( MaNCC like '%" + txtTim.Text + "%' or TenNCC like N'%" + txtTim.Text + "%' " +
                    "or DiaChi like N'%" + txtTim.Text + "%' )";

            dgvNCC.DataSource = dtBase.DataReader(sqlSelect);
        }

        
    }
}
