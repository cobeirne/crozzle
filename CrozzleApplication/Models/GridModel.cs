/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System.Text;
using CrozzleGame.Models;
using System.Collections.Generic;
using System.Linq;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This model represents a crozzle grid. It includes grid building and validation 
    /// functions.
    /// </summary>
    public class GridModel
    {
        #region Class Properties

        /// <summary>
        /// A multidimensional array that represents the crozzle grid.
        /// </summary>
        public LetterModel[,] Grid { get; set; }

        /// <summary>
        /// The crozzle model object associated with the grid.
        /// </summary>
        public CrozzleModel Crozzle { get; set; }

        /// <summary>
        /// The number of rows specified for the grid.
        /// </summary>
        public int GridRows { get; set; }

        /// <summary>
        /// The number of columns specified for the grid.
        /// </summary>
        public int GridColumns { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Grid Model constructor, initialises the object with null properties.
        /// </summary>
        public GridModel()
        { }

        /// <summary>
        /// Grid Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="rows">The number of grid rows.</param>
        /// <param name="columns">The number of grid columns.</param>
        public GridModel(int rows, int columns)
        {
            this.Grid = new LetterModel[rows, columns];
            this.GridRows = rows;
            this.GridColumns = columns;
        }

        /// <summary>
        /// Grid Model constructor, is called on object creation to initialises object properties 
        /// and insert the crozzle solution into the grid.
        /// </summary>
        /// <param name="Crozzle">Crozzle model object associated with the grid.</param>
        public GridModel(CrozzleModel Crozzle)
        {
            this.Crozzle = Crozzle;
            this.Grid = new LetterModel[Crozzle.Rows,Crozzle.Columns];
            this.GridRows = Crozzle.Rows;
            this.GridColumns = Crozzle.Columns;

            // Load the crozzle into the grid.
            InsertCrozzleWords();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method inserts a sub solution into the current grid.
        /// </summary>
        /// <param name="subSolution">The sub solution to be inserted.</param>
        /// <returns>TRUE if the solution was inserted.</returns>
        public bool InsertSubSolution(SubSolutionModel subSolution)
        {
            bool solutionInserted = true;

            // Offset each sub solution word by the current starting point.
            List<WordModel> offsetWords = new List<WordModel>();
            foreach (WordModel word in subSolution.GroupWords)
            {
                offsetWords.Add(new WordModel(word.Orientation, word.StartRow + subSolution.ArrayRowffset,
                    word.StartColumn + subSolution.ArrayColumnOffset, word.Word));
            }

            // Attempt to insert each offset words into the target grid.
            foreach (WordModel word in offsetWords)
            {
                // Only attemp to insert if a pvrevious word has not failed.
                if (solutionInserted)
                {
                    solutionInserted = InsertWord(word) ? true : false;
                }
            }

            // Only update the crozzle word list if the word was successfully entered.
            if (solutionInserted)
            {
                this.Crozzle.WordList.AddRange(offsetWords);
            }
            
            return solutionInserted;
        }


        /// <summary>
        /// This function inserts a single word into the crozzle grid. Intersecting letters are
        /// flagged as each word is added to the grid.
        /// </summary>
        /// <param name="gridWord">The word to be inserted.</param>
        public bool InsertWord(WordModel gridWord)
        {
            const int GridArrayRowIndexOffset = -1;
            const int GridArrayColumnIndexOffset = -1;
            const int RowStartIndex = 0;
            const int ColumnStartIndex = 0;
            const string OrientationVerticalLiteral = "VERTICAL";

            bool wordInserted = true;

            // Calculate column boundary.
            int lastColumnIndex;
            if(gridWord.Orientation == OrientationVerticalLiteral)
            {
                lastColumnIndex = gridWord.StartColumn + GridArrayColumnIndexOffset;
            }
            else
            {
                lastColumnIndex = (gridWord.StartColumn + GridArrayColumnIndexOffset) + 
                    (gridWord.Word.Length + GridArrayColumnIndexOffset);
            }

            // Calculate row boundary.
            int lastRowIndex;
            if (gridWord.Orientation == OrientationVerticalLiteral)
            {
                lastRowIndex = (gridWord.StartRow + GridArrayRowIndexOffset) + 
                    (gridWord.Word.Length + GridArrayColumnIndexOffset);
            }
            else
            {
                lastRowIndex = gridWord.StartRow + GridArrayRowIndexOffset;
            }

            // Check the the word fits within the grid.
            if ((lastColumnIndex < this.GridColumns) && (lastRowIndex < this.GridRows))
            {
                // Index through each word letter.         
                for (int characterIndex = 0; characterIndex < gridWord.Word.Length; characterIndex++)
                {
                    // Initialise the word starting position and indexing direction.
                    int rowIndex = RowStartIndex;
                    int columnIndex = ColumnStartIndex;

                    // Index through the word in the correct direction. 
                    if (gridWord.Orientation.ToUpper() == OrientationVerticalLiteral)
                    {
                        // Index top to bottom.
                        rowIndex = gridWord.StartRow + characterIndex + GridArrayRowIndexOffset;
                        columnIndex = gridWord.StartColumn + GridArrayColumnIndexOffset;
                    }
                    else
                    {
                        // Index left to right.
                        rowIndex = gridWord.StartRow + GridArrayRowIndexOffset;
                        columnIndex = gridWord.StartColumn + characterIndex
                            + GridArrayColumnIndexOffset;
                    }

                    // Get new character.
                    char newCharacter = gridWord.Word[characterIndex];

                    // Get the existing grid letter.
                    LetterModel existingLetter = this.Grid[rowIndex, columnIndex];
                                        
                    if (existingLetter == null)
                    {
                        // Insert a new letter if the current position is empty.
                        LetterModel newLetter = new LetterModel(newCharacter, false, gridWord);
                        this.Grid[rowIndex, columnIndex] = newLetter;
                    }
                    else if (existingLetter.Letter != newCharacter)
                    {
                        // Dont overwrite an existing letter.
                        wordInserted = false;
                    }
                    else if (gridWord.Orientation != existingLetter.AssociatedWords.First().Orientation)
                    {
                        // Intersection found, mark as intersecting and add new word to associated 
                        // words.
                        existingLetter.IsIntersecting = true;
                        existingLetter.AssociatedWords.Add(gridWord);
                    }
                }
            }
            else
            {
                wordInserted = false;
            }

            return wordInserted;
        }


        /// <summary>
        /// This function builds a html table that represents the current grid.
        /// </summary>
        /// <returns>A HTML table in string format.</returns>
        public string GetGridHtml()
        {
            const string EmptyCellValue = "&nbsp";
            const int StartRow = 0;
            const int StartColumn = 0;

            StringBuilder htmlBuilder = new StringBuilder();

            // Create and style the HTML table header.
            htmlBuilder.Append("<html><body><table style='font-size:x-small;font-family=sans-serif;border-collapse:collapse'>");

            // Iterate through each row.
            for (int gridRow = StartRow; gridRow < this.Crozzle.Rows; gridRow++)
            {
                // Create a new HTML table row.
                htmlBuilder.Append("<tr height='35px' style='border:1px solid black;'>");

                // Iterate through each column.
                for (int gridColumn = StartColumn; gridColumn < this.Crozzle.Columns; gridColumn++)
                {
                    string gridLetter = string.Empty;

                    // Load the next grid letter.
                    if (this.Grid[gridRow, gridColumn] != null)
                    {
                        // There is a valid letter at the current grid position.
                        gridLetter = this.Grid[gridRow, gridColumn].Letter.ToString();
                    }
                    else
                    {
                        // There is no letter at the current grid position.
                        gridLetter = EmptyCellValue;
                    }

                    // Append a HTML table column for each letter.
                    htmlBuilder.AppendFormat("<td width='35px' style='border:1px solid black;text-align:center;font-weight:bold;'>{0}</td>", gridLetter);
                }

                // Close the current HTML table row.
                htmlBuilder.Append("</tr>");
            }

            // Close the current HTML table.
            htmlBuilder.Append("</table></body></html>");

            // Return the html formated text as a string.
            return htmlBuilder.ToString();
        }

        /// <summary>
        /// This function returns a deep copy of the current grid object.
        /// </summary>
        /// <returns>A deep copy of the current grid.</returns>
        public GridModel DeepCopy()
        {
            GridModel gridCopy = new GridModel();

            // Copy the basic grid properties.
            gridCopy.Crozzle = this.Crozzle.DeepCopy();
            gridCopy.GridColumns = this.GridColumns;
            gridCopy.GridRows = this.GridRows;

            // Iterate through the currnt grid, create a new grid referencing simple types.
            LetterModel[,] newGrid = new LetterModel[this.GridRows, this.GridColumns];

            for(int rowIndex = 0; rowIndex < this.GridRows; rowIndex++)
            {
                for (int columnIndex = 0; columnIndex < this.GridColumns; columnIndex++)
                {
                    LetterModel currentLetter = this.Grid[rowIndex, columnIndex];

                    // Check for a letter at the current grid position.
                    if (currentLetter != null)
                    {
                        // Create a new letter.
                        LetterModel newLetter = new LetterModel
                        {
                            AssociatedWords = currentLetter.AssociatedWords.ToList(),
                            IsIntersecting = currentLetter.IsIntersecting,
                            Letter = currentLetter.Letter,
                            Score = currentLetter.Score
                        };

                        // Insert the new letter into the new grid.
                        newGrid[rowIndex, columnIndex] = newLetter;
                    }
                }
            }

            // Update the return grid.
            gridCopy.Grid = newGrid;

            return gridCopy;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This function inserts each word from the current Crozzle into the crozzle grid.
        /// </summary>
        private void InsertCrozzleWords()
        {
            foreach(WordModel word in this.Crozzle.WordList)
            {
                InsertWord(word);
            }
        }

        #endregion
    }
}
