/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 2
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       02/10/16
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrozzleGame.Models
{
    public class SubSolutionModel
    {
        #region Class Properties

        /// <summary>
        /// The words included in the sub solution.
        /// </summary>
        public List<WordModel> GroupWords { get; set; }

        /// <summary>
        /// The sub solution score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The starting array row offset required to insert the sub solution.
        /// </summary>
        public int ArrayRowffset { get; set; }

        /// <summary>
        /// The starting array column offset required to insert the sub solution.
        /// </summary>
        public int ArrayColumnOffset { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Solution Node constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        public SubSolutionModel()
        {
            this.GroupWords = new List<WordModel>();
        }

        /// <summary>
        /// Solution Node constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="subSolution">The sub solution to copy.</param>
        /// <param name="arrayRowOffset">The starting array row offset.</param>
        /// <param name="arrayColumnOffset">The starting array column offset.</param>
        public SubSolutionModel(SubSolutionModel subSolution, int arrayRowOffset, int arrayColumnOffset)
        {
            this.GroupWords = subSolution.GroupWords.ToList();
            this.Score = subSolution.Score;
            this.ArrayRowffset = arrayRowOffset;
            this.ArrayColumnOffset = arrayColumnOffset;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function checks if the words used in the sub solution parameter are also used by 
        /// the current object.
        /// </summary>
        /// <param name="matchingSolution">The sub solution to check.</param>
        /// <returns>TRUE if the sub solution uses the same words.</returns>
        public bool MatchSubSolutionWords(SubSolutionModel matchingSolution)
        {
            bool subSolutionIsMatch = true;

            // Build a working list of words to check for.
            List<string> workingWords = new List<string>();
            matchingSolution.GroupWords.ForEach(w => workingWords.Add(w.Word));

            // Check the current object for matching words.
            foreach (WordModel word in this.GroupWords)
            {
                subSolutionIsMatch = workingWords.Contains(word.Word) ? subSolutionIsMatch : false;
            }
            
            return subSolutionIsMatch;
        }

        #endregion
    }
}
