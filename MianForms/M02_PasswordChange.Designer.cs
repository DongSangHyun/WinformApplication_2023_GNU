namespace MianForms
{
    partial class M02_PasswordChange
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtNowPW = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtChangePW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPWChange = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "사용자 ID";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(111, 15);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(223, 25);
            this.txtUserId.TabIndex = 1;
            // 
            // txtNowPW
            // 
            this.txtNowPW.Location = new System.Drawing.Point(111, 46);
            this.txtNowPW.Name = "txtNowPW";
            this.txtNowPW.Size = new System.Drawing.Size(223, 25);
            this.txtNowPW.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "현재 비밀번호";
            // 
            // txtChangePW
            // 
            this.txtChangePW.Location = new System.Drawing.Point(111, 77);
            this.txtChangePW.Name = "txtChangePW";
            this.txtChangePW.Size = new System.Drawing.Size(223, 25);
            this.txtChangePW.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "변경 비밀번호";
            // 
            // btnPWChange
            // 
            this.btnPWChange.Location = new System.Drawing.Point(210, 108);
            this.btnPWChange.Name = "btnPWChange";
            this.btnPWChange.Size = new System.Drawing.Size(124, 55);
            this.btnPWChange.TabIndex = 6;
            this.btnPWChange.Text = "비밀번호 변경";
            this.btnPWChange.UseVisualStyleBackColor = true;
            this.btnPWChange.Click += new System.EventHandler(this.btnPWChange_Click);
            // 
            // M02_PasswordChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 178);
            this.Controls.Add(this.btnPWChange);
            this.Controls.Add(this.txtChangePW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNowPW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserId);
            this.Controls.Add(this.label1);
            this.Name = "M02_PasswordChange";
            this.Text = "비밀번호 변경";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.TextBox txtNowPW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtChangePW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPWChange;
    }
}