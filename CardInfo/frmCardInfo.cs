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

namespace CardInfo
{
    public partial class frmCardInfo : Form
    {
        #region Variables
        private List<FileItem> sourceFiles;
        private CardChecker cardChecker;
        #endregion

        #region Constructor
        public frmCardInfo()
        {
            InitializeComponent();

            sourceFiles = new List<FileItem>();
            cardChecker = new CardChecker();

            pictureBoxChara.Visible = false;
            pictureBoxScene.Visible = false;
        }
        #endregion

        #region ListView Events
        private void listView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && files.Length > 0)
            {
                var fileList = files.Where(f => f.ToLowerInvariant().EndsWith(".png")).ToArray();
                LoadFiles(fileList);
            }
        }

        private void listView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBoxChara.Visible = false;
            pictureBoxScene.Visible = false;

            if (listView.SelectedItems.Count > 0)
            {
                var item = listView.SelectedItems[0];
                if (item.Tag != null)
                {
                    int index = (int)item.Tag;
                    if (index > -1)
                    {
                        var card = sourceFiles[index];
                        if (card.Image != null)
                        {
                            if (card.Image.Width > card.Image.Height)
                            {
                                pictureBoxScene.Image = card.Image;
                                pictureBoxChara.Visible = false;
                                pictureBoxScene.Visible = true;
                            }
                            else
                            {
                                pictureBoxChara.Image = card.Image;
                                pictureBoxChara.Visible = true;
                                pictureBoxScene.Visible = false;
                            }
                        }
                        else
                        {
                            pictureBoxScene.Image = null;
                            pictureBoxChara.Image = null;
                        }
                    }
                }
            }
        }
        #endregion

        private void panelCardImage_Resize(object sender, EventArgs e)
        {
            pictureBoxChara.Left = (panelCardImage.Width - pictureBoxChara.Width) / 2;
        }

        #region Menu Events
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Illusion Card File|*.png";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string[] fileList = openFileDialog.FileNames;
                LoadFiles(fileList);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBoxScene.Image = null;
            pictureBoxChara.Image = null;
            pictureBoxChara.Visible = false;
            pictureBoxScene.Visible = false;

            listView.Items.Clear();
            sourceFiles.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region BackgroundWorker
        private void backgroundWorkerLoad_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];

            if (files != null)
            {
                foreach (var f in files)
                {
                    var file = new FileInfo(f);
                    if (cardChecker.TryParse(file))
                    {
                        var cardType = cardChecker.CardType;

                        sourceFiles.Add(new FileItem(file)
                        {
                            CardType = cardType
                        });
                    }
                }

                e.Result = files;
            }
        }

        private void backgroundWorkerLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listView.Items.Clear();

            foreach (var file in sourceFiles)
            {
                int idx = listView.Items.Count + 1;
                var item = listView.Items.Add(new ListViewItem(idx.ToString()));

                item.Tag = idx - 1;
                item.SubItems.Add(file.Name);
                item.SubItems.Add(file.CardType.ToString());
                item.SubItems.Add(file.Size);
            }

            statusProgressBar.Style = ProgressBarStyle.Blocks;
            statusLabelStatus.Text = "Ready";

            EnableControls(true);
        }
        #endregion

        #region Methods
        private void LoadFiles(string[] fileList)
        {
            if (fileList.Length > 0)
            {
                EnableControls(false);
                statusLabelStatus.Text = "Loading ...";
                statusProgressBar.Style = ProgressBarStyle.Marquee;
                backgroundWorkerLoad.RunWorkerAsync(fileList);
            }
        }

        private void EnableControls(bool enabled)
        {
            listView.Enabled = enabled;
            propertyInfo.Enabled = enabled;
        }
        #endregion

        
    }
}
