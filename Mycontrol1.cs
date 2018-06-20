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


namespace test
{
	public partial class Mycontrol1 : UserControl
	{
		//int x, y;
		public Mycontrol1()
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
		List<Panel> SelectedPanels = new List<Panel>();
		List<int> SelectedNumbers = new List<int>();
		List<MoveHx> UndoList = new List<MoveHx>();
		List<MoveHx> RedoList = new List<MoveHx>();
		List<int> MoveCountRedo = new List<int>();
		List<int> MoveCountUndo = new List<int>();
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

		private void Mycontrol1_Load(object sender, EventArgs e)
		{
			//Focus();
			//KeyDown += Delete_Button;
			MouseDown += Clear_MouseDown;
			KeyDown += Selectall;
			MouseDown += Control_MouseDown;
			MouseUp += Control_MouseUp;
			MouseMove += Control_MouseMove;
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
					Control target = Mypanel;
					MoveHx moveHx = new MoveHx(target.Left, target.Top, target);
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
						Console.WriteLine("Redo: " + RedoList.Count + "," + "Undo: " + UndoList.Count);
						SelectedPanels.Clear();
					}
				}
			}

			else if (e.Control & e.KeyCode == Keys.Z)
			{
				for (int i = 0; i < MoveCountUndo[MoveCountUndo.Count - 1]; i++)
				{
					if (UndoList.Count > 0)
					{
						MoveHx latestHx = UndoList[UndoList.Count - 1];
						RedoList.Add(latestHx);
						//UndoList[UndoList.Count - 1].target.Location = new Point(UndoList[UndoList.Count - 1].x, UndoList[UndoList.Count - 1].y);
						latestHx.target.Location = new Point(latestHx.x, latestHx.y);
						UndoList.RemoveAt(UndoList.Count - 1);

						Console.WriteLine("Redo: " + RedoList.Count + "," + "Undo: " + UndoList.Count);
					}
				}
				MoveCountRedo.Add(MoveCountUndo[MoveCountUndo.Count - 1]);
				MoveCountUndo.Remove(MoveCountUndo[MoveCountUndo.Count - 1]);
			}
			else if (e.Control & e.KeyCode == Keys.Y)
			{
				for (int i = 0; i < MoveCountRedo[MoveCountRedo.Count - 1]; i++)
				{
					if (RedoList.Count > 0)
					{
						MoveHx latestHx = RedoList[RedoList.Count - 1];
						UndoList.Add(latestHx);
						latestHx.target.Location = new Point(latestHx.x, latestHx.y);
						RedoList.RemoveAt(RedoList.Count - 1);
						//
						Console.WriteLine("Redo: " + RedoList.Count + "," + "Undo: " + UndoList.Count);
					}
					//Console.WriteLine("");
				}
				MoveCountUndo.Add(MoveCountRedo[MoveCountRedo.Count - 1]);
				MoveCountRedo.Remove(MoveCountRedo[MoveCountRedo.Count - 1]);
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
				int number = SelectedPanels.Count;
				MoveCountUndo.Add(number);
				//Console.WriteLine(number);

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

		class TargetUndo
		{
			public int x;
			public int y;
			public int target;
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

			foreach (Panel c in Controls)
			{
				if (c is Control)
				{
					if (selection.IntersectsWith(c.Bounds))
					{
						c.BackColor = Color.Yellow;
						MoveHx moveHx = new MoveHx(c.Left, c.Top, c);
						UndoList.Add(moveHx);
						SelectedPanels.Add(c);
					}
				}
			}
			//MouseDown += Clear_MouseDown;

			// Replace with your input box
			//MessageBox.Show("You selected " + SelectedPanels.Count + " textbox controls.");
		}
		private void Control_MouseMove(object sender, MouseEventArgs e)
		{
			if (!mouseDown)
			{

				return;
			}

			selectionEnd = PointToClient(MousePosition);
			SetSelectionRect();

			Invalidate();
		}

		private void Control_MouseUp(object sender, MouseEventArgs e)
		{
			mouseDown = false;
			SetSelectionRect();
			Invalidate();

			GetSelectedTextBoxes();
		}

		private void Control_MouseDown(object sender, MouseEventArgs e)
		{
			selectionStart = PointToClient(MousePosition);
			mouseDown = true;
		}


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
			int x, y;
			int width, height;

			x = selectionStart.X > selectionEnd.X ? selectionEnd.X : selectionStart.X;
			y = selectionStart.Y > selectionEnd.Y ? selectionEnd.Y : selectionStart.Y;

			width = selectionStart.X > selectionEnd.X ? selectionStart.X - selectionEnd.X : selectionEnd.X - selectionStart.X;
			height = selectionStart.Y > selectionEnd.Y ? selectionStart.Y - selectionEnd.Y : selectionEnd.Y - selectionStart.Y;

			selection = new Rectangle(x, y, width, height);
		}

		//*********************JSON*******************************
		public class SelectUndo {
			public int Select;
		}
		public void SaveJSON()
		{
			JsonSerializer serializer1 = new JsonSerializer();
			JsonSerializer serializer2 = new JsonSerializer();
			JsonSerializer serializer3 = new JsonSerializer();
			//serializer.Converters.Add(new JavaScriptDateTimeConverter());
			serializer1.NullValueHandling = NullValueHandling.Ignore;
			List<TargetJSON> saveobj = new List<TargetJSON>();
			List<TargetUndo> saveundo = new List<TargetUndo>();
			List<SelectUndo> saveselect = new List<SelectUndo>();
			using (StreamWriter sw = new StreamWriter("Save_JSON.txt"))
			{
				using (JsonWriter writer = new JsonTextWriter(sw))
				{
					foreach (Control c in Controls)
					{
						TargetJSON js = new TargetJSON();
						js.x = c.Left;
						js.y = c.Top;
						saveobj.Add(js);
					}
					serializer1.Serialize(writer, saveobj);
				}
			}
			using (StreamWriter se = new StreamWriter("Undo_JSON.txt"))
			{
				using (JsonWriter writer = new JsonTextWriter(se))
				{
					foreach (MoveHx c in UndoList)
					{
						TargetUndo js = new TargetUndo();
						js.x = c.x;
						js.y = c.y;
						js.target = (int)c.target.Tag;
						//for (int i = 0; i < MoveCountUndo.Count - 1; i++)
						//{
						//	js.SelectCount = MoveCountUndo[i - 1];
						//}
						saveundo.Add(js);
					}
					serializer2.Serialize(writer, saveundo);
				}
			}
			using (StreamWriter st = new StreamWriter("SelectUndo_JSON.txt"))
			{
				//int cc = MoveCountUndo.Count;
				using (JsonWriter writer = new JsonTextWriter(st))
				{
					for (int i = 0; i < MoveCountUndo.Count - 1; i++)
					{
						SelectUndo x = new SelectUndo();
						x.Select = MoveCountUndo[i];
						saveselect.Add(x);
						//Console.WriteLine(MoveCountUndo[i]);
					}
					serializer2.Serialize(writer, saveselect);
				}
			}
		}
		public class TargetJSON
		{
			public int x;
			public int y;
		}
		public void LoadJSON()
		{
			Controls.Clear();
			UndoList.Clear();
			RedoList.Clear();
			MoveCountUndo.Clear();
			MoveCountRedo.Clear();
			List<TargetJSON> loadobj = new List<TargetJSON>();
			using (FileStream f_p = new FileStream("Save_JSON.txt", FileMode.Open))
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
						myPanel1.Location = new Point(loadobj[i].x, loadobj[i].y);
						myPanel1.BackColor = Color.Blue;
						myPanel1.Tag = i;
						Controls.Add(myPanel1);
					}
					f_p.Close();
					Read_Pnl(Controls);
				}
			}
			List<TargetUndo> undoobj = new List<TargetUndo>();
			using (FileStream f_p = new FileStream("Undo_JSON.txt", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{

					string json = file.ReadToEnd();
					undoobj = JsonConvert.DeserializeObject<List<TargetUndo>>(json);

					int count = undoobj.Count();
					for (int i = 0; i < count; i++)
					{
						PanelObjectHistory newobj = new PanelObjectHistory();
						Control xx = new Control();
						newobj.x = undoobj[i].x;
						newobj.y = undoobj[i].y;
						xx.Location = new Point(i*10, i*10);
						xx.Size = new Size(10, 10);
						MoveHx moveHx = new MoveHx(newobj.x, newobj.y, xx);
						UndoList.Add(moveHx);
						//MoveCountUndo.Add(MoveCountRedo[MoveCountRedo.Count - 1]);
					}
					f_p.Close();
					//Read_Pnl(Controls);
					Read_Pnl(Controls);
				}


			}
			List<SelectUndo> selectundoobj = new List<SelectUndo>();
			using (FileStream f_p = new FileStream("SelectUndo_JSON.txt", FileMode.Open))
			{
				using (StreamReader file = new StreamReader(f_p))
				{
					string json = file.ReadToEnd();
					selectundoobj = JsonConvert.DeserializeObject<List<SelectUndo>>(json);

					int count = selectundoobj.Count();
					for (int i = 0; i < count; i++)
					{
						int newselect = selectundoobj[i].Select;
						MoveCountUndo.Add(newselect);
						//MoveCountUndo.Add(MoveCountRedo[MoveCountRedo.Count - 1]);
					}
					f_p.Close();
					//Read_Pnl(Controls);
					Read_Pnl(Controls);
				}
			}
				//using (StreamReader file = File.OpenText("Save_JSON.txt")) {
				//	JsonSerializer serializer = new JsonSerializer();
				//	TargetJSON eiei1 = (TargetJSON)serializer.Deserialize(file, typeof(TargetJSON));
				//	TargetUndo eiei2 = (TargetUndo)serializer.Deserialize(file, typeof(TargetUndo));
				//}
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
