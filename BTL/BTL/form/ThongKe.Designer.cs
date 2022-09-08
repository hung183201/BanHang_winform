
namespace BTL.form
{
    partial class ThongKe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBan = new System.Windows.Forms.Button();
            this.btnNhap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBan
            // 
            this.btnBan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBan.Location = new System.Drawing.Point(198, 42);
            this.btnBan.Name = "btnBan";
            this.btnBan.Size = new System.Drawing.Size(389, 81);
            this.btnBan.TabIndex = 0;
            this.btnBan.Text = "Doanh Số Bán Hàng ";
            this.btnBan.UseVisualStyleBackColor = false;
            this.btnBan.Click += new System.EventHandler(this.btnBan_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhap.Location = new System.Drawing.Point(198, 165);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(389, 81);
            this.btnNhap.TabIndex = 1;
            this.btnNhap.Text = "Thống kê Nhập Hàng ";
            this.btnNhap.UseVisualStyleBackColor = false;
            this.btnNhap.Click += new System.EventHandler(this.btnNhap_Click);
            // 
            // ThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(189)))), ((int)(((byte)(167)))));
            this.ClientSize = new System.Drawing.Size(800, 337);
            this.Controls.Add(this.btnNhap);
            this.Controls.Add(this.btnBan);
            this.Name = "ThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống kê";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBan;
        private System.Windows.Forms.Button btnNhap;
    }
}