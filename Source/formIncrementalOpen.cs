using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace IncrementalOpener
{
    public partial class formIncrementalOpen : Form
    {
        private ListViewColumnSorter _columnSorter = new ListViewColumnSorter();

        /// <summary>
        /// A list of all files in the solution.
        /// </summary>
        private List<FileDetails> _fileDetails;

        /// <summary>
        /// The file the user has selected in the list.
        /// </summary>
        private FileDetails _resultFile = null;
        
        /// <summary>
        /// The file the user selected the last time the 
        /// dialog was shown. Is stored so that we can auto-select
        /// the same file the next time the dialog is shown.
        /// </summary>
        private string _lastSelectedFile = "";
 
        /// <summary>
        /// Flag indicating whether the dialog is loading. During load,
        /// the content of the filtering textbox is changed. We don't want
        /// this to trigger an actual filtering, hence the need for this flag.
        /// </summary>
        private bool _loading = true;

        /// <summary>
        /// Take a long guess.
        /// </summary>
        private const string WINDOW_TITLE = "Open file";

        public formIncrementalOpen()
        {
            InitializeComponent();

            _columnSorter.Order = SortOrder.Ascending;

            this.listFiles.ListViewItemSorter = _columnSorter;
        }

        /// <summary>
        /// Displays the dialog and a list of files. The file list 
        /// will be filtered according to user input.
        /// </summary>
        /// <param name="fileDetails"></param>
        /// <returns></returns>
        public FileDetails Show(List<FileDetails> fileDetails)
        {
            _fileDetails = fileDetails;

            LoadWindowSettings();

            _loading = false;

            this.ShowDialog();

            SaveWindowSettings();

            return _resultFile;
        }

        private List<FileDetails> GetMatchingFiles()
        {
            
            string searchString = textFileName.Text.ToLower();

            List<FileDetails> matchingDetails = new List<FileDetails>();

            foreach (FileDetails fileDetail in _fileDetails)
            {
                if (Match(fileDetail, searchString))
                    matchingDetails.Add(fileDetail);
            }

            return matchingDetails;

        }

        private void FilterFileList()
        {
            // While loading Window settings, we set the last search phrase.
            // At this time, we don't want any filtering.
            if (_loading)
                return;

            this.Cursor = Cursors.WaitCursor;

            // To prevent flickering
            this.listFiles.BeginUpdate();

            // We need to retrieve a list of all files matching the
            // search criteria.
            List<FileDetails> matchingDetails = GetMatchingFiles();

            // Remove the old items to prevent duplicates.
            listFiles.Items.Clear();

            List<ListViewItem> matchingItems = new List<ListViewItem>();

            // Add all files matching the filter. We add them to a list first,
            // so that we can use AddRange later on. This gives better performance
            // than calling Add() over and over again.
            ListViewItem itemToSelect = null;
            foreach (FileDetails matchingFile in matchingDetails)
            {
                ListViewItem item = new ListViewItem(matchingFile.FileName, 0);
                item.SubItems.Add(matchingFile.Path);
                item.SubItems.Add(matchingFile.Project);
                item.Tag = matchingFile;

                matchingItems.Add(item);

                if (itemToSelect == null && string.IsNullOrEmpty(_lastSelectedFile) == false)
                {
                    // Is this the file we selected the last time? If so, select it again.
                    if (System.IO.Path.Combine(matchingFile.Path, matchingFile.FileName) == _lastSelectedFile)
                    {
                        itemToSelect = item;
                    }
                }

            }

            // Disable sorting while we're adding the items. Not sure if this is 
            // actually needed now when AddRange is being used.
            listFiles.ListViewItemSorter = null;
            listFiles.Items.AddRange(matchingItems.ToArray());
            listFiles.ListViewItemSorter = _columnSorter;

            // Select the correct item. Either choose the item the user
            // selected the last time, or just select the first item in the list.
            listFiles.SelectedIndices.Clear();

            if (itemToSelect != null)
            {
                itemToSelect.Selected = true;
                listFiles.EnsureVisible(itemToSelect.Index);
            }
            else if (listFiles.SelectedIndices.Count == 0)
            {
                if (listFiles.Items.Count > 0)
                    listFiles.SelectedIndices.Add(0);
            }

            this.listFiles.EndUpdate();

            this.Text = WINDOW_TITLE + string.Format(" ({0}/{1})", matchingDetails.Count, _fileDetails.Count);
            this.Cursor = Cursors.Default;
            
        }

        /// <summary>
        /// Check if a specific project file matches the search criteria.
        /// </summary>
        /// <returns>true if the project file matches the search criteria</returns>
        private bool Match(FileDetails fileDetails, string searchString)
        {
            if (fileDetails.FileName.ToLower().Contains(searchString))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Triggers filtering of the files.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textFileName_TextChanged(object sender, EventArgs e)
        {
            _lastSelectedFile = "";
            FilterFileList();
        }

        /// <summary>
        /// If the user double clicks on a file, we should open that one.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listFiles.SelectedItems.Count != 1)
                return;

            OpenSelectedFile();
       }

        /// <summary>
        /// Opens the selected file. If there is only one file in the list, that file is opened.
        /// </summary>
        private void OpenSelectedFile()
        {
            if (listFiles.SelectedItems.Count == 0)
                _resultFile = listFiles.Items[0].Tag as FileDetails;
            else
                _resultFile = listFiles.SelectedItems[0].Tag as FileDetails;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void formIncrementalOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                MoveSelection(1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                MoveSelection(-1);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                OpenSelectedFile();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                MoveSelection(-10);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                MoveSelection(10);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }

        /// <summary>
        /// Select previous or next file in the list.
        /// </summary>
        private void MoveSelection(int jump)
        {
            if (listFiles.Items.Count == 0)
                return;

            int moveToIndex = 0;

            if (listFiles.SelectedItems.Count > 0)
            {
                moveToIndex = listFiles.SelectedIndices[0] + jump;
            }

            // Are we moving out of bound?
            if (moveToIndex >= listFiles.Items.Count)
                moveToIndex = listFiles.Items.Count - 1;
            else if (moveToIndex < 0)
                moveToIndex = 0;
            
            listFiles.SelectedIndices.Clear();
            listFiles.SelectedIndices.Add(moveToIndex);
            listFiles.EnsureVisible(moveToIndex);
        }
 
        /// <summary>
        /// When a file is selected, change its background color.
        /// This is made so that we always get coloring of the selected
        /// file, even if the list view isn't focused.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listFiles_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                e.Item.BackColor = Color.LightGray;
            else
                e.Item.BackColor = Color.White;
        }

        /// <summary>
        /// Take a guess.
        /// </summary>
        private void SaveWindowSettings()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey windowKey = currentUserKey.CreateSubKey("Software\\IncrementalFileOpener\\WindowSettings");

            windowKey.SetValue("WindowWidth", this.Width);
            windowKey.SetValue("WindowHeight", this.Height);

            windowKey.SetValue("Column1Width", this.listFiles.Columns[0].Width);
            windowKey.SetValue("Column2Width", this.listFiles.Columns[1].Width);
            windowKey.SetValue("Column3Width", this.listFiles.Columns[2].Width);

            windowKey.SetValue("SearchPhrase", this.textFileName.Text);
            
            if (_resultFile != null)
                windowKey.SetValue("LastSelectedFile", System.IO.Path.Combine(_resultFile.Path, _resultFile.FileName));

        }

        /// <summary>
        /// Take a guess.
        /// </summary>
        private void LoadWindowSettings()
        {
            try
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey windowKey = currentUserKey.CreateSubKey("Software\\IncrementalFileOpener\\WindowSettings");

                object value = windowKey.GetValue("WindowWidth", "850");
                if (value != null)
                    this.Width = Convert.ToInt32(value);

                value = windowKey.GetValue("WindowHeight", "500");
                if (value != null)
                    this.Height = Convert.ToInt32(value);

                value = windowKey.GetValue("Column1Width", "200");
                if (value != null && Convert.ToInt32(value) > 0)
                    this.listFiles.Columns[0].Width = Convert.ToInt32(value);

                value = windowKey.GetValue("Column2Width", "400");
                if (value != null && Convert.ToInt32(value) > 0)
                    this.listFiles.Columns[1].Width = Convert.ToInt32(value);

                value = windowKey.GetValue("Column3Width", "200");
                if (value != null && Convert.ToInt32(value) > 0)
                    this.listFiles.Columns[2].Width = Convert.ToInt32(value);

                value = windowKey.GetValue("SearchPhrase", "");
                if (value != null)
                    this.textFileName.Text = Convert.ToString(value);

                value = windowKey.GetValue("LastSelectedFile", "");
                if (value != null)
                    _lastSelectedFile = Convert.ToString(value);


            }
            catch (Exception)
            {
                // Make sure that loading of settings dosen't cause the entire app to die.
            }
        }

        /// <summary>
        /// Sorts the file list.
        /// </summary>
        private void listFiles_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == _columnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (_columnSorter.Order == SortOrder.Ascending)
                {
                    _columnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    _columnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                _columnSorter.SortColumn = e.Column;
                _columnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            listFiles.Sort();
        }

        private void formIncrementalOpen_Shown(object sender, EventArgs e)
        {
            FilterFileList();

            _columnSorter.SortColumn = listFiles.Columns[0].Index;
            listFiles.Sort();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
                MoveSelection(-1);    
            else
                MoveSelection(1);
            

            base.OnMouseWheel(e);
        }

        private void listFiles_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void contextOpenContainingFolder_Click(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count != 1)
                return;

            FileDetails details = listFiles.SelectedItems[0].Tag as FileDetails;

            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = details.Path;
            proc.Start();
        }
    }
}