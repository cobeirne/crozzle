/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System.Collections.Generic;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This model represents the data stored in a Configuration File.
    /// </summary>
    public class ConfigModel
    {
        #region Class Properties
        
        /// <summary>
        /// The maximum number of groups allowed in the associated crozzle.
        /// </summary>
        public int GroupsLimit { get; set; }

        /// <summary>
        /// The points allocated to each word formed in the associated crozzle.
        /// </summary>
        public int PointsPerWord { get; set; }

        /// <summary>
        /// A dictionary of intersecting letter scores.
        /// </summary>
        public Dictionary<string, int> IntersectingPoints = new Dictionary<string, int>();

        /// <summary>
        /// A dictionary of non-intersecting letter scores.
        /// </summary>
        public Dictionary<string, int> NonIntersectingPoints = new Dictionary<string, int>();

        /// <summary>
        /// A list of validation errors detected during during parsing.
        /// </summary>
        public List<string> ValidationErrors { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Configuration Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        public ConfigModel()
        {
            this.ValidationErrors = new List<string>();
        }

        #endregion
    }
}
