namespace SharpConnect
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
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.TimeCheckBox = new System.Windows.Forms.CheckBox();
			this.SpeedCheckBox = new System.Windows.Forms.CheckBox();
			this.timerBtn = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.invokeValue = new System.Windows.Forms.TextBox();
			this.button9 = new System.Windows.Forms.Button();
			this.button10 = new System.Windows.Forms.Button();
			this.button11 = new System.Windows.Forms.Button();
			this.mycontrol11 = new SharpConnect.MyControl1();
			this.button12 = new System.Windows.Forms.Button();
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
			// TimeCheckBox
			// 
			this.TimeCheckBox.AutoSize = true;
			this.TimeCheckBox.Location = new System.Drawing.Point(700, 42);
			this.TimeCheckBox.Name = "TimeCheckBox";
			this.TimeCheckBox.Size = new System.Drawing.Size(77, 17);
			this.TimeCheckBox.TabIndex = 11;
			this.TimeCheckBox.Text = "Fixed Time";
			this.TimeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.TimeCheckBox.UseVisualStyleBackColor = true;
			this.TimeCheckBox.CheckedChanged += new System.EventHandler(this.TimeCheckBox_ChechedChanged);
			// 
			// SpeedCheckBox
			// 
			this.SpeedCheckBox.AutoSize = true;
			this.SpeedCheckBox.Location = new System.Drawing.Point(700, 59);
			this.SpeedCheckBox.Name = "SpeedCheckBox";
			this.SpeedCheckBox.Size = new System.Drawing.Size(85, 17);
			this.SpeedCheckBox.TabIndex = 12;
			this.SpeedCheckBox.Text = "Fixed Speed";
			this.SpeedCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.SpeedCheckBox.UseVisualStyleBackColor = true;
			this.SpeedCheckBox.CheckedChanged += new System.EventHandler(this.SpeedCheckBox_CheckedChanged);
			// 
			// timerBtn
			// 
			this.timerBtn.Location = new System.Drawing.Point(619, 9);
			this.timerBtn.Name = "timerBtn";
			this.timerBtn.Size = new System.Drawing.Size(75, 23);
			this.timerBtn.TabIndex = 13;
			this.timerBtn.Text = "Start Undo";
			this.timerBtn.UseVisualStyleBackColor = true;
			this.timerBtn.Click += new System.EventHandler(this.timerBtn_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(700, 9);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(75, 23);
			this.button8.TabIndex = 14;
			this.button8.Text = "Start Redo";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// invokeValue
			// 
			this.invokeValue.Location = new System.Drawing.Point(617, 40);
			this.invokeValue.Name = "invokeValue";
			this.invokeValue.Size = new System.Drawing.Size(77, 20);
			this.invokeValue.TabIndex = 15;
			this.invokeValue.TextChanged += new System.EventHandler(this.invokeValue_TextChanged);
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(455, 96);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(156, 23);
			this.button9.TabIndex = 16;
			this.button9.Text = "Load Form Webserver";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(655, 150);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(133, 23);
			this.button10.TabIndex = 17;
			this.button10.Text = "Browse Picture";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(655, 179);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(133, 23);
			this.button11.TabIndex = 18;
			this.button11.Text = "Save Picture";
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new System.EventHandler(this.button11_Click);
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
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(655, 208);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(133, 23);
			this.button12.TabIndex = 19;
			this.button12.Text = "Load Picture";
			this.button12.UseVisualStyleBackColor = true;
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// Form2
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.button12);
			this.Controls.Add(this.button11);
			this.Controls.Add(this.button10);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.invokeValue);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.timerBtn);
			this.Controls.Add(this.SpeedCheckBox);
			this.Controls.Add(this.TimeCheckBox);
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
		private MyControl1 mycontrol11;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.TextBox txtHistory;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.CheckBox TimeCheckBox;
		private System.Windows.Forms.CheckBox SpeedCheckBox;
		private System.Windows.Forms.Button timerBtn;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.TextBox invokeValue;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.Button button12;
	}
}