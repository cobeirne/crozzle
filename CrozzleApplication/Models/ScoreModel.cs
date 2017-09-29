/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System.Text;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This model defines the rules for scoring a crozzle. It includes functions for calculating
    /// individual attribute scores and crozzle total score.
    /// </summary>
    public class ScoreModel
    {
        #region Class Properties

        /// <summary>
        /// The Crozzle Grid to be scored.
        /// </summary>
        public GridModel CrozzleGrid { get; set; }

        /// <summary>
        /// The total words score.
        /// </summary>
        public int WordScore { get; set; }

        /// <summary>
        /// The total non-intersecting letters score.
        /// </summary>
        public int NonIntersectingLetterScore { get; set; }

        /// <summary>
        /// The total intersecting letters score.
        /// </summary>
        public int IntersectingLetterScore { get; set; }

        /// <summary>
        /// The total crozzle score.
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// The crozzle validation status. If FALSE the crozzle is scored 0.
        /// </summary>
        public bool CrozzleIsValid { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Score Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="grid">The crozzle grid to be scored.</param>
        /// <param name="crozzleIsValid">The crozzle validation status.</param>
        public ScoreModel(GridModel grid, bool crozzleIsValid)
        {
            this.CrozzleGrid = grid;
            this.CrozzleIsValid = crozzleIsValid;
            UpdateTotalScore();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function calculates the score of a sub solution.
        /// </summary>
        /// <param name="subSolution">The sub solution to be scored.</param>
        /// <returns>The sub solution score.</returns>
        public int CalculateSubSolutionScore(SubSolutionModel subSolution)
        {
            const int InitialScore = 0;

            int score = InitialScore;

            score += this.CrozzleGrid.Crozzle.Configuration.PointsPerWord *
                subSolution.GroupWords.Count;

            score += CalculateGridPoints();

            return score;
        }

        /// <summary>
        /// This function builds a HTML table including word, letter and total scores.
        /// </summary>
        /// <returns>A HTML table in string format.</returns>
        public string GetScoreHtml()
        {
            StringBuilder htmlBuilder = new StringBuilder();

            // Create and style a HTML table struture.
            htmlBuilder.Append("<html><body><table style='font-size:x-small;font-family=sans-serif;border-collapse:collapse'>");
            htmlBuilder.AppendFormat("<tr>");
            htmlBuilder.AppendFormat("<td width='20%' style='text-align:right;'>Crozzle Difficulty:</td>");
            htmlBuilder.AppendFormat("<td width='20%' style='text-align:left;padding:6px;font-weight:bold;'>{0}</td>", this.CrozzleGrid.Crozzle.Difficulty);
            htmlBuilder.AppendFormat("<td width='20%' style='text-align:right;'>Word Score:</td>");
            htmlBuilder.AppendFormat("<td width='20%' style='text-align:left;padding:6px;'>{0}</td>", this.WordScore);
            htmlBuilder.AppendFormat("</tr>");

            htmlBuilder.AppendFormat("<tr>");
            htmlBuilder.AppendFormat("<td style='text-align:right;'>Crozzle Validity:</td>");
            htmlBuilder.AppendFormat("<td style='text-align:left;padding:6px;font-weight:bold;'>{0}</td>", this.CrozzleIsValid ? "VALID" : "INVALID");
            htmlBuilder.AppendFormat("<td style='text-align:right;'>Non-Intersecting Score:</td>");
            htmlBuilder.AppendFormat("<td style='text-align:left;padding:6px;'>{0}</td>", this.NonIntersectingLetterScore);
            htmlBuilder.AppendFormat("</tr>");

            htmlBuilder.AppendFormat("<tr>");
            htmlBuilder.AppendFormat("<td></td>");
            htmlBuilder.AppendFormat("<td></td>");
            htmlBuilder.AppendFormat("<td style='text-align:right;'>Intersecting Score:</td>");
            htmlBuilder.AppendFormat("<td style='text-align:left;padding:6px;'>{0}</td>", this.IntersectingLetterScore);
            htmlBuilder.AppendFormat("</tr>");

            htmlBuilder.AppendFormat("<tr>");
            htmlBuilder.AppendFormat("<td></td>");
            htmlBuilder.AppendFormat("<td></td>");
            htmlBuilder.AppendFormat("<td style='text-align:right;font-weight:bold;font-size:medium;'>Total Score:</td>");
            htmlBuilder.AppendFormat("<td style='text-align:left;font-weight:bold;font-size:medium;padding:6px;'>{0}</td>", this.TotalScore);
            htmlBuilder.AppendFormat("</tr>");

            // Close the HTML table.
            htmlBuilder.Append("</table></body></html>");

            // Return the html formated text as a string.
            return htmlBuilder.ToString();
        }

        /// <summary>
        /// This function calculates and updates the total crozzle score property.
        /// </summary>
        public void UpdateTotalScore()
        {
            const int InitialTotalScore = 0;

            int totalScore = InitialTotalScore;

            // Only award scores to valid crozzles
            if (this.CrozzleIsValid)
            {
                // Add the words score to the total.
                CalculateWordCountPoints();
                totalScore += this.WordScore;

                // Add the letter scores to the total.
                CalculateGridPoints();
                totalScore += this.IntersectingLetterScore;
                totalScore += this.NonIntersectingLetterScore;
            }

            // Update the Total Score property.
            this.TotalScore = totalScore;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This function interates through each grid letter to calculate an individual letter
        /// score.
        /// </summary>
        private int CalculateGridPoints()
        {
            const int InitialScore = 0;

            int score = InitialScore;
            this.IntersectingLetterScore = InitialScore;
            this.NonIntersectingLetterScore = InitialScore;

            // Iterate through each grid position
            foreach (LetterModel curentLetter in this.CrozzleGrid.Grid)
            {
                if (curentLetter != null)
                {
                    // Add the letter score to the respective letter collection subtotal.
                    if (curentLetter.IsIntersecting)
                    {
                        this.IntersectingLetterScore += this.CrozzleGrid.Crozzle.Configuration.
                            IntersectingPoints[curentLetter.Letter.ToString()];
                    }
                    else
                    {
                        this.NonIntersectingLetterScore += this.CrozzleGrid.Crozzle.Configuration.
                            NonIntersectingPoints[curentLetter.Letter.ToString()];
                    }
                }
            }

            // Tally the total score.
            score = this.IntersectingLetterScore + this.NonIntersectingLetterScore;

            return score;
        }

        /// <summary>
        /// This function calculates and updates the individual word score based on the 
        /// respective Crozzle configuration.
        /// </summary>
        private void CalculateWordCountPoints()
        {
            // Count the number of words and multiple by the words score factor.
            this.WordScore = this.CrozzleGrid.Crozzle.Configuration.PointsPerWord * 
                this.CrozzleGrid.Crozzle.WordList.Count;
        }
        
        #endregion
    }
}
