using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace BTL.form
{
    public partial class frmChatLieu : Form
    {
        Classes.Functions dtBase = new Classes.Functions();
        public frmChatLieu()
        {
            InitializeComponent();
        }
        void ResetText()
        {
            txtMCL.Text = "";
            txtTCL.Text = "";
            txtMCL.Focus();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string sqlInsert, maCL;
            DataTable dtChatLieu;
            if (txtMCL.Text.Trim()==""||txtTCL.Text.Trim()=="")
            {
                MessageBox.Show("Bạn cần nhập đầy đủ dữ liệu");
                return;
            }
            maCL = txtMCL.Text;
            dtChatLieu = dtBase.DataReader("Select * from tChatLieu where MaChatLieu='"+maCL+"'");
            if (dtChatLieu.Rows.Count > 0)
            {
                MessageBox.Show("Mã " + txtMCL.Text + " đã có. Mời bạn nhập lại");
                txtMCL.Focus();
                return;
            }
            sqlInsert = "insert into tChatLieu values('" + maCL + "',N'" + txtTCL.Text + "')";
            dtBase.DataChange(sqlInsert);
            LoadData();
            ResetText();
        }

        private void frmChatLieu_Load(object sender, EventArgs e)
        {
            LoadData();
            dgvChatLieu.Columns[0].HeaderText = "Mã Chất Liệu";
            dgvChatLieu.Columns[1].HeaderText = "Tên Chất Liệu";
            dgvChatLieu.BackgroundColor = Color.GreenYellow;
            dgvChatLieu.Font = new Font(dgvChatLieu.Font.Name, 12, dgvChatLieu.Font.Style);
            dgvChatLieu.Columns[1].Width = 100;
        }
        void LoadData()
        {
            DataTable dtChatLieu = dtBase.DataReader("Select * from tChatLieu");
            dgvChatLieu.DataSource = dtChatLieu;
            txtMCL.Focus();
        }

        private void dgvChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMCL.Text = dgvChatLieu.CurrentRow.Cells[0].Value.ToString();
                txtTCL.Text = dgvChatLieu.CurrentRow.Cells[1].Value.ToString();
            }
            catch
            {

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(txtMCL.Text=="")
            {
                MessageBox.Show("Bạn chưa chọn chất liệu để xoá");
                return;
            }    
            if (MessageBox.Show("Bạn có muốn xoá chất liệu có mã " + txtMCL.Text + " không?",
                "Lựa chọn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                dtBase.DataChange("delete tChatLieu where MaChatLieu='" + txtMCL.Text + "'");
            LoadData();
            ResetText();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMCL.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn chất liệu để sửa");
                return;
            }
            dtBase.DataChange("update tChatLieu set TenChatLieu=N'" + txtTCL.Text + "'where MaChatLieu='" + txtMCL.Text + "'");
            LoadData();
            MessageBox.Show("Bạn đã sủa thành công");
            ResetText();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không", "Lựa chọn",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

    
    }
}