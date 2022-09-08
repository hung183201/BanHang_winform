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
    public partial class ThongKe : Form
    {
        public ThongKe()
        {
            InitializeComponent();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            BCTongNhaptrongthang bc = new BCTongNhaptrongthang();
            this.Visible = false;
            bc.ShowDialog();
            this.Visible = true;


        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            BCTongBantrongthang bc = new BCTongBantrongthang();
            this.Visible = false;
            bc.ShowDialog();
            this.Visible = true;

        }
    }
}
