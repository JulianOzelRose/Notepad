/*
    Notepad
    by Julian O. Rose
    10-11-2023
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace Notepad
{
    public partial class Notepad : Form
    {
        public string RichTextBoxText
        {
            get { return richTextBox.Text; }
            set { richTextBox.Text = value; }
        }

        public Notepad()
        {
            InitializeComponent();

            // Set toolStrip orientation
            toolStrip.Dock = DockStyle.Top;

            // Set richTextBox properties
            richTextBox.Dock = DockStyle.None;
            richTextBox.Height = this.ClientSize.Height - statusStrip.Height - toolStrip.Height;

            // Set state of Paste buttons
            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
            pasteContextMenuStripButton.Enabled = Clipboard.ContainsText();

            // Set state of Select All buttons
            selectAllToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBox.Text);
            selectAllContextMenuStripButton.Enabled = !string.IsNullOrEmpty(richTextBox.Text);

            // Other text properties
            showUnicodeControlChars = false;
            displayRightToLeft = false;

            // Default filename to Untitled
            fileName = "Untitled";

            // Display default encoding as UTF-8
            charEncodingToolStripStatusLabel.Text = Encoding.UTF8.EncodingName;

            // Set window title
            this.Text = fileName + " - Notepad";
        }

        // File and display vars
        private string filePath;
        private string fileName;
        private string fileContent;
        private string lastSearch;
        private bool showUnicodeControlChars;
        private bool displayRightToLeft;  
        string lineEnding = "Windows (CRLF)";
        // Search vars
        string lastGoToLine;
        string searchText;
        string replaceWithText;
        bool searchUp = false;
        bool searchDown = true;
        bool wrapAround = false;
        bool matchCase = false;

        private void SaveFile()
        {
            if (File.Exists(filePath))
            {
                // Create a StreamWriter with UTF-8 encoding
                using (StreamWriter writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    // Format line endings and write to file
                    string[] formattedLines = FormatLineEndings(richTextBox.Text.Split('\n'));
                    writer.Write(string.Join("", formattedLines));
                }

                // Update richTextBox Modified state
                richTextBox.Modified = false;

                // Update window title
                this.Text = fileName + " - Notepad";
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = saveFileDialog.FileName;

                    // Create a StreamWriter with UTF-8 encoding
                    using (StreamWriter writer = new StreamWriter(selectedFilePath, false, new UTF8Encoding(true)))
                    {
                        // Format line endings and write to file
                        string[] formattedLines = FormatLineEndings(richTextBox.Text.Split('\n'));
                        writer.Write(string.Join("", formattedLines));
                    }
                }
            }
        }

        string[] FormatLineEndings(string[] lines)
        {
            string lineEndingTemplate = GetLineEndingTemplate(lineEnding);
            string[] formattedLines = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == formattedLines.Length - 1)
                {
                    formattedLines[i] = lines[i];
                }
                else
                {
                    formattedLines[i] = ApplyLineEndingTemplate(lines[i], lineEndingTemplate);
                }
            }

            return formattedLines;
        }

        string ApplyLineEndingTemplate(string line, string template)
        {
            return line.TrimEnd('\r', '\n') + template;
        }

        string GetLineEndingTemplate(string lineEnding)
        {
            switch (lineEnding)
            {
                case "Macintosh (CR)":
                    return "\r";
                case "Unix (LF)":
                    return "\n";
                case "Windows (CRLF)":
                    return "\r\n";
                default:
                    return "\r\n"; // Default to Windows (CRLF)
            }
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Read the selected file and load its content into richTextBox
                filePath = openFileDialog.FileName;
                fileContent = File.ReadAllText(filePath);
                fileName = Path.GetFileName(filePath);
                richTextBox.Text = fileContent;

                // Detect encoding and line endings
                DetectEncoding();
                DetectLineEnding();

                // Update window title
                this.Text = fileName + " - Notepad";
            }
        }

        private void SearchWithGoogle()
        {
            // Get the selected text from the RichTextBox
            string selectedText = richTextBox.SelectedText;

            // Check if there is selected text
            if (!string.IsNullOrEmpty(selectedText))
            {
                // Construct the Google search URL with the selected text
                string googleSearchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(selectedText)}";

                // Open the default web browser to the Google search URL
                System.Diagnostics.Process.Start(googleSearchUrl);
            }
        }

        public void ReceiveDataFromGoToForm(string lastLineText)
        {
            lastGoToLine = lastLineText;
        }

        public void ReceiveDataFromFindForm(string lastSearchText, bool lastRadioUp, bool lastRadioDown, bool lastMatchCase, bool lastWrapAround)
        {
            searchText = lastSearchText;
            searchUp = lastRadioUp;
            searchDown = lastRadioDown;
            matchCase = lastMatchCase;
            wrapAround = lastWrapAround;
            lastSearch = lastSearchText;
        }

        public void ReceiveDataFromReplaceForm(string lastSearchText, string lastReplaceText, bool lastMatchCase, bool lastWrapAround)
        {
            searchText = lastSearchText;
            replaceWithText = lastReplaceText;
            matchCase = lastMatchCase;
            wrapAround = lastWrapAround;
            lastSearch = lastSearchText;
        }

        private void ReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceForm replaceForm = new ReplaceForm(searchText, replaceWithText, matchCase, wrapAround);
            replaceForm.SetParentRichTextBox(richTextBox);
            replaceForm.RichTextBoxControl = richTextBox;
            replaceForm.Owner = this;

            replaceForm.SendDataToParent();
            replaceForm.ShowDialog();
        }

        private void Find()
        {
            FindForm findForm = new FindForm(searchText, searchUp, searchDown, matchCase, wrapAround);
            findForm.RichTextBoxControl = richTextBox;
            findForm.Owner = this;

            findForm.SendDataToParent();
            findForm.ShowDialog();

            // Save richTextBox Modified state
            bool rtbPrevState = richTextBox.Modified;

            // Check the FoundTextPosition property after the child form is closed
            if (findForm.FoundTextPosition >= 0)
            {
                // Clear previous selection highlight
                richTextBox.Select(0, richTextBox.Text.Length);
                richTextBox.SelectionBackColor = Color.White;

                // Select the found text in the parent richTextBox
                richTextBox.Select(findForm.FoundTextPosition, findForm.findWhatTextBox.Text.Length);
            }
            else
            {
                // Handle no match found
                MessageBox.Show("No more matches found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Update Find Next button state
            findNextToolStripMenuItem.Enabled = true;

            // Reset richTextBox Modified to previous state
            richTextBox.Modified = rtbPrevState;
        }

        private void FindNext()
        {
            // Save richTextBox Modified state
            bool rtbPrevState = richTextBox.Modified;

            // Determine the starting position based on the current selection
            int startPosition = richTextBox.SelectionStart + richTextBox.SelectionLength;

            // Ensure that the startPosition is within the valid range
            startPosition = Math.Max(startPosition, 0);
            startPosition = Math.Min(startPosition, richTextBox.Text.Length - 1);

            // Define the search options based on matchCase
            RichTextBoxFinds searchOptions = matchCase ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;

            // Perform the search
            int index = richTextBox.Find(lastSearch, startPosition, searchOptions);

            if (index >= 0)
            {
                // Select the found text
                richTextBox.Select(index, lastSearch.Length);
            }
            else if (wrapAround)
            {
                // Handle wrap around if enabled
                index = richTextBox.Find(lastSearch, 0, searchOptions);

                if (index >= 0)
                {
                    // Select the found text
                    richTextBox.Select(index, lastSearch.Length);
                }
                else
                {
                    // No match found, display message
                    MessageBox.Show($"Cannot find \"{lastSearch}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // No match found, display message
                MessageBox.Show($"Cannot find \"{lastSearch}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Reset richTextBox Modified to previous state
            richTextBox.Modified = rtbPrevState;
        }

        private void FindPrevious()
        {
            // Save richTextBox Modified state
            bool rtbPrevState = richTextBox.Modified;

            // Determine the starting position based on the current selection
            int startPosition = richTextBox.SelectionStart - 1;

            // Ensure that startPosition is within valid range
            startPosition = Math.Max(startPosition, 0);

            // Define the search options based on matchCase
            RichTextBoxFinds searchOptions = matchCase ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;

            // Perform the reverse search
            int index = -1;

            if (startPosition >= 0)
            {
                index = richTextBox.Find(lastSearch, 0, startPosition, searchOptions | RichTextBoxFinds.Reverse);
            }

            if (index >= 0)
            {
                // Select the found text
                richTextBox.Select(index, lastSearch.Length);
            }
            else if (wrapAround)
            {
                // Handle wrap around if enabled
                index = richTextBox.Find(lastSearch, richTextBox.Text.Length - 1, searchOptions | RichTextBoxFinds.Reverse);

                if (index >= 0)
                {
                    // Select the found text
                    richTextBox.Select(index, lastSearch.Length);
                }
                else
                {
                    // No match found, display message
                    MessageBox.Show($"Cannot find more instances of \"{lastSearch}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // No match found, display message
                MessageBox.Show($"Cannot find more instances of \"{lastSearch}\"", "Notepad", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Reset richTextBox Modified to previous state
            richTextBox.Modified = rtbPrevState;
        }

        private void Paste()
        {
            if (richTextBox.CanPaste(DataFormats.GetFormat(DataFormats.Text)))
            {
                // Get selection info
                int selectionStart = richTextBox.SelectionStart;
                int selectionLength = richTextBox.SelectionLength;

                // Get clipboard text
                string clipboardText = (string)Clipboard.GetDataObject().GetData(DataFormats.Text);

                // Replace the selected text with the pasted text
                richTextBox.Text = richTextBox.Text.Remove(selectionStart, selectionLength).Insert(selectionStart, clipboardText);

                // Set the SelectionStart to the end of the pasted text
                richTextBox.SelectionStart = selectionStart + clipboardText.Length;

                // Set SelectionLength to 0 to clear any selected text
                richTextBox.SelectionLength = 0;

                // Scroll to the caret position if necessary
                richTextBox.ScrollToCaret();

                // Manually set richTextBox Modified state
                richTextBox.Modified = true;
            }
        }

        public void UpdateUIAfterHighlight()
        {
            // Change the background color of the selected text
            richTextBox.SelectionBackColor = Color.FromKnownColor(KnownColor.Highlight);

            // Reset the background color after a delay
            Timer resetHighlightTimer = new Timer();
            resetHighlightTimer.Interval = 1000;

            resetHighlightTimer.Tick += (sender, e) =>
            {
                // Reset background color, stop timer
                richTextBox.SelectionBackColor = Color.White;
                resetHighlightTimer.Stop();
            };

            resetHighlightTimer.Start();
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (statusStrip.Visible)
            {
                // Hide statusStrip
                statusStrip.Visible = false;
                statusBarToolStripMenuItem.Checked = false;

                // Set richTextBox dock style to automatic
                richTextBox.Dock = DockStyle.Fill;
            }
            else
            {
                // Show statusStrip
                statusStrip.Visible = true;
                statusBarToolStripMenuItem.Checked = true;

                // Set richTextBox dock style for manual adjustment
                richTextBox.Dock = DockStyle.None;

                // Manually adjust the size of richTextBox based on the window size, statusStrip height, and toolStrip height
                richTextBox.Height = this.ClientSize.Height - statusStrip.Height - toolStrip.Height;
                richTextBox.Width = this.ClientSize.Width;
            }
        }

        private void FontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Save richTextBox Modified state
            bool rtbPrevState = richTextBox.Modified;

            // Create an instance of the FontDialog
            FontDialog fontDialog = new FontDialog();

            // Set initial font properties
            fontDialog.Font = richTextBox.Font;
            fontDialog.Color = richTextBox.ForeColor;

            // Show the font dialog and capture the result
            DialogResult result = fontDialog.ShowDialog();

            // Check if the user clicked "OK" in the dialog
            if (result == DialogResult.OK)
            {
                // Apply the selected font to richTextBox
                richTextBox.Font = fontDialog.Font;
                richTextBox.ForeColor = fontDialog.Color;
            }

            // Reset richTextBox Modified to previous state
            richTextBox.Modified = rtbPrevState;
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Prompt save dialog before new file if text changed
            if (richTextBox.Modified)
            {
                DialogResult result = MessageBox.Show($"Do you want to save changes to {fileName}?", "Notepad",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button3);

                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }

            // Clear richTextBox contents
            richTextBox.Clear();
            filePath = null;
            fileName = "Untitled";

            // Update window title
            this.Text = fileName + " - Notepad";

            // Display default line ending
            lineEndingToolStripStatusLabel.Text = "Windows (CRLF)";

            // Display default encoding as UTF-8
            charEncodingToolStripStatusLabel.Text = Encoding.UTF8.EncodingName;
        }

        private void NewWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Notepad notepad = new Notepad();
            notepad.Show();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Prompt save dialog before opening file if text changed
            if (richTextBox.Modified)
            {
                DialogResult result = MessageBox.Show($"Do you want to save changes to {fileName}?", "Notepad",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button3);

                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
            }

            OpenFile();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.WordWrap)
            {
                richTextBox.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                goToToolStripMenuItem.Enabled = true;
            }
            else
            {
                richTextBox.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                goToToolStripMenuItem.Enabled = false;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                SaveFile();
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFilePath = saveFileDialog.FileName;

                    // Create a StreamWriter with UTF-8 encoding
                    using (StreamWriter writer = new StreamWriter(selectedFilePath, false, new UTF8Encoding(true)))
                    {
                        // Format line endings and write to file
                        string[] formattedLines = FormatLineEndings(richTextBox.Text.Split('\n'));
                        writer.Write(string.Join("", formattedLines));
                    }

                    // Update file data
                    filePath = selectedFilePath;
                    fileName = Path.GetFileName(filePath);

                    // Update richTextBox Modified state
                    richTextBox.Modified = false;

                    // Update window title
                    this.Text = fileName + " - Notepad";

                    // Display default line ending
                    lineEndingToolStripStatusLabel.Text = "Windows (CRLF)";
                }
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = saveFileDialog.FileName;

                // Create a StreamWriter with UTF-8 encoding
                using (StreamWriter writer = new StreamWriter(selectedFilePath, false, new UTF8Encoding(true)))
                {
                    // Format line endings and write to file
                    string[] formattedLines = FormatLineEndings(richTextBox.Text.Split('\n'));
                    writer.Write(string.Join("", formattedLines));
                }

                // Update file data
                filePath = selectedFilePath;
                fileName = Path.GetFileName(filePath);

                // Update window title
                this.Text = fileName + " - Notepad";

                // Display default line ending
                lineEndingToolStripStatusLabel.Text = "Windows (CRLF)";
            }
        }

        private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintDocument printDocument = new PrintDocument())
            using (PrintDialog printDialog = new PrintDialog())
            {
                printDialog.Document = printDocument;

                DialogResult result = printDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Define the PrintPage event handler for the printDocument
                    printDocument.PrintPage += (printDialogSender, printArgs) =>
                    {
                        // Define the area to print (the entire content of the richTextBox)
                        RectangleF areaToPrint = new RectangleF(printArgs.MarginBounds.Left, printArgs.MarginBounds.Top,
                            printArgs.MarginBounds.Width, printArgs.MarginBounds.Height);

                        // Create a StringFormat object to control formatting during printing
                        StringFormat format = new StringFormat(StringFormatFlags.LineLimit);

                        // Print the content of the richTextBox
                        printArgs.Graphics.DrawString(richTextBox.Text, richTextBox.Font, Brushes.Black, areaToPrint, format);
                    };

                    // Start the printing process
                    printDocument.Print();
                }
            }
        }

        private void DetectLineEnding()
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Initialize line ending detection flags
                    bool isWindows = false;
                    bool isUnix = false;
                    bool isMac = false;
                    int currentByte;
                    int previousByte = -1;

                    while ((currentByte = fileStream.ReadByte()) != -1)
                    {
                        if (currentByte == '\n')
                        {
                            if (previousByte == '\r')
                            {
                                isWindows = true;
                            }
                            else
                            {
                                isUnix = true;
                            }
                        }
                        else if (currentByte == '\r')
                        {
                            isMac = true;
                        }

                        previousByte = currentByte;
                    }

                    if (isWindows)
                    {
                        lineEnding = "Windows (CRLF)";
                    }
                    else if (isUnix)
                    {
                        lineEnding = "Unix (LF)";
                    }
                    else if (isMac)
                    {
                        lineEnding = "Macintosh (CR)";
                    }
                    else
                    {
                        lineEnding = "Windows (CRLF)";
                    }
                }
            }
            catch (Exception)
            {
                // Default to Windows line endings
                lineEnding = "Windows (CRLF)";
            }

            // Update toolStrip line ending label
            lineEndingToolStripStatusLabel.Text = lineEnding;
        }

        public void DetectEncoding()
        {
            try
            {
                using (var reader = new StreamReader(filePath, Encoding.ASCII, true))
                {
                    // Peek ahead to detect the encoding
                    reader.Peek();
                    var encoding = reader.CurrentEncoding;
                    charEncodingToolStripStatusLabel.Text = encoding.EncodingName;
                }
            }
            catch (Exception)
            {
                // Handle errors
                charEncodingToolStripStatusLabel.Text = "Unknown";
            }
        }

        private void PageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            using (PageSetupDialog pageSetupDialog = new PageSetupDialog())
            {
                PageSettings pageSettings = new PageSettings();
                pageSetupDialog.PageSettings = pageSettings;

                DialogResult result = pageSetupDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Apply page settings
                }
            }
            */
        }

        private void Notepad_SizeChanged(object sender, EventArgs e)
        {
            // Dynamically adjust the size of richTextBox based on the window size
            richTextBox.Height = this.ClientSize.Height - statusStrip.Height - toolStrip.Height;
            richTextBox.Width = this.ClientSize.Width;
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectedText.Length > 0)
            {
                richTextBox.SelectedText = "";
            }
        }

        private void SearchWithGoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchWithGoogle();
        }

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        private void TimeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Get the current date and time
            DateTime currentDateTime = DateTime.Now;
            string formattedDateTime = currentDateTime.ToString("h:mm tt M/d/yyyy");
            richTextBox.SelectedText = formattedDateTime;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void RichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            int line = richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart) + 1;
            int column = richTextBox.SelectionStart - richTextBox.GetFirstCharIndexFromLine(line - 1) + 1;

            // Update line and column information in toolStrip
            lineColumnToolStripStatusLabel.Text = "  Ln " + line + ", Col " + column;

            if (!string.IsNullOrEmpty(richTextBox.SelectedText))
            {
                // Update toolStrip items
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                searchWithGoogleToolStripMenuItem.Enabled = true;

                // Update contextMenu items
                cutContextMenuStripButton.Enabled = true;
                copyContextMenuStripButton.Enabled = true;
                deleteContextMenuStripButton.Enabled = true;
                searchWithGoogleContextMenuStripButton.Enabled = true;

            }
            else
            {
                // Update toolStrip items
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                searchWithGoogleToolStripMenuItem.Enabled = false;

                // Update contextMenu items
                cutContextMenuStripButton.Enabled = false;
                copyContextMenuStripButton.Enabled = false;
                deleteContextMenuStripButton.Enabled = false;
                searchWithGoogleContextMenuStripButton.Enabled = false;
            }

            // Update state of Paste buttons
            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
            pasteContextMenuStripButton.Enabled = Clipboard.ContainsText();
        }

        private void ZoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.ZoomFactor += 0.1f;
            float zoomFactor = richTextBox.ZoomFactor * 100;

            // Round to the nearest integer, display new zoom
            int roundedZoomFactor = (int)Math.Round(zoomFactor);
            zoomToolStripStatusLabel.Text = roundedZoomFactor + "%";
        }

        private void ZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.ZoomFactor -= 0.1f;
            float zoomFactor = richTextBox.ZoomFactor * 100;

            // Round to the nearest integer, display new zoom
            int roundedZoomFactor = (int)Math.Round(zoomFactor);
            zoomToolStripStatusLabel.Text = roundedZoomFactor + "%";
        }

        private void RestoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.ZoomFactor = 1f;

            // Display new zoom
            float zoomFactor = richTextBox.ZoomFactor * 100;
            zoomToolStripStatusLabel.Text = zoomFactor + "%";
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            DetectLineEnding();

            // Update toolStrip buttons
            findToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBox.Text);
            findPreviousToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBox.Text);

            // Update state of Select All buttons
            selectAllToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBox.Text);
            selectAllContextMenuStripButton.Enabled = !string.IsNullOrEmpty(richTextBox.Text);

            // Update state of Undo buttons
            undoToolStripMenuItem.Enabled = richTextBox.CanUndo;
            undoContextMenuStripButton.Enabled = richTextBox.CanUndo;

            // Update state of Paste buttons
            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
            pasteContextMenuStripButton.Enabled = Clipboard.ContainsText();

            // Update window title
            if (richTextBox.Modified)
            {
                this.Text = "*" + fileName + " - Notepad";
            }
            else
            {
                this.Text = fileName + " - Notepad";
            }
        }

        private void FindToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Find();
        }

        private void GoToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoToForm goToForm = new GoToForm(lastGoToLine);
            goToForm.TotalLines = richTextBox.Lines.Length;
            goToForm.Owner = this;
            goToForm.ShowDialog();

            // Check if a valid line number was entered in the child form
            if (goToForm.LineNumber > 0)
            {
                int lineIndex = goToForm.LineNumber - 1;
                int charIndex = richTextBox.GetFirstCharIndexFromLine(lineIndex);
                if (charIndex >= 0)
                {
                    richTextBox.SelectionStart = charIndex;
                    richTextBox.ScrollToCaret();
                }
            }
        }

        private void Notepad_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox.Modified)
            {
                // Save dialog
                DialogResult result = MessageBox.Show($"Do you want to save changes to {fileName}?", "Notepad",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button3);

                if (result == DialogResult.Yes)
                {
                    SaveFile();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void FindNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastSearch))
            {
                Find();
            }
            else
            {
                FindNext();
            }
        }

        private void FindPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastSearch))
            {
                Find();
            }
            else
            {
                FindPrevious();
            }
        }

        private void UndoContextMenuStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void CutContextMenuStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Cut();
        }

        private void CopyContextMenuStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.Copy();
        }

        private void PasteContextMenuStripButton_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void DeleteContextMenuStripButton_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectedText.Length > 0)
            {
                richTextBox.SelectedText = "";
            }
        }

        private void SelectAllContextMenuStripButton_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        private void SearchWithGoogleContextMenuStripButton_Click(object sender, EventArgs e)
        {
            SearchWithGoogle();
        }

        private readonly Dictionary<char, string> controlCharacterMappings = new Dictionary<char, string>
        {
            { '\u200E', "[LRM]" },   // Left-to-Right Mark
            { '\u200F', "[RLM]" },   // Right-to-Left Mark
            { '\u200D', "[ZWJ]" },   // Zero Width Joiner
            { '\u200C', "[ZWNJ]" },  // Zero Width Non-Joiner
            { '\u202A', "[LRE]" },   // Left-to-Right Embedding
            { '\u202B', "[RLE]" },   // Right-to-Left Embedding
            { '\u202D', "[LRO]" },   // Left-to-Right Override
            { '\u202E', "[RLO]" },   // Right-to-Left Override
            { '\u202C', "[PDF]" },   // Pop Directional Formatting
            { '\u206A', "[NADS]" },  // National Digit Shapes
            { '\u206B', "[NODS]" },  // Nominal Digit Shapes
            { '\u206F', "[ISS]" },   // Inhibit Symmetric Swapping
            { '\u2061', "[IAFS]" },  // Inhibit Arabic Form Shaping
            { '\u206E', "[ASS]" },   // Activate Symmetric Swapping
            { '\u206D', "[AAFS]" },  // Activate Arabic Form Shaping
            { '\u2028', "[RS]" },    // Record Separator
            { '\u2029', "[PS]" }     // Paragraph Separator
        };

        private void ShowUnicodeControlCharactersContextMenuStripButton_Click(object sender, EventArgs e)
        {
            // Update vars and buttons
            showUnicodeControlChars = !showUnicodeControlChars;
            showUnicodeControlCharactersContextMenuStripButton.Checked = showUnicodeControlChars;

            // Backup original text
            string originalText = richTextBox.Text;

            // Show unicode control characters
            if (showUnicodeControlChars)
            {
                // Replace control characters with visible representations
                string textWithControlChars = richTextBox.Text;

                for (int i = 0; i < textWithControlChars.Length; i++)
                {
                    char currentChar = textWithControlChars[i];

                    if (controlCharacterMappings.ContainsKey(currentChar))
                    {
                        // Replace the control character with its mapped value
                        textWithControlChars = textWithControlChars.Remove(i, 1).Insert(i, controlCharacterMappings[currentChar]);
                    }
                }

                richTextBox.Text = textWithControlChars;
            }
            else
            {
                // Restore the original text without visible representations
                richTextBox.Text = originalText;
            }
        }

        private void RightToLeftReadingOrderContextMenuStripButton_Click(object sender, EventArgs e)
        {
            // Save richTextBox Modified state
            bool rtbPrevState = richTextBox.Modified;

            // Update vars and buttons
            rightToLeftReadingOrderContextMenuStripButton.Checked = !rightToLeftReadingOrderContextMenuStripButton.Checked;
            displayRightToLeft = !displayRightToLeft;

            // Set the RightToLeft property of richTextBox
            richTextBox.RightToLeft = displayRightToLeft ? RightToLeft.Yes : RightToLeft.No;

            // Reset richTextBox Modified to previous state
            richTextBox.Modified = rtbPrevState;
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // Adjust contextMenuStrip display style
            contextMenuStrip.RightToLeft = RightToLeft.No;
            contextMenuStrip.Show(Cursor.Position);
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Suppress center paragraph shortcut key
            if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
