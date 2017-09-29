/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This class models the process for parsing and validating properties from a Configuration 
    /// file.
    /// </summary>
    public class CrozzleParserModel
    {
        #region Class Constants
        
        const int HeaderLineIndex = 0;
        const int WordPoolLineIndex = 1;
        const int FirstWordLineIndex = 2;
        const int WordPoolRangeMin = 10;
        const int WordPoolRangeMax = 1000;
        const int GridRowRangeMin = 4;
        const int GridRowRangeMax = 400;
        const int GridColumnRangeMin = 8;
        const int GridColumnRangeMax = 800;
        const int HorizontalWordsMin = 0;
        const int HorizontalWordsMax = 1000;
        const int VerticalWordsMin = 0;
        const int VerticalWordsMax = 1000;
        const int WordStartRowMin = 0;
        const int WordStartColumnMin = 0;

        #endregion

        #region Class Properties

        /// <summary>
        /// A list of validation errors detected during parsing.
        /// </summary>
        public List<string> ValidationErrors { get; set; }

        /// <summary>
        /// The Crozzle File text lines to be parsed.
        /// </summary>
        public string[] TextLines { get; set; }

        /// <summary>
        /// The Crozzle Model created by the Try Parse function.
        /// </summary>
        public CrozzleModel Crozzle { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Crozzle Parser constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="fileTextLines">The Crozzle File lines to be parsed.</param>
        public CrozzleParserModel(string[] fileTextLines)
        {
            this.ValidationErrors = new List<string>();
            this.TextLines = fileTextLines;
            this.Crozzle = new CrozzleModel();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function uses a subset of functions to parse the Crozzle Model properties 
        /// from the associated file content. Inputs are check for format, range and validity as 
        /// applicable before their respective property is updated. The function will return TRUE
        /// if all properties are parsed successfully.
        /// </summary>
        /// <returns>TRUE if all Crozzle File Properties are parsed without error.</returns>
        public bool TryParseCrozzle(bool isSolved)
        {
            // Initialise variables.
            bool parseOk = true;
            
            // Get header text.
            string headerText = this.TextLines[HeaderLineIndex];

            // Parse header properties.
            parseOk = TryParseDifficulty(headerText) ? parseOk : false;
            parseOk = TryParseWordPoolSize(headerText) ? parseOk : false;
            parseOk = TryParseGridRows(headerText) ? parseOk : false;
            parseOk = TryParseGridColumns(headerText) ? parseOk : false;
            parseOk = TryParseHorizontalWords(headerText) ? parseOk : false;
            parseOk = TryParseVerticalWords(headerText) ? parseOk : false;

            // Get word pool text.
            string wordPoolText = this.TextLines[WordPoolLineIndex];

            // Parse word pool.
            parseOk = TryParseWordPool(wordPoolText) ? parseOk : false;

            // If the file contains a solution, try parse remaining file lines as crozzle words.
            if (isSolved)
            {
                for (int i = FirstWordLineIndex; i < this.TextLines.Length; i++)
                {
                    bool wordParseOk = true;
                    WordModel newCrozzleWord = new WordModel();

                    // Parse crozzle word properties.
                    wordParseOk = TryParseWordOrientation(this.TextLines[i], ref newCrozzleWord)
                        ? wordParseOk : false;
                    wordParseOk = TryParseWordStartRow(this.TextLines[i], ref newCrozzleWord)
                        ? wordParseOk : false;
                    wordParseOk = TryParseWordStartColumn(this.TextLines[i], ref newCrozzleWord)
                        ? wordParseOk : false;
                    wordParseOk = TryParseWord(this.TextLines[i], ref newCrozzleWord)
                        ? wordParseOk : false;

                    // If parse OK, add the new word to the word list.
                    if (wordParseOk)
                    {
                        this.Crozzle.WordList.Add(newCrozzleWord);
                    }
                }
            }

            return parseOk;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// <summary>
        /// This function attempts to parse the Difficulty value from the expected Crozzle File 
        /// text line. It returns true if the value matches the expected format. An error is added
        /// to the Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseDifficulty(string headerText)
        {
            const int ValueSplitIndex = 0;
            const char SplitCharacter = ',';
            const string RegexPattern = @"^(EASY|MEDIUM|HARD)$";

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the text line.
            string parseValue = SplitValue(headerText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Header '{0}' difficulty invalid.", parseValue));
            }
            else
            {                    
                // Parse OK, update property.
                this.Crozzle.Difficulty = parseValue.ToUpper();
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Word Pool Size value from the expected Crozzle File 
        /// text line. It returns true if the value matches the expected format and range. An error
        /// is added to the Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordPoolSize(string headerText)
        {
            const int ValueSplitIndex = 1;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(headerText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Header '{0}' Word Pool Size invalid format.", 
                    parseValue));
            }
            else
            {
                // Test value range.
                int wordPoolSize = Convert.ToInt32(parseValue);
                if ((wordPoolSize < WordPoolRangeMin) || (wordPoolSize > WordPoolRangeMax))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Header Word Pool Size '{0}' out of range ({1} to {2}).", 
                        wordPoolSize, WordPoolRangeMin, WordPoolRangeMax));

                    // Parse not OK, but update property for pool count range check.
                    this.Crozzle.WordPoolSize = wordPoolSize;
                }
                else
                {
                    // Parse OK, update property.
                    this.Crozzle.WordPoolSize = wordPoolSize;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Grid Rows value from the expected Crozzle File 
        /// text line. It returns true if the value matches the expected format and range. An error
        /// is added to the Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseGridRows(string headerText)
        {
            const int ValueSplitIndex = 2;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(headerText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Header '{0}' Rows invalid format.", parseValue));
            }
            else
            {
                // Test value range.
                int gridRows = Convert.ToInt32(parseValue);
                if ((gridRows < GridRowRangeMin) || (gridRows > GridRowRangeMax))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Header Rows '{0}' out of range ({1} to {2}).",
                        gridRows, GridRowRangeMin, GridRowRangeMax));

                    // Parse not OK, but update property for word range check.
                    this.Crozzle.Rows = gridRows;
                }
                else
                {
                    // Parse OK, update property.
                    this.Crozzle.Rows = gridRows;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Grid Columns value from the expected Crozzle File 
        /// text line. It returns true if the value matches the expected format and range. An error
        /// is added to the Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseGridColumns(string headerText)
        {
            const int ValueSplitIndex = 3;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(headerText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Header '{0}' Columns invalid format.", parseValue));
            }
            else
            {
                // Test value range.
                int gridColumns = Convert.ToInt32(parseValue);
                if ((gridColumns < GridColumnRangeMin) || (gridColumns > GridColumnRangeMax))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Header Columns '{0}' out of range ({1} to {2}).",
                        gridColumns, GridColumnRangeMin, GridColumnRangeMax));

                    // Parse not OK, but update property for word range check.
                    this.Crozzle.Columns = gridColumns;
                }
                else
                {
                    // Parse OK, update property.
                    this.Crozzle.Columns = gridColumns;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Horizontal Words value from the expected Crozzle 
        /// File text line. It returns true if the value matches the expected format and range. An 
        /// error is added to the Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseHorizontalWords(string headerText)
        {
            const int ValueSplitIndex = 4;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(headerText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Header '{0}' Horizontal Words invalid format.", 
                    parseValue));
            }
            else
            {
                // Test value range.
                int horizontalWords = Convert.ToInt32(parseValue);
                if ((horizontalWords < HorizontalWordsMin) || (horizontalWords > HorizontalWordsMax))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Header Horizontal Words '{0}' out of range ({1} to {2}).",
                        horizontalWords, HorizontalWordsMin, HorizontalWordsMax));
                }
                else
                {
                    // Parse OK, update property.
                    this.Crozzle.HorizontalWords = horizontalWords;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Vertical Words value from the expected Crozzle 
        /// File text line. It returns true if the value matches the expected format and range. An 
        /// error is added to the Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseVerticalWords(string headerText)
        {
            const int ValueSplitIndex = 5;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(headerText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Header '{0}' Vertical Words invalid format.",
                    parseValue));
            }
            else
            {
                // Test value range.
                int VerticalWords = Convert.ToInt32(parseValue);
                if ((VerticalWords < VerticalWordsMin) || (VerticalWords > VerticalWordsMax))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Header Vertical Words '{0}' out of range ({1} to {2}).",
                        VerticalWords, VerticalWordsMin, VerticalWordsMax));
                }
                else
                {
                    // Parse OK, update property.
                    this.Crozzle.VerticalWords = VerticalWords;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Word Pool values from the expected Crozzle File 
        /// text line. It returns true if the values matche the expected format and the number of 
        /// words found matches the number specified in the header. An error is added to the 
        /// Validation Errors collection.
        /// </summary>
        /// <param name="headerText">The file header line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordPool(string headerText)
        {
            const string RegexPattern = @"^([A-Z])+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string[] parseValues = headerText.Split(SplitCharacter);

            foreach (string parseValue in parseValues)
            {
                // Match the parse value with a regular expression.
                Match match = MatchValue(RegexPattern, parseValue);

                if (!match.Success)
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.Add(string.
                        Format("Error: Crozzle File - Word Pool word '{0}' invalid format.",
                        parseValue));
                }
                else
                {
                    // Parse OK, update property.
                    this.Crozzle.WordPool.Add(parseValue);
                }
            }

            // Check for missing words.
            if(this.Crozzle.WordPool.Count != this.Crozzle.WordPoolSize)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Word Pool count '{0}' does not match header specification {1}.",
                    this.Crozzle.WordPool.Count, this.Crozzle.WordPoolSize));
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Word Orientation from the text line passed. It 
        /// returns true if the value matches the expected format. An error is added to the 
        /// Validation Errors collection.
        /// </summary>
        /// <param name="wordText">The text line of the crozzle word.</param>
        /// <param name="crozzleWord">A Word Model object reference that is updated with the parsed
        /// Orientation value.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordOrientation(string wordText, ref WordModel crozzleWord)
        {
            const int ValueSplitIndex = 0;
            const string RegexPattern = @"^(HORIZONTAL|VERTICAL)$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(wordText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Word Orientation '{0}' invalid format in text '{1}'.",
                    parseValue, wordText));
            }
            else
            {
                // Parse OK, update property.
                crozzleWord.Orientation = parseValue;
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Word Start Row from the text line passed. It 
        /// returns true if the value matches the expected format and range. An error is added to 
        /// the Validation Errors collection.
        /// </summary>
        /// <param name="wordText">The text line of the crozzle word.</param>
        /// <param name="crozzleWord">A Word Model object reference that is updated with the parsed
        /// Start Row value.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordStartRow(string wordText, ref WordModel crozzleWord)
        {
            const int ValueSplitIndex = 1;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(wordText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Word Row '{0}' invalid format in text '{1}'.",
                    parseValue, wordText));
            }
            else
            {
                // Test value range.
                int startRow = Convert.ToInt32(parseValue);
                if ((startRow < WordStartRowMin) || (startRow > this.Crozzle.Rows))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Word Row '{0}' out of range ({1} to {2}) in text '{3}'.",
                        startRow, WordStartRowMin, this.Crozzle.Rows, wordText));
                }
                else
                {
                    // Parse OK, update property.
                    crozzleWord.StartRow = startRow;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Word Start Column from the text line passed. It 
        /// returns true if the value matches the expected format and range. An error is added to 
        /// the Validation Errors collection.
        /// </summary>
        /// <param name="wordText">The text line of the crozzle word.</param>
        /// <param name="crozzleWord">A Word Model object reference that is updated with the parsed
        /// Start Column value.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordStartColumn(string wordText, ref WordModel crozzleWord)
        {
            const int ValueSplitIndex = 2;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(wordText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Word Column '{0}' invalid format in text '{1}'.",
                    parseValue, wordText));
            }
            else
            {
                // Test value range.
                int startColumn = Convert.ToInt32(parseValue);
                if ((startColumn < WordStartColumnMin) || (startColumn > this.Crozzle.Columns))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Word Column '{0}' out of range ({1} to {2}) in text '{3}'.",
                        startColumn, WordStartColumnMin, this.Crozzle.Columns, wordText));
                }
                else
                {
                    // Parse OK, update property.
                    crozzleWord.StartColumn = startColumn;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Word from the text line passed. It returns true if 
        /// the value matches the expected format and range. An error is added to the Validation 
        /// Errors collection.
        /// </summary>
        /// <param name="wordText">The text line of the crozzle word.</param>
        /// <param name="crozzleWord">A Word Model object reference that is updated with the parsed
        /// Word value.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWord(string wordText, ref WordModel crozzleWord)
        {
            const int ValueSplitIndex = 3;
            const string RegexPattern = @"^([A-Z])+$";
            const char SplitCharacter = ',';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the header.
            string parseValue = SplitValue(wordText, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Crozzle File - Word '{0}' invalid format in text '{1}'.",
                    parseValue, wordText));
            }
            else
            {
                // Test value range.
                if (!this.Crozzle.WordPool.Contains(parseValue))
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Crozzle File - Word '{0}' from text '{1}', not in word pool.",
                        parseValue, wordText));
                }
                else
                {
                    // Parse OK, update property.
                    crozzleWord.Word = parseValue;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function splits the text passed using the split character delimiter, then returns
        /// the value at the specified split array index.
        /// </summary>
        /// <param name="valueTextLine">The text containing the value to be split.</param>
        /// <param name="splitCharacter">The delimiter character used to split the text.</param>
        /// <param name="valueSplitIndex">The split array index of the required value.</param>
        /// <returns>The value split from the text passed.</returns>
        private string SplitValue(string valueTextLine, char splitCharacter, int valueSplitIndex)
        {
            // Split the string by delimiter and load each part into an array.
            string[] headerSplit = valueTextLine.Split(splitCharacter);

            // If the array size is adequate, return the split value.
            string splitValue = headerSplit.Length > valueSplitIndex ? headerSplit[valueSplitIndex] : " ";

            return splitValue;
        }

        /// <summary>
        /// This function uses a regular expression to match the text and pattern passed. The 
        /// function returns TRUE if the text matches the pattern.
        /// </summary>
        /// <param name="regexPattern">The Regular Expression pattern.</param>
        /// <param name="testValue">The text value to be tested.</param>
        /// <returns>TRUE of the test value matches the regular expression pattern.</returns>
        private Match MatchValue(string regexPattern, string testValue)
        {
            // Match the regular expression.
            Match regexMatch = Regex.Match(testValue, regexPattern, RegexOptions.IgnoreCase);

            return regexMatch;
        }

        #endregion
    }
}
