using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonUtils;
using WindowsFormsApp.Models;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        List<SearchResult> _lst;
        public Form1()
        {
            InitializeComponent();
            label5.Text = string.Empty;
            txtRegex.Text = @"(""([^""]*[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ][^""]*)((\\""){1}([^""]*)((\\""){1}))([^""]*[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ][^""]*)"")|(""([^""])*((\\""){1})([^""])*[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+([^""])*((\\""){1})([^""])*"")|(""([^""])*[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]([^""])*"")";
        }

        private List<SearchResult> SearchVietnamese(FileInfo fileInfo, ProgressBar progressBar)
        {
            List<SearchResult> result = new List<SearchResult>();

            string regexVietnameseStr = txtRegex.Text;
            Regex regex = new Regex(regexVietnameseStr, RegexOptions.IgnoreCase);
            var lines = File.ReadAllLines(fileInfo.FullName);
            progressBar.Maximum = lines.Length;
            progressBar.Value = 0;
            string moduleName = fileInfo.Name.Split('.')[0].ToLowerFirstCharacter();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                var matches = regex.Matches(line);

                foreach (Match item in matches)
                {
                    //if (result.Any(x => item.Value.Equals(x.Text)))
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    int keyIndex = 0;
                    if (result.Any(x => item.Value.GenerateKey(moduleName).Equals(x.Key)))
                    {
                        keyIndex = i;
                    }
                    result.Add(new SearchResult(i + 1, item, fileInfo, keyIndex));
                    //}

                }
                progressBar.PerformStep();
            }

            return result;
        }

        private List<FileInfo> SearchAllFiles(string folderPath)
        {
            List<FileInfo> result = new List<FileInfo>();
            try
            {
                DirectoryInfo di = new DirectoryInfo(folderPath);
                result.AddRange(di.GetFiles("*.java"));

                var subDir = di.GetDirectories();
                foreach (var item in subDir)
                {
                    result.AddRange(SearchAllFiles(item.FullName));
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }

            return result;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string filePath = listBoxFiles.SelectedValue.ToString();

            _lst = new List<SearchResult>();
            SearchResult.x = 1;
            _lst = SearchVietnamese(new FileInfo(filePath), progressBar);
            gridResult.DataSource = _lst;

            label5.Text = _lst.Count.ToString();
            progressBar.Value = 0;
        }

        private void folderPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
            folderBrowserDialog.SelectedPath = @"D:\vhr_mytel\src\java\com\viettel";
            var result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderPath = folderBrowserDialog.SelectedPath;
                this.folderPath.Text = folderPath;

                listBoxFiles.DataSource = SearchAllFiles(folderPath);
                listBoxFiles.DisplayMember = "Name";
                listBoxFiles.ValueMember = "FullName";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var files = SearchAllFiles(folderPath.Text);
            progressBar1.Maximum = files.Count;
            _lst = new List<SearchResult>();
            SearchResult.x = 1;
            foreach (var item in files)
            {
                _lst.AddRange(SearchVietnamese(new FileInfo(item.FullName), progressBar));
                progressBar1.PerformStep();
            }
            gridResult.DataSource = _lst;

            label5.Text = _lst.Count.ToString();
            progressBar1.Value = 0;
        }

        private void gridResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1 && e.ColumnIndex > -1)
            //{
            //    int id = (int)gridResult.Rows[e.RowIndex].Cells[0].Value;
            //    var seachResult = _lst.First(x => x.Id == id);
            //    Clipboard.Clear();
            //    Clipboard.SetText(seachResult.ReplacementString);
            //    Process.Start("code", seachResult.GetCmdOpenVisualCode());
            //}
        }

        private void gridResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && e.ColumnIndex > -1)
                {
                    int id = (int)gridResult.Rows[e.RowIndex].Cells[0].Value;
                    var seachResult = _lst.First(x => x.Id == id);
                    Clipboard.Clear();
                    Clipboard.SetDataObject(seachResult.ReplacementString, false, 5, 200);
                    Process.Start("code", seachResult.GetCmdOpenVisualCode());
                }
            }
            catch (Exception)
            {

            }
        }

        private void gridResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (e.RowIndex > -1 && e.ColumnIndex > -1)
                    {
                        int id = (int)gridResult.Rows[e.RowIndex].Cells[0].Value;
                        var seachResult = _lst.First(x => x.Id == id);

                        File.AppendAllText(@"F:\Desktop\Untitled-2.txt", seachResult.Resource + Environment.NewLine);
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void listBoxFiles_Click(object sender, EventArgs e)
        {
            string filePath = listBoxFiles.SelectedValue.ToString();

            _lst = new List<SearchResult>();
            SearchResult.x = 1;
            _lst = SearchVietnamese(new FileInfo(filePath), progressBar);
            gridResult.DataSource = _lst;

            label5.Text = _lst.Count.ToString();
            progressBar.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string regex = @"(""([^""])*((\\""){ 1})([^""])*[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]+([^""])*((\\""){1})([^""])*"")|(""([^""])*[àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ]([^""])*"")";
                Clipboard.Clear();
                Clipboard.SetDataObject(regex, false, 5, 200);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
