using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotepadProgram
{
    public partial class Form1 : Form
    {
        string fileName = "Untitled";
        public Form1()
        {
            InitializeComponent();
        }

        private void SaveSetting()
        {
            Properties.Settings.Default.Location = this.Location;
            Properties.Settings.Default.Height = this.Height;
            Properties.Settings.Default.Width = this.Width;
            Properties.Settings.Default.Font = textpad.Font;
            Properties.Settings.Default.Save();
        }
        private void LoadSetting()
        {
            this.Location = Properties.Settings.Default.Location;
            this.Height = Properties.Settings.Default.Height;
            this.Width = Properties.Settings.Default.Width;
            textpad.Font = Properties.Settings.Default.Font;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSetting();
            this.Text = fileName + " - Notepad";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSetting();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = textpad.Font;
            if(fontDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textpad.Font = fontDialog1.Font;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                System.IO.File.WriteAllText(fileName, textpad.Text);
                this.Text = fileName + " - Notepad";
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fileName == "Untitled")
            {
                saveAsToolStripMenuItem_Click(null,null);
            }
            else
            {
                System.IO.File.WriteAllText(fileName, textpad.Text);
                this.Text = fileName + " - Notepad";
            }
        }

        private void textpad_TextChanged(object sender, EventArgs e)
        {
            if (textpad.Modified)
            {
                this.Text = fileName + "* " + " - Notepad";
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(textpad.Modified)
            {
                if (MessageBox.Show("Do you want to save changes to " + fileName, "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None) == System.Windows.Forms.DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(null, null);
                }
            }
            if(openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                textpad.Text = System.IO.File.ReadAllText(fileName);
                this.Text = fileName + " - Notepad";
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textpad.Modified)
            {
                if (MessageBox.Show("Do you want to save changes to " + fileName, "Notepad", MessageBoxButtons.YesNoCancel, MessageBoxIcon.None) == System.Windows.Forms.DialogResult.Yes)
                {
                    saveAsToolStripMenuItem_Click(null, null);
                }
            }
            fileName = "Untitled";
            textpad.Text = "";
            this.Text = "Notepad - " + fileName;
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pdoc = new PrintDocument();
            pdoc.DocumentName = fileName;
            pageSetupDialog1.Document = pdoc;
            pageSetupDialog1.ShowDialog();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textpad.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textpad.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textpad.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textpad.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textpad.SelectAll();
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textpad.WordWrap = wordWrapToolStripMenuItem.Checked;
            wordWrapToolStripMenuItem.Checked = textpad.WordWrap;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
