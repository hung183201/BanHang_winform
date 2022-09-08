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
    public partial class frmHeThong : Form
    {
        private Button currentButton;
        private Form activeForm;
        public frmHeThong()
        {
            InitializeComponent();
        }
        private void activeButton(object btn)
        {
            if (btn != null)
            {
                if (currentButton != (Button)btn)
                {
                    DisableButton();
                    currentButton = (Button)btn;
                    currentButton.BackColor = Color.FromArgb(224, 224, 224);
                    currentButton.ForeColor = Color.Black;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    btnClose.Visible = true;

                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(209, 189, 167);
                    previousBtn.ForeColor = Color.Black;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }
        private void OpenChildForm(Form childForm, object btn)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeButton(btn);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(childForm);
            //this.panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
            lblTitle.ForeColor = Color.Black;
        }

        private void frmHeThong_Load(object sender, EventArgs e)
        {

        }
        private void Reset()
        {
            DisableButton();
            lblTitle.Text = "";
            currentButton = null;
            btnClose.Visible = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhaCungCap(), sender);
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLKH(), sender);
        }

        private void btnNV_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQLNV(), sender);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSanPham(), sender);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            Reset();
        }
    }
}
