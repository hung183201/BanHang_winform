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
    public partial class frmKichThuoc : Form
    {

        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmKichThuoc()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tKichThuoc";
            dtTable = dtBase.DataReader(sql);
            dgvKichThuoc.DataSource = dtTable;
            dgvKichThuoc.Columns[0].HeaderText = "Mã Kích Thước";
            dgvKichThuoc.Columns[1].HeaderText = "Tên Kích Thước";
            dgvKichThuoc.Columns[0].Width = 150;
            dgvKichThuoc.Columns[1].Width = 200;
            dgvKichThuoc.AllowUserToAddRows = false;
            dgvKichThuoc.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frmKichThuoc_Load(object sender, EventArgs e)
        {

            txtMKT.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }

        private void dgvKichThuoc_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMKT.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMKT.Text = dgvKichThuoc.CurrentRow.Cells[0].Value.ToString();
            txtTKT.Text = dgvKichThuoc.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMKT.Text = "";
            txtTKT.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMKT.Text = dtBase.SinhMaTuDong("tKichThuoc", "KT0", "MaKThuoc");
            txtTKT.Focus();// đặt con trỏ vào textbox đó
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMKT.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã Kích Thước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKT.Focus();
                return;
            }
            if (txtTKT.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên Kích Thước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTKT.Focus();
                return;
            }
            sql = "SELECT * FROM tKichThuoc WHERE MaKThuoc = N'" + txtMKT.Text.Trim() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã Kích Thước này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKT.Focus();
                txtMKT.Text = "";
                return;
            }
            sql = "INSERT INTO tKichThuoc" +
            " VALUES (N'" + txtMKT.Text.Trim() + "'" +
            ",N'" + txtTKT.Text.Trim() + "')";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMKT.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMKT.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã Kích Thước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMKT.Focus();
                return;
            }
            if (txtTKT.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tên Kích Thước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTKT.Focus();
                return;
            }
            sql = "UPDATE tKichThuoc SET  TenKThuoc = N'" + txtTKT.Text.Trim().ToString() +
                    "' WHERE MaKThuoc=N'" + txtMKT.Text + "'";
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
            if (txtMKT.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "update tSanPham Set MaKThuoc = null where MaKThuoc=N'" + txtMKT.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tKichThuoc WHERE MaKThuoc=N'" + txtMKT.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }
    }
}
