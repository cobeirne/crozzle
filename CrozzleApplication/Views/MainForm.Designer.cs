namespace CrozzleGame
{
    /// <summary>
    /// The main form is the primary application interface. All user interactions are intiated from
    /// this form.
    /// </summary>
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, 
        /// false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPenConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solveCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdCrozzleFile = new System.Windows.Forms.OpenFileDialog();
            this.wbCrozzleViewer = new System.Windows.Forms.WebBrowser();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.wbLogViewer = new System.Windows.Forms.WebBrowser();
            this.tpScore = new System.Windows.Forms.TabPage();
            this.wbScoreViewer = new System.Windows.Forms.WebBrowser();
            this.tcResults = new System.Windows.Forms.TabControl();
            this.ofdConfigFile = new System.Windows.Forms.OpenFileDialog();
            this.lblCrozzle = new System.Windows.Forms.Label();
            this.lblConfig = new System.Windows.Forms.Label();
            this.lblCrozzleFile = new System.Windows.Forms.Label();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.sfdCrozzleFile = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslMainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbWordsSolved = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslWordsSolved = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTimeRemaining = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbTimeRemaining = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslTimeRemaining = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpScore.SuspendLayout();
            this.tcResults.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.crozzleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(764, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openCrozzleToolStripMenuItem,
            this.oPenConfigurationToolStripMenuItem,
            this.saveCrozzleToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // openCrozzleToolStripMenuItem
            // 
            this.openCrozzleToolStripMenuItem.Name = "openCrozzleToolStripMenuItem";
            this.openCrozzleToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.openCrozzleToolStripMenuItem.Text = "Open Crozzle...";
            this.openCrozzleToolStripMenuItem.Click += new System.EventHandler(this.OpenCrozzleToolStripMenuItem_Click);
            // 
            // oPenConfigurationToolStripMenuItem
            // 
            this.oPenConfigurationToolStripMenuItem.Name = "oPenConfigurationToolStripMenuItem";
            this.oPenConfigurationToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.oPenConfigurationToolStripMenuItem.Text = "Open Configuration...";
            this.oPenConfigurationToolStripMenuItem.Click += new System.EventHandler(this.OpenConfigurationToolStripMenuItem_Click);
            // 
            // saveCrozzleToolStripMenuItem
            // 
            this.saveCrozzleToolStripMenuItem.Name = "saveCrozzleToolStripMenuItem";
            this.saveCrozzleToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.saveCrozzleToolStripMenuItem.Text = "Save Crozzle...";
            this.saveCrozzleToolStripMenuItem.Click += new System.EventHandler(this.SaveCrozzleToolStripMenuItem_Click);
            // 
            // crozzleToolStripMenuItem
            // 
            this.crozzleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playCrozzleToolStripMenuItem,
            this.solveCrozzleToolStripMenuItem});
            this.crozzleToolStripMenuItem.Name = "crozzleToolStripMenuItem";
            this.crozzleToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.crozzleToolStripMenuItem.Text = "&Crozzle";
            // 
            // playCrozzleToolStripMenuItem
            // 
            this.playCrozzleToolStripMenuItem.Name = "playCrozzleToolStripMenuItem";
            this.playCrozzleToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.playCrozzleToolStripMenuItem.Text = "Play Crozzle";
            this.playCrozzleToolStripMenuItem.Click += new System.EventHandler(this.PlayCrozzleToolStripMenuItem_Click);
            // 
            // solveCrozzleToolStripMenuItem
            // 
            this.solveCrozzleToolStripMenuItem.Name = "solveCrozzleToolStripMenuItem";
            this.solveCrozzleToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.solveCrozzleToolStripMenuItem.Text = "Solve Crozzle";
            this.solveCrozzleToolStripMenuItem.Click += new System.EventHandler(this.SolveCrozzleToolStripMenuItem_Click);
            // 
            // ofdCrozzleFile
            // 
            this.ofdCrozzleFile.FileName = "openFileDialog1";
            // 
            // wbCrozzleViewer
            // 
            this.wbCrozzleViewer.Location = new System.Drawing.Point(12, 103);
            this.wbCrozzleViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbCrozzleViewer.Name = "wbCrozzleViewer";
            this.wbCrozzleViewer.Size = new System.Drawing.Size(728, 630);
            this.wbCrozzleViewer.TabIndex = 3;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.wbLogViewer);
            this.tpLog.Location = new System.Drawing.Point(4, 22);
            this.tpLog.Name = "tpLog";
            this.tpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpLog.Size = new System.Drawing.Size(720, 163);
            this.tpLog.TabIndex = 1;
            this.tpLog.Text = "Event Log";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // wbLogViewer
            // 
            this.wbLogViewer.Location = new System.Drawing.Point(6, 6);
            this.wbLogViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbLogViewer.Name = "wbLogViewer";
            this.wbLogViewer.Size = new System.Drawing.Size(708, 151);
            this.wbLogViewer.TabIndex = 2;
            // 
            // tpScore
            // 
            this.tpScore.Controls.Add(this.wbScoreViewer);
            this.tpScore.Location = new System.Drawing.Point(4, 22);
            this.tpScore.Name = "tpScore";
            this.tpScore.Padding = new System.Windows.Forms.Padding(3);
            this.tpScore.Size = new System.Drawing.Size(720, 163);
            this.tpScore.TabIndex = 0;
            this.tpScore.Text = "Score";
            this.tpScore.UseVisualStyleBackColor = true;
            // 
            // wbScoreViewer
            // 
            this.wbScoreViewer.Location = new System.Drawing.Point(6, 6);
            this.wbScoreViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbScoreViewer.Name = "wbScoreViewer";
            this.wbScoreViewer.Size = new System.Drawing.Size(708, 151);
            this.wbScoreViewer.TabIndex = 6;
            // 
            // tcResults
            // 
            this.tcResults.Controls.Add(this.tpScore);
            this.tcResults.Controls.Add(this.tpLog);
            this.tcResults.Location = new System.Drawing.Point(12, 739);
            this.tcResults.Name = "tcResults";
            this.tcResults.SelectedIndex = 0;
            this.tcResults.Size = new System.Drawing.Size(728, 189);
            this.tcResults.TabIndex = 7;
            // 
            // ofdConfigFile
            // 
            this.ofdConfigFile.FileName = "openFileDialog1";
            // 
            // lblCrozzle
            // 
            this.lblCrozzle.AutoSize = true;
            this.lblCrozzle.Location = new System.Drawing.Point(40, 38);
            this.lblCrozzle.Name = "lblCrozzle";
            this.lblCrozzle.Size = new System.Drawing.Size(63, 13);
            this.lblCrozzle.TabIndex = 9;
            this.lblCrozzle.Text = "Crozzle File:";
            this.lblCrozzle.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.Location = new System.Drawing.Point(12, 69);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(91, 13);
            this.lblConfig.TabIndex = 10;
            this.lblConfig.Text = "Configuration File:";
            // 
            // lblCrozzleFile
            // 
            this.lblCrozzleFile.AutoSize = true;
            this.lblCrozzleFile.Location = new System.Drawing.Point(109, 38);
            this.lblCrozzleFile.Name = "lblCrozzleFile";
            this.lblCrozzleFile.Size = new System.Drawing.Size(39, 13);
            this.lblCrozzleFile.TabIndex = 11;
            this.lblCrozzleFile.Text = "[None]";
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(109, 69);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(39, 13);
            this.lblConfigFile.TabIndex = 12;
            this.lblConfigFile.Text = "[None]";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslMainStatus,
            this.tspbWordsSolved,
            this.tsslWordsSolved,
            this.tslTimeRemaining,
            this.tspbTimeRemaining,
            this.tsslTimeRemaining});
            this.statusStrip1.Location = new System.Drawing.Point(0, 939);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(764, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslMainStatus
            // 
            this.tsslMainStatus.Name = "tsslMainStatus";
            this.tsslMainStatus.Size = new System.Drawing.Size(39, 17);
            this.tsslMainStatus.Text = "Ready";
            // 
            // tspbWordsSolved
            // 
            this.tspbWordsSolved.Name = "tspbWordsSolved";
            this.tspbWordsSolved.Size = new System.Drawing.Size(170, 16);
            this.tspbWordsSolved.Value = 50;
            this.tspbWordsSolved.Visible = false;
            // 
            // tsslWordsSolved
            // 
            this.tsslWordsSolved.Name = "tsslWordsSolved";
            this.tsslWordsSolved.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.tsslWordsSolved.Size = new System.Drawing.Size(153, 17);
            this.tsslWordsSolved.Text = "0 of 0 Words | Score: 0";
            this.tsslWordsSolved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsslWordsSolved.Visible = false;
            // 
            // tslTimeRemaining
            // 
            this.tslTimeRemaining.Name = "tslTimeRemaining";
            this.tslTimeRemaining.Size = new System.Drawing.Size(97, 17);
            this.tslTimeRemaining.Text = "Time Remaining:";
            this.tslTimeRemaining.Visible = false;
            // 
            // tspbTimeRemaining
            // 
            this.tspbTimeRemaining.Name = "tspbTimeRemaining";
            this.tspbTimeRemaining.Size = new System.Drawing.Size(170, 16);
            this.tspbTimeRemaining.Visible = false;
            // 
            // tsslTimeRemaining
            // 
            this.tsslTimeRemaining.Name = "tsslTimeRemaining";
            this.tsslTimeRemaining.Size = new System.Drawing.Size(72, 17);
            this.tsslTimeRemaining.Text = "999 Seconds";
            this.tsslTimeRemaining.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(764, 961);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lblConfigFile);
            this.Controls.Add(this.lblCrozzleFile);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.lblCrozzle);
            this.Controls.Add(this.tcResults);
            this.Controls.Add(this.wbCrozzleViewer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(780, 1000);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SIT323 Crozzle Game";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tpLog.ResumeLayout(false);
            this.tpScore.ResumeLayout(false);
            this.tcResults.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openCrozzleToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdCrozzleFile;
        private System.Windows.Forms.WebBrowser wbCrozzleViewer;
        private System.Windows.Forms.TabPage tpLog;
        private System.Windows.Forms.WebBrowser wbLogViewer;
        private System.Windows.Forms.TabPage tpScore;
        private System.Windows.Forms.WebBrowser wbScoreViewer;
        private System.Windows.Forms.TabControl tcResults;
        private System.Windows.Forms.ToolStripMenuItem oPenConfigurationToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog ofdConfigFile;
        private System.Windows.Forms.Label lblCrozzle;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.Label lblCrozzleFile;
        private System.Windows.Forms.Label lblConfigFile;
        private System.Windows.Forms.ToolStripMenuItem crozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCrozzleToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog sfdCrozzleFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslMainStatus;
        private System.Windows.Forms.ToolStripMenuItem solveCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar tspbWordsSolved;
        private System.Windows.Forms.ToolStripStatusLabel tsslWordsSolved;
        private System.Windows.Forms.ToolStripStatusLabel tslTimeRemaining;
        private System.Windows.Forms.ToolStripProgressBar tspbTimeRemaining;
        private System.Windows.Forms.ToolStripStatusLabel tsslTimeRemaining;
    }
}

