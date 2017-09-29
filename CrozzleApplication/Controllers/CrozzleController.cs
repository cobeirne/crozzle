/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using CrozzleGame.Models;
using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Linq;

namespace CrozzleGame.Controllers
{
    /// <summary>
    /// The Crozzle Controller handles requests from the user interface and other controllers to
    /// initiate actions or retreive data from crozzle related objects.
    /// </summary>
    public class CrozzleController
    {
        #region Class Events

        // The Solver Event is triggered when the SolveCrozzle() function completes.
        public event SolverFinishedHandler SolverFinished;
        public delegate void SolverFinishedHandler(CrozzleController controller, EventArgs e);

        #endregion

        #region Class Properties

        /// <summary>
        /// A Log Model instance used to record all crozzle related events.
        /// </summary>
        public LogModel CrozzleLog { get; set; }

        /// <summary>
        /// A Crozzle Model instance that models the current crozzle solution.
        /// </summary>
        public CrozzleModel Crozzle { get; set; }

        /// <summary>
        /// A Configuration Model instance that models the current crozzle configuration.
        /// </summary>
        public ConfigModel Configuration { get; set; }

        /// <summary>
        /// A Grid Model instance that models the current crozzle solution in grid format.
        /// </summary>
        public GridModel Grid { get; set; }

        /// <summary>
        /// A Score Model instance that models the current crozzle score.
        /// </summary>
        public ScoreModel Score { get; set; }

        /// <summary>
        /// A flag the indicates that the current Crozzle file has been validated and is valid.
        /// </summary>
        public bool CrozzleFileIsValid { get; set; }

        /// <summary>
        /// A flag the indicates that the current Configuration file has been validated and is 
        /// valid.
        /// </summary>
        public bool ConfigFileIsValid { get; set; }

        /// <summary>
        /// A flag the indicates that the current crozzle solution has been validated and is 
        /// valid.
        /// </summary>
        public bool CrozzleIsValid { get; set; }

        /// <summary>
        /// A Solver Model instance used to solve a crozzle.
        /// </summary>
        public CrozzleSolverModel CrozzleSolver { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Crozzle Controller constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        public CrozzleController()
        {
            this.CrozzleLog = new LogModel();
            this.CrozzleFileIsValid = false;
            this.ConfigFileIsValid = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Attempts to open a Crozzle file from the specified path and parse the contents into 
        /// the Crozzle Controller Crozzle property.
        /// </summary>
        /// <param name="crozzleFilePath">The full full path of the crozzle file to be opened.
        /// </param>
        /// <exception cref=“System.Exception”>Crozzle file open or parse exception.</exception>
        public void OpenCrozzle(string crozzleFilePath, bool isSolved)
        {
            // Log open file attempt.
            this.CrozzleLog.AddLog(string.Format("Info: Attempting to open crozzle {0}.",
                crozzleFilePath));
            try
            {
                // Open Crozzle file and load content.
                 string textDocument = OpenFile(crozzleFilePath);

                // Split text file document into lines.
                string[] textDocumentLines = textDocument.
                    Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

                // Log successfull open file attempt.
                this.CrozzleLog.AddLog("Info: Attempting to parse crozzle.");

                // Try to parse the file contents into a new Crozzle Model.
                CrozzleParserModel crozzleParser = new CrozzleParserModel(textDocumentLines);
                if (crozzleParser.TryParseCrozzle(isSolved))
                {
                    // Parse OK, update the controller properties.
                    this.Crozzle = crozzleParser.Crozzle;
                    this.CrozzleFileIsValid = true;
                }
                else
                {
                    // Parse failed, flag the file as invalid. 
                    this.CrozzleFileIsValid = false;
                }

                // Add parser errors to the log.
                crozzleParser.ValidationErrors.ForEach(e => this.CrozzleLog.AddLog(e));
            }
            catch (Exception e)
            {
                // Flag the file as invalid and log the exception message.
                this.CrozzleFileIsValid = false;
                this.CrozzleLog.AddLog(string.Format("Error: While opening crozzle file. {0}.", 
                    e.Message));
            }            
        }

        /// <summary>
        /// Attempts to serialize crozzle properties and save to a text file at a user specified
        /// path.
        /// </summary>
        /// <param name="crozzleFilePath">The saved file path.</param>
        public void SaveCrozzle(string crozzleFilePath)
        {
            // Update the log file.
            this.CrozzleLog.AddLog(string.Format("Info: Attempting to save crozzle {0}.",
                crozzleFilePath));

            try
            {
                // Format current crozzle.
                CrozzleSerializerModel serailizer = new CrozzleSerializerModel(this.Crozzle);
                string fileBody = serailizer.Serialize();

                // Open Crozzle file and load content.
                SaveFile(crozzleFilePath, fileBody);
            }
            catch (Exception e)
            {
                // Flag the file as invalid and log the exception message.
                this.CrozzleFileIsValid = false;
                this.CrozzleLog.AddLog(string.Format("Error: While saving crozzle file. {0}.",
                    e.Message));
            }
        }

        /// <summary>
        /// Attempts to open a Configuration file from the specified path and parse the contents 
        /// into the Crozzle Controller Configuration property.
        /// </summary>
        /// <param name="configFilePath">The full full path of the configuration file to be opened.
        /// </param>
        /// <exception cref=“System.Exception”>Configuration file open or parse exception.
        /// </exception>
        public void OpenConfiguration(string configFilePath)
        {
            // Log open file attempt.
            this.CrozzleLog.AddLog(string.Format("Info: Attempting to open configuration {0}.",
                configFilePath));
            try
            {
                // Open configuration file and load content.
                string textDocument = OpenFile(configFilePath);

                // Split text file document into lines.
                string[] textDocumentLines = textDocument.Split(new string[] { Environment.NewLine },
                    StringSplitOptions.None);

                // Log successfull open file attempt.
                this.CrozzleLog.AddLog("Info: Attempting to parse configuration.");

                // Try to parse the file contents into a new Configuration Model.
                ConfigParserModel configParser = new ConfigParserModel(textDocumentLines);
                if (configParser.TryParseConfiguration())
                {
                    // Parse OK, update the controller properties.
                    this.Configuration = configParser.Configuration;
                    this.ConfigFileIsValid = true;
                }
                else
                {
                    // Parse failed, flag the file as invalid. 
                    this.ConfigFileIsValid = false;
                }

                // Add parser errors to the log.
                configParser.ValidationErrors.ForEach(e => this.CrozzleLog.AddLog(e));
            }
            catch (Exception e)
            {
                // Flag the file as invalid and log the exception message.
                this.ConfigFileIsValid = false;
                this.CrozzleLog.AddLog(string.Format("Error: While opening configuration file {0}",
                    e.Message));
            }
        }

        /// <summary>
        /// Attempts to play the crozzle if both input crozzle/configuration files are valid. This
        /// involves loading the crozzle words into a grid, validating the solution based on 
        /// difficulty constraints and then calculating a score.
        /// NB: An invalid crozzle will be allocated a score of zero.
        /// </summary>
        /// <exception cref=“System.Exception”>A crozzle grid creation, crozzle validation or 
        /// scoring exception.
        /// </exception>
        public void PlayCrozzle()
        { 
            // Only process the crozzle if both files are valid.
            if (this.CrozzleFileIsValid && this.ConfigFileIsValid)
            {
                try
                {
                    // Log crozzle play attempt.
                    this.CrozzleLog.AddLog("Info: Playing Crozzle.");

                    // Update the Crozzle configuration.
                    this.Crozzle.Configuration = this.Configuration;

                    // Create a crozzle Grid Model.
                    this.CrozzleLog.AddLog(string.Format("Info: Creating Crozzle Grid."));
                    this.Grid = new GridModel(this.Crozzle);

                    // Create a Crozzle Validator and validate the solution.
                    this.CrozzleLog.AddLog(string.Format("Info: Validating Crozzle."));
                    CrozzleValidationModel crozzleValidator = new CrozzleValidationModel(this.Grid);
                    this.CrozzleIsValid = crozzleValidator.CrozzleIsValid;

                    // Log all validation errors.
                    crozzleValidator.ValidationErrors.ForEach(e => this.CrozzleLog.AddLog(e));

                    // Score the current crozzle solution.
                    this.CrozzleLog.AddLog(string.Format("Info: Scoring Crozzle."));
                    this.Score = new ScoreModel(this.Grid, this.CrozzleIsValid);
                }
                catch (Exception e)
                {
                    // Log an exception occuring during crozzle grid rendering, validation or 
                    //scoring.
                    this.CrozzleLog.AddLog(string.Format("Error: While playing crozzle. {0}", 
                        e.Message));
                }
            }
            else
            {       
                // Log a failed crozzle play attempt.
                this.CrozzleLog.AddLog(string.
                    Format("Info: File errors detected, Play Crozzle aborted."));

                // Reset the grid and score is the current crozzle solution is invalid.
                this.Grid = null;
                this.Score = null;
            }

            // Write all buffered logs to the log file.
            WriteLogFile();
        }

        /// <summary>
        /// Initialise the controller solver before it is called asyncronously.
        /// </summary>
        public void InitialiseSolver()
        {
            this.Crozzle.Configuration = this.Configuration;

            this.CrozzleSolver = new CrozzleSolverModel(this.Crozzle);
        }

        /// <summary>
        /// This function attempts to solve the crozzle based on the current crozzle and 
        /// configuration file. The method is called asyncronously and triggers an event when
        /// completed, this includes the solution score and grid.
        /// </summary>
        public void SolveCrozzle()
        {
            const string HorizontalLiteral = "HORIZONTAL";

            // Only process the crozzle if both files are valid.
            if (this.CrozzleFileIsValid && this.ConfigFileIsValid)
            {
                try
                {
                    // Log crozzle solve attempt.
                    this.CrozzleLog.AddLog("Info: Solving Crozzle.");

                    // Attempt to solve the crozzle.
                    this.CrozzleSolver.SolveCrozzle();

                    // Update the current controller with the best solutions found.
                    this.Grid = this.CrozzleSolver.BestSolutionNode.NodeGrid;
                    this.Crozzle.WordList = 
                        this.CrozzleSolver.BestSolutionNode.NodeGrid.Crozzle.WordList;
                    this.Crozzle.HorizontalWords =
                        this.Crozzle.WordList.Where(w => w.Orientation == HorizontalLiteral)
                        .Count();
                    this.Crozzle.HorizontalWords =
                    this.Crozzle.VerticalWords = 
                        this.Crozzle.WordList.Where(w => w.Orientation != HorizontalLiteral)
                        .Count();

                    // Create a Crozzle Validator and validate the solution.
                    this.CrozzleLog.AddLog(string.Format("Info: Validating Solution."));
                    CrozzleValidationModel crozzleValidator = new CrozzleValidationModel(this.Grid);
                    this.CrozzleIsValid = crozzleValidator.CrozzleIsValid;

                    // Log all validation errors.
                    crozzleValidator.ValidationErrors.ForEach(e => this.CrozzleLog.AddLog(e));

                    // Score the current crozzle solution.
                    this.Score = new ScoreModel(this.Grid, this.CrozzleIsValid);

                    // Log crozzle score.
                    string scoreInfo = string.Format("Info: {0} of {1} Words Solved, Score: {2}", 
                        this.CrozzleSolver.WordsSolved, this.CrozzleSolver.WordCount, 
                        this.Score.TotalScore);
                    this.CrozzleLog.AddLog(scoreInfo);

                    // Trigger the Solver Finished Event.
                    SolverFinished(this, null);
                }
                catch (Exception e)
                {
                    // Log a failed crozzle solve attempt.
                    this.CrozzleLog.AddLog(string.
                        Format("Error: While attempting to solve crozzle.{0}", e.Message));

                    // Reset the grid and score, the current crozzle solution is invalid.
                    this.Grid = null;
                    this.Score = null;
                }
            }

            // Write all buffered logs to the log file.
            WriteLogFile();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Opens a file and return its contents as a single string.
        /// </summary>
        /// <param name="filePath">The full path of the file to be opened.</param>
        /// <returns>The file contents in string format.</returns>
        /// <exception cref=“System.Exception”>A file open exception.</exception>
        private string OpenFile(string filePath)
        {
            string fileText = "";

            try
            {
                // Attempt to open the file.
                using (StreamReader reader = new StreamReader(filePath)) 
                {
                    // Load the full file contents into a single string.
                    fileText = reader.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                // Throw a new exception if a file access error occurs.
                throw new Exception("Open File Exception.", e);
            }

            return fileText;
        }

        /// <summary>
        /// Attempts to save text data to a target file.
        /// </summary>
        /// <param name="filePath">The file to be saved.</param>
        /// <param name="textData">The text to be saved to the file.</param>
        private void SaveFile(string filePath, string textData)
        {
            try
            {
                // Attempt to open the file.
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Load the full file contents into a single string.
                    writer.Write(textData);
                }
            }
            catch (Exception e)
            {
                // Throw a new exception if a file access error occurs.
                throw new Exception("Save File Exception.", e);
            }
        }

        /// <summary>
        /// This function writes the current log buffer to the specified log file.
        /// </summary>
        /// <exception cref=“System.Exception”>A file access exception.</exception>
        private void WriteLogFile()
        {            
            try
            {
                // Get the App Log Path and File Name from Application Settings.
                string logPath = Properties.Settings.Default.AppLogPath;
                string logFileName = Properties.Settings.Default.AppLogFileName;

                // Create to Log Directory if it does not exist.
                System.IO.Directory.CreateDirectory(logPath);

                // Open the log file.
                string filePath = string.Format("{0}//{1}", logPath, logFileName);
                using (StreamWriter writer = new StreamWriter(filePath, true)) 
                {
                    // Write each log entry to a new line.
                    foreach (var log in this.CrozzleLog.LogBuffer)
                    {
                        writer.WriteLine(string.Format("{0} {1}", log.Key, log.Value));
                    }
                }
            }
            catch(Exception e)
            {
                // Throw a new exception if a file access error occurs.
                throw new Exception("Write Log File Exception.", e);
            }
        }

        #endregion
    }
}
