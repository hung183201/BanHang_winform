using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTL
{
    public partial class frmQLNV : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;

        public frmQLNV()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tNhanVien";
            dtTable = dtBase.DataReader("SELECT * FROM tNhanVien" );
            dgvNhanVien.DataSource = dtTable;
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Giới tính";
            dgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[4].HeaderText = "Điện thoại";
            dgvNhanVien.Columns[5].HeaderText = "Ngày sinh";
           
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frmQLNV_Load(object sender, EventArgs e)
        {
            txtMNV.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMNV.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMNV.Text = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();
            txtTenNV.Text = dgvNhanVien.CurrentRow.Cells[1].Value.ToString();
            if (dgvNhanVien.CurrentRow.Cells[2].Value.ToString() == "Nam")
            {
                rdNam.Checked = true ;
                rdNu.Checked = false;
            }
            else if(dgvNhanVien.CurrentRow.Cells[2].Value.ToString() == "Nữ")
            {
                rdNam.Checked = false;
                rdNu.Checked = true;
            }
            dtNgaySinh.Text = dgvNhanVien.CurrentRow.Cells[3].Value.ToString();

            txtDiaChi.Text = dgvNhanVien.CurrentRow.Cells[4].Value.ToString();
            txtSDT.Text = dgvNhanVien.CurrentRow.Cells[5].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMNV.Text = "";
            txtTenNV.Text = "";
            rdNam.Checked = false;
            rdNu.Checked = false;
            txtDiaChi.Text = "";
            dtNgaySinh.Value = DateTime.Now;
            txtSDT.Text = "";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMNV.Text = dtBase.SinhMaTuDong("tNhanVien", "NV0", "MaNV");
            txtTenNV.Focus();// đặt con trỏ vào textbox đó
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            string gt="";
            if (txtMNV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMNV.Focus();
                return;
            }
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (txtSDT.Text== "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            if(txtSDT.Text.Length !=10 || txtSDT.Text.Length != 11)
            {
                MessageBox.Show("Bạn phải nhập đung số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if(rdNam.Checked == false && rdNu.Checked == false)
            {
                MessageBox.Show("Bạn phải chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rdNam.Checked == true)
            {
                 gt = "Nam";
            }
            else if (rdNu.Checked == true)
            {
                 gt = "Nữ";
            }
            
            sql = "SELECT * FROM tNhanVien WHERE MaNV=N'" + txtMNV.Text.Trim() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMNV.Focus();
                txtMNV.Text = "";
                return;
            }
            sql = "INSERT INTO tNhanVien(MaNV,TenNV,GioiTinh,NgaySinh, DiaChi,DienThoai)" +
                " VALUES (N'" + txtMNV.Text.Trim() + "'" +
                ",N'" + txtTenNV.Text.Trim() + "',N'" + gt + "'" +
                ",'" + dtNgaySinh.Value.ToString() + "'" +
                ",N'" +txtDiaChi.Text.Trim() + "'" +
                ",N'" +txtSDT.Text.Trim() + "')";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMNV.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt ="";
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMNV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMNV.Focus();
                return;
            }
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNV.Focus();
                return;
            }
            if (txtSDT.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (rdNam.Checked == false && rdNu.Checked == false)
            {
                MessageBox.Show("Bạn phải chọn giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (rdNam.Checked == true)
            {
                gt = "Nam";
            }
            else if (rdNu.Checked == true)
            {
                gt = "Nữ";
            }
            sql = "UPDATE tNhanVien SET  TenNV=N'" + txtTenNV.Text.Trim().ToString() +
                    "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',DienThoai=N'" + txtSDT.Text.ToString() + "',GioiTinh=N'" + gt +
                    "',NgaySinh='" + dtNgaySinh.Value.ToString() +
                    "' WHERE MaNV=N'" + txtMNV.Text + "'";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMNV.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tChiTietHDB WHERE MaHDB=(select hdb.MaHDB from tNhanVien as nv, tHoaDonBan as hdb where nv.MaNV = hdb.MaNV and nv.MaNV=N'" + txtMNV.Text + "')";
                dtBase.DataChange(sql);
                sql = "DELETE tHoaDonBan WHERE MaNV=N'" + txtMNV.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tNhanVien WHERE MaNV=N'" + txtMNV.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }

        private void txtSDT_KeyPress(object sender, KeyPressEventArgs e)
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
            sqlSelect = "SELECT * FROM tNhanVien";
            if (txtTim.Text.Trim() != "")
                sqlSelect = sqlSelect + " Where( MaNV like '%" + txtTim.Text + "%' or TenNV like N'%" + txtTim.Text + "%' " +
                    "or DiaChi like N'%" + txtTim.Text + "%' or GioiTinh like N'%" + txtTim.Text + "%'   )";
            dgvNhanVien.DataSource = dtBase.DataReader(sqlSelect);
        }

        private void txtSDT_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
