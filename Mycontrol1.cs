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
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using System.Net;
using SharpConnect;
using SharpConnect.WebServers;
//using SharpConnect.WebServers;

//using SharpConnect;



namespace SharpConnect
{
	public partial class MyControl1 : UserControl
	{

		//int x, y;
		public MyControl1()
		{
			InitializeComponent();
			DoubleBuffered = true;
			/*ControlMover.Init(this);
			ControlMover.Init(this, ControlMover.Direction.Vertical);
		*/
		}
		enum HistoryCommand
		{
			Unknown,
			PanelChange,
			ColorChange,
			Deleted
		}
		List<PanelObjectHistory> _objHxList = new List<PanelObjectHistory>();
		List<Control> SelectedPanels = new List<Control>();
		List<int> SelectedNumbers = new List<int>();
		List<MoveHx> UndoList = new List<MoveHx>();
		List<MoveHx> RedoList = new List<MoveHx>();
		public List<int> MoveCountRedo = new List<int>();
		public List<int> MoveCountUndo = new List<int>();
		public int time_value;
		class PanelObjectHistory
		{
			public Control targetPanel;
			public int x;
			public int y;
			//public int locate;
			public Color color;
			public HistoryCommand command;
			public override string ToString()
			{
				if (targetPanel.Tag != null)
				{
					return command + "." + (int)targetPanel.Tag + ". " + x + "," + y + color;
				}
				else
				{
					return command + "." + x + "," + y + color;
				}
			}
		}
		public int mode;
		public int listcountundo;
		public int listcountredo;
		public int timecheck;
		private void Mycontrol1_Load(object sender, EventArgs e)
		{
			//WEBSERVER

			


			//Focus();
			//KeyDown += Delete_Button;
			KeyDown += Selectall;
			MouseDown += Control_MouseDown;
			MouseUp += Control_MouseUp;
			MouseMove += Control_MouseMove;
			MouseDown += Clear_MouseDown;
			timer1.Tick += (s2, e2) =>
			{
				this.Invoke(new MethodInvoker(() =>
			   {
				   //timer1.Start();
				   if (mode == 1)
				   {
					   if (timecheck == 1)
					   {
						   listcountundo = UndoList.Count;
						   for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
						   {
							   MoveHx latestHx = UndoList[UndoList.Count - 1 - i];
							   if (!RedoList.Contains(latestHx))
							   {
								   RedoList.Add(latestHx);
							   }
						   }
						   Console.WriteLine("Redolist count: " + RedoList.Count);
						   timecheck = 2;
					   }
					   else if (timecheck == 2)
					   {
						   for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
						   {
							   MoveHx latestHx = RedoList[RedoList.Count - 1 - i];
							   //newHx.target.Tag = newPx.Tag;
							   if (MoveCountUndo.Count > 0 && UndoList.Count > 0)
							   {
								   if (latestHx.target.Left != latestHx.x)
								   {
									   if (latestHx.target.Left < latestHx.x)
									   {
										   latestHx.target.Left += 1;
									   }
									   else
									   {
										   latestHx.target.Left -= 1;
									   }
								   }
								   if (latestHx.target.Top != latestHx.y)
								   {
									   if (latestHx.target.Top < latestHx.y)
									   {
										   latestHx.target.Top += 1;
									   }
									   else
									   {
										   latestHx.target.Top -= 1;
									   }
								   }
								   if ((latestHx.target.Left == latestHx.x) && (latestHx.target.Top == latestHx.y))
								   {
									   //Console.WriteLine(MoveCountUndo[MoveCountUndo.Count - 1]);
									   //Console.WriteLine("i is:" + i);
									   timecheck = 3;
								   }
							   }
						   }
					   }
					   else if (timecheck == 3)
					   {
						   for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
						   {
							   UndoList.RemoveAt(UndoList.Count - 1);
						   }
						   //Console.WriteLine("Undolist count is: " + UndoList.Count);
						   MoveCountRedo.Add(MoveCountUndo[MoveCountUndo.Count - 1]);
						   MoveCountUndo.RemoveAt(MoveCountUndo.Count - 1);
						   //Console.WriteLine("Move count redo is: " + MoveCountRedo.Count);
						   timecheck = 0;
					   }
				   }
				   if (mode == 2)
				   {
					   if (timecheck == 1)
					   {
						   listcountredo = RedoList.Count;
						   for (int i = 0; i < MoveCountRedo[MoveCountRedo.Count - 1]; i++)
						   {
							   MoveHx latestHx = RedoList[RedoList.Count - 1 - i];
							   if (!UndoList.Contains(latestHx))
							   {
								   UndoList.Add(latestHx);
							   }
						   }
						   Console.WriteLine("Redolist count: " + RedoList.Count);
						   timecheck = 2;
					   }
					   else if (timecheck == 2)
					   {
						   for (int i = 0; i < MoveCountRedo[MoveCountRedo.Count - 1]; i++)
						   {
							   MoveHx latestHx = RedoList[RedoList.Count - 1 - i];
							   //newHx.target.Tag = newPx.Tag;
							   if (MoveCountRedo.Count > 0 && RedoList.Count > 0)
							   {
								   if (latestHx.target.Left != latestHx.x)
								   {
									   if (latestHx.target.Left < latestHx.x)
									   {
										   latestHx.target.Left += 1;
									   }
									   else
									   {
										   latestHx.target.Left -= 1;
									   }
								   }
								   if (latestHx.target.Top != latestHx.y)
								   {
									   if (latestHx.target.Top < latestHx.y)
									   {
										   latestHx.target.Top += 1;
									   }
									   else
									   {
										   latestHx.target.Top -= 1;
									   }
								   }
								   if ((latestHx.target.Left == latestHx.x) && (latestHx.target.Top == latestHx.y))
								   {
									   //Console.WriteLine(MoveCountRedo[MoveCountRedo.Count - 1]);
									   //Console.WriteLine("i is:" + i);
									   timecheck = 3;
								   }
							   }
						   }
					   }
					   else if (timecheck == 3)
					   {
						   for (int i = 0; i < MoveCountRedo[MoveCountRedo.Count - 1]; i++)
						   {
							   RedoList.RemoveAt(RedoList.Count - 1);
						   }
						   Console.WriteLine("RedoList count is: " + RedoList.Count);
						   MoveCountUndo.Add(MoveCountRedo[MoveCountRedo.Count - 1]);
						   MoveCountRedo.RemoveAt(MoveCountRedo.Count - 1);
						   Console.WriteLine("Move count redo is: " + MoveCountRedo.Count);
						   timecheck = 0;
						   timer1.Stop();
					   }
				   }
				   if (mode == 3)
				   { //Undo by time
					   if (timecheck == 1)
					   {
						   listcountundo = UndoList.Count;
						   for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
						   {
							   MoveHx latestHx = UndoList[UndoList.Count - 1 - i];
							   if (!RedoList.Contains(latestHx))
							   {
								   RedoList.Add(latestHx);
							   }
						   }
						   Console.WriteLine("Redolist count: " + RedoList.Count);
						   timecheck = 2;
					   }
					   else if (timecheck == 2)
					   {
						   for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
						   {
							   MoveHx latestHx = RedoList[RedoList.Count - 1 - i];
							   //newHx.target.Tag = newPx.Tag;
							   if (MoveCountUndo.Count > 0 && UndoList.Count > 0)
							   {
								   if (latestHx.target.Left != latestHx.x)
								   {
									   if (latestHx.target.Left < latestHx.x)
									   {
										   latestHx.target.Left += 1;
									   }
									   else
									   {
										   latestHx.target.Left -= 1;
									   }
								   }
								   if (latestHx.target.Top != latestHx.y)
								   {
									   if (latestHx.target.Top < latestHx.y)
									   {
										   latestHx.target.Top += 1;
									   }
									   else
									   {
										   latestHx.target.Top -= 1;
									   }
								   }
								   if ((latestHx.target.Left == latestHx.x) && (latestHx.target.Top == latestHx.y))
								   {
									   //Console.WriteLine(MoveCountUndo[MoveCountUndo.Count - 1]);
									   //Console.WriteLine("i is:" + i);
									   timecheck = 3;
								   }
							   }

							   //MoveHx latestHx = RedoList[RedoList.Count - 1 - i];
							   ////newHx.target.Tag = newPx.Tag;
							   //if (MoveCountUndo.Count > 0 && UndoList.Count > 0)
							   //{
							   // int x_distance = Math.Abs(latestHx.target.Left + (latestHx.x- latestHx.target.Top));
							   // int y_distance = Math.Abs(latestHx.target.Top + (latestHx.y- latestHx.target.Top));
							   // int x_step = x_distance/(time_value * 10);
							   // int y_step = y_distance/(time_value * 10);
							   // //int distance = (latestHx.x - (latestHx.target.Left - latestHx.x))/ (latestHx.y - (latestHx.target.Top-latestHx.x));
							   // if (latestHx.target.Left != latestHx.x)
							   // {
							   //  if (x_distance < (latestHx.x))
							   //  {
							   //   latestHx.target.Left = x_distance;
							   //  }
							   //  if (latestHx.target.Left < latestHx.x)
							   //  {
							   //   latestHx.target.Left += x_step;
							   //  }
							   //  else
							   //  {
							   //   latestHx.target.Left -= x_step;
							   //  }
							   // }
							   // if (latestHx.target.Top != latestHx.y)
							   // {
							   //  if (y_distance < (latestHx.y))
							   //  {
							   //   latestHx.target.Top = y_distance;
							   //  }
							   //  if (latestHx.target.Top < latestHx.y)
							   //  {
							   //   latestHx.target.Top += y_step;
							   //  }
							   //  else
							   //  {
							   //   latestHx.target.Top -= y_step;
							   //  }
							   // }
							   // if ((latestHx.target.Left == (latestHx.x)) && (latestHx.target.Top == (latestHx.y)))
							   // {
							   //  //Console.WriteLine(MoveCountUndo[MoveCountUndo.Count - 1]);
							   //  //Console.WriteLine("i is:" + i);
							   //  timecheck = 3;
							   // }

						   }
					   }
					   else if (timecheck == 3)
					   {
						   for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
						   {
							   UndoList.RemoveAt(UndoList.Count - 1);
						   }
						   //Console.WriteLine("Undolist count is: " + UndoList.Count);
						   MoveCountRedo.Add(MoveCountUndo[MoveCountUndo.Count - 1]);
						   MoveCountUndo.RemoveAt(MoveCountUndo.Count - 1);
						   //Console.WriteLine("Move count redo is: " + MoveCountRedo.Count);
						   timecheck = 0;
					   }
				   }
			   }
			   ));
			};
		}



		public string RefreshHxbox()
		{
			StringBuilder stbuilder = new StringBuilder();
			int j = _objHxList.Count;
			for (int i = 0; i < j; ++i)
			{
				stbuilder.AppendLine(_objHxList[i].ToString());
			}
			return stbuilder.ToString();
			// = stbuilder.ToString();
		}

		private void Mycontrol1_MouseDown(object sender, MouseEventArgs e)
		{
			//throw new NotImplementedException();
		}
		public string dataJSON;
		public void Delete_Button(object sender, KeyEventArgs e)
		{
			int j = SelectedPanels.Count;
			if (e.KeyCode == Keys.Delete)
			{
				if (j > 0)
				{
					//Panel pickBtn = (Panel)sender;

					foreach (Panel deleter in SelectedPanels)
					{
						PanelObjectHistory hs = new PanelObjectHistory();
						hs.targetPanel = deleter;
						hs.command = HistoryCommand.Deleted;
						_objHxList.Add(hs);
						Controls.Remove(deleter);
					}
					SelectedPanels.Clear();
				}
			}
		}

		//***********************************SelectAll & Multiple Select************************************

		public void Selectall(object sender, KeyEventArgs e)
		{
			int c = Controls.Count;
			if (e.Control & e.KeyCode == Keys.A)
			{
				foreach (Control Mypanel in Controls)
				{
					Mypanel.BackColor = Color.Yellow;
					Control target = Mypanel;
					MoveHx moveHx = new MoveHx();
					moveHx.x = target.Left;
					moveHx.y = target.Top;
					moveHx.target = target;
					UndoList.Add(moveHx);
					if (!SelectedPanels.Contains(Mypanel))
					{
						PanelObjectHistory hs = new PanelObjectHistory();
						hs.targetPanel = (Panel)sender;
						hs.x = Mypanel.Left;
						hs.y = Mypanel.Top;
						hs.command = HistoryCommand.ColorChange;
						hs.color = Color.Yellow;
						_objHxList.Add(hs);
						SelectedPanels.Add((Panel)Mypanel);
					}
				}
				int movecount = SelectedPanels.Count;
				Console.WriteLine("move is : " + movecount);

				MoveCountUndo.Add(movecount);
				//Debug code
				/*for (int i = 0; i < c; i++)
				{
					SelectedPanels[i].BackColor = Color.Yellow;
					PanelObjectHistory hx = new PanelObjectHistory();
					hx.targetPanel = SelectedPanels[i];
					hx.color = SelectedPanels[i].BackColor;
					hx.command = HistoryCommand.ColorChange;
					_objHxList.Add(hx);
				}*/
			}
			else if (e.KeyCode == Keys.Delete)
			{
				int j = SelectedPanels.Count;
				if (e.KeyCode == Keys.Delete)
				{
					if (j > 0)
					{
						foreach (Panel deleter in SelectedPanels)
						{
							MoveHx moveHx = new MoveHx();
							moveHx.x = deleter.Left;
							moveHx.y = deleter.Top;
							moveHx.target = deleter;
							UndoList.Add(moveHx);
							Controls.Remove(deleter);
						}
						Console.WriteLine("Redo: " + RedoList.Count + "," + "Undo: " + UndoList.Count);
						SelectedPanels.Clear();
					}
				}
			}

			else if (e.Control & e.KeyCode == Keys.Z)
			{
				if (MoveCountUndo.Count > 0 && UndoList.Count > 0)
				{
					for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
					{
						MoveHx latestHx = UndoList[UndoList.Count - 1];
						Control newPx = UndoList[UndoList.Count - 1].target;
						MoveHx newHx = new MoveHx();
						newHx.x = latestHx.target.Left;
						newHx.y = latestHx.target.Top;
						newHx.target = newPx;
						newHx.target.Tag = newPx.Tag;
						RedoList.Add(newHx);
						//Console.WriteLine(RedoList.Count);
						latestHx.target.Location = new Point(latestHx.x, latestHx.y);
						UndoList.RemoveAt(UndoList.Count - 1);
					}
					MoveCountRedo.Add(MoveCountUndo[MoveCountUndo.Count - 1]);
					MoveCountUndo.RemoveAt(MoveCountUndo.Count - 1);

					//MoveCountUndo.Remove(MoveCountUndo[MoveCountUndo.Count - 1]);
				}
			}
			else if (e.Control & e.KeyCode == Keys.Y)
			{
				//Console.WriteLine(RedoList[RedoList.Count - 1].x + "," + RedoList[RedoList.Count - 1].y);

				Console.WriteLine("Redo count is:" + MoveCountRedo.Count);
				if (MoveCountRedo.Count > 0 && RedoList.Count > 0)
				{
					for (int i = 0; i < MoveCountRedo[MoveCountRedo.Count - 1]; i++)
					{
						MoveHx latestHx = RedoList[RedoList.Count - 1];
						Control newPx = RedoList[RedoList.Count - 1].target;
						MoveHx newHx = new MoveHx();
						newHx.x = latestHx.target.Left;
						newHx.y = latestHx.target.Top;
						newHx.target = newPx;
						UndoList.Add(newHx);
						latestHx.target.Location = new Point(latestHx.x, latestHx.y);
						RedoList.RemoveAt(RedoList.Count - 1);
						//
						//Console.WriteLine(RedoList[i].target.Tag);
						Console.WriteLine("Redo: " + RedoList.Count + "," + "Undo: " + UndoList.Count);
						//Console.WriteLine("Redoxy is :"+latestHx.x + "," + latestHx.y);
						//Console.WriteLine("");
					}
					MoveCountUndo.Add(MoveCountRedo[MoveCountRedo.Count - 1]);
					MoveCountRedo.RemoveAt(MoveCountRedo.Count - 1);

					//MoveCountRedo.Remove(MoveCountRedo[MoveCountRedo.Count - 1]);
				}
			}
		}
		bool m_ismousedown;
		int m_lastx;
		int m_lasty;

		private void Select_MouseUp(object sender, MouseEventArgs e)
		{
			m_ismousedown = false;
			int c = SelectedPanels.Count;
			if (ModifierKeys == Keys.Control)
			{
				Panel pickbTn = (Panel)sender;
				//KeyDown += Delete_Button;
				PanelObjectHistory hs = new PanelObjectHistory();
				hs.targetPanel = pickbTn;
				hs.command = HistoryCommand.ColorChange;
				hs.x = pickbTn.Left;
				hs.y = pickbTn.Top;
				hs.color = Color.Yellow;
				_objHxList.Add(hs);
				MoveHx moveHx = UndoList[UndoList.Count - 1];
				moveHx.target = pickbTn;
				moveHx.x = UndoList[UndoList.Count - 1].x;
				moveHx.y = UndoList[UndoList.Count - 1].y;
				moveHx.target.Tag = UndoList[UndoList.Count - 1].target.Tag;
				Console.WriteLine(pickbTn.Left + "," + pickbTn.Top);
				UndoList.RemoveAt(UndoList.Count - 1);
				UndoList.Add(moveHx);
				return;
			}
			for (int i = 0; i < c; ++i)
			{
				Panel selecter = (Panel)sender;
				PanelObjectHistory hx = new PanelObjectHistory();
				hx.targetPanel = selecter;
				hx.x = selecter.Left;
				hx.y = selecter.Top;
				hx.color = Color.Blue;
				hx.command = HistoryCommand.PanelChange;
				_objHxList.Add(hx);
				SelectedPanels[0].BackColor = Color.Blue;
				SelectedPanels.RemoveAt(0);
			}
			MoveCountUndo.Add(c);
			//	target.Location = new Point(e.X, e.Y);
		}
		private void Select_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_ismousedown)
			{
				//Console.WriteLine(e.X + "," + e.Y);
				int dx = e.X - m_lastx;
				int dy = e.Y - m_lasty;
				Panel p = (Panel)sender;
				foreach (Control cc in SelectedPanels)
				{
					Panel pp = (Panel)cc;
					pp.Location = new Point(pp.Left + dx, pp.Top + dy);
				}
			}
			/*foreach (Control Mypanel in Controls)
			{
				if (Mypanel.BackColor == Color.Yellow)
				{
					Mypanel.Location = new Point(e.X + Mypanel.Left - x, e.Y + Mypanel.Top - y);
				}
			}*/
		}
		private void Select_MouseDown(object sender, MouseEventArgs e)
		{
			//if (e.Button == MouseButtons.Left)
			//{
			m_ismousedown = true;
			var p = (Panel)sender;
			p.BackColor = Color.Yellow;
			m_lastx = e.X;
			m_lasty = e.Y;
			if (!SelectedPanels.Contains(p))
			{
				SelectedPanels.Add(p);
				Control target = (Control)sender;
				MoveHx moveHx = new MoveHx();
				moveHx.x = target.Left;
				moveHx.y = target.Top;
				Console.WriteLine(target.Left + "tttt" + target.Top);
				UndoList.Add(moveHx);
				//PanelObjectHistory hx = new PanelObjectHistory();
				//hx.targetPanel = p;
				//hx.command = HistoryCommand.ColorChange;
				//_objHxList.Add(hx);
			}
		}
		private void Clear_MouseDown(object sender, MouseEventArgs e)
		{
			int j = SelectedPanels.Count;
			for (int i = 0; i < j; i++)
			{
				SelectedPanels[i].BackColor = Color.Blue;
			}
			SelectedPanels.Clear();
		}
		public void Read_Pnl(object sender)
		{
			foreach (Control myPanel1 in Controls)
			{
				myPanel1.Focus();
				myPanel1.MouseUp += Select_MouseUp;
				myPanel1.MouseDown += Select_MouseDown;
				myPanel1.MouseMove += Select_MouseMove;
				myPanel1.KeyDown += Selectall;
				//myPanel1.MouseDown += Clear_MouseDown;
			}
		}

		public void Read_PnlObject(object sender)
		{
			foreach (Control myPanel1 in Controls)
			{
				myPanel1.Focus();
				myPanel1.KeyDown += Selectall;
				//myPanel1.MouseDown += Clear_MouseDown;
			}
		}
		//*********************************Code from Form2************************************

		public void Mybutt1_Click(int num)
		{
			Controls.Clear();
			UndoList.Clear();
			RedoList.Clear();
			MoveCountUndo.Clear();
			MoveCountRedo.Clear();
			for (int i = 0; i < num; ++i)
			{
				Panel myPanel1 = new Panel();
				myPanel1.Size = new Size(10, 10);
				myPanel1.Location = new Point(i * 10, i * 10);
				myPanel1.BackColor = Color.Blue;
				myPanel1.Tag = i;
				Controls.Add(myPanel1);
				//myPanel1.MouseDown += Button_MouseDown;
				//myPanel1.MouseUp += Button_MouseUp;
				//myPanel1.MouseMove += Button_MouseMove;
			}
			Read_Pnl(this.Controls);
		}


		public void Savefiles()
		{
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


		public void Loadfiles()
		{
			Controls.Clear();
			UndoList.Clear();
			RedoList.Clear();
			MoveCountUndo.Clear();
			MoveCountRedo.Clear();
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
						Mypanel1.Tag = (int)r.BaseStream.Position;
						Controls.Add(Mypanel1);
						Console.Write(r.BaseStream.Position + "\n");
					}

				}
			}
			foreach (Control Mypanel in Controls)
			{
				Mypanel.Focus();
				Mypanel.KeyDown += Selectall;
				Mypanel.MouseDown += Select_MouseDown;
				Mypanel.MouseUp += Select_MouseUp;
				Mypanel.MouseMove += Select_MouseMove;
			}
		}


		//***********************************Undo Redo*****************************************
		public class MoveHx
		{
			public int x;
			public int y;
			public Control target;

			public override string ToString()
			{
				return x + "," + y;
			}
		}

		public class TargetUndo
		{
			public int X;
			public int Y;
			public int T;
		}
		//****************************Regtangle Drag mouse************************************
		/// <summary>
		/// Initializes a new instance of the WindowsFormsApplication5.Form1 class
		/// </summary>

		private Point selectionStart;
		private Point selectionEnd;
		private Rectangle selection;
		private bool mouseDown;

		private void GetSelectedTextBoxes()
		{
			foreach (Control c in Controls)
			{
				if (c is Panel)
				{
					if (selection.IntersectsWith(c.Bounds))
					{
						MoveHx moveHx = new MoveHx();
						moveHx.x = c.Left;
						moveHx.y = c.Top;
						moveHx.target = c;
						UndoList.Add(moveHx);
						SelectedPanels.Add(c);
						c.BackColor = Color.Yellow;
					}
				}
			}

			// Replace with your input box
			//MessageBox.Show("You selected " + SelectedPanels.Count + " textbox controls.");
		}
		//protected override void OnMouseDown(MouseEventArgs e)
		//{
		//	selectionStart = PointToClient(MousePosition);
		//	mouseDown = true;
		//}

		//protected override void OnMouseUp(MouseEventArgs e)
		//{
		//	mouseDown = false;

		//	SetSelectionRect();
		//	Invalidate();

		//	GetSelectedTextBoxes();
		//}

		private void Control_MouseMove(object sender, MouseEventArgs e)
		{
			if (!mouseDown)
			{
				return;
			}

			selectionEnd = PointToClient(MousePosition);
			SetSelectionRect();
			//Select_MouseMove(this, e);
			Invalidate();
		}

		private void Control_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
			SetSelectionRect();
			//Select_MouseUp(this, e);
			Invalidate();
			GetSelectedTextBoxes();
		}

		private void Control_MouseDown(object sender, MouseEventArgs e)
		{
			selectionStart = PointToClient(MousePosition);
			mouseDown = true;
			//Select_MouseDown(this, e);
		}

		//protected override void OnMouseMove(MouseEventArgs e)
		//{
		//	if (!mouseDown)
		//	{
		//		return;
		//	}

		//	selectionEnd = PointToClient(MousePosition);
		//	SetSelectionRect();

		//	Invalidate();
		//}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (mouseDown)
			{
				using (Pen pen = new Pen(Color.Black, 1F))
				{
					pen.DashStyle = DashStyle.Dash;
					e.Graphics.DrawRectangle(pen, selection);
				}
			}
		}

		private void SetSelectionRect()
		{
			int x1, y1;
			int width, height;

			x1 = selectionStart.X > selectionEnd.X ? selectionEnd.X : selectionStart.X;
			y1 = selectionStart.Y > selectionEnd.Y ? selectionEnd.Y : selectionStart.Y;

			width = selectionStart.X > selectionEnd.X ? selectionStart.X - selectionEnd.X : selectionEnd.X - selectionStart.X;
			height = selectionStart.Y > selectionEnd.Y ? selectionStart.Y - selectionEnd.Y : selectionEnd.Y - selectionStart.Y;

			selection = new Rectangle(x1, y1, width, height);
		}

		//*********************JSON*******************************
		public class SelectUndo
		{
			public int Select;
		}
		//public class JSONsave {
		//	JsonSerializer targetUndo;
		//	JsonSerializer targetJson;
		//	JsonSerializer selectUndo;
		//}
		public class JSONsave
		{
			public string SavePanel;
			public string History;
			public string ListCountHistory;
		}
		WebClient wb1 = new WebClient();
		public string History1;
		public string ListCountHistory1;
		public string SavePanel1;
		string listJSON;
		public List<JSONsave> save = new List<JSONsave>();
		public string content;
		public void SaveJSON()
		{
			JsonSerializer serializer1 = new JsonSerializer();
			JsonSerializer serializer2 = new JsonSerializer();
			JsonSerializer serializer3 = new JsonSerializer();
			JsonSerializer serializer4 = new JsonSerializer();

			//List<JSONsave> jSONsaves = new List<JSONsave>();
			//WebClient wb = new WebClient();
			//serializer.Converters.Add(new JavaScriptDateTimeConverter());
			serializer1.NullValueHandling = NullValueHandling.Ignore;
			List<TargetJSON> saveobj = new List<TargetJSON>();
			List<TargetUndo> saveundo = new List<TargetUndo>();
			List<SelectUndo> saveselect = new List<SelectUndo>();
			//string head[];
			using (StreamWriter sw = new StreamWriter("SavePanel.json"))
			{
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					foreach (Control c in Controls)
					{
						TargetJSON js = new TargetJSON();
						js.X = c.Left;
						js.Y = c.Top;
						js.T = (int)c.Tag;
						saveobj.Add(js);
					}
					serializer1.Serialize(writer, saveobj);
				}
			}
			using (StreamWriter se = new StreamWriter("History.json"))
			{
				using (JsonWriter writer = new JsonTextWriter(se))
				{
					foreach (MoveHx c in UndoList)
					{
						TargetUndo js = new TargetUndo();
						js.X = c.x;
						js.Y = c.y;
						js.T = (int)c.target.Tag;
						saveundo.Add(js);
					}
					serializer2.Serialize(writer, saveundo);
				}
			}
			using (StreamWriter st = new StreamWriter("ListCountHistory.json"))
			{
				using (JsonWriter writer = new JsonTextWriter(st))
				{
					serializer2.Serialize(writer, MoveCountUndo);
				}
			}


			JSONsave ss = new JSONsave();


			List<TargetJSON> loadobj = new List<TargetJSON>();
			using (FileStream f_p = new FileStream("SavePanel.json", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{
					SavePanel1 = file.ReadToEnd();
					loadobj = JsonConvert.DeserializeObject<List<TargetJSON>>(SavePanel1);
					f_p.Close();
					ss.SavePanel = SavePanel1;
					//string test = wb1.UploadString("http://localhost:8080/JSONLoad/SavePanel", SavePanel1);
				}
			}

			List<TargetUndo> undoobj = new List<TargetUndo>();
			using (FileStream f_p = new FileStream("History.json", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{

					History1 = file.ReadToEnd();
					undoobj = JsonConvert.DeserializeObject<List<TargetUndo>>(History1);
					f_p.Close();
					ss.History = History1;

				}
			}

			List<int> selectundoobj = new List<int>();
			using (FileStream f_p = new FileStream("ListCountHistory.json", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{
					ListCountHistory1 = file.ReadToEnd();
					selectundoobj = JsonConvert.DeserializeObject<List<int>>(ListCountHistory1);
					f_p.Close();
					ss.ListCountHistory = ListCountHistory1;
				}
			}

			listJSON = SavePanel1 + "|" + ListCountHistory1 + "|" + History1;
			wb1.UploadString("http://localhost:8080/JSONLoad/SavePanel", listJSON);
			//string result = wb1.UploadString("http://localhost:8080/MyModule/Go?a=20&b=20", listJSON);


			//using (StreamWriter st = new StreamWriter("List.json"))
			//{
			//	using (JsonWriter writer = new JsonTextWriter(st))
			//	{
			//		serializer4.Serialize(writer, ss);
			//	}
			//}

			//List<JSONsave> jsonobj = new List<JSONsave>();

			//using (FileStream f_p = new FileStream("List.json", FileMode.Open))
			//{
			//	using (StreamReader file = new StreamReader(f_p))
			//	{
			//		string jsonData = file.ReadToEnd();
			//		//jsonobj = JsonConvert.DeserializeObject<List<JSONsave>>(jsonData);
			//		f_p.Close();
			//	}
			//}
		}
		public class TargetJSON
		{
			public int X;
			public int Y;
			public int T;
		}
		public void LoadJSON()
		{
			Controls.Clear();
			UndoList.Clear();
			RedoList.Clear();
			MoveCountUndo.Clear();
			MoveCountRedo.Clear();
			List<TargetJSON> loadobj = new List<TargetJSON>();
			using (FileStream f_p = new FileStream("SavePanel.json", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{

					string json = file.ReadToEnd();
					loadobj = JsonConvert.DeserializeObject<List<TargetJSON>>(json);

					int count = loadobj.Count();
					for (int i = 0; i < count; i++)
					{
						Panel myPanel1 = new Panel();
						myPanel1.Size = new Size(10, 10);
						myPanel1.Location = new Point(loadobj[i].X, loadobj[i].Y);
						myPanel1.BackColor = Color.Blue;
						myPanel1.Tag = i;
						Controls.Add(myPanel1);
					}
					f_p.Close();
				}
			}
			List<TargetUndo> undoobj = new List<TargetUndo>();
			using (FileStream f_p = new FileStream("History.json", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{
					string json = file.ReadToEnd();
					undoobj = JsonConvert.DeserializeObject<List<TargetUndo>>(json);

					//UndoList = undoobj;
					int count = undoobj.Count();
					for (int i = 0; i < count; i++)
					{
						foreach (Control pickobj in Controls)
						{
							if ((int)pickobj.Tag == undoobj[i].T)
							{
								MoveHx moveHx = new MoveHx();
								moveHx.x = undoobj[i].X;
								moveHx.y = undoobj[i].Y;
								moveHx.target = pickobj;
								UndoList.Add(moveHx);
							}
						}
					}
					f_p.Close();
				}

			}
			List<int> selectundoobj = new List<int>();
			using (FileStream f_p = new FileStream("ListCountHistory.json", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{
					string json = file.ReadToEnd();
					selectundoobj = JsonConvert.DeserializeObject<List<int>>(json);

					int count = selectundoobj.Count();
					Console.WriteLine(count);
					MoveCountUndo = selectundoobj;
					//for (int i = 0; i < count; i++)
					//{
					//	Console.WriteLine("Undolistis=" + SelectedNumbers[i]);
					//	//SelectedNumbers.Add(MoveCountUndo[i]);
					//}
					f_p.Close();
					Read_Pnl(Controls);
				}
			}
			//using (StreamReader file = File.OpenText("Save_JSON.txt")) {
			//	JsonSerializer serializer = new JsonSerializer();
			//	TargetJSON eiei1 = (TargetJSON)serializer.Deserialize(file, typeof(TargetJSON));
			//	TargetUndo eiei2 = (TargetUndo)serializer.Deserialize(file, typeof(TargetUndo));
			//}
		}
		List<MoveHx> TimerUndo = new List<MoveHx>();
		public void RedowithTimer(int value)
		{
			timer1.Interval = value;
			timer1.Start();
		}
		public void UndowithSpeed(int value)
		{
			timer1.Interval = value;
			timer1.Start();
		}
		//Time_Undo();
		public void UndowithTime(int value)
		{
			int v, s;
			int x_distance = Math.Abs(UndoList[UndoList.Count - 1].target.Left - UndoList[UndoList.Count - 1].x);
			int y_distance = Math.Abs(UndoList[UndoList.Count - 1].target.Top - UndoList[UndoList.Count - 1].y);
			double distance = (Math.Sqrt((x_distance * x_distance) + (y_distance * y_distance)));
			s = (int)distance;
			time_value = value * 1000;
			v = (time_value) / s;
			timer1.Interval = v;
			timer1.Start();
		}

		public void WebServerPanel()
		{
			List<MyControl1.TargetJSON> savePanels = new List<MyControl1.TargetJSON>();
			List<MyControl1.TargetUndo> historyPanels = new List<MyControl1.TargetUndo>();
			List<int> listCountHistory = new List<int>();

			Controls.Clear();
			UndoList.Clear();
			RedoList.Clear();
			MoveCountUndo.Clear();
			MoveCountRedo.Clear();
			if (dataJSON != null)
			{
				string[] data = dataJSON.Split('|');
				if (data.Length == 3)
				{
					savePanels = JsonConvert.DeserializeObject<List<MyControl1.TargetJSON>>(data[0]);
					historyPanels = JsonConvert.DeserializeObject<List<MyControl1.TargetUndo>>(data[2]);
					listCountHistory = JsonConvert.DeserializeObject<List<int>>(data[1]);
					int count1 = savePanels.Count();
					for (int i = 0; i < count1; i++)
					{
						Panel myPanel1 = new Panel();
						myPanel1.Size = new Size(10, 10);
						myPanel1.Location = new Point(savePanels[i].X, savePanels[i].Y);
						myPanel1.BackColor = Color.Red;
						myPanel1.Tag = i;
						Controls.Add(myPanel1);
					}
					int count2 = historyPanels.Count();
					for (int i = 0; i < count2; i++)
					{
						foreach (Control pickobj in Controls)
						{
							if ((int)pickobj.Tag == historyPanels[i].T)
							{
								MoveHx moveHx = new MoveHx();
								moveHx.x = historyPanels[i].X;
								moveHx.y = historyPanels[i].Y;
								moveHx.target = pickobj;
								UndoList.Add(moveHx);
							}
						}
					}
					MoveCountUndo = listCountHistory;

				}

			}
		}
	}
}
/*public void Selected(object sender, MouseEventArgs e)
		{
			int c = Controls.Count;
			if (ModifierKeys == Keys.Control)
			{

				foreach (Control myPanel1 in Controls)
				{
					//myPanel1.KeyDown += Selectall;
					myPanel1.MouseUp += Select_MouseUp;
					myPanel1.MouseDown += Select_MouseDown;
					myPanel1.MouseMove += Select_MouseMove;
					//Controls.Remove(C);
				}

				//SelectedPanels.Add(Mypanel);
				//}
				//Mypanel.MouseUp += Delete_Button;
				//hx.command = HistoryCommand.ColorChange;
				//_objHxList.Add(hx);
				//SelectedPanels.Add(Mypanel);
			}
			int j = SelectedPanels.Count;

			for (int i = 0; i < j; i++)
			{
				//SelectedPanels[i].BackColor = Color.Yellow;
				PanelObjectHistory hx = new PanelObjectHistory();
				hx.targetPanel = SelectedPanels[i];
				hx.color = SelectedPanels[i].BackColor;
				hx.command = HistoryCommand.ColorChange;
				_objHxList.Add(hx);
			}

			if (ModifierKeys == Keys.Delete) {
				foreach (Control Mypanel in Controls) {
					if (Mypanel.BackColor == Color.Yellow) {
						Controls.Remove(Mypanel);
						break;
					}
				}
			}
		}*/
