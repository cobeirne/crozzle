/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This class models the process for validating a crozzle solution.
    /// </summary>
    public class CrozzleValidationModel
    {
        #region Class Constants

        const int MinimumWordLength = 2;

        const string EasyDifficultyLiteral = "EASY";
        const int EasyHorizontalWordIntersectionsMin = 1;
        const int EasyHorizontalWordIntersectionsMax = 2;
        const int EasyVerticalWordIntersectionsMin = 1;
        const int EasyVerticalWordIntersectionsMax = 2;

        const string MediumDifficultyLiteral = "MEDIUM";
        const int MediumHorizontalWordIntersectionsMin = 1;
        const int MediumHorizontalWordIntersectionsMax = 3;
        const int MediumVerticalWordIntersectionsMin = 1;
        const int MediumVerticalWordIntersectionsMax = 3;

        const string HardDifficultyLiteral = "HARD";
        const int HardHorizontalWordIntersectionsMin = 1;
        const int HardHorizontalWordIntersectionsMax = -1;
        const int HardVerticalWordIntersectionsMin = 1;
        const int HardVerticalWordIntersectionsMax = -1;

        const string VerticalOrientationLiteral = "VERTICAL";
        const string HorizontalOrientationLiteral = "HORIZONTAL";
        
        #endregion

        #region Class Properties

        /// <summary>
        /// The Grid Model to be validated.
        /// </summary>
        public GridModel CrozzleGrid { get; set; }

        /// <summary>
        /// Indicates that the current crozzle is valid.
        /// </summary>
        public bool CrozzleIsValid { get; set; }

        /// <summary>
        /// A collection of horizontal words found in the grid.
        /// </summary>
        private List<string> HorizontalGridWords = new List<string>();

        /// <summary>
        /// A collection of vertical words found in the grid.
        /// </summary>
        private List<string> VerticalGridWords = new List<string>();

        /// <summary>
        /// A list of errors detected during during validation.
        /// </summary>
        public List<string> ValidationErrors { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Crozzle Validation Model constructor, is called on object creation and initialises 
        /// object properties.
        /// </summary>
        /// <param name="grid">Crozzle Grid to be validated.</param>
        public CrozzleValidationModel(GridModel grid)
        {
            this.ValidationErrors = new List<string>();
            this.CrozzleGrid = grid;
            ValidateCrozzle();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function uses a subset of functions to validate the crozzle solution loaded into
        /// the crozzle grid.  Crozzle constraints vary depending on the difficulty level of the
        /// crozzle solution.. The function will return TRUE if all constraints are satisfied.
        /// </summary>
        /// <returns>TRUE if all constraints are validated without error.</returns>
        public void ValidateCrozzle()
        {
            // Initialise variables.
            bool isValid = true;

            // Validate common contraints.
            isValid = ValidateHorizontalWords() ? isValid : false;
            isValid = ValidateVerticalWords() ? isValid : false;
            isValid = ValidateNoDuplicateWords() ? isValid : false;
            isValid = ValidateGroupLimit() ? isValid : false;

            // Validate by difficulty level.
            switch (this.CrozzleGrid.Crozzle.Difficulty)
            {
                case EasyDifficultyLiteral:
                    // Validate minimum word intersections.
                    isValid = ValidateWordIntersections(VerticalOrientationLiteral, 
                        EasyVerticalWordIntersectionsMin, EasyVerticalWordIntersectionsMax) 
                        ? isValid : false;

                    // Validate maximum word intersections.
                    isValid = ValidateWordIntersections(HorizontalOrientationLiteral, 
                        EasyHorizontalWordIntersectionsMin, EasyHorizontalWordIntersectionsMax) 
                        ? isValid : false;

                    // Validate word buffer.
                    isValid = ValidateWordBuffer() ? isValid : false;
                    break;

                case MediumDifficultyLiteral:
                    // Validate minimum word intersections.
                    isValid = ValidateWordIntersections(VerticalOrientationLiteral, 
                        MediumVerticalWordIntersectionsMin, MediumVerticalWordIntersectionsMax) 
                        ? isValid : false;

                    // Validate maximum word intersections.
                    isValid = isValid = ValidateWordIntersections(HorizontalOrientationLiteral, 
                        MediumHorizontalWordIntersectionsMin, 
                        MediumHorizontalWordIntersectionsMax) ? isValid : false;
                    break;

                case HardDifficultyLiteral:
                    // Validate minimum word intersections.
                    isValid = ValidateWordIntersections(VerticalOrientationLiteral, 
                        HardVerticalWordIntersectionsMin, HardVerticalWordIntersectionsMax) 
                        ? isValid : false;

                    // Validate maximum word intersections.
                    isValid = isValid = ValidateWordIntersections(HorizontalOrientationLiteral,
                        HardHorizontalWordIntersectionsMin, HardHorizontalWordIntersectionsMax) 
                        ? isValid : false;
                    break;
            }

            this.CrozzleIsValid = isValid;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets all the grid letters assocaited with the passed word.
        /// </summary>
        /// <param name="searchWord">Crozzle word of letters returned.</param>
        /// <returns>A list of Letter Model objects assocaited with the Search Word.</returns>
        private List<LetterModel> GetWordGridLetters(WordModel searchWord)
        {
            const int GridColumnOffset = -1;
            const int GridRowOffset = -1;

            // Initialise the letter collection.
            List<LetterModel> wordLetters = new List<LetterModel>();

            try
            {
                // Iterate through each word letter.
                for (int letterIndex = 0; letterIndex < searchWord.Word.Length; letterIndex++)
                {
                    int startRow = searchWord.StartRow + GridRowOffset;
                    int startColumn = searchWord.StartColumn + GridColumnOffset;

                    // Index by word orientation, and add word letters to collection.
                    if (searchWord.Orientation == VerticalOrientationLiteral)
                    {
                        wordLetters.Add(this.CrozzleGrid.Grid[startRow + letterIndex, startColumn]);
                    }
                    else
                    {
                        wordLetters.Add(this.CrozzleGrid.Grid[startRow, startColumn + letterIndex]);
                    }
                }
            }
            catch(Exception e)
            {                
                throw new Exception("Exception while getting word grid letters.", e);
            }

            return wordLetters;
        }

        /// <summary>
        /// Get the words assocaited with a grid letter.
        /// </summary>
        /// <param name="gridLetters">Grid letter to find assocaited words.</param>
        /// <returns>A list of words assocaited with the grid letter.</returns>
        private List<WordModel> GetLetterAssociatedWords(List<LetterModel> gridLetters)
        {
            // Initialie the word list.
            List<WordModel> associatedWords = new List<WordModel>();

            // Index through each letter and build a list of assocaited words.
            foreach (var letter in gridLetters)
            {
                if (letter != null)
                {
                    associatedWords.AddRange(letter.AssociatedWords);
                }
            }

            // Return a distinct list of associated words.
            return associatedWords.Distinct().ToList();
        }

        /// <summary>
        /// Validate that all horizontal words found in the crozzle grid are in the word pool.
        /// </summary>
        /// <returns>TRUE if all vertical words are in teh word pool.</returns>
        private bool ValidateHorizontalWords()
        {
            const int GridStartRow = 0;
            const int GridStartColumn = 0;
            const string EmptyLetterValue = " ";
            const char SplitDelimiter = ' ';

            // Initialise status.
            bool wordsAreValid = true;

            try
            {
                // For each grid row.
                for (int gridRow = GridStartRow; gridRow < this.CrozzleGrid.Crozzle.Rows; gridRow++)
                {
                    // Index through each row cell and build a string
                    string rowString = string.Empty;
                    for (int gridColumn = GridStartColumn; gridColumn < this.CrozzleGrid.Crozzle.Columns; gridColumn++)
                    {
                        if (this.CrozzleGrid.Grid[gridRow, gridColumn] != null)
                        {
                            rowString += this.CrozzleGrid.Grid[gridRow, gridColumn].Letter.ToString();
                        }
                        else
                        {
                            rowString += EmptyLetterValue;
                        }
                    }

                    // Split the string by space to find word candidates.
                    this.HorizontalGridWords.AddRange(rowString.Split(SplitDelimiter));
                }

                // Remove any words smaller than the minum word length fromt the colllection.
                this.HorizontalGridWords.RemoveAll(w => w.Length < MinimumWordLength);

                // Check that each word in the collection exists in the Crozzle word pool.
                foreach (var word in this.HorizontalGridWords)
                {
                    bool wordExistsInList = this.CrozzleGrid.Crozzle.WordPool.
                        Exists(w => w == word);

                    if (!wordExistsInList)
                    {
                        // Validation failed, update status and add validation error. 
                        wordsAreValid = false;
                        this.ValidationErrors.Add(string.
                            Format("Error: Crozzle Validation - Horizontal word {0} is not in the word pool.",
                            word));
                    }
                }
            }
            catch (Exception e)
            {
                wordsAreValid = false;
                throw new Exception("Exception while validating horizontal words.", e);
            }

            return wordsAreValid;
        }

        /// <summary>
        /// Validate that all vertical words found in the crozzle grid are in the word pool.
        /// </summary>
        /// <returns>TRUE if validation succeeded.</returns>
        private bool ValidateVerticalWords()
        {
            const int GridStartRow = 0;
            const int GridStartColumn = 0;
            const string EmptyLetterValue = " ";
            const char SplitDelimiter = ' ';

            // Initialise status.
            bool wordsAreValid = true;

            try
            {
                // For each grid row.
                for (int gridColumn = GridStartColumn; gridColumn < this.CrozzleGrid.Crozzle.Columns; gridColumn++)
                {
                    // Index through each row cell and build a string
                    string columnString = "";
                    for (int gridRow = GridStartRow; gridRow < this.CrozzleGrid.Crozzle.Rows; gridRow++)
                    {
                        if (this.CrozzleGrid.Grid[gridRow, gridColumn] != null)
                        {
                            columnString += this.CrozzleGrid.Grid[gridRow, gridColumn].Letter.ToString();
                        }
                        else
                        {
                            columnString += EmptyLetterValue;
                        }
                    }

                    // Split the string by space to find word candidates.
                    this.VerticalGridWords.AddRange(columnString.Split(SplitDelimiter));
                }

                // Remove any words smaller than the minum word length from the colllection.
                this.VerticalGridWords.RemoveAll(w => w.Length < MinimumWordLength);

                // Check that each word in the collection exists in the Crozzle word pool.
                foreach (var word in this.VerticalGridWords)
                {
                    bool wordExistsInList = this.CrozzleGrid.Crozzle.WordPool.
                        Exists(w => w == word);

                    if (!wordExistsInList)
                    {
                        // Validation failed, update status and add validation error. 
                        wordsAreValid = false;
                        this.ValidationErrors.Add(string.
                            Format("Error: Crozzle Validation - Vertical word {0} is not in the word pool.",
                            word));
                    }
                }
            }
            catch (Exception e)
            {
                wordsAreValid = false;
                throw new Exception("Exception while validation vertical words.", e);
            }

            return wordsAreValid;
        }

        /// <summary>
        /// Validate that no duplicate words have been found in the grid.
        /// </summary>
        /// <returns>TRUE if validation succeeded.</returns>
        private bool ValidateNoDuplicateWords()
        {
            const int MaxDuplicateWords = 0;
            const int MaxGroupWords = 1;

            // Initialise status;
            bool noDuplicates = true;

            try
            {
                // Merge horizontal and vertical collections.
                List<string> gridWords = new List<string>();
                gridWords.AddRange(this.HorizontalGridWords);
                gridWords.AddRange(this.VerticalGridWords);

                // Remove distinct words from the word list.
                var duplicateWords = gridWords.GroupBy(w => w).
                    Where(group => group.Count() > MaxGroupWords).ToList();
                ;
                // A duplicate will be present if the actual count != distinct count.
                if (duplicateWords.Count > MaxDuplicateWords)
                {
                    noDuplicates = false;
                    foreach (var word in duplicateWords)
                    {
                        // Validation failed, update status and add validation error. 
                        this.ValidationErrors.Add(string.
                            Format("Error: Crozzle Validation - Duplicate word {0} detected.",
                            word.Key));
                    }
                }
            }
            catch(Exception e)
            {
                noDuplicates = false;
                throw new Exception("Exception while validating duplicate words.", e);
            }

            return noDuplicates;
        }

        /// <summary>
        /// Validates that the number of word groups does not exceed the 
        /// configuration limit.
        /// </summary>
        /// <returns>TRUE if validation succeeded.</returns>
        private bool ValidateGroupLimit()
        {
            const int InitialGroupCount = 0;

            // Initialise variables.
            bool isWithinLimit = true;
            int groupCount = InitialGroupCount;

            try
            {
                // Create a full list of words to scan.
                Dictionary<WordModel, bool> fullList = new Dictionary<WordModel, bool>();
                foreach (WordModel word in this.CrozzleGrid.Crozzle.WordList)
                {
                    fullList.Add(word, false);
                }

                // Loop while there are unscanned words in the full list.
                while (fullList.ContainsValue(false))
                {
                    WordModel nextFullListWord = fullList.First(w => w.Value == false).Key;

                    Dictionary<WordModel, bool> groupWords = new Dictionary<WordModel, bool>();
                    groupWords.Add(nextFullListWord, false);

                    // Continue scanning until all group list words have been checked.  
                    while (groupWords.ContainsValue(false))
                    {
                        WordModel nextGroupWord = groupWords.First(w => w.Value == false).Key;

                        // Get the grid letters for the current word.
                        List<LetterModel> wordLetters = GetWordGridLetters(nextGroupWord);

                        // Check each grid letter for intersecting words.
                        List<WordModel> associatedWords = GetLetterAssociatedWords(wordLetters);
                        foreach (WordModel word in associatedWords)
                        {
                            // Add new words to the group list.
                            if (!groupWords.ContainsKey(word))
                            {
                                groupWords.Add(word, false);
                            }
                        }

                        // Flag the current group word as checked.
                        groupWords[nextGroupWord] = true;

                    }

                    // Increment the number of groups found.
                    groupCount++;

                    // Flag all words in the current group as checked in the full list.
                    foreach (var word in groupWords)
                    {
                        fullList[word.Key] = true;
                    }
                }

                // Check that the group limit has not been exceeded.
                if (groupCount > this.CrozzleGrid.Crozzle.Configuration.GroupsLimit)
                {
                    // Validation failed, update status and add validation error.
                    isWithinLimit = false;
                    this.ValidationErrors.Add(string.
                        Format("Error: Crozzle Validation - {0} Groups detected, maximum of {1} exceeded.",
                        groupCount, this.CrozzleGrid.Crozzle.Configuration.GroupsLimit));
                }
            }
            catch (Exception e)
            {
                isWithinLimit = false;
                throw new Exception("Exception thrown while while validating group limit.", e);
            }

            return isWithinLimit;
        }

        /// <summary>
        /// Validate that each word complies to the minumum and maximum number of intersecting 
        /// words constraint.
        /// </summary>
        /// <param name="orientation">Orientation of the word being validated.</param>
        /// <param name="minIntersects">Minimum number of intersecting words required.</param>
        /// <param name="maxIntersects">Maximum number of intersecting words allowed.</param>
        /// <returns>TRUE if validation succeeded.</returns>
        private bool ValidateWordIntersections(string orientation, int minIntersects, 
            int maxIntersects)
        {
            const int lowerIntersectLimit = 0;

            // Initialise status.
            bool intersectionsValid = true;

            try
            {
                List<WordModel> shortListWords = new List<WordModel>();
                try
                {
                    // Retrieve a shortlist of words.
                    shortListWords = this.CrozzleGrid.Crozzle.WordList.
                        Where(w => w.Orientation == orientation.ToUpper()).ToList();
                }
                catch (Exception e)
                {
                    throw new Exception("Exception while creating shortlist.", e);
                }

                // For each vertical word in the Crozzle.
                foreach (var testWord in shortListWords)
                {
                    int intersectionCount;

                    try
                    {
                        // Find the number of word letters with intersects.
                        List<LetterModel> wordLetters = GetWordGridLetters(testWord);
                        intersectionCount = wordLetters.Where(w => w.IsIntersecting).Count();
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Exception while creating word letters.", e);
                    }

                    // Check if the number of intersecting words is below the lower limit.
                    if ((minIntersects >= lowerIntersectLimit) && 
                        (intersectionCount < minIntersects))
                    {
                        // Validation failed, update status and add validation error.
                        intersectionsValid = false;
                        this.ValidationErrors.Add(string.
                            Format("Error: Crozzle Validation - Number of intersections with word {0} is less than the minimum of {1}.",
                            testWord.Word, minIntersects));
                    }

                    // Check if the number of intersecting words is above the higher limit.
                    if ((maxIntersects >= lowerIntersectLimit) && 
                        (intersectionCount > maxIntersects))
                    {
                        // Validation failed, update status and add validation error.
                        intersectionsValid = false;
                        this.ValidationErrors.Add(string.
                            Format("Error: Crozzle Validation - Number of intersections with word {0} is more than the maximum of {1}.",
                            testWord.Word, maxIntersects));
                    }
                }
            }
            catch (Exception e)
            {
                intersectionsValid = false;
                throw new Exception("Exception while validating word intersections.", e);
            }

            return intersectionsValid;
        }

        /// <summary>
        /// Validate that each word has a minimum buffer from adjacent words of the same 
        /// orientation.
        /// </summary>
        /// <returns>TRUE if validation succeeded.</returns>
        private bool ValidateWordBuffer()
        {
            const int BufferSize = 1;
            const int BufferLowerLimit = 0;
            const int BufferInitialEndRow = 0;
            const int BufferInitialEndColumn = 0;
            const int GridArrayOffset = 1;
            const int MaxWordsInBuffer = 1;

            // Initialise status.
            bool bufferOk = true;

            try
            {
                // Check each word in the word list
                foreach (WordModel currentWord in this.CrozzleGrid.Crozzle.WordList)
                {
                    // Set buffer bounds by word orientation.
                    int bufferStartRow = currentWord.StartRow - BufferSize - GridArrayOffset;
                    int bufferStartColumn = currentWord.StartColumn - BufferSize - GridArrayOffset;
                    int bufferEndRow = BufferInitialEndRow;
                    int bufferEndColumn = BufferInitialEndColumn;

                    if (currentWord.Orientation == VerticalOrientationLiteral)
                    {
                        bufferEndRow = (currentWord.StartRow - GridArrayOffset) + 
                            currentWord.Word.Length;
                        bufferEndColumn = (currentWord.StartColumn - GridArrayOffset) + BufferSize;
                    }
                    else
                    {
                        bufferEndRow = currentWord.StartRow + BufferSize - GridArrayOffset;
                        bufferEndColumn = (currentWord.StartColumn - GridArrayOffset) + 
                            currentWord.Word.Length;
                    }

                    // Limit the buffer to within grid limits.
                    if (bufferStartRow < BufferLowerLimit)
                    {
                        bufferStartRow = BufferLowerLimit;
                    }

                    if (bufferStartColumn < BufferLowerLimit)
                    {
                        bufferStartColumn = BufferLowerLimit;
                    }

                    if (bufferEndRow >= this.CrozzleGrid.Crozzle.Rows)
                    {
                        bufferEndRow = this.CrozzleGrid.Crozzle.Rows - GridArrayOffset;
                    }

                    if (bufferEndColumn >= this.CrozzleGrid.Crozzle.Columns)
                    {
                        bufferEndColumn = this.CrozzleGrid.Crozzle.Columns - GridArrayOffset;
                    }

                    // Get a collection of letters within the buffer bounds.
                    List<LetterModel> subGridLetters = new List<LetterModel>();
                    for (int rowIndex = bufferStartRow; rowIndex <= bufferEndRow; rowIndex++)
                    {
                        for (int columnIndex = bufferStartColumn; columnIndex <= 
                            bufferEndColumn; columnIndex++)
                        {
                            if (this.CrozzleGrid.Grid[rowIndex, columnIndex] != null)
                            {
                                subGridLetters.Add(this.CrozzleGrid.Grid[rowIndex, columnIndex]);
                            }
                        }
                    }

                    // Get a collection of words from the sub grid sharing the same orientation.
                    List<WordModel> subGridWords = GetLetterAssociatedWords(subGridLetters);
                    subGridWords = 
                        subGridWords.Where(w => w.Orientation == currentWord.Orientation).ToList();

                    // Validate only one word of the same orientation exists in the sub grid.
                    if (subGridWords.Count > MaxWordsInBuffer)
                    {
                        bufferOk = false;

                        // Remove the current word from the sub grid, add errors for all remaining.
                        subGridWords.Remove(currentWord);
                        foreach (WordModel word in subGridWords)
                        {
                            // Validation failed, update status and add validation error.
                            this.ValidationErrors.Add(string.
                                Format("Error: Crozzle Validation - There is insufficent space bewteen word {0} and adjacent {1} word {2}.",
                                currentWord.Word, currentWord.Orientation, word.Word));
                        }
                    }
                }
            }
            catch(Exception e)
            {
                bufferOk = false;
                throw new Exception("Exception while validating word buffer.", e);
            }

            return bufferOk;
        }

        #endregion
    }
}
