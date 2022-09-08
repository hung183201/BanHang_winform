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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void hungToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form.frmQLKH a = new form.frmQLKH();
            a.MdiParent = this;
            a.Show();

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void hungToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            form.frmQLKH a = new form.frmQLKH();
            a.MdiParent = this;
            a.Show();
        }

        private void asdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form.frmSanPham a = new form.frmSanPham();
            a.MdiParent = this;
            a.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
