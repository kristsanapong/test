using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace test
{
	public partial class Mycontrol1 : UserControl
	{
		int x, y;
		public Mycontrol1()
		{
			InitializeComponent();
			/*ControlMover.Init(this);
			ControlMover.Init(this, ControlMover.Direction.Vertical);
		*/
		}
		private void Mycontrol1_Load(object sender, EventArgs e)
		{
			KeyDown += Selectall;
			//MouseDown += Selected;
		}

		public void Selectall(object sender, KeyEventArgs e)
		{
			int c = Controls.Count;
			if (e.Control & e.KeyCode == Keys.A)
			{
				foreach (Control Mypanel in Controls)
				{
					Panel newPanel = new Panel();
					newPanel.BackColor = Color.Yellow;
					newPanel.Size = new Size(10, 10);
					newPanel.Location = Mypanel.Location;
					//newPanel.BackColor = Color.Blue;
					Controls.Add(newPanel);
					//Mypanel.BackColor = Color.Yellow;
					newPanel.MouseDown += SelectAll_MouseDown;
					newPanel.MouseUp += SelectAll_MouseUp;
					newPanel.MouseMove += SelectAll_MouseMove;
					//Mypanel.MouseDown += MoveAll;
					//Init(newPanel);
				}
				for (int i = 0; i < c; i++)
				{
					Controls.RemoveAt(0);
				}
				/*if (e.KeyCode == Keys.Delete)
				{
					for (int i = 0; i < c; i++)
					{
						Controls.RemoveAt(0);
					}

				}
				*/
			}
		}
		private void SelectAll_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				x = e.X;
				y = e.Y;
			}
			//throw new NotImplementedException();
		}

		private void SelectAll_MouseUp(object sender, MouseEventArgs e)
		{
			int c = Controls.Count;
			foreach (Control Mypanel in Controls) {
				Panel newPanel = new Panel();
				newPanel.BackColor = Color.Blue;
				newPanel.Size = new Size(10, 10);
				newPanel.Location = Mypanel.Location;
				//newPanel.BackColor = Color.Blue;
				Controls.Add(newPanel);

				newPanel.MouseDown += Button_MouseDown;
				newPanel.MouseUp += Button_MouseUp;
				newPanel.MouseMove += Button_MouseMove;
			}
			for (int i = 0; i < c;i++) {
				Controls.RemoveAt(0);
			}

		}

		public void SelectAll_MouseMove(object sender,MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				foreach (Control c in Controls)
				{
					c.Location = new Point(e.X + c.Left - x, e.Y + c.Top - y);
				}
			}
		}

		public void Selected(object sender, MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Control)
			{
				//Console.WriteLine("Success");
				Control C = (Control)sender;
				Panel Mypanel = new Panel();
				Mypanel.Size = new Size(10, 10);
				Mypanel.Location = C.Location;
				Mypanel.BackColor = Color.Yellow;
				Mypanel.MouseUp += Select_MouseUp;
				Mypanel.MouseDown += Select_MouseDown;
				Mypanel.MouseMove += Select_MouseMove;
				Controls.Remove(C);
				Controls.Add(Mypanel);
			}
		}

		private void Select_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				foreach (Control Mypanel in Controls)
				{
					if (Mypanel.BackColor == Color.Yellow)
					{
						Mypanel.Location = new Point(e.X + Mypanel.Left - x, e.Y + Mypanel.Top - y);
					}
				}
				
			}
		}
		private void Select_MouseDown(object sender, MouseEventArgs e)
		{
			x = e.X;
			y = e.Y;
			if (ModifierKeys == Keys.Control)
			{
				Control con = (Control)sender;
				con.BackColor = Color.Yellow;
			}
			
		}

		private void Select_MouseUp(object sender, MouseEventArgs e)
		{
			int c = Controls.Count;
			foreach (Control Mypanel in Controls)
			{
				Panel newPanel = new Panel();
				newPanel.BackColor = Color.Blue;
				newPanel.Size = new Size(10, 10);
				newPanel.Location = Mypanel.Location;
				//newPanel.BackColor = Color.Blue;
				Controls.Add(newPanel);
				//Controls.RemoveAt(0);
				newPanel.MouseUp += Selected;

				newPanel.MouseDown += Button_MouseDown;
				newPanel.MouseUp += Button_MouseUp;
				newPanel.MouseMove += Button_MouseMove;
			}
			for (int i = 0; i < 5; i++)
			{
				Controls.RemoveAt(0);
			}
		}

		public enum Direction
		{
			Any,
			Horizontal,
			Vertical
		}

		public static void Init(Control control)
		{
			Init(control, Direction.Any);
		}

		public static void Init(Control control, Direction direction)
		{
			Init(control, control, direction);
		}

		public static void Init(Control control, Control container, Direction direction)
		{	
			bool Dragging = false;
			Point DragStart = Point.Empty;
			control.MouseDown += delegate (object sender, MouseEventArgs e)
			{
				Dragging = true;
				DragStart = new Point(e.X, e.Y);
				control.Capture = true;
			};
			control.MouseUp += delegate (object sender, MouseEventArgs e)
			{
				Dragging = false;
				control.Capture = false;
			};
			control.MouseMove += delegate (object sender, MouseEventArgs e)
			{
				if (Dragging)
				{
					if (direction != Direction.Vertical)
						container.Left = Math.Max(0, e.X + container.Left - DragStart.X);
					if (direction != Direction.Horizontal)
						container.Top = Math.Max(0, e.Y + container.Top - DragStart.Y);
				}
			};
		}
		

		//*********************************Code from Form2********************************
	
	public void Mybutt1_Click(int num)
		{
			Controls.Clear();
			for (int i = 0; i < num; i++)
			{
				Panel Mypanel1 = new Panel();
				Mypanel1.Size = new Size(10, 10);
				Mypanel1.Location = new Point(i * 10, i * 10);
				Mypanel1.BackColor = Color.Blue;
				Controls.Add(Mypanel1);
				//onclick_panel(Mypanel1);
			}
			foreach (Control Mypanel in Controls)
			{
				Mypanel.MouseUp += Selected;
				Mypanel.MouseDown += Button_MouseDown;
				Mypanel.MouseUp += Button_MouseUp;
				Mypanel.MouseMove += Button_MouseMove;
			}
		}
		//public event KeyPressEventHandler KeyPress;

		
		public void Savefiles() {
			using (FileStream stream = new FileStream("answer.bin", FileMode.Create))
			{
				using (BinaryWriter writer = new BinaryWriter(stream))
				{
					foreach (Control s in this.Controls)
					{
						writer.Write(s.Left);
						writer.Write(s.Top);
					}
					writer.Close();
				}
			}
		}


		public void Loadfiles() {
			Controls.Clear();
			using (FileStream fs = new FileStream("answer.bin", FileMode.Open))
			{
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
						Controls.Add(Mypanel1);
					}

				}
			}
			foreach (Control Mypanel in Controls)
			{
				Mypanel.MouseDown += Button_MouseDown;
				Mypanel.MouseUp += Button_MouseUp;
				Mypanel.MouseMove += Button_MouseMove;
			}
		}

		private void Button_MouseDown(object sender, MouseEventArgs e)
		{
			//if (ModifierKeys == Keys.Control)
			//{
				//foreach (Control Mypanel in Controls) {
				//	Mypanel.MouseDown += Selected;
				//}
			//}
			x = e.X;
			y = e.Y;
		//	MouseDown += Select_Move;
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
	}
}
