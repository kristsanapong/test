namespace test
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
			this.button1 = new System.Windows.Forms.Button();
			this.txtNum = new System.Windows.Forms.TextBox();
			this.txtBox = new System.Windows.Forms.TextBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 38);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(115, 33);
			this.button1.TabIndex = 0;
			this.button1.Text = "Write";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtNum
			// 
			this.txtNum.Location = new System.Drawing.Point(12, 12);
			this.txtNum.Name = "txtNum";
			this.txtNum.Size = new System.Drawing.Size(115, 20);
			this.txtNum.TabIndex = 1;
			this.txtNum.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// txtBox
			// 
			this.txtBox.Location = new System.Drawing.Point(133, 11);
			this.txtBox.Multiline = true;
			this.txtBox.Name = "txtBox";
			this.txtBox.Size = new System.Drawing.Size(301, 250);
			this.txtBox.TabIndex = 2;
			this.txtBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(458, 50);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(115, 33);
			this.button2.TabIndex = 3;
			this.button2.Text = "Files";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(458, 11);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(115, 33);
			this.button3.TabIndex = 4;
			this.button3.Text = "Folder";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// treeView1
			// 
			this.treeView1.Location = new System.Drawing.Point(133, 277);
			this.treeView1.Name = "treeView1";
			this.treeView1.Size = new System.Drawing.Size(301, 249);
			this.treeView1.TabIndex = 5;
			this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(579, 11);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(301, 238);
			this.listBox1.TabIndex = 6;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 534);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.txtBox);
			this.Controls.Add(this.txtNum);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Test";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtNum;
		private System.Windows.Forms.TextBox txtBox;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ListBox listBox1;
	}
}

