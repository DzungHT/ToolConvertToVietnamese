using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.SelectedPath = @"D:\vhr_mytel\src\java\com\viettel";
            var result = folderBrowserDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                textBox1.Text = folderPath;

                DirectoryInfo di = new DirectoryInfo(folderPath);

                var files = di.GetFiles("*.java");

                ((ListBox)checkedListBox1).DataSource = files;
                ((ListBox)checkedListBox1).DisplayMember = "Name";
                ((ListBox)checkedListBox1).ValueMember = "FullName";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }
    }
}
