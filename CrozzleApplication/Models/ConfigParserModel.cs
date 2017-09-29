/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This class models the process for parsing and validating properties from a Crozzle file.
    /// </summary>
    public class ConfigParserModel
    {
        #region Class Constants

        const int GroupsLimitLineIndex = 0;
        const int WordPointsLineIndex = 1;
        const int FirstLetterLineIndex = 2;
        const int GroupLimitRangeMin = 0;
        const int PointsPerWordMin = 0;
        const int PointsPerLetterMin = 0;

        #endregion

        #region Class Properties

        /// <summary>
        /// A list of validation errors detected during parsing.
        /// </summary>
        public List<string> ValidationErrors { get; set; }

        /// <summary>
        /// The Configuration File text lines to be parsed.
        /// </summary>
        public string[] TextLines { get; set; }

        /// <summary>
        /// The Configuration Model created by the Try Parse function.
        /// </summary>
        public ConfigModel Configuration { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Configuration Parser constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="fileTextLines">The Configuration File lines to be parsed.</param>
        public ConfigParserModel(string[] fileTextLines)
        {
            this.ValidationErrors = new List<string>();
            this.TextLines = fileTextLines;
            this.Configuration = new ConfigModel();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function uses a subset of functions to parse the Configuration Model properties 
        /// from the associated file content. Inputs are check for format, range and validity as 
        /// applicable before their respective property is updated. The function will return TRUE
        /// if all properties are parsed successfully.
        /// </summary>
        /// <returns>TRUE if all Configuration File Properties are parsed without error.</returns>
        public bool TryParseConfiguration()
        {
            // Initialise variables.
            bool parseOk = true;

            // Get groups limit.
            string groupsLimitText = this.TextLines[GroupsLimitLineIndex];

            // Parse group limit properties.
            parseOk = TryParseGroupsLimitIdentifier(groupsLimitText) ? parseOk : false;
            parseOk = TryParseGroupsLimit(groupsLimitText) ? parseOk : false;

            // Get word points text.
            string wordPointsText = this.TextLines[WordPointsLineIndex];

            // Parse word points.
            parseOk = TryParseWordPointsIdentifier(wordPointsText) ? parseOk : false;
            parseOk = TryParseWordPoints(wordPointsText) ? parseOk : false;

            // Try parse remaining file lines as configuration letters.
            for (int i = FirstLetterLineIndex; i < this.TextLines.Length; i++)
            {
                bool letterParseOk = true;
                LetterModel newletter = new LetterModel();

                // Parse configuration letter properties.
                letterParseOk = TryParseLetterIdentifier(this.TextLines[i], ref newletter)
                    ? letterParseOk : false;
                letterParseOk = TryParseLetterPoints(this.TextLines[i], ref newletter)
                    ? letterParseOk : false;

                // If parse OK, add the new word to the respective word list.
                if (letterParseOk && newletter.IsIntersecting)
                {
                    this.Configuration.IntersectingPoints.Add(newletter.Letter.ToString(), 
                        newletter.Score);
                }

                if (letterParseOk && !newletter.IsIntersecting)
                {
                    this.Configuration.NonIntersectingPoints.Add(newletter.Letter.ToString(), 
                        newletter.Score);
                }                
            }

            // Check the letter collections for missing letters.
            parseOk = ValidateLetterCollection(this.Configuration.IntersectingPoints, true) 
                ? parseOk: false;
            parseOk = ValidateLetterCollection(this.Configuration.NonIntersectingPoints, false)
                ? parseOk : false;

            return parseOk;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This function attempts to parse the Groups Per Crozzle Limit identifier from the 
        /// expected Configuration File text line. It returns true if the identifier matches the
        /// expected format. An error is added to the Validation Errors collection.
        /// </summary>
        /// <param name="fileTextLine">The file text line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseGroupsLimitIdentifier(string fileTextLine)
        {
            const int IdentifierSplitIndex = 0;
            const char SplitCharacter = '=';
            const string RegexPattern = @"^GROUPSPERCROZZLELIMIT$";

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the text line.
            string parseValue = SplitValue(fileTextLine, SplitCharacter, IdentifierSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);
                        
            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Configuration File - Identifier '{0}' invalid format in text '{1}'.",
                    parseValue, fileTextLine));
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Groups Per Crozzle Limit from the expected 
        /// Configuration File text line. It returns true if the value matches the expected
        /// format and is within valid range. An error is added to the Validation Errors 
        /// collection.
        /// </summary>
        /// <param name="fileTextLine">The file text line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseGroupsLimit(string fileTextLine)
        {
            const int ValueSplitIndex = 1;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = '=';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the text line.
            string parseValue = SplitValue(fileTextLine, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Configuration File - Group Limit '{0}' invalid format in text '{1}'.",
                    parseValue, fileTextLine));
            }
            else
            {
                // Test parse value range.
                int groupLimit = Convert.ToInt32(parseValue);
                if (groupLimit < GroupLimitRangeMin)
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Configuration File - Group Limit '{0}' out of range ({1} minimum) in text '{3}'.",
                        groupLimit, GroupLimitRangeMin));
                }
                else
                {
                    // Parse OK, update property.
                    Configuration.GroupsLimit = groupLimit;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Points Per Word identifier from the 
        /// expected Configuration File text line. It returns true if the identifier matches the
        /// expected format.  An error is added to the Validation Errors collection.
        /// </summary>
        /// <param name="fileTextLine">The file text line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordPointsIdentifier(string fileTextLine)
        {
            const int IdentifierSplitIndex = 0;
            const char SplitCharacter = '=';
            const string RegexPattern = @"^POINTSPERWORD$";

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the text line.
            string parseValue = SplitValue(fileTextLine, SplitCharacter, IdentifierSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Configuration File - Identifier '{0}' invalid format in text '{1}'.",
                    parseValue, fileTextLine));
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Points Per Word value from the expected 
        /// Configuration File text line. It returns true if the value matches the expected
        /// format and is within valid range. An error is added to the Validation Errors 
        /// collection.
        /// </summary>
        /// <param name="fileTextLine">The file text line to be parsed.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseWordPoints(string fileTextLine)
        {
            const int ValueSplitIndex = 1;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = '=';

            // Initialise the parse result.
            bool parseOk = true;

            // Split the parse value from the text line.
            string parseValue = SplitValue(fileTextLine, SplitCharacter, ValueSplitIndex);

            // Match the parse value with a regular expression.
            Match match = MatchValue(RegexPattern, parseValue);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Configuration File - Points Per Word '{0}' invalid format in text '{1}'.",
                    parseValue, fileTextLine));
            }
            else
            {
                // Test parse value range.
                int pointsPerWord = Convert.ToInt32(parseValue);
                if (pointsPerWord < PointsPerWordMin)
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.
                        Add(string.Format("Error: Configuration File - Points Per Word '{0}' out of range ({1} minimum) in text '{2}'.",
                        pointsPerWord, PointsPerWordMin, fileTextLine));
                }
                else
                {
                    // Parse OK, update property.
                    Configuration.PointsPerWord = pointsPerWord;
                }
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Letter identifier from the expected Configuration 
        /// File text line. It returns true if the identifier matches the expected format. An error
        /// is added to the Validation Errors collection.
        /// </summary>
        /// <param name="letterText">The letter identifier to be parsed.</param>
        /// <param name="newLetter">A Letter Model object reference that is updated with the parsed
        /// intersection and letter value.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseLetterIdentifier(string letterText, ref LetterModel newLetter)
        {
            const int IntersectSplitIndex = 0;
            const char IntersectSplitCharacter = ':';
            const string RegexPattern = @"^(INTERSECTING|NONINTERSECTING):[A-Z]=";
            const int LetterSplitIndex = 1;
            const string IntersectingLiteral = "INTERSECTING";
            const int ParseValueLetterIndex = 0;

            // Initialise the parse result.
            bool parseOk = true;

            // Check general format before splitting
            Match match = MatchValue(RegexPattern, letterText);

            if (!match.Success)
            {
                // Parse failed, update status and add validation error.
                parseOk = false;
                this.ValidationErrors.Add(string.
                    Format("Error: Configuration File - Invalid indetifiers format in text '{0}'.",
                    letterText));
            }
            else
            {
                // Split the text to derive intersection and update referenced Letter Model object.
                string intersectValue = SplitValue(letterText, IntersectSplitCharacter,
                    IntersectSplitIndex);
                newLetter.IsIntersecting = intersectValue == IntersectingLiteral ? true : false;

                // Split the text to derive Letter and update referenced Letter Model object.
                string letterValue = SplitValue(letterText, IntersectSplitCharacter,
                    LetterSplitIndex);
                newLetter.Letter = letterValue[ParseValueLetterIndex];
            }

            return parseOk;
        }

        /// <summary>
        /// This function attempts to parse the Letter Points value from the expected 
        /// Configuration File text line. It returns true if the value matches the expected
        /// format and is within valid range. An error is added to the Validation Errors 
        /// collection.
        /// </summary>
        /// <param name="fileTextLine">The file text line to be parsed.</param>
        /// <param name="newLetter">A Letter Model object reference that is updated with the parsed
        /// intersection and letter value.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool TryParseLetterPoints(string fileTextLine, ref LetterModel newLetter)
        {
            const int ValueSplitIndex = 1;
            const string RegexPattern = @"^[0-9]+$";
            const char SplitCharacter = '=';

            // Initialise the parse result.
            bool parseOk = true;

            // Only split and match if the delimiter is present.
            if (fileTextLine.Contains(SplitCharacter))
            {
                // Split the parse value from the header.
                string parseValue = SplitValue(fileTextLine, SplitCharacter, ValueSplitIndex);

                // Match the parse value with a regular expression.
                Match match = MatchValue(RegexPattern, parseValue);

                if (!match.Success)
                {
                    // Parse failed, update status and add validation error.
                    parseOk = false;
                    this.ValidationErrors.Add(string.
                        Format("Error: Configuration File - Letter Points '{0}' invalid format in text '{1}'.",
                        parseValue, fileTextLine));
                }
                else
                {
                    // Test parse value range.
                    int pointsPerLetter = Convert.ToInt32(parseValue);
                    if (pointsPerLetter < PointsPerLetterMin)
                    {
                        // Parse failed, update status and add validation error.
                        parseOk = false;
                        this.ValidationErrors.
                            Add(string.Format("Error: Configuration File - Points Per Word '{0}' out of range ({1} minimum) in text '{2}'.",
                            pointsPerLetter, PointsPerWordMin, fileTextLine));
                    }
                    else
                    {
                        // Parse OK, update property.
                        newLetter.Score = pointsPerLetter;
                    }
                }
            }
            else
            {
                // Parse failed, update status.
                parseOk = false;
            }

            return parseOk;
        }

        /// <summary>
        /// This function checks for all 26 letters of the alphabet in the collection. It returns 
        /// true if all 26 letters are present. An missing letter error is added to the Validation 
        /// Errors collection.
        /// </summary>
        /// <param name="letters">The letter collection to be validated.</param>
        /// <param name="isIntersecting">The letter collection identifier.</param>
        /// <returns>TRUE if the parse succeeded.</returns>
        private bool ValidateLetterCollection(Dictionary<string,int> letters, bool isIntersecting)
        {
            const char FirstAlphabetLetter = 'A';
            const char LastAlphabetLetter = 'Z';

            // Initialise the parse result.
            bool validationOk = true;

            // Index through each letter of the alphabet.
            for (char characterIndex = FirstAlphabetLetter; characterIndex <= LastAlphabetLetter; characterIndex++)
            {
                // Count the number of mathing letters in the collection.
                int characterCount = letters.Where(l => l.Key == characterIndex.ToString()).
                    ToList().Count();

                if (characterCount < 1)
                {
                    // Validation for letter failed, update status and add validation error.
                    validationOk = false;
                    if (isIntersecting)
                    {
                        this.ValidationErrors.
                            Add(string.Format("Error: Configuration File - No valid INTERSECTING letter '{0}' in configuration.",
                            characterIndex));
                    }
                    else
                    {
                        this.ValidationErrors.
                            Add(string.Format("Error: Configuration File - No valid NONINTERSECTING letter '{0}' in configuration.",
                            characterIndex));
                    }
                }
            }

            return validationOk;
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
            string splitValue = headerSplit.Length > valueSplitIndex ? headerSplit[valueSplitIndex] : string.Empty;

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
