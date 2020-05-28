using System;
using System.Windows.Forms;
using System.IO;

namespace Shuffle_Player
{
    public partial class Form1 : Form
    {
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        OpenFileDialog ofd = new OpenFileDialog();
        private static Random rng = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    loadList(files);
                }
            }
            catch
            {
                
            }
            
        }

        private void Shuffle(ListBox.ObjectCollection list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Object value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        private void loadList(string[] items)
        {
            try
            {
                foreach (string item in items)
                    displayList.Items.Add(item);
            }
            catch {  }
        }

        private void shuffleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Shuffle(displayList.Items);
            }
            catch
            { }
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in displayList.SelectedItems)
                    System.Diagnostics.Process.Start(item.ToString());
            }
            catch
            {
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            int count = displayList.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                displayList.Items.Remove(displayList.SelectedItems[0]);
            }
        }

        private void loadPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Text|*.txt|All|*.*";
            ofd.Multiselect = false;
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string[] lines = System.IO.File.ReadAllLines(ofd.FileName);
                    loadList(lines);
                }
            }
            catch
            {
            }
        }

        private void savePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String writeStr = "";
                for (int i = 0; i < displayList.Items.Count; i++) {
                    writeStr += displayList.Items[i] + "\r\n";
                }
                writeStr = writeStr.TrimEnd('\n');
                writeStr = writeStr.TrimEnd('\r');
                File.WriteAllText(saveFileDialog1.FileName, writeStr);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            displayList.Items.Clear();
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.Filter = "";
            ofd.Multiselect = true;
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    loadList(ofd.FileNames);
                }
            }
            catch
            {
            }
        }
    }
}
