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
    public partial class frmMau : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmMau()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tMau";
            dtTable = dtBase.DataReader(sql);
            dgvMau.DataSource = dtTable;
            dgvMau.Columns[0].HeaderText = "Mã Màu";
            dgvMau.Columns[1].HeaderText = "Tên Màu";
            dgvMau.Columns[0].Width = 150;
            dgvMau.Columns[1].Width = 200;
            dgvMau.AllowUserToAddRows = false;
            dgvMau.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void frmMau_Load(object sender, EventArgs e)
        {
            txtMM.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }

        private void dgvMau_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMM.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMM.Text = dgvMau.CurrentRow.Cells[0].Value.ToString();
            txtTM.Text = dgvMau.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMM.Text = "";
            txtTM.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMM.Text = dtBase.SinhMaTuDong("tMau", "M0", "MaMau");
            txtTM.Focus();// đặt con trỏ vào textbox đó
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMM.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã Màu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMM.Focus();
                return;
            }
            if (txtTM.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên Màu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTM.Focus();
                return;
            }
            sql = "SELECT * FROM tMau WHERE MaMau = N'" + txtMM.Text.Trim() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã Màu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMM.Focus();
                txtMM.Text = "";
                return;
            }
            sql = "INSERT INTO tMau" +
            " VALUES (N'" + txtMM.Text.Trim() + "'" +
            ",N'" + txtTM.Text.Trim() + "')";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMM.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMM.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã Màu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMM.Focus();
                return;
            }
            if (txtTM.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tên Màu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTM.Focus();
                return;
            }
            sql = "UPDATE tMau SET  TenMau = N'" + txtTM.Text.Trim().ToString() +
                    "' WHERE MaMau=N'" + txtMM.Text + "'";
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
            if (txtMM.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "update tSanPham Set MaMau = null where MaMau=N'" + txtMM.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tMau WHERE MaMau=N'" + txtMM.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }
    }
}
