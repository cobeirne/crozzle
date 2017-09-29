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
    /// <summary>
    /// This class models a crozzle solution as a tree node for recursive processing.
    /// </summary>
    public class SolutionNode
    {
        #region Class Properties

        /// <summary>
        /// The solution grid of the current node.
        /// </summary>
        public GridModel NodeGrid { get; set; }

        /// <summary>
        /// The sub solutions in the current node.
        /// </summary>
        public List<SubSolutionModel> SubSolutions { get; set; }

        /// <summary>
        /// The child solution nodes of the current node.
        /// </summary>
        public List<SolutionNode> ChildNodes { get; set; }

        /// <summary>
        /// The solution score of the current node.
        /// </summary>
        public int NodeScore { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Solution Node constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        public SolutionNode()
        {
            this.NodeGrid = new GridModel();
            this.SubSolutions = new List<SubSolutionModel>();
            this.ChildNodes = new List<SolutionNode>();
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Returns a deep copy of the current Solution Node object.
        /// </summary>
        /// <returns>A deep copy of the solution node.</returns>
        public SolutionNode DeepCopy()
        {
            SolutionNode nodeCopy = new SolutionNode();

            nodeCopy.NodeGrid = this.NodeGrid.DeepCopy();
            nodeCopy.SubSolutions = this.SubSolutions.ToList();
            nodeCopy.ChildNodes = this.ChildNodes.ToList();
            nodeCopy.NodeScore = this.NodeScore;

            return nodeCopy;
        }

        #endregion
    }
}
