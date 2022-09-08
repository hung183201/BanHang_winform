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
    public partial class frmHoaVan : Form
    {

        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmHoaVan()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            string sql;
            sql = "SELECT * FROM tHoaVan";
            dtTable = dtBase.DataReader(sql);
            dgvHoaVan.DataSource = dtTable;
            dgvHoaVan.Columns[0].HeaderText = "Mã Hoa Văn";
            dgvHoaVan.Columns[1].HeaderText = "Tên Hoa Văn";
            dgvHoaVan.Columns[0].Width = 150;
            dgvHoaVan.Columns[1].Width = 200;
            dgvHoaVan.AllowUserToAddRows = false;
            dgvHoaVan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void frmHoaVan_Load(object sender, EventArgs e)
        {
            txtMHV.Enabled = false;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }

        private void dgvHoaVan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMHV.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMHV.Text = dgvHoaVan.CurrentRow.Cells[0].Value.ToString();
            txtTHV.Text = dgvHoaVan.CurrentRow.Cells[1].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void ResetValues()
        {
            txtMHV.Text = "";
            txtTHV.Text = "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMHV.Text = dtBase.SinhMaTuDong("tHoaVan", "HV0", "MaHoaVan");
            txtTHV.Focus();// đặt con trỏ vào textbox đó
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMHV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã Hoa Văn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMHV.Focus();
                return;
            }
            if (txtTHV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên hoa văn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTHV.Focus();
                return;
            }
            sql = "SELECT * FROM tHoaVan WHERE MaHoaVan = N'" + txtMHV.Text.Trim() + "'";
            if (dtBase.CheckKey(sql))
            {
                MessageBox.Show("Mã Hoa Văn này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMHV.Focus();
                txtMHV.Text = "";
                return;
            }
            sql = "INSERT INTO tHoaVan" +
            " VALUES (N'" + txtMHV.Text.Trim() + "'" +
            ",N'" + txtTHV.Text.Trim() + "')";
            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMHV.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMHV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập mã hoa Van", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMHV.Focus();
                return;
            }
            if (txtTHV.Text == "")
            {
                MessageBox.Show("Bạn phải nhập tên tên hoa văn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTHV.Focus();
                return;
            }
            sql = "UPDATE tHoaVan SET  TenHoaVan=N'" + txtTHV.Text.Trim().ToString() +
                    "' WHERE MaHoaVan=N'" + txtMHV.Text + "'";
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
            if (txtMHV.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "update tSanPham Set MaHoaVan = null where MaHoaVan=N'" + txtMHV.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tHoaVan WHERE MaHoaVan=N'" + txtMHV.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }
    }
}
