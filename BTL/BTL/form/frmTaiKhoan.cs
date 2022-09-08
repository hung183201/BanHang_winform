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
    public partial class frmTaiKhoan : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tDangNhap";
            dtTable = dtBase.DataReader(sql);
            dgvKhachHang.DataSource = dtTable;
            dgvKhachHang.Columns[0].HeaderText = "Mã Đăng Nhập";
            dgvKhachHang.Columns[1].HeaderText = "Mã Nhân Viên";
            dgvKhachHang.Columns[2].HeaderText = "Tài khoản";
            dgvKhachHang.Columns[3].HeaderText = "Mật Khẩu ";
            dgvKhachHang.Columns[4].HeaderText = "ID ";
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            txtMDN.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
             string sql = "select * from tNhanVien";
            dtBase.FillCombo(sql, cmbNV, "MaNV", "TenNV");
            cmbNV.SelectedIndex = -1;
            cmbID.Items.Add("0");
            cmbID.Items.Add("1");
            cmbID.SelectedIndex = -1;

        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMDN.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMDN.Text = dgvKhachHang.CurrentRow.Cells[0].Value.ToString();
            string NV = dgvKhachHang.CurrentRow.Cells[1].Value.ToString();
            string sql = "SELECT TenNV FROM tNhanVien WHERE MaNV=N'" + NV + "'";
            cmbNV.Text = dtBase.GetFieldValues(sql);
            txtTK.Text = dgvKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtMK.Text = dgvKhachHang.CurrentRow.Cells[3].Value.ToString();
            cmbID.Text = dgvKhachHang.CurrentRow.Cells[4].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMDN.Text = "";
            cmbID.SelectedIndex = -1;
            cmbNV.SelectedIndex = -1;
            txtMK.Text = "";
            txtTK.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMDN.Text = dtBase.SinhMaTuDong("tDangNhap", "DN0", "MaDangNhap");
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
          
            if (txtTK.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK.Focus();
                return;
            }
            if (txtMK.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Mật Khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMK.Focus();
                return;
            }
            if (cmbNV.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn Nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbNV.Focus();
                return;
            }
            if (cmbID.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải nhập ID ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbID.Focus();
                return;
            }
            sql = "SELECT * FROM tDangNhap WHERE MaNV=N'" + cmbNV.SelectedValue.ToString() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã khách này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbNV.Focus();
                return;
            }
            sql = "INSERT INTO tDangNhap VALUES (N'" + txtMDN.Text.Trim() +
                "',N'" + cmbNV.SelectedValue.ToString() + "',N'" + txtTK.Text.Trim() + "'" +
                ",N'" + txtMK.Text + "',"+int.Parse(cmbID.Text)+")";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMDN.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE TDangNhap WHERE MaDangNhap=N'" + txtMDN.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMDN.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bản ghi cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTK.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTK.Focus();
                return;
            }
            if (txtMK.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Mật Khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMK.Focus();
                return;
            }
            if (cmbNV.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn Nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbNV.Focus();
                return;
            }
            if (cmbID.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải nhập ID ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbID.Focus();
                return;
            }
            

            sql = "UPDATE tDangNhap SET MaNV =N'" + cmbNV.SelectedValue.ToString() + "'" +
               ",TaiKhoan=N'" + txtTK.Text + "',MatKhau=N'" + txtMK.Text +"',IDPer = "+int.Parse(cmbID.Text)+" " +
               "WHERE MaDangNhap=N'" + txtMDN.Text + "'";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {

        }
    }
}
