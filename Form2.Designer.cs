namespace test
{
	partial class Form2
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.txtNum1 = new System.Windows.Forms.TextBox();
			this.button4 = new System.Windows.Forms.Button();
			this.txtHistory = new System.Windows.Forms.TextBox();
			this.mycontrol11 = new test.Mycontrol1();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Location = new System.Drawing.Point(190, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(236, 113);
			this.panel1.TabIndex = 0;
			this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 38);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(172, 20);
			this.button1.TabIndex = 1;
			this.button1.Text = "Create Pixel";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(12, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(172, 21);
			this.button2.TabIndex = 2;
			this.button2.Text = "Save in Binary File";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(12, 91);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(172, 34);
			this.button3.TabIndex = 3;
			this.button3.Text = "Read from Binary File";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// txtNum1
			// 
			this.txtNum1.Location = new System.Drawing.Point(12, 12);
			this.txtNum1.Name = "txtNum1";
			this.txtNum1.Size = new System.Drawing.Size(172, 20);
			this.txtNum1.TabIndex = 4;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(536, 9);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 6;
			this.button4.Text = "button4";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// txtHistory
			// 
			this.txtHistory.Location = new System.Drawing.Point(432, 150);
			this.txtHistory.Multiline = true;
			this.txtHistory.Name = "txtHistory";
			this.txtHistory.Size = new System.Drawing.Size(207, 268);
			this.txtHistory.TabIndex = 7;
			this.txtHistory.TextChanged += new System.EventHandler(this.txtHistory_TextChanged);
			// 
			// mycontrol11
			// 
			this.mycontrol11.BackColor = System.Drawing.Color.White;
			this.mycontrol11.Location = new System.Drawing.Point(12, 150);
			this.mycontrol11.Name = "mycontrol11";
			this.mycontrol11.Size = new System.Drawing.Size(414, 268);
			this.mycontrol11.TabIndex = 5;
			this.mycontrol11.Load += new System.EventHandler(this.mycontrol11_Load);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(455, 8);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 8;
			this.button5.Text = "button5";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(455, 38);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(156, 23);
			this.button6.TabIndex = 9;
			this.button6.Text = "Save To JSON";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(455, 67);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(156, 23);
			this.button7.TabIndex = 10;
			this.button7.Text = "Read From JSON";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.txtHistory);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.mycontrol11);
			this.Controls.Add(this.txtNum1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.Name = "Form2";
			this.Text = "Assignment2";
			this.Load += new System.EventHandler(this.Form2_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox txtNum1;
		private Mycontrol1 mycontrol11;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox txtHistory;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
	}
}