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
    public partial class frmMain : Form
    {
        public static string userName = "";
        public static string id = "";
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.ShowDialog();
            lblUser.Text = "Xin chào: " + userName;
            if(id == "0")
            {
                btnHeThong.Visible = false;
                btnTaiKhoan.Visible= false;
                btnThongKe.Visible = false;
            }

            if(lblUser.Text == "Xin chào: ")
            {
                this.Close();
            }    
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            frmHDB HDB = new frmHDB();
            this.Visible = false;
            HDB.ShowDialog();
            this.Visible = true;
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
            frmHeThong hethong = new frmHeThong();
            this.Visible = false;
            hethong.ShowDialog();
            this.Visible = true;

        }



        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            frmHDN HDN = new frmHDN();
            this.Visible = false;
            HDN.ShowDialog();
            this.Visible = true;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
                //frmLogin login = new frmLogin(this);
                //login.Show();
            }
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            frmTaiKhoan frmtk = new frmTaiKhoan();
            this.Visible = false;
            frmtk.ShowDialog();
            this.Visible = true;

        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            ThongKe tk = new ThongKe();
            this.Visible = false;
            tk.ShowDialog();
            this.Visible = true;
        }
    }
}
