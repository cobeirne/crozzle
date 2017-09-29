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
    /// This is a test class for the CrozzleParserModel Class.
    /// </summary>
    [TestClass()]
    public class CrozzleParserModelTests
    {
        /// <summary>
        /// Test for successful parsing of a crozzle file with no errors.
        /// </summary>
        [TestMethod()]
        public void TryParseCrozzleTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[16];
            CrozzleLines[0] = "EASY,30,10,15,7,7";
            CrozzleLines[1] = "ALAN,ANGELA,BETTY,BILL,BRENDA,CHARLES,FRED,GARY,GEORGE,GRAHAM,HARRY,JACK,JESSICA,JILL,JOHNATHON,LARRY,MARK,MARY,MATTHEW,OSCAR,PAM,PETER,ROBERT,ROGER,RON,RONALD,ROSE,SUSAN,TOM,WENDY";
            CrozzleLines[2] = "HORIZONTAL,1,2,ROBERT";
            CrozzleLines[3] = "HORIZONTAL,2,9,OSCAR";
            CrozzleLines[4] = "HORIZONTAL,3,2,JILL";
            CrozzleLines[5] = "HORIZONTAL,6,4,MARY";
            CrozzleLines[6] = "HORIZONTAL,6,11,LARRY";
            CrozzleLines[7] = "HORIZONTAL,8,6,GARY";
            CrozzleLines[8] = "HORIZONTAL,9,1,JACK";
            CrozzleLines[9] = "VERTICAL,3,2,JESSICA";
            CrozzleLines[10] = "VERTICAL,1,4,BILL";
            CrozzleLines[11] = "VERTICAL,6,4,MARK";
            CrozzleLines[12] = "VERTICAL,6,6,ROGER";
            CrozzleLines[13] = "VERTICAL,4,9,HARRY";
            CrozzleLines[14] = "VERTICAL,2,11,CHARLES";
            CrozzleLines[15] = "VERTICAL,2,15,WENDY";

            CrozzleParserModel parser = new CrozzleParserModel(CrozzleLines);

            // Act.
            bool parseOk = parser.TryParseCrozzle(true);

            // Assert.
            Assert.IsTrue(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 0);
            Assert.IsTrue(parser.Crozzle.Difficulty == "EASY");
            Assert.IsTrue(parser.Crozzle.WordPoolSize == 30);
            Assert.IsTrue(parser.Crozzle.Rows == 10);
            Assert.IsTrue(parser.Crozzle.Columns == 15);
            Assert.IsTrue(parser.Crozzle.HorizontalWords == 7);
            Assert.IsTrue(parser.Crozzle.VerticalWords == 7);
            Assert.IsTrue(parser.Crozzle.WordPool.Count == 30);
            Assert.IsTrue(parser.Crozzle.WordList.Count == 14);
            Assert.IsTrue(parser.Crozzle.WordList[0].Orientation == "HORIZONTAL");
            Assert.IsTrue(parser.Crozzle.WordList[7].Orientation == "VERTICAL");
            Assert.IsTrue(parser.Crozzle.WordList[0].StartRow == 1);
            Assert.IsTrue(parser.Crozzle.WordList[0].StartColumn == 2);
            Assert.IsTrue(parser.Crozzle.WordList[0].Word == "ROBERT");
        }

        /// <summary>
        /// Test for validation errors whilst parsing a crozzle file with format errors.
        /// </summary>
        [TestMethod()]
        public void TryParseCrozzleFormatInvalidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[16];
            CrozzleLines[0] = "SIMPLE,A,B,C,D,E";
            CrozzleLines[1] = "ALAN1,ANGELA,BETTY,BILL,BRENDA,CHARLES,FRED,GARY,GEORGE,GRAHAM,HARRY,JACK,JESSICA,JILL,JOHNATHON,LARRY,MARK,MARY,MATTHEW,OSCAR,PAM,PETER,ROBERT,ROGER,RON,RONALD,ROSE,SUSAN,TOM,WENDY";
            CrozzleLines[2] = "XXXXXX,1,2,ROBERT";
            CrozzleLines[3] = "HORIZONTAL,A,B,:->";
            CrozzleLines[4] = "HORIZONTAL,3,2,JILL";
            CrozzleLines[5] = "HORIZONTAL,6,4,MARY";
            CrozzleLines[6] = "HORIZONTAL,6,11,LARRY";
            CrozzleLines[7] = "HORIZONTAL,8,6,GARY";
            CrozzleLines[8] = "HORIZONTAL,9,1,JACK";
            CrozzleLines[9] = "VERTICAL,3,2,JESSICA";
            CrozzleLines[10] = "VERTICAL,1,4,BILL";
            CrozzleLines[11] = "VERTICAL,6,4,MARK";
            CrozzleLines[12] = "VERTICAL,6,6,ROGER";
            CrozzleLines[13] = "VERTICAL,4,9,HARRY";
            CrozzleLines[14] = "VERTICAL,2,11,CHARLES";
            CrozzleLines[15] = "VERTICAL,2,15,WENDY";

            CrozzleParserModel parser = new CrozzleParserModel(CrozzleLines);

            // Act.
            bool parseOk = parser.TryParseCrozzle(true);

            // Assert.
            Assert.IsFalse(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 38);
        }

        /// <summary>
        /// Test for validation errors whilst parsing a crozzle file with lower range errors.
        /// </summary>
        [TestMethod()]
        public void TryParseCrozzleLowerInvalidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[16];
            CrozzleLines[0] = "SIMPLE,9,3,7,-1,-1";
            CrozzleLines[1] = " ,ANGELA,BETTY,BILL,BRENDA,CHARLES,FRED,GARY,GEORGE,GRAHAM,HARRY,JACK,JESSICA,JILL,JOHNATHON,LARRY,MARK,MARY,MATTHEW,OSCAR,PAM,PETER,ROBERT,ROGER,RON,RONALD,ROSE,SUSAN,TOM,WENDY";
            CrozzleLines[2] = "HORIZONTAL,-1,-2,ROBERT";
            CrozzleLines[3] = "HORIZONTAL,2,9,,";
            CrozzleLines[4] = "HORIZONTAL,3,2,JILL";
            CrozzleLines[5] = "HORIZONTAL,6,4,MARY";
            CrozzleLines[6] = "HORIZONTAL,6,11,LARRY";
            CrozzleLines[7] = "HORIZONTAL,8,6,GARY";
            CrozzleLines[8] = "HORIZONTAL,9,1,JACK";
            CrozzleLines[9] = "VERTICAL,3,2,JESSICA";
            CrozzleLines[10] = "VERTICAL,1,4,BILL";
            CrozzleLines[11] = "VERTICAL,6,4,MARK";
            CrozzleLines[12] = "VERTICAL,6,6,ROGER";
            CrozzleLines[13] = "VERTICAL,4,9,HARRY";
            CrozzleLines[14] = "VERTICAL,2,11,CHARLES";
            CrozzleLines[15] = "VERTICAL,2,15,WENDY";

            CrozzleParserModel parser = new CrozzleParserModel(CrozzleLines);

            // Act.
            bool parseOk = parser.TryParseCrozzle(true);

            // Assert.
            Assert.IsFalse(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 23);
        }

        /// <summary>
        /// Test for validation errors whilst parsing a crozzle file with upper range errors.
        /// </summary>
        [TestMethod()]
        public void TryParseCrozzleUpperInvalidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[16];
            CrozzleLines[0] = "SIMPLE,1001,401,801,1001,1001";
            CrozzleLines[1] = "CHRIS,ALAN,ANGELA,BETTY,BILL,BRENDA,CHARLES,FRED,GARY,GEORGE,GRAHAM,HARRY,JACK,JESSICA,JILL,JOHNATHON,LARRY,MARK,MARY,MATTHEW,OSCAR,PAM,PETER,ROBERT,ROGER,RON,RONALD,ROSE,SUSAN,TOM,WENDY";
            CrozzleLines[2] = "HORIZONTAL,401,801,ROBERT";
            CrozzleLines[3] = "HORIZONTAL,2,9,OSCAR";
            CrozzleLines[4] = "HORIZONTAL,3,2,JILL";
            CrozzleLines[5] = "HORIZONTAL,6,4,MARY";
            CrozzleLines[6] = "HORIZONTAL,6,11,ETHAN";
            CrozzleLines[7] = "HORIZONTAL,8,6,GARY";
            CrozzleLines[8] = "HORIZONTAL,9,1,JACK";
            CrozzleLines[9] = "VERTICAL,3,2,JESSICA";
            CrozzleLines[10] = "VERTICAL,1,4,BILL";
            CrozzleLines[11] = "VERTICAL,6,4,MARK";
            CrozzleLines[12] = "VERTICAL,6,6,ROGER";
            CrozzleLines[13] = "VERTICAL,4,9,HARRY";
            CrozzleLines[14] = "VERTICAL,2,11,CHARLES";
            CrozzleLines[15] = "VERTICAL,2,15,WENDY";

            CrozzleParserModel parser = new CrozzleParserModel(CrozzleLines);

            // Act.
            bool parseOk = parser.TryParseCrozzle(true);

            // Assert.
            Assert.IsFalse(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 8);
        }
    }
}