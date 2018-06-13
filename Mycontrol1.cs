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
		//int x, y;
		public Mycontrol1()
		{
			InitializeComponent();
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
		List<Panel> SelectedPanels = new List<Panel>();
		List<int> SelectedNumbers = new List<int>();
		List<MoveHx> UndoList = new List<MoveHx>();
		List<MoveHx> RedoList = new List<MoveHx>();

		class PanelObjectHistory
		{
			public Panel targetPanel;
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

		private void Mycontrol1_Load(object sender, EventArgs e)
		{
			//Focus();
			//KeyDown += Delete_Button;
			MouseDown += Clear_MouseDown;
			KeyDown += Selectall;
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
							MoveHx moveHx = new MoveHx(deleter.Left, deleter.Top, deleter);
							UndoList.Add(moveHx);
							Controls.Remove(deleter);
						}
						SelectedPanels.Clear();
					}
				}
			}
			else if (e.Control & e.KeyCode == Keys.Y)
			{
				if (RedoList.Count > 0)
				{
					MoveHx latestHx = RedoList[RedoList.Count - 1];
					RedoList.RemoveAt(RedoList.Count - 1);
					latestHx.target.Location = new Point(latestHx.x, latestHx.y);
					//
					UndoList.Add(latestHx);
				}
			}
			else if (e.Control & e.KeyCode == Keys.Z)
			{
				if (UndoList.Count > 0)
				{
					MoveHx latestHx = UndoList[UndoList.Count - 1];
					UndoList.RemoveAt(UndoList.Count - 1);
					latestHx.target.Location =
					new Point(latestHx.x, latestHx.y);
					//
					RedoList.Add(latestHx);
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
				return;
			}
			for (int i = 0; i < c; ++i)
			{
				Panel selecter = (Panel)sender;
				PanelObjectHistory hx = new PanelObjectHistory();
				hx.targetPanel = selecter;
				hx.x = selecter.Top;
				hx.y = selecter.Left;
				hx.color = Color.Blue;
				hx.command = HistoryCommand.PanelChange;
				_objHxList.Add(hx);
				SelectedPanels[0].BackColor = Color.Blue;
				SelectedPanels.RemoveAt(0);
			}
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
				Control target = (Control)sender;
				MoveHx moveHx = new MoveHx(target.Left, target.Top, target);
				UndoList.Add(moveHx);
				SelectedPanels.Add(p);
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


		//*********************************Code from Form2************************************

		public void Mybutt1_Click(int num)
		{
			Controls.Clear();
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
				Mypanel.Focus();
				Mypanel.KeyDown += Selectall;
				Mypanel.MouseDown += Select_MouseDown;
				Mypanel.MouseUp += Select_MouseUp;
				Mypanel.MouseMove += Select_MouseMove;

			}
		}


		//***********************************Undo Redo*****************************************
		class MoveHx
		{
			public readonly int x;
			public readonly int y;
			public readonly Control target;
			public MoveHx(int x, int y, Control target)
			{
				this.x = x;
				this.y = y;
				this.target = target;
			}
			public override string ToString()
			{
				return x + "," + y;
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
