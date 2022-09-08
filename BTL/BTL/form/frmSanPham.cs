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
    public partial class frmSanPham : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        string imageName;
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            string sql;
            sql = "select MaSP, TenSP, hv.TenHoaVan, cl.TenChatLieu, DonGiaNhap, DonGiaBan, Soluong, kt.TenKThuoc, m.TenMau, HinhAnh " +
                "from tSanPham as sp ,tChatLieu as cl ,tHoaVan as hv,tMau as m,tKichThuoc as kt " +
                "where sp.MaChatLieu = cl.MaChatLieu " +
                "and sp.MaHoaVan = hv.MaHoaVan " +
                "and sp.MaKThuoc = kt.MaKThuoc " +
                "and sp.MaMau = m.MaMau";
            dtTable = dtBase.DataReader(sql);
            dgvSanPham.DataSource = dtTable;
            dgvSanPham.Columns[0].HeaderText = "Mã SP";
            dgvSanPham.Columns[1].HeaderText = "Tên SP";
            dgvSanPham.Columns[2].HeaderText = "Hoa văn";
            dgvSanPham.Columns[3].HeaderText = "Chất Liệu";
            dgvSanPham.Columns[4].HeaderText = "Đơn Giá Nhập";
            dgvSanPham.Columns[5].HeaderText = "Đơn Giá Bán";
            dgvSanPham.Columns[6].HeaderText = "Số Lượng";
            dgvSanPham.Columns[7].HeaderText = "Kích Thước";
            dgvSanPham.Columns[8].HeaderText = "Màu";
            dgvSanPham.Columns[9].HeaderText = "Hình ảnh";
            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "select * from tKichThuoc";
            dtBase.FillCombo(sql, cbKichThuoc, "MaKThuoc", "TenKThuoc");
            cbKichThuoc.SelectedIndex = -1;
            sql = "select * from tChatLieu";
            dtBase.FillCombo(sql, cbChatLieu, "MaChatLieu", "TenChatLieu");
            cbChatLieu.SelectedIndex = -1;
            sql = "select * from tHoaVan";
            dtBase.FillCombo(sql, cbHoaVan, "MaHoaVan", "TenHoaVan");
            cbHoaVan.SelectedIndex = -1;
            sql = "select * from tMau";
            dtBase.FillCombo(sql, cbMau, "MaMau", "TenMau");
            cbMau.SelectedIndex = -1;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            LoadData();
        }
        private void ResetValues()
        {
            txtMSP.Text = "";
            txtTenSP.Text = "";
            cbChatLieu.Text = "";
            txtSL.Text = "";
            txtGiaBan.Text = "";
            txtGiaNhap.Text = "";
            txtSL.Enabled = true;
            txtGiaBan.Enabled = false;
            txtGiaNhap.Enabled = false;
            pbAnh.Text = "";
            pbAnh.Image = null;
            cbHoaVan.Text = "";
            cbKichThuoc.Text = "";
            cbMau.Text = "";
        }
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSP.Focus();
                return;
            }
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMSP.Text = dgvSanPham.CurrentRow.Cells[0].Value.ToString();
            txtTenSP.Text = dgvSanPham.CurrentRow.Cells[1].Value.ToString();
            cbHoaVan.Text = dgvSanPham.CurrentRow.Cells[2].Value.ToString();
            cbChatLieu.Text = dgvSanPham.CurrentRow.Cells[3].Value.ToString();
            txtGiaNhap.Text = dgvSanPham.CurrentRow.Cells[4].Value.ToString();
            txtGiaBan.Text = dgvSanPham.CurrentRow.Cells[5].Value.ToString();
            txtSL.Text = dgvSanPham.CurrentRow.Cells[6].Value.ToString();
            cbKichThuoc.Text = dgvSanPham.CurrentRow.Cells[7].Value.ToString();
            cbMau.Text = dgvSanPham.CurrentRow.Cells[8].Value.ToString();
            cbChatLieu.SelectedIndex = cbChatLieu.FindStringExact(dgvSanPham.CurrentRow.Cells[3].Value.ToString());
            cbHoaVan.SelectedIndex = cbHoaVan.FindStringExact(dgvSanPham.CurrentRow.Cells[2].Value.ToString());
            cbKichThuoc.SelectedIndex = cbKichThuoc.FindStringExact(dgvSanPham.CurrentRow.Cells[7].Value.ToString());
            cbMau.SelectedIndex = cbMau.FindStringExact(dgvSanPham.CurrentRow.Cells[8].Value.ToString());
            try
            {
                imageName = dgvSanPham.CurrentRow.Cells[9].Value.ToString();
                pbAnh.Image = Image.FromFile(Application.StartupPath.ToString() + "\\Image\\sanpham\\" + imageName);
            }
            catch
            {

            }
            txtGiaBan.Enabled = true;
            txtGiaNhap.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMSP.Text = dtBase.SinhMaTuDong("tSanPham","SP00","MaSP");
            txtTenSP.Focus();
            txtSL.Enabled = true;
            txtGiaBan.Enabled = true;
            txtGiaNhap.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSP.Focus();
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (cbChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbChatLieu.Focus();
                return;
            }
            if (cbHoaVan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn Hoa văn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbHoaVan.Focus();
                return;
            }
            if (cbKichThuoc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn kích thước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbKichThuoc.Focus();
                return;
            }
            if (cbMau.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn màu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbKichThuoc.Focus();
                return;
            }
           
         
            sql = "INSERT INTO tSanPham VALUES(N'"+txtMSP.Text.Trim()+"',N'"+txtTenSP.Text.Trim()+"'" +
                ",N'"+cbHoaVan.SelectedValue.ToString()+"',N'"+cbChatLieu.SelectedValue.ToString()+"'" +
                ",'"+float.Parse(txtGiaNhap.Text)+ "','" + float.Parse(txtGiaBan.Text) + "'" +
                ",'" + int.Parse(txtSL.Text) + "',N'"+cbKichThuoc.SelectedValue.ToString()+"'" +
                ",N'"+cbMau.SelectedValue.ToString()+"',N'"+imageName+"')";

            dtBase.DataChange(sql);
            LoadData();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            string[] pathAnh;
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "PNG Images|*.jpg|All Files|*.*";
            dlgOpen.InitialDirectory = Application.StartupPath.ToString() + "\\Image\\sanpham";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                pbAnh.Image = Image.FromFile(dlgOpen.FileName);
                pathAnh = dlgOpen.FileName.Split('\\');
                imageName = pathAnh[pathAnh.Length - 1];
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (dtTable.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMSP.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tChiTietHDB WHERE MaSP=N'" + txtMSP.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tChiTietHDN WHERE MaSP=N'" + txtMSP.Text + "'";
                dtBase.DataChange(sql);
                sql = "DELETE tSanPham WHERE MaSP=N'" + txtMSP.Text + "'";
                dtBase.DataChange(sql);
                LoadData();
                ResetValues();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMSP.Text == "" || txtTenSP.Text == "" || txtGiaBan.Text == "" || txtGiaNhap.Text == "" || txtSL.Text == "" ||
                cbChatLieu.Text == "" || cbHoaVan.Text == "" || cbKichThuoc.Text == "" || cbMau.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn dữ liệu để sửa");
                return;
            }
            string machatlieu = "";
            string makichthuoc = "";
            string mahoavan = "";
            string mamau = "";
            try
            {
                machatlieu = cbChatLieu.SelectedValue.ToString();
                mahoavan = cbHoaVan.SelectedValue.ToString();
                makichthuoc = cbKichThuoc.SelectedValue.ToString();
                mamau = cbMau.SelectedValue.ToString();

            }
            catch
            {
                DataTable dtsanpham = dtBase.DataReader("select * from tSanPham where MaSP='" + txtMSP.Text + "'");
                machatlieu = dtsanpham.Rows[0]["MaChatLieu"].ToString();
                makichthuoc = dtsanpham.Rows[0]["MaKThuoc"].ToString();
                mahoavan = dtsanpham.Rows[0]["MaHoaVan"].ToString();
                mamau = dtsanpham.Rows[0]["MaMau"].ToString();
            }

            dtBase.DataChange("update tSanPham set TenSP=N'" + txtTenSP.Text + "', MaChatLieu=N'" + machatlieu + "', " +
                    "MaHoaVan=N'" + mahoavan + "',MaKThuoc=N'" + makichthuoc + "', " +
                    "MaMau=N'" + mamau + "',SoLuong=" + int.Parse(txtSL.Text) + ",DonGiaNhap=" + float.Parse(txtGiaNhap.Text) + ", " +
                    "DonGiaBan=" + float.Parse(txtGiaBan.Text) + ",HinhAnh=N'" + imageName + "' where MaSP='" + txtMSP.Text + "'");
            LoadData();
            ResetValues();
            MessageBox.Show("Sửa thành công");

        }

        private void btnThemKT_Click(object sender, EventArgs e)
        {
            frmKichThuoc KichThuoc = new frmKichThuoc();
            KichThuoc.ShowDialog();
        }

        private void btnThemCL_Click(object sender, EventArgs e)
        {
            frmChatLieu ChatLieu = new frmChatLieu();
            ChatLieu.ShowDialog();
        }

        private void btnThemHV_Click(object sender, EventArgs e)
        {
            frmHoaVan HoaVan = new frmHoaVan();
            HoaVan.ShowDialog();
        }

        private void btnThemMau_Click(object sender, EventArgs e)
        {
            frmMau Mau = new frmMau();
            Mau.ShowDialog();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sqlSelect = "select MaSP,TenSP,TenChatLieu,TenHoaVan,TenKThuoc,TenMau,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh" +
               " from tSanPham sp inner join tChatLieu cl on sp.MaChatLieu=cl.MaChatLieu inner join tHoaVan hv on sp.MaHoaVan=hv.MaHoaVan " +
               "inner join tKichThuoc kt on sp.MaKThuoc=kt.MaKThuoc inner join tMau m on sp.MaMau=m.MaMau" +
               " where MaSP is not null";
            if (txtTim.Text.Trim() != "")
                sqlSelect = sqlSelect + " and (MaSP like '%" + txtTim.Text + "%' or TenSP like N'%" + txtTim.Text + "%' or TenChatLieu like N'%" + txtTim.Text + "'" +
                    " or TenHoaVan like N'%" + txtTim.Text + "%' or TenKThuoc like N'%" + txtTim.Text + "%' or TenMau like N'%" + txtTim.Text + "%')";
            dgvSanPham.DataSource = dtBase.DataReader(sqlSelect);
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) < Convert.ToInt32('0') || Convert.ToInt32(e.KeyChar)
                > Convert.ToInt32(Convert.ToInt32('9')) && Convert.ToInt16(e.KeyChar) != 8)
            {
                MessageBox.Show("chi được phép nhập số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
            }
        }
    }
}
