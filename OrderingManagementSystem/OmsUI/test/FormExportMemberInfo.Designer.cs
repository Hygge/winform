namespace OmsUI.test
{
    partial class FormExportMemberInfo
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
            this.btnExportMemberInfo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExportMemberInfo
            // 
            this.btnExportMemberInfo.Location = new System.Drawing.Point(320, 31);
            this.btnExportMemberInfo.Name = "btnExportMemberInfo";
            this.btnExportMemberInfo.Size = new System.Drawing.Size(138, 52);
            this.btnExportMemberInfo.TabIndex = 0;
            this.btnExportMemberInfo.Text = "导出会员信息";
            this.btnExportMemberInfo.UseVisualStyleBackColor = true;
            this.btnExportMemberInfo.Click += new System.EventHandler(this.btnExportMemberInfo_Click);
            // 
            // FormExportMemberInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 467);
            this.Controls.Add(this.btnExportMemberInfo);
            this.Name = "FormExportMemberInfo";
            this.Text = "FormExportMemberInfo";
            this.Load += new System.EventHandler(this.FormExportMemberInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExportMemberInfo;
    }
}