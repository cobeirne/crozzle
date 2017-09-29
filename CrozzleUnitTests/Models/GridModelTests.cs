/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 2
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       02/10/16
/// </summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrozzleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrozzleGame.Models.Tests
{
    /// <summary>
    /// This is a test class for the GridModel Class.
    /// </summary>
    [TestClass()]
    public class GridModelTests
    {
        /// <summary>
        /// Test for successful grid creation.
        /// </summary>
        [TestMethod()]
        public void GridModelTest()
        {
            // Arrange.
            string[] testLines = new string[6];
            testLines[0] = "EASY,30,20,20,7,7";
            testLines[1] = "ROBERT,JESSICA,BETTY,BILL,BRENDA,CHARLES,JAMES,JOHN,GEORGE";
            testLines[2] = "HORIZONTAL,1,2,ROBERT";
            testLines[3] = "VERTICAL,3,2,JESSICA";
            testLines[4] = "HORIZONTAL,10,2,JOHN";
            testLines[5] = "VERTICAL,10,2,JAMES";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(testLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;

            // Act.
            GridModel crozzleGrid = new GridModel(crozzle);

            // Assert.
            Assert.IsTrue(crozzleGrid.Grid[0, 1].Letter == 'R');
            Assert.IsTrue(crozzleGrid.Grid[0, 6].Letter == 'T');
            Assert.IsTrue(crozzleGrid.Grid[2, 1].Letter == 'J');
            Assert.IsTrue(crozzleGrid.Grid[8, 1].Letter == 'A');
            Assert.IsTrue(crozzleGrid.Grid[0, 1].IsIntersecting == false);
            Assert.IsTrue(crozzleGrid.Grid[9, 1].IsIntersecting == true);
        }

        /// <summary>
        /// Test for successful grid HTML serialisation.
        /// </summary>
        [TestMethod()]
        public void GetGridHtmlTest()
        {
            // Arrange.
            string[] testLines = new string[4];
            testLines[0] = "EASY,2,5,5,1,1";
            testLines[1] = "JAMES,JOHN";
            testLines[2] = "HORIZONTAL,1,1,JOHN";
            testLines[3] = "VERTICAL,1,1,JAMES";
            
            CrozzleParserModel crozzleParser = new CrozzleParserModel(testLines);
            crozzleParser.TryParseCrozzle(true);
                        
            CrozzleModel crozzle = crozzleParser.Crozzle;

            GridModel crozzleGrid = new GridModel(crozzle);

            // Act.
            string gridHtml = crozzleGrid.GetGridHtml();

            // Assert.
            Assert.IsTrue(gridHtml != null);
        }
    }
}