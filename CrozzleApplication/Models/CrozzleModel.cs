/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System.Collections.Generic;
using System.Linq;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This model represents the data stored in a Crozzle File.
    /// </summary>
    public class CrozzleModel
    {
        #region Class Properties
        
        /// <summary>
        /// A Configuration Model instance associated with this crozzle.
        /// </summary>
        public ConfigModel Configuration { get; set; }

        /// <summary>
        /// The number of words expected in the crozzle word pool.
        /// </summary>
        public int WordPoolSize { get; set; }

        /// <summary>
        /// The number of crozzle rows.
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// The number of crozzle columns.
        /// </summary>
        public int Columns { get; set; }

        /// <summary>
        /// The number of horizontal words expected in the crozzle.
        /// </summary>
        public int HorizontalWords { get; set; }

        /// <summary>
        /// The number of vertical words expected in the crozzle.
        /// </summary>
        public int VerticalWords { get; set; }

        /// <summary>
        /// A list of words from the crozzle word pool.
        /// </summary>
        public List<string> WordPool { get; set; }

        /// <summary>
        /// A list of Word Models representing each word used in the crozzle solution.
        /// </summary>
        public List<WordModel> WordList { get; set; }

        /// <summary>
        /// A list of validation errors detected during during parsing.
        /// </summary>
        public List<string> ValidationErrors { get; set; }

        /// <summary>
        /// The difficulty rating of the solution.
        /// </summary>
        public string Difficulty { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Configuration Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        public CrozzleModel()
        {
            this.ValidationErrors = new List<string>();
            this.WordPool = new List<string>();
            this.WordList = new List<WordModel>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Return a deep copy of the crozzle instance.
        /// </summary>
        /// <returns>A depp copy of the crozzle.</returns>
        public CrozzleModel DeepCopy()
        {
            CrozzleModel crozzleCopy = new CrozzleModel();

            crozzleCopy.Configuration = this.Configuration;
            crozzleCopy.WordPoolSize = this.WordPoolSize;
            crozzleCopy.Rows = this.Rows;
            crozzleCopy.Columns = this.Columns;
            crozzleCopy.HorizontalWords = this.HorizontalWords;
            crozzleCopy.VerticalWords = this.VerticalWords;
            crozzleCopy.WordPool = this.WordPool.ToList();
            crozzleCopy.WordList = this.WordList.ToList();
            crozzleCopy.ValidationErrors = this.ValidationErrors;
            crozzleCopy.Difficulty = this.Difficulty;

            return crozzleCopy;
        }

        #endregion
    }
}
