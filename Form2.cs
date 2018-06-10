using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace test
{
	public partial class Form2 : Form
	{
		public Form2()
		{
			InitializeComponent();
			mycontrol11.KeyDown += new KeyEventHandler(mycontrol11_KeyDown);
			//button1.ContextMenuStrip = new ContextMenuStrip();
		}
		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
		private void button1_Click(object sender, EventArgs e)
		{
			panel1.Controls.Clear();
			int num = int.Parse(this.txtNum1.Text);
			mycontrol11.Mybutt1_Click(num);
			/*for (int i = 0; i < num; i++)
			{
				Panel Mypanel1 = new Panel();
				Mypanel1.Size = new Size(10, 10);
				Mypanel1.Location = new Point(i * 10, i * 10);
				Mypanel1.BackColor = Color.Blue;
				this.panel1.Controls.Add(Mypanel1);
				//onclick_panel(Mypanel1);
			}
			foreach (Control Mypanel in this.panel1.Controls) {
				Mypanel.MouseDown += Button_MouseDown;
				Mypanel.MouseUp += Button_MouseUp;
				Mypanel.MouseMove += Button_MouseMove;
			}*/
		}
		/*
		private void Button_MouseDown(object sender, MouseEventArgs e)
		{
			x = e.X;
			y = e.Y;
			Control c = (Control)sender;
			c.BackColor = Color.Yellow;
		}
		
		private void Button_MouseUp(object sender, MouseEventArgs e)
		{
			Control c = (Control)sender;
			c.BackColor = Color.Blue;
		}

		private void Button_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				Control c = (Control)sender;
				c.Location = new Point(e.X + c.Left - x, e.Y + c.Top - y);
			}
		}
		*/
		private void onclick_panel(Panel Mypanel1)
		{
			//......
			Mypanel1.MouseDown += delegate
				{
					Mypanel1.BackColor = Color.Yellow;
				};
			//Mypanel1.MouseEnter += delegate { Mypanel1.BackColor = Color.Yellow; };
			//......
			Mypanel1.MouseUp += delegate
				{
					Mypanel1.BackColor = Color.Blue;
				};
		}

		private void button2_Click(object sender, EventArgs e)
		{
			mycontrol11.Savefiles();
			//Panel p = (Panel)sender;
			//this.button2.Click += new EventHandler(this.button2_Click);
			/*using (FileStream stream = new FileStream("answer.bin", FileMode.Create))
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					foreach (Control s in this.panel1.Controls){
						writer.Write(s.Left);
						writer.Write(s.Top);
					}
					writer.Close();
				}
			}*/
		}

		private void mycontrol11_KeyDown(object sender, KeyEventArgs e) {
			

		}

		private void button3_Click(object sender, EventArgs e)
		{
			mycontrol11.Loadfiles();
			/*panel1.Controls.Clear();
			using (FileStream fs = new FileStream("answer.bin", FileMode.Open)) {
				using (BinaryReader r = new BinaryReader(fs))
				{
					while (r.BaseStream.Position < r.BaseStream.Length)
					{
						int obj_left = r.ReadInt32();
						int obj_top = r.ReadInt32();
						Panel Mypanel1 = new Panel();
						Mypanel1.Size = new Size(10, 10);
						Mypanel1.Location = new Point(obj_left, obj_top);
						Mypanel1.BackColor = Color.Blue;
						this.panel1.Controls.Add(Mypanel1);
					}
				}
			}
			foreach (Control Mypanel in this.panel1.Controls)
			{
				Mypanel.MouseDown += Button_MouseDown;
				Mypanel.MouseUp += Button_MouseUp;
				Mypanel.MouseMove += Button_MouseMove;
			}
			*/
		}

		private void mycontrol11_Load(object sender, EventArgs e)
		{
			
		}

		private void button4_Click(object sender, EventArgs e)
		{
			
		}

		private void Form2_MouseClick(object sender, MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Control)
			{
				Console.WriteLine("Ctrl+Click");
			}
		}

		private void Form2_Load(object sender, EventArgs e)
		{

		}


		/*private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
			Control butt = (Control)sender;
			//on website if (e.Button == MouseButtons.Left) PanelMouseDownLocation = e.Location;
		}

		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
			Control c = (Control)sender;
			c.Location = new Point(e.X + c.Left - x, e.Y + c.Top - y);


			//Point p = c.Location;
			//....On website
			if (e.Button == MouseButtons.Left)

			{

				panel1.Left += e.X - PanelMouseDownLocation.X;

				panel1.Top += e.Y - PanelMouseDownLocation.Y;

			}*/

	}
}
