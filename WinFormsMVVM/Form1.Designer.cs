namespace WinFormsMVVM
{
    partial class Form1
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
            this.btnBlocking = new System.Windows.Forms.Button();
            this.btnNonBlocking = new System.Windows.Forms.Button();
            this.cmbTest = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBlocking
            // 
            this.btnBlocking.Location = new System.Drawing.Point(12, 12);
            this.btnBlocking.Name = "btnBlocking";
            this.btnBlocking.Size = new System.Drawing.Size(75, 23);
            this.btnBlocking.TabIndex = 0;
            this.btnBlocking.Text = "Blocking UI";
            this.btnBlocking.UseVisualStyleBackColor = true;
            this.btnBlocking.Click += new System.EventHandler(this.BtnBlocking_Click);
            // 
            // btnNonBlocking
            // 
            this.btnNonBlocking.Location = new System.Drawing.Point(105, 12);
            this.btnNonBlocking.Name = "btnNonBlocking";
            this.btnNonBlocking.Size = new System.Drawing.Size(75, 23);
            this.btnNonBlocking.TabIndex = 1;
            this.btnNonBlocking.Text = "Non blocking";
            this.btnNonBlocking.UseVisualStyleBackColor = true;
            this.btnNonBlocking.Click += new System.EventHandler(this.BtnNonBlocking_Click);
            // 
            // cmbTest
            // 
            this.cmbTest.FormattingEnabled = true;
            this.cmbTest.Location = new System.Drawing.Point(21, 19);
            this.cmbTest.Name = "cmbTest";
            this.cmbTest.Size = new System.Drawing.Size(121, 21);
            this.cmbTest.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbTest);
            this.groupBox1.Location = new System.Drawing.Point(243, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Test area";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 141);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNonBlocking);
            this.Controls.Add(this.btnBlocking);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBlocking;
        private System.Windows.Forms.Button btnNonBlocking;
        private System.Windows.Forms.ComboBox cmbTest;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

