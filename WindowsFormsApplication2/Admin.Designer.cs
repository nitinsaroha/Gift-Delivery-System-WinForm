namespace WindowsFormsApplication2
{
    partial class Admin
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
            this.logout = new System.Windows.Forms.Button();
            this.gift = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.report = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(174, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Admin";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logout
            // 
            this.logout.BackColor = System.Drawing.Color.LightSeaGreen;
            this.logout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logout.Location = new System.Drawing.Point(426, 3);
            this.logout.Name = "logout";
            this.logout.Size = new System.Drawing.Size(65, 27);
            this.logout.TabIndex = 1;
            this.logout.Text = "Log Out";
            this.logout.UseVisualStyleBackColor = false;
            this.logout.Click += new System.EventHandler(this.button1_Click);
            // 
            // gift
            // 
            this.gift.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.gift.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gift.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gift.Location = new System.Drawing.Point(93, 143);
            this.gift.Name = "gift";
            this.gift.Size = new System.Drawing.Size(125, 27);
            this.gift.TabIndex = 2;
            this.gift.Text = "Add Gift";
            this.gift.UseVisualStyleBackColor = false;
            this.gift.Click += new System.EventHandler(this.gift_Click);
            // 
            // update
            // 
            this.update.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update.Location = new System.Drawing.Point(285, 143);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(125, 27);
            this.update.TabIndex = 4;
            this.update.Text = "Update Status";
            this.update.UseVisualStyleBackColor = false;
            this.update.Click += new System.EventHandler(this.button4_Click);
            // 
            // report
            // 
            this.report.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.report.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.report.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.report.Location = new System.Drawing.Point(184, 201);
            this.report.Name = "report";
            this.report.Size = new System.Drawing.Size(125, 27);
            this.report.TabIndex = 5;
            this.report.Text = "Generate Report";
            this.report.UseVisualStyleBackColor = false;
            this.report.Click += new System.EventHandler(this.button5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gift);
            this.panel1.Controls.Add(this.logout);
            this.panel1.Controls.Add(this.update);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.report);
            this.panel1.Location = new System.Drawing.Point(63, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(494, 267);
            this.panel1.TabIndex = 6;
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(616, 386);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Admin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Admin_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Admin_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button logout;
        private System.Windows.Forms.Button gift;
        private System.Windows.Forms.Button update;
        private System.Windows.Forms.Button report;
        private System.Windows.Forms.Panel panel1;
    }
}