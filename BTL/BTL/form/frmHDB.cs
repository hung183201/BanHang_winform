using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace BTL.form
{
    public partial class frmHDB : Form
    {
        public static string nhanvien;
        Classes.Functions dtBase = new Classes.Functions();
        DataTable dtTable;//đổ dữ liệu từ sql vào datatable sau đó đổ vào datagridview
        double tongTien = 0;//tổng tiền hoá đơn
        string imageName;
        public frmHDB()
        {
            InitializeComponent();
        }

        private void frmHDB_Load(object sender, EventArgs e)
        {
            LoadDongHo();
            LoadComboBox();
            LoadDataGridView();//Load combobox trước và chọn index trước khi load datagridview
            LoadListView();
            LoadThongTinMuaHang();
            txtGiaBan.Text = "0";
            txtKhuyenMai.Text = "0";
        }
        private void ResetValue_ALL()
        {
            txtSDT.Text = "";
            txtTKH.Text = "";
            txtMSP.Text = "";
            txtTSP.Text = "";
            txtChatLieu.Text = "";
            txtHoaVan.Text = "";
            txtKichThuoc.Text = "";
            txtMauSac.Text = "";
            nudSL.Value = 1;
            txtGiaBan.Text = "0";
            txtKhuyenMai.Text = "0";
            txtThanhTien.Text = "";
            tongTien = 0;
            txtTongTien.Text = tongTien.ToString();
            lstcthdb.Clear();
        }
        private void ResetValue_SanPham()
        {

            txtMSP.Text = "";
            txtTSP.Text = "";
            txtChatLieu.Text = "";
            txtHoaVan.Text = "";
            txtKichThuoc.Text = "";
            txtMauSac.Text = "";
            nudSL.Value = 1;
            txtGiaBan.Text = "0";
            txtKhuyenMai.Text = "0";
            txtThanhTien.Text = "";
            txtTongTien.Text = tongTien.ToString();
        }
        private void LoadDataGridView()
        {


            string sql;
            sql = "select MaSP, TenSP, hv.TenHoaVan, cl.TenChatLieu, DonGiaBan, Soluong, kt.TenKThuoc, m.TenMau, HinhAnh " +
                "from tSanPham as sp ,tChatLieu as cl ,tHoaVan as hv,tMau as m,tKichThuoc as kt " +
                "where sp.MaChatLieu = cl.MaChatLieu " +
                "and sp.MaHoaVan = hv.MaHoaVan " +
                "and sp.MaKThuoc = kt.MaKThuoc " +
                "and sp.MaMau = m.MaMau";
            dtTable = dtBase.DataReader(sql);
            dgvCTSP.DataSource = dtTable;
            dgvCTSP.Columns[0].HeaderText = "Mã SP";
            dgvCTSP.Columns[1].HeaderText = "Tên SP";
            dgvCTSP.Columns[2].HeaderText = "Hoa văn";
            dgvCTSP.Columns[3].HeaderText = "Chất Liệu";
            dgvCTSP.Columns[4].HeaderText = "Đơn Giá Bán";
            dgvCTSP.Columns[5].HeaderText = "Số Lượng Tồn";
            dgvCTSP.Columns[6].HeaderText = "Kích Thước";
            dgvCTSP.Columns[7].HeaderText = "Màu";
            dgvCTSP.Columns[8].HeaderText = "Hình ảnh";
            dgvCTSP.AllowUserToAddRows = false;
            dgvCTSP.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvCTSP.Columns[4].DefaultCellStyle.Format = "#";//ép kiểu định dạng money từ sql(bỏ đi số 0 sau dấu ,)
        }
        private void LoadDataGridView(string MaChatLieu)
        {
            string sql;
            sql = "select MaSP, TenSP, hv.TenHoaVan, cl.TenChatLieu, DonGiaBan, Soluong, kt.TenKThuoc, m.TenMau, HinhAnh " +
                "from tSanPham as sp ,tChatLieu as cl ,tHoaVan as hv,tMau as m,tKichThuoc as kt " +
                "where sp.MaChatLieu = cl.MaChatLieu " +
                "and sp.MaHoaVan = hv.MaHoaVan " +
                "and sp.MaKThuoc = kt.MaKThuoc " +
                "and sp.MaMau = m.MaMau " +
                "and cl.MaChatLieu = '" + MaChatLieu + "'";
            dtTable = dtBase.DataReader(sql);
            dgvCTSP.DataSource = dtTable;
            dgvCTSP.Columns[0].HeaderText = "Mã SP";
            dgvCTSP.Columns[1].HeaderText = "Tên SP";
            dgvCTSP.Columns[2].HeaderText = "Hoa văn";
            dgvCTSP.Columns[3].HeaderText = "Chất Liệu";
            dgvCTSP.Columns[4].HeaderText = "Đơn Giá Bán";
            dgvCTSP.Columns[5].HeaderText = "Số Lượng Tồn";
            dgvCTSP.Columns[6].HeaderText = "Kích Thước";
            dgvCTSP.Columns[7].HeaderText = "Màu";
            dgvCTSP.Columns[8].HeaderText = "Hình ảnh";
            dgvCTSP.AllowUserToAddRows = false;
            dgvCTSP.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvCTSP.Columns[4].DefaultCellStyle.Format = "#";//ép kiểu định dạng money từ sql(bỏ đi số 0 sau dấu ,)
        }
        private void LoadListView()
        {
            lstcthdb.View = View.Details;
            lstcthdb.GridLines = true;
            lstcthdb.FullRowSelect = true;
            lstcthdb.Columns.Add("Mã sản phẩm", 110);//item.text
            lstcthdb.Columns.Add("Tên sản phẩm", 180);//subitem[1]
            lstcthdb.Columns.Add("Chất Liệu", 80);//subitem[2]
            lstcthdb.Columns.Add("HoaVan", 80);//subitem[3]
            lstcthdb.Columns.Add("kích thước", 80);//subitem[4]
            lstcthdb.Columns.Add("Màu", 80);//subitem[5]
            lstcthdb.Columns.Add("SL", 20);//subitem[6]
            lstcthdb.Columns.Add("Đơn giá", 70);//subitem[7]
            lstcthdb.Columns.Add("Giảm (%)", 75);//subitem[8]
            lstcthdb.Columns.Add("Thành tiền", 90);//subitem[9]
        }
        private void LoadComboBox()
        {
            String sql_Chatlieu = "Select * from tChatLieu";
            dtBase.FillCombo(sql_Chatlieu, cbLoc, "MaChatLieu", "TenChatLieu");
            this.cbLoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLoc.SelectedIndex = -1;
        }
        private void LoadDongHo()
        {
            timerDongHo.Start();
        }
        private void LoadThongTinMuaHang()
        {
            txtMHDB.Text = dtBase.SinhMaTuDong("tHoaDonBan", "HDB0", "MaHDB");
            txtMKH.Text = dtBase.SinhMaTuDong("tKhachHang", "KH0", "MaKH");
            txtNV.Text = nhanvien;
        }

        private void timerDongHo_Tick(object sender, EventArgs e)
        {
            lbTimer.Text = DateTime.Now.ToLongTimeString();
            lbDate.Text = DateTime.Now.ToShortDateString();
        }
        private void TinhTongTien()
        {
            tongTien = 0;
            if (lstcthdb.Items.Count != 0)
                foreach (ListViewItem i in lstcthdb.Items)
                {
                    double thanhTien = double.Parse(i.SubItems[9].Text.ToString());
                    tongTien += thanhTien;
                }
            txtTongTien.Text = tongTien.ToString();
        }

        private void dgvCTSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            double thanhTien;
            int soLuong;
            int giamGia = 0;
            float donGia;
            txtMSP.Text = dgvCTSP.CurrentRow.Cells[0].Value.ToString();
            txtTSP.Text = dgvCTSP.CurrentRow.Cells[1].Value.ToString();
            txtHoaVan.Text = dgvCTSP.CurrentRow.Cells[2].FormattedValue.ToString();
            txtChatLieu.Text = dgvCTSP.CurrentRow.Cells[3].FormattedValue.ToString();
            txtKichThuoc.Text = dgvCTSP.CurrentRow.Cells[6].FormattedValue.ToString();
            txtMauSac.Text = dgvCTSP.CurrentRow.Cells[7].FormattedValue.ToString();
            try
            {
                imageName = dgvCTSP.CurrentRow.Cells[8].Value.ToString();
                pbAnh.Image = Image.FromFile(Application.StartupPath.ToString() + "\\Image\\sanpham\\" + imageName);
            }
            catch
            {

            }
            float.TryParse(dgvCTSP.CurrentRow.Cells[4].Value.ToString(), out donGia);
            txtGiaBan.Text = donGia.ToString();
            txtKhuyenMai.Text = giamGia.ToString();
            soLuong = int.Parse(nudSL.Value.ToString());
            thanhTien = soLuong * donGia * (100 - giamGia) / 100;
            txtThanhTien.Text = thanhTien.ToString();
        }

        private void nudSL_ValueChanged(object sender, EventArgs e)
        {
            double thanhTien;
            int soLuong;
            int khuyenmai;
            float donGia;
            donGia = float.Parse(txtGiaBan.Text);
            soLuong = int.Parse(nudSL.Value.ToString());
            int.TryParse(txtKhuyenMai.Text, out khuyenmai);
            txtKhuyenMai.Text = khuyenmai.ToString();
            thanhTien = soLuong * donGia * (100 - khuyenmai) / 100;
            txtThanhTien.Text = thanhTien.ToString();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn huỷ đơn hàng không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetValue_ALL();
                LoadDataGridView();//Load combobox trước và chọn index trước khi load datagridview
                LoadListView();
                LoadThongTinMuaHang();
                nudSL.Value = 1;
            }
        }

        private void cbLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbLoc.SelectedIndex != -1)
            {
                string maChatlieu = this.cbLoc.SelectedValue.ToString();
                LoadDataGridView(maChatlieu);
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (lstcthdb.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Bạn muốn xóa sản phẩm?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    lstcthdb.Items.Remove(lstcthdb.SelectedItems[0]);
                    TinhTongTien();
                }
            }
            else MessageBox.Show("Bạn cần chọn sản phẩm để xoá!", "Lưu ý");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMSP.Text.Length == 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo");
                return;
            }
            if(txtTKH.Text ==""||txtSDT.Text== "")
            {
                MessageBox.Show("Vui lòng điền đủ thông tin khách hàng.", "Thông báo");
                return;
            }
            string sql = "select SoLuong from tSanPham where MaSP = '" + txtMSP.Text + "'";
            string a = dtBase.GetFieldValues(sql);
            if (nudSL.Value >int.Parse(a))
            {
                MessageBox.Show("số lượng vượt qua sô lượng tồn", "Thông báo");
                return;
            }

            ListViewItem item = new ListViewItem();
            item.Text = txtMSP.Text;
            item.SubItems.Add(txtTSP.Text);
            item.SubItems.Add(txtHoaVan.Text);
            item.SubItems.Add(txtChatLieu.Text);
            item.SubItems.Add(txtKichThuoc.Text);
            item.SubItems.Add(txtMauSac.Text);
            item.SubItems.Add(nudSL.Value.ToString());
            item.SubItems.Add(txtGiaBan.Text);
            item.SubItems.Add(txtKhuyenMai.Text);
            item.SubItems.Add(txtThanhTien.Text);

            //kiểm tra sản phẩm đã có trong danh sách mua hàng 
            foreach (ListViewItem i in lstcthdb.Items)
            {
                if (item.Text == i.Text)
                {
                    MessageBox.Show("Sản phẩm này bạn đã chọn.", "Thông báo");
                    ResetValue_SanPham();
                    return;
                }
            }
            lstcthdb.Items.Add(item);
            //Tính Tổng tiền
            TinhTongTien();
            ResetValue_SanPham();
        }
        private bool Insert_KhachHang()
        {
            if (txtMKH.TextLength == 0)
            {
                MessageBox.Show("Lỗi mã khách hàng", "Thông báo");
                return false;
            }
            try
            {
                string sql = "INSERT INTO tKhachHang VALUES (N'" + txtMKH.Text.Trim() +
               "',N'" + txtTKH.Text.Trim() + "',null,'" + txtSDT.Text + "')";
                dtBase.DataChange(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi thêm khách hàng" + e.Message, "Thông báo");
            }
            return true;
        }
        private bool Insert_HoaDonBan()
        {
            if (txtMHDB.Text.Length == 0 || txtNV.Text.Length == 0)
            {
                MessageBox.Show("Kiểm tra lại mã hoá đơn và nhân viên lập", "Thông báo");
                return false;
            }
            try
            {
               
                string sql = "INSERT[dbo].[tHoaDonBan]([MaHDB], [MaNV], [MaKH], [NgayBan], [TongTien]) " +
                   "VALUES(N'" + txtMHDB.Text + "', N'" + txtNV.Text + "', N'" + txtMKH.Text + "'" +
                   ",CAST(N'" + DateTime.Now.ToString("yyyy-MM-dd") + "' AS Date), " + float.Parse(txtTongTien.Text) + ")";
                 dtBase.DataChange(sql);
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi thêm hoá đơn bán " + e.Message, "Thông báo");
            }
            return true;
        }
        private bool Insert_ChiTietHDB()
        {
            if (lstcthdb.Items.Count == 0)
            {
                MessageBox.Show("Chưa thêm sản phẩm nào trong giỏ hàng", "Thông báo");
                return false;
            }
            try
            {
                foreach (ListViewItem i in lstcthdb.Items)
                {
                    string sql = "INSERT [dbo].[tChiTietHDB] ([MaHDB], [MaSP], [SoLuong], [khuyenmai], [ThanhTien]) " +
                        "VALUES (N'"+txtMHDB.Text+ "', N'" + i.Text + "'," + int.Parse(i.SubItems[6].Text) + "" +
                        ","+ int.Parse(i.SubItems[8].Text) + "," + float.Parse(i.SubItems[9].Text) + ")";
                    dtBase.DataChange(sql);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi thêm sản phẩm vào hoá đơn" + e.Message, "Thông báo");
            }
            return true;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (Insert_KhachHang())
                if (Insert_HoaDonBan())
                    if(Insert_ChiTietHDB())
                    {
                        //this.btnHuyHoaDon_Click();
                         txtMHDB.Text = dtBase.SinhMaTuDong("tHoaDonBan", "HDB0", "MaHDB");
                         txtMKH.Text = dtBase.SinhMaTuDong("tKhachHang", "KH0", "MaKH");

                        ResetValue_ALL();
                        LoadThongTinMuaHang();
                        lstcthdb.Items.Clear();
                        TinhTongTien();
                        LoadListView();
                       
                    }
            MessageBox.Show("Thanh toán thành công", "Thông báo");
           
            
        }
        private void inhoadon()
        {
            /*
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MaHDB, a.NgayBan, a.TongTien, b.TenKH, b.DiaChi, b.DienThoai, c.TenNV " +
                "FROM tHoaDonBan AS a, tKhachHang AS b, tNhanVien AS c WHERE a.MaHDB = N'" + txtMHDB.Text + "' AND a.MaKH = b.MaKH AND a.MaNV = c.MaNV";
            tblThongtinHD = dtBase.DataReader(sql);
            exRange.Range["B6:C6"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TenHang, a.SoLuong, b.DonGiaBan, a.GiamGia, a.ThanhTien " +
                  "FROM tChiTietHDB AS a , tSanPham AS b WHERE a.MaHDB = N'" +
                  txtMHDB.Text + "' AND a.MaSP = b.MaSP";
            tblThongtinHang = dtBase.DataReader(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + Functions.ChuyenSoSangChu(tblThongtinHD.Rows[0][2].ToString());
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;*/
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
    }
    
}
