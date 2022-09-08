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
    public partial class frmLogin : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        private frmMain f;
        public bool ischeck = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                f.Close();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtUserName.Text;
            string password = txtPassWord.Text;
            if (user == "" || password == "")
            {
                MessageBox.Show("Bạn cần điền đẩy đủ thông tin !", "Thông báo", MessageBoxButtons.OK);
            }
            DataTable dtLogin = dtBase.DataReader("select * from tDangNhap where TaiKhoan=N'" + user + "' and MatKhau = N'" + password + "'");
            if (dtLogin.Rows.Count > 0)
            {
              
                DataTable dtTenNV = dtBase.DataReader("select nv.MaNV, nv.TenNV,dn.IDPer from tDangNhap as dn, tNhanVien as nv where dn.MaNV = nv.MaNV and dn.TaiKhoan=N'" + user + "'");
                ischeck = true;
                frmMain.userName = dtTenNV.Rows[0]["TenNV"].ToString();
                frmHDB.nhanvien = dtTenNV.Rows[0]["MaNV"].ToString();
                frmHDN.nhanvien = dtTenNV.Rows[0]["MaNV"].ToString();
                frmMain.id = dtTenNV.Rows[0]["IDPer"].ToString();
                // frmAccount.user = txtUserName.Text;
                //frmAccount.pass = txtPassWord.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc tài khoản không chính xác !", "Thông báo", MessageBoxButtons.OK);

            }
        }

        private void cbPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPassword.Checked == true)
            {
                txtPassWord.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassWord.UseSystemPasswordChar = true;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
