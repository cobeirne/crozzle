/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System.Collections.Generic;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This model represents a crozzle grid letter.
    /// </summary>
    public class LetterModel
    {
        #region Class Propoerties

        /// <summary>
        /// The character of the grid letter.
        /// </summary>
        public char Letter { get; set; }

        /// <summary>
        /// Indicates that multiple words intersect at this letter.
        /// </summary>
        public bool IsIntersecting { get; set; }

        /// <summary>
        /// The individual allocated score of this letter.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// A list of words assocaited with this grid letter.
        /// </summary>
        public List<WordModel> AssociatedWords { get; set; }

        #endregion

        #region Class Construtors

        /// <summary>
        /// Letter Model default constructor, allows object creation without property 
        /// initialisation.
        /// </summary>
        public LetterModel()
        { }

        /// <summary>
        /// Letter Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="letterCharacter">The character of the grid letter.</param>
        /// <param name="isIntersecting">Multiple words intersect at this letter.</param>
        /// <param name="associatedWord">The initial word assocaited with this grid letter.</param>
        public LetterModel(char letterCharacter, bool isIntersecting, WordModel associatedWord)
        {
            this.Letter = letterCharacter;
            this.IsIntersecting = isIntersecting;
            this.AssociatedWords = new List<WordModel> { associatedWord };
        }

        #endregion
    }
}
