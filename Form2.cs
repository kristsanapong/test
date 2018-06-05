using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
	public partial class Form2 : Form
	{
		int x;
		int y;
		public Form2()
		{
			InitializeComponent();


		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
		private void button1_Click(object sender, EventArgs e)
		{
			int num = int.Parse(this.txtNum1.Text);
			for (int i = 0; i < num; i++)
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
			}
		}

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
