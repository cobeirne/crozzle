/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 2
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       02/10/16
/// </summary>

using CrozzleGame.Controllers;
using CrozzleGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrozzleGame
{
    /// <summary>
    /// This is the main user iteraction form. It allows users to intiate controller actions and
    /// display the results returned.
    /// </summary>
    public partial class frmMain : Form
    {
        #region Class Constants

        const string ErrorMessageTitle = "Crozzle Error";
        const string LogTabName = "tpLog";
        const string ScoreTabName = "tpScore";
        const string CrozzleDialogueFilter = "txt files (*.txt)|*.txt";
        const string DefaultFileName = "MyCrozzle.txt";
        const int CrozzleDialogueFilterIndex = 2;

        #endregion

        #region Class Properties

        private string CrozzleFilePath = "";
        private string ConfigFilePath = "";

        #endregion

        #region Class Constructors

        public frmMain()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This function prompts the user for a Configuration File to open. It then calls on the 
        /// Crozzle controller to open the file.
        /// </summary>
        private void OpenConfigurationFile()
        {
            const string ConfigDialogueTitle = "Open Configuration File";
            const string ConfigDialogueFilter = "txt files (*.txt)|*.txt";
            const int ConfigDialogueFilterIndex = 2;


            // Open file dialog.
            ofdConfigFile.Title = ConfigDialogueTitle;
            ofdConfigFile.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            ofdConfigFile.Filter = ConfigDialogueFilter;
            ofdConfigFile.FilterIndex = ConfigDialogueFilterIndex;
            ofdConfigFile.RestoreDirectory = true;
            DialogResult result = ofdConfigFile.ShowDialog();

            // Check if file exists.
            if (result == DialogResult.OK)
            {
                // Update form status feilds.
                lblConfigFile.Text = ofdConfigFile.FileName;
                this.ConfigFilePath = ofdConfigFile.FileName;
                tsslMainStatus.Text = "Configuration file opened.";
            }
            else if (result == DialogResult.Cancel)
            {
                this.ConfigFilePath = string.Empty;
            }
            else
            {
                // Reset form status feilds.
                this.ConfigFilePath = this.CrozzleFilePath = string.Empty;
                lblConfigFile.Text = ofdConfigFile.FileName;
                tsslMainStatus.Text = "Ready.";

                // Prompt with user error.
                string errorMsg = string.Format("The selected configuration file does not exists.");
                MessageBox.Show(errorMsg, "Configuration Open Error");
            }
        }

        /// <summary>
        /// This function prompts the user for a Crozzle File to open. It then calls on the 
        /// Crozzle controller to open the file.
        /// </summary>
        private void OpenCrozzleFile()
        {
            const string CrozzleDialogueTitle = "Open Crozzle File";
            const int CrozzleDialogueFilterIndex = 2;

            // Open file dialog.
            ofdCrozzleFile.Title = CrozzleDialogueTitle;
            ofdCrozzleFile.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            ofdCrozzleFile.Filter = CrozzleDialogueFilter;
            ofdCrozzleFile.FilterIndex = CrozzleDialogueFilterIndex;
            ofdCrozzleFile.RestoreDirectory = true;
            ofdCrozzleFile.FileName = DefaultFileName;
            DialogResult result = ofdCrozzleFile.ShowDialog();

            // Check if file exists.
            if (result == DialogResult.OK)
            {
                // Update form status feilds.
                lblCrozzleFile.Text = ofdCrozzleFile.FileName;
                this.CrozzleFilePath = ofdCrozzleFile.FileName;
                tsslMainStatus.Text = "Crozzle file opened.";
            }
            else if(result == DialogResult.Cancel)
            {
                this.CrozzleFilePath = string.Empty;
            }
            else
            {
                // Reset form status feilds.
                this.CrozzleFilePath = string.Empty;
                lblCrozzleFile.Text = ofdCrozzleFile.FileName;
                tsslMainStatus.Text = "Ready.";

                // Prompt with user error.
                string errorMsg = string.Format("The selected Crozzle file does not exists.");
                MessageBox.Show(errorMsg, "Crozzle Open Error");
            }
        }

        /// <summary>
        /// Prompt the user for a crozzle file using a file dialogue. Check if the file exists and
        /// if so call the Crozzle Controller to open the file.
        /// </summary>
        /// <exception cref=“System.Exception”>Crozzle controller threw an exception while 
        /// attemptng to play.</exception>
        private void PlayCrozzle()
        {
            const string LogTabName = "tpLog";
            const string ScoreTabName = "tpScore";

            if ((this.CrozzleFilePath != string.Empty) && (this.ConfigFilePath != string.Empty))
            {
                // Clear log buffer.
                Program.CrozzleControl.CrozzleLog.LogBuffer.Clear();

                // Reset the crozzle viewers.
                ResetViewer(wbCrozzleViewer);
                ResetViewer(wbScoreViewer);
                ResetViewer(wbLogViewer);
                tcResults.SelectedTab = tcResults.TabPages[LogTabName];

                try
                {
                    // Open the selected files.
                    Program.CrozzleControl.OpenCrozzle(this.CrozzleFilePath, true);
                    Program.CrozzleControl.OpenConfiguration(this.ConfigFilePath);

                    // Play the crozzle.
                    Program.CrozzleControl.PlayCrozzle();

                    // Update the crozzle viewers content.
                    if (Program.CrozzleControl.Grid != null)
                    {
                        string crozzleHtml = Program.CrozzleControl.Grid.GetGridHtml();
                        WriteViewerContent(wbCrozzleViewer, crozzleHtml);
                    }

                    if (Program.CrozzleControl.Score != null)
                    {
                        string scoreHtml = Program.CrozzleControl.Score.GetScoreHtml();
                        WriteViewerContent(wbScoreViewer, scoreHtml);
                        tcResults.SelectedTab = tcResults.TabPages[ScoreTabName];
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format("An error occured while trying to play the crozzle: {0}",
                        e.Message), ErrorMessageTitle);
                }

                // Initialise the Log Viewer and show all buffered logs.
                string logHtml = Program.CrozzleControl.CrozzleLog.GetLogBufferHtml();
                WriteViewerContent(wbLogViewer, logHtml);
            }
            else
            {
                MessageBox.Show(string.Format("Please select a valid crozzle and configuration file."),
                    ErrorMessageTitle);
            }
        }

        /// <summary>
        /// Reset the HTML markup in the passed web browser.
        /// </summary>
        /// <param name="webBrowser"></param>
        private void ResetViewer(WebBrowser webBrowser)
        {
            const string DefaultHtml = "<html><body>&nbsp</body></html>";

            WriteViewerContent(webBrowser, DefaultHtml);
        }

        /// <summary>
        /// This function prompts the user for a Crozzle File path to save. If successfull, the 
        /// current crozzle targets the saved file.
        /// </summary>
        private void SaveCrozzleFile()
        {
            const string CrozzleDialogueTitle = "Save Crozzle File";

            // Open the Save File Dialog.
            sfdCrozzleFile.Title = CrozzleDialogueTitle;
            sfdCrozzleFile.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
            sfdCrozzleFile.Filter = CrozzleDialogueFilter;
            sfdCrozzleFile.FilterIndex = CrozzleDialogueFilterIndex;
            sfdCrozzleFile.RestoreDirectory = true;
            sfdCrozzleFile.FileName = DefaultFileName;
            DialogResult result = sfdCrozzleFile.ShowDialog();

            // Check if file saved OK.
            if (result == DialogResult.OK)
            {
                try
                {
                    // Set current crozzle to saved file.
                    this.CrozzleFilePath = sfdCrozzleFile.FileName;
                    Program.CrozzleControl.SaveCrozzle(this.CrozzleFilePath);
                    lblCrozzleFile.Text = this.CrozzleFilePath;
                    tsslMainStatus.Text = "Crozzle file saved.";
                }
                catch (Exception e)
                {
                    // Prompt with user error.
                    MessageBox.Show(string.Format("An error occured while trying to save the crozzle: {0}",
                    e.Message), ErrorMessageTitle);
                }
            }
            else if (result == DialogResult.Cancel)
            {
                // Reset the crozzle file path if the user cancels the save dialog.
                this.CrozzleFilePath = string.Empty;
            }
            else
            {
                // Reset form status feilds.
                this.CrozzleFilePath = string.Empty;
                tsslMainStatus.Text = "Ready.";

                // Prompt with user error.
                string errorMsg = string.Format("The selected Crozzle file is invalid.");
                MessageBox.Show(errorMsg, "Crozzle Save Error");
            }
        }

        /// <summary>
        /// This function initialisies the form for solving, then calls the crozzle solver 
        /// asyncronously. Event handlers are initialised for the solver thread to trigger form
        /// updates.
        /// </summary>
        private void StartSolveCrozzle()
        {
            // Only attempt to solve if a crozzle and configuration file has been selected.
            if ((this.CrozzleFilePath != string.Empty) && (this.ConfigFilePath != string.Empty))
            {
                // Clear log buffer.
                Program.CrozzleControl.CrozzleLog.LogBuffer.Clear();

                // Reset the crozzle viewers.
                ResetViewer(wbCrozzleViewer);
                ResetViewer(wbScoreViewer);
                ResetViewer(wbLogViewer);
                tcResults.SelectedTab = tcResults.TabPages[LogTabName];

                try
                {
                    // Open the selected files.
                    Program.CrozzleControl.OpenCrozzle(this.CrozzleFilePath, false);
                    Program.CrozzleControl.OpenConfiguration(this.ConfigFilePath);


                    // Initialise solver.
                    Program.CrozzleControl.InitialiseSolver();

                    // The solver will run asyncronously, so initialise event handers to trigger 
                    // form status updates.
                    Program.CrozzleControl.CrozzleSolver.SolverUpdate += 
                        new CrozzleSolverModel.SolverUpdateHandler(UpdateSolveCrozzle);
                    Program.CrozzleControl.SolverFinished += 
                        new CrozzleController.SolverFinishedHandler(FinishSolveCrozzle);

                    // Initialise the status bar.
                    tsslMainStatus.Text = "Solved:";
                    tsslWordsSolved.Visible = true;
                    tspbWordsSolved.Value = 0;
                    tspbWordsSolved.Visible = true;

                    tslTimeRemaining.Visible = true;
                    tsslTimeRemaining.Visible = true;
                    tspbTimeRemaining.Value = 0;
                    tspbTimeRemaining.Visible = true;

                    // Start a new solver thread.
                    Thread solverThread = new Thread(Program.CrozzleControl.SolveCrozzle);
                    solverThread.Start();
                    
                    // Reset the grid viewer content.
                    if (Program.CrozzleControl.Grid != null)
                    {
                        string crozzleHtml = Program.CrozzleControl.Grid.GetGridHtml();
                        WriteViewerContent(wbCrozzleViewer, string.Empty);
                    }

                    // Reset the score viewer content.
                    if (Program.CrozzleControl.Score != null)
                    {
                        string scoreHtml = Program.CrozzleControl.Score.GetScoreHtml();
                        WriteViewerContent(wbScoreViewer, string.Empty);
                    }

                    // Change to Log View while solving.
                    tcResults.SelectedTab = tcResults.TabPages[LogTabName];
                }
                catch (Exception e)
                {
                    // Prompt with user error.
                    MessageBox.Show(string.Format("An error occured while trying to play the crozzle: {0}",
                        e.Message), ErrorMessageTitle);
                }

                // Initialise the Log Viewer and show all buffered logs.
                string logHtml = Program.CrozzleControl.CrozzleLog.GetLogBufferHtml();
                WriteViewerContent(wbLogViewer, logHtml);
            }
            else
            {
                // Prompt with user error.
                MessageBox.Show(string.Format("Please select a valid crozzle and configuration file."),
                    ErrorMessageTitle);
            }
        }
        
        /// <summary>
        /// Updates the web browser content with the passed html.
        /// </summary>
        /// <param name="webBrowser">The web browser to be updated.</param>
        /// <param name="htmlText">The HTML to be rendered.</param>
        private void WriteViewerContent(WebBrowser webBrowser, string htmlText)
        {
            const string DefaultHtmlPage = "about:blank";

            webBrowser.Navigate(DefaultHtmlPage);
            webBrowser.Document.OpenNew(false);
            webBrowser.Document.Write(htmlText);
            webBrowser.Refresh();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This event is triggered by the crozzle solver when completed. The solver runs on its 
        /// own thread, therefore the event needs to be syncronised to the form thread to enable
        /// interaction.
        /// </summary>
        /// <param name="controller">The crozzle controller that triggered the event.</param>
        /// <param name="e">The calling event arguments.</param>
        private void FinishSolveCrozzle(CrozzleController controller, EventArgs e)
        {
            // Invoke required to syncronise the event with the form thread.
            this.Invoke((MethodInvoker)delegate {

                // Update form status.
                tsslMainStatus.Text = string.Format("{0} Words Solved", 
                    controller.CrozzleSolver.WordsSolved);
                tsslWordsSolved.Visible = false;
                tspbWordsSolved.Visible = false;
                tslTimeRemaining.Visible = false;
                tsslTimeRemaining.Visible = false;
                tspbTimeRemaining.Visible = false;

                try
                {
                    // Update the crozzle viewer content.
                    if (controller.Grid != null)
                    {
                        string crozzleHtml = controller.Grid.GetGridHtml();
                        WriteViewerContent(wbCrozzleViewer, crozzleHtml);
                    }

                    // Update the score viewer content.
                    if (controller.Score != null)
                    {
                        string scoreHtml = controller.Score.GetScoreHtml();
                        WriteViewerContent(wbScoreViewer, scoreHtml);
                        tcResults.SelectedTab = tcResults.TabPages[ScoreTabName];
                    }

                    // Initialise the Log Viewer and show all buffered logs.
                    string logHtml = Program.CrozzleControl.CrozzleLog.GetLogBufferHtml();
                    WriteViewerContent(wbLogViewer, logHtml);
                }
                catch (Exception ex)
                {
                    // Prompt with user error.
                    MessageBox.Show(string.Format("An error occured while trying to solve the crozzle: {0}",
                        ex.Message), ErrorMessageTitle);
                }
            });
        }

        /// <summary>
        /// Attempts to open the configuration file on user menu selection.
        /// </summary>
        /// <param name="sender">Menu item sender object.</param>
        /// <param name="e">The calling event arguments.</param>
        private void OpenConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenConfigurationFile();
        }

        /// <summary>
        /// Attempts to open the crozzle file on user menu selection.
        /// </summary>
        /// <param name="sender">Menu item sender object.</param>
        /// <param name="e">The calling event arguments.</param>
        private void OpenCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCrozzleFile();
        }

        /// <summary>
        /// Attempts to play the saved crozzle on user menu selection.
        /// </summary>
        /// <param name="sender">Menu item sender object.</param>
        /// <param name="e">The calling event arguments.</param>
        private void PlayCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayCrozzle();
        }

        /// <summary>
        /// Attempts to save the crozzle on user menu selection.
        /// </summary>
        /// <param name="sender">Menu item sender object.</param>
        /// <param name="e">The calling event arguments.</param>
        private void SaveCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveCrozzleFile();
        }

        /// <summary>
        /// Attempts to solve the crozzle on user menu selection.
        /// </summary>
        /// <param name="sender">Menu item sender object.</param>
        /// <param name="e">The calling event arguments.</param>
        private void SolveCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartSolveCrozzle();
        }

        /// <summary>
        /// This event is triggered by the crozzle solver at set intervals while solving. The 
        /// solver runs on its own thread, therefore the event needs to be syncronised to the form 
        /// thread to enable interaction.
        /// </summary>
        /// <param name="solver"></param>
        /// <param name="e"></param>
        private void UpdateSolveCrozzle(CrozzleSolverModel solver, EventArgs e)
        {
            const decimal PercentageFactor = 100.0M;
            const decimal InitialPercentage = 0.0M;
            const int MinimumProgressValue = 1;
            const int MillisecondFactor = 1000;

            // Initialise progress bars.
            decimal wordsPercentage = InitialPercentage;
            decimal timePercentage = InitialPercentage;

            // Calculate word count progress.
            if (solver.WordCount >= MinimumProgressValue)
            {
                wordsPercentage = (Convert.ToDecimal(solver.WordsSolved) /
                    Convert.ToDecimal(solver.WordCount)) * PercentageFactor;
            }

            // Calculate time remaining.
            if (solver.TimeRemaining >= (double)MinimumProgressValue)
            {
                timePercentage = PercentageFactor - ((Convert.ToDecimal(solver.TimeRemaining) /
                    (Convert.ToDecimal(CrozzleSolverModel.TimeoutInterval) / MillisecondFactor) *
                    PercentageFactor));
            }

            // Invoke required to syncronise the event with the form thread.
            this.Invoke((MethodInvoker)delegate {

                // Update words solved progress bar
                tspbWordsSolved.Value = Convert.ToInt32(wordsPercentage);
                tsslWordsSolved.Text = string.Format("{0} of {1} Words | Score: {2}", 
                    solver.WordsSolved, solver.WordCount, solver.BestSolutionNode.NodeScore);

                // Update time remaining progress bar.
                tspbTimeRemaining.Value = Convert.ToInt32(Math.Round(timePercentage, 0));
                tsslTimeRemaining.Text = string.Format("{0} Seconds", 
                    Math.Round(solver.TimeRemaining));

                // Update log viewer.
                string logHtml = Program.CrozzleControl.CrozzleLog.GetLogBufferHtml();
                WriteViewerContent(wbLogViewer, logHtml);
            });
        }

        #endregion
    }
}
