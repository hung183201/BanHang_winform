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
    public partial class frmHDN : Form
    {
        public static string nhanvien = "";
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;
        public frmHDN()
        {
            InitializeComponent();
        }

        private void frmHDN_Load(object sender, EventArgs e)
        {
            LoadDongHo();
            LoadComboBox();
            LoadThongTinMuaHang();
            LoadListView();
            txtDongia.Text = "0";
        }
        private void LoadThongTinMuaHang()
        {
            txtMaHoaDon.Enabled = false;
            txtMaHoaDon.Text = dtBase.SinhMaTuDong("tHoaDonNhap", "HDN0", "MaHDN");
            txtMaNV.Text = nhanvien;
            
        }
        private void LoadListView()
        {
            lstvHoaDon.View = View.Details;
            lstvHoaDon.GridLines = true;
            lstvHoaDon.FullRowSelect = true;
            lstvHoaDon.MultiSelect = false;

            lstvHoaDon.Columns.Add("Mã sản phẩm", 120);//item.text
            lstvHoaDon.Columns.Add("Tên sản phẩm", 250);//subitem[1]
            lstvHoaDon.Columns.Add("Hoa văn", 120);//subitem[2]
            lstvHoaDon.Columns.Add("Chât liệu", 120);//subitem[2]
            lstvHoaDon.Columns.Add("Kich thước", 120);//subitem[2]
            lstvHoaDon.Columns.Add("Màu", 120);//subitem[2]
            lstvHoaDon.Columns.Add("Nhà cung cấp", 200);//subitem[3]
            lstvHoaDon.Columns.Add("Số Lượng", 80);//subitem[4]
            lstvHoaDon.Columns.Add("Đơn giá", 100);//subitem[5]
            lstvHoaDon.Columns.Add("Thành tiền", 120);//subitem[6]

        }


        private void ResetValue()
        {
            cmbMaSP.Text = "";
            cmbNhaCungCap.Text = "";
            cmbMaSP.SelectedItem = -1;
            cmbNhaCungCap.SelectedItem = -1;
            txtChatLieu.Text = "";
            txtHoaVan.Text = "";
            txtKichThuoc.Text = "";
            txtMau.Text = "";
            nudSoLuong.Value = 1;
            txtDongia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private bool Checkvalue()
        {
            if (txtMaHoaDon.TextLength == 0 || txtDongia.TextLength == 0 || cmbNhaCungCap.SelectedIndex == -1 )
            {
                MessageBox.Show("Bạn cần bổ sung thông tin sản phẩm", "Thông báo");
                return false;
            }
            return true;
        }
        private void LoadComboBox()
        {
            String sql_NhaCungCap = "Select MaNCC, TenNCC from tNhaCungCap";
            String sql_SanPham = "Select MaSP, TenSP from tSanPham";
            dtBase.FillCombo(sql_NhaCungCap, cmbNhaCungCap, "MaNCC", "TenNCC");
            dtBase.FillCombo(sql_SanPham, cmbMaSP, "MaSP", "TenSP");
            this.cmbNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbMaSP.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNhaCungCap.SelectedIndex = -1;
            cmbMaSP.SelectedIndex = -1;
        }
        private void LoadDongHo()
        {
            timerDongHo.Start();
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if (!Checkvalue())
            {
                return;
            }

            ListViewItem item = new ListViewItem();
            item.Text = cmbMaSP.SelectedValue.ToString();
            //item.SubItems.Add(cmbMaSP.SelectedValue.ToString());
            item.SubItems.Add(cmbMaSP.Text);
            item.SubItems.Add(txtHoaVan.Text);
            item.SubItems.Add(txtChatLieu.Text);
            item.SubItems.Add(txtKichThuoc.Text);
            item.SubItems.Add(txtMau.Text);
            item.SubItems.Add(cmbNhaCungCap.Text);
            item.SubItems.Add(nudSoLuong.Value.ToString());
            item.SubItems.Add(txtDongia.Text);
            item.SubItems.Add(txtThanhTien.Text);


            //kiểm tra sản phẩm đã có trong danh sách mua hàng 
            foreach (ListViewItem i in lstvHoaDon.Items)
            {
                if (item.Text == i.Text)
                {
                    MessageBox.Show("Sản phẩm này bạn đã chọn.", "Thông báo");
                    return;
                }
            }
            lstvHoaDon.Items.Add(item);

            TinhTongTien();


        }
        private void TinhTongTien()
        {
            double tongTien = 0;
            if (lstvHoaDon.Items.Count != 0)
                foreach (ListViewItem i in lstvHoaDon.Items)
                {
                    double thanhTien = double.Parse(i.SubItems[9].Text.ToString());
                    tongTien += thanhTien;
                }
            txtTongTien.Text = tongTien.ToString();
        }
        private void btnXoaSanPham_Click(object sender, EventArgs e)
        {
            if (lstvHoaDon.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (ListViewItem x in lstvHoaDon.SelectedItems)
                        lstvHoaDon.Items.Remove(x);
                }
            }
            else
                MessageBox.Show("Bạn cần chọn một dòng");
            TinhTongTien();
        }
        private bool Insert_HoaDonNhap()
        {
            if (txtMaHoaDon.TextLength == 0 || txtMaNV.Text.Length == 0)
            {
                MessageBox.Show("Kiểm tra lại mã hoá đơn và nhân viên lập", "Thông báo");
                return false;
            }
            try
            {
                 string sql = "INSERT[dbo].[tHoaDonNhap]([MaHDN], [MaNV], [NgayNhap], [MaNCC], [TongTien]) " +
                   "VALUES(N'" + txtMaHoaDon.Text.Trim() + "', N'" + txtMaNV.Text + "'" +
                   ",CAST(N'" + DateTime.Now.ToString("yyyy-MM-dd") + "' AS Date), N'" + cmbNhaCungCap.SelectedValue.ToString() + "'" +
                   ", " + float.Parse(txtTongTien.Text) + ")";
                 dtBase.DataChange(sql);
                 return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi thêm hoá đơn bán" + e.Message, "Thông báo");
                return false;
            }
        }

        private bool Insert_ChiTietHDB()
        {
            if (lstvHoaDon.Items.Count == 0)
            {
                MessageBox.Show("Chưa thêm sản phẩm nào trong giỏ hàng", "Thông báo");
                return false;
            }
            try
            {
                foreach (ListViewItem i in lstvHoaDon.Items)
                {
                    string sql = "INSERT [dbo].[tChiTietHDN] ([MaHDN], [MaSP], [SoLuong], [khuyenmai], [ThanhTien]) " +
                     "VALUES (N'" + txtMaHoaDon.Text + "', N'" + i.Text + "'," + int.Parse(i.SubItems[7].Text) + "" +
                     "," + int.Parse(i.SubItems[8].Text) + "," + float.Parse(i.SubItems[9].Text) + ")";
                    dtBase.DataChange(sql);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi thêm sản phẩm vào hoá đơn :" + e.Message, "Thông báo");
                return false;
            }

            return true;
        }
        
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValue();
            LoadThongTinMuaHang();
        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            ResetValue();
            LoadThongTinMuaHang();
            lstvHoaDon.Items.Clear();
            TinhTongTien();

        }

        private void timerDongHo_Tick(object sender, EventArgs e)
        {
            lbTimer.Text = DateTime.Now.ToLongTimeString();
            lbDate.Text = DateTime.Now.ToShortDateString();
        }


        private void nudSoLuong_ValueChanged_1(object sender, EventArgs e)
        {
            double thanhTien;
            int soLuong;
            float donGia;
            donGia = float.Parse(txtDongia.Text);
            soLuong = int.Parse(nudSoLuong.Value.ToString());
            thanhTien = soLuong * donGia;
            txtThanhTien.Text = thanhTien.ToString();
        }

        private void btnThemNCC_Click(object sender, EventArgs e)
        {
            Form f = new frmNhaCungCap();
            f.ShowDialog();
        }
        private void cmbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
     
            String sql = "SELECT TenHoaVan  FROM tHoaVan as hv, tSanPham as sp WHERE sp.MaSP=N'" + cmbMaSP.SelectedValue+ "' and sp.MaHoaVan = hv.MaHoaVan";
            txtHoaVan.Text = dtBase.GetFieldValues(sql);
            sql = "SELECT TenChatLieu  FROM tSanPham as sp ,tChatLieu as cl WHERE MaSP=N'" + cmbMaSP.SelectedValue + "' and sp.MaChatLieu = cl.MaChatLieu";
            txtChatLieu.Text = dtBase.GetFieldValues(sql);
            sql = "SELECT TenKThuoc FROM tSanPham as sp ,tKichThuoc  as kt WHERE MaSP=N'" + cmbMaSP.SelectedValue + "' and sp.MaKThuoc = kt.MaKThuoc";
            txtKichThuoc.Text = dtBase.GetFieldValues(sql);
            sql = "SELECT TenMau  FROM tSanPham as sp ,tMau as m WHERE MaSP=N'" + cmbMaSP.SelectedValue + "' and sp.MaMau = m.MaMau";
            txtMau.Text = dtBase.GetFieldValues(sql);
            sql = "SELECT DonGiaNhap  FROM tSanPham  WHERE MaSP=N'" + cmbMaSP.SelectedValue + "'";
            txtDongia.Text = dtBase.GetFieldValues(sql);
            double thanhTien;
            int soLuong;
            float donGia;
            float.TryParse(dtBase.GetFieldValues(sql), out donGia);
            txtDongia.Text = donGia.ToString();
            soLuong = int.Parse(nudSoLuong.Value.ToString());
            thanhTien = soLuong * donGia ;
            txtThanhTien.Text = thanhTien.ToString();

        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {           
            if (Insert_HoaDonNhap() || lstvHoaDon.Items.Count > 0)
            {
                if (Insert_ChiTietHDB())
                {
                    MessageBox.Show("Thanh toán thành công", "Thông báo");
                   
                }
            }
            else MessageBox.Show("Không có sản phẩm nào trong danh sách", "Thông báo");
            
            ResetValue();
            LoadThongTinMuaHang();
            lstvHoaDon.Items.Clear();

            TinhTongTien();

        }
            

        private void btnTimKiemMaSP_Click(object sender, EventArgs e)
        {
            frmSanPham a = new frmSanPham();
            a.Show();
        }
        /*
private void frmNhapHang_FormClosing(object sender, FormClosingEventArgs e)
{
if (DialogResult.Yes == MessageBox.Show("Bạn có chắc muốn thoát không?",
"Error", MessageBoxButtons.YesNoCancel))
{
frmMenu form = new frmMenu();
form.Show();
this.Hide();
}
else
return;
}*/

    }
}
