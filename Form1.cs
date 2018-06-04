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
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			string CurDir = Directory.GetCurrentDirectory();
			CurDir = "..\\..";
			DirectoryInfo directortInfo = new DirectoryInfo(CurDir);
			if (directortInfo.Exists) {
				treeView1.AfterSelect += treeView1_AfterSelect;
				BuildTree(directortInfo, treeView1.Nodes);
			}
		}

		private void BuildTree(DirectoryInfo directoryInfo, TreeNodeCollection addInMe)
		{
			TreeNode curNode = addInMe.Add(directoryInfo.Name);

			foreach (FileInfo file in directoryInfo.GetFiles())
			{
				curNode.Nodes.Add(file.FullName, file.Name);
			}
			foreach (DirectoryInfo subdir in directoryInfo.GetDirectories())
			{
				BuildTree(subdir, curNode.Nodes);
			}
		}

		/*private TreeListNode FindTreeNode(TreeListNode node, Enumerations.ItemType type, Nullable<long> id)
		{
			foreach (TreeListNode child in node.Nodes)
			{
				if ((Enumerations.ItemType)child[2] == type &&
					(id == null || (long)child[0] == id.Value))
				{

					return child;
				}

				if (child.HasChildren)
				{
					TreeListNode found = FindTreeNode(child, type, id);
					if (found != null)
					{
						return found;
					}
				}
			}
			return null;
		}
		*/

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			//Write
			string txt="hello world\r\n";
			string ans = txt;
			string txtNum = this.txtNum.Text;
			this.Text = txtNum;
			int num = int.Parse(this.txtNum.Text);
			for (int i = 1; i < num; i++)
			{
				ans = ans + txt;
				this.txtBox.Text = i.ToString();
			}
			File.WriteAllText("Files.txt", ans);
			//File.WriteAllText("Files.txt",txt);


			//Read
			string contents = File.ReadAllText("Files.txt");
			this.txtBox.Text = contents;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			string CurDir = Directory.GetCurrentDirectory();
			CurDir = "..\\..";

			string[] curFolder = Directory.GetDirectories(CurDir);
			foreach (string folder in curFolder)
			{
				listBox1.Items.Add(folder);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			string CurDir = Directory.GetCurrentDirectory();
			CurDir = "..\\..";
			string[] curFiles = Directory.GetFiles(CurDir);
			foreach (string file in curFiles)
			{
				listBox1.Items.Add(file);
			}
		}
		
		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			
		}

		private void richTextBox1_TextChanged(object sender, EventArgs e)
		{

		}
	}
}
