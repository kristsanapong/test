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

using SharpConnect;
using SharpConnect.WebServers;
using System.Net;



namespace SharpConnect
{

	public partial class Form2 : Form
	{
		SharpConnect.AppHost testApp;
		public Form2()
		{
			InitializeComponent();

			mycontrol11.KeyDown += new KeyEventHandler(mycontrol11_KeyDown);
			//button1.ContextMenuStrip = new ContextMenuStrip();
			
		}
		public int value;
		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}
		private void Form2_Load(object sender, EventArgs e)
		{
			//txtHistory.Text = ToString();
			testApp = new SharpConnect.AppHost();
		
			//test websocket 
			JSONLoad jsons = new JSONLoad();
			jsons.DataArrived3 += Module_DataArrived;
			testApp.RegisterModule(new MyModule());
			testApp.RegisterModule(new MyModule2());
			testApp.RegisterModule(new MyModule3());
			testApp.RegisterModule(new MyAdvanceMathModule());
			testApp.RegisterModule(new MMath1());
			testApp.RegisterModule(jsons);

			WebServer webServer = new WebServer(8080, true, testApp.HandleRequest);
			var webSocketServer = new WebSocketServer();
			webSocketServer.SetOnNewConnectionContext(ctx =>
			{
				ctx.SetMessageHandler(testApp.HandleWebSocket);
			});
			webServer.WebSocketServer = webSocketServer;
			webServer.Start();
		}
		private void Module_DataArrived(object sender, UserUnHxListEventArgs e)
		{
			mycontrol11.dataJSON = e.DataJSON;
		}

		private void button1_Click(object sender, EventArgs e)
		{

			panel1.Controls.Clear();
			int num = int.Parse(this.txtNum1.Text);
			mycontrol11.Mybutt1_Click(num);
			
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
		

		private void txtHistory_TextChanged(object sender, EventArgs e)
		{

		}

		private void button5_Click(object sender, EventArgs e)
		{
			//txtHistory.Text = ToString();
			txtHistory.Text = mycontrol11.RefreshHxbox();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			mycontrol11.SaveJSON();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			mycontrol11.LoadJSON();
		}
		
		private void timerBtn_Click(object sender, EventArgs e)
		{
			if (SpeedCheckBox.Checked == true && TimeCheckBox.Checked == false)
			{
				mycontrol11.mode = 1;
				mycontrol11.timecheck = 1;
				//mycontrol11.value = invokeValue.Text;
				value = int.Parse(this.invokeValue.Text);
				mycontrol11.UndowithSpeed(value);
			}
			else if (TimeCheckBox.Checked == true && SpeedCheckBox.Checked == false) {
				mycontrol11.timecheck = 1;
				mycontrol11.mode = 3;
				value = int.Parse(this.invokeValue.Text);
				mycontrol11.UndowithTime(value);
			}
			//mycontrol11.MoveCountRedo.Add(mycontrol11.MoveCountUndo[mycontrol11.MoveCountUndo.Count - 1]);
			//mycontrol11.MoveCountUndo.Remove(mycontrol11.MoveCountUndo[mycontrol11.MoveCountUndo.Count - 1]);
		}

		public void TimeCheckBox_ChechedChanged(object sender, EventArgs e)
		{

		}

		private void SpeedCheckBox_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void button8_Click(object sender, EventArgs e)
		{
			if (SpeedCheckBox.Checked == true)
			{
				mycontrol11.mode = 2;
				mycontrol11.timecheck = 1;
				value = int.Parse(this.invokeValue.Text);
				mycontrol11.RedowithTimer(value);
			}
		}
		private void invokeValue_TextChanged(object sender, EventArgs e)
		{

		}

		private void button9_Click(object sender, EventArgs e)
		{
			mycontrol11.WebServerPanel();
		}
	}
}
