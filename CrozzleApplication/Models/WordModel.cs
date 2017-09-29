/// <summary>
/// Project:    SIT323 SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

namespace CrozzleGame.Models
{
    public class WordModel
    {
        #region Class Properties

        /// <summary>
        /// Inicates the word orientation in the grid.
        /// </summary>
        public string Orientation { get; set; }

        /// <summary>
        /// The grid starting row of the word.
        /// </summary>
        public int StartRow { get; set; }

        /// <summary>
        /// The grid starting column of the word.
        /// </summary>
        public int StartColumn { get; set; }

        /// <summary>
        /// The crozzle word value.
        /// </summary>
        public string Word { get; set; }

        #endregion

        #region Class Construtors

        /// <summary>
        /// Word Model default constructor, allows object creation without property initialisation.
        /// </summary>
        public WordModel()
        { }

        /// <summary>
        /// Word Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="orientation">Word orientation in the grid</param>
        /// <param name="startRow">Grid starting row of the word</param>
        /// <param name="startColumn">Grid starting column of the word</param>
        /// <param name="word">Crozzle word value</param>
        public WordModel(string orientation, int startRow, int startColumn, string word)
        {
            this.Orientation = orientation;
            this.StartRow = startRow;
            this.StartColumn = startColumn;
            this.Word = word;
        }

        #endregion
    }
}
