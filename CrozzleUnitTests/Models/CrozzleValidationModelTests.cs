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
    /// This is a test class for the CrozzleValidationModel Class.
    /// </summary>
    [TestClass()]
    public class CrozzleValidationModelTests
    {
        /// <summary>
        /// Test for successful horizontal words validation.
        /// </summary>
        [TestMethod()]
        public void ValidateCrozzleHorizontalWordsValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,4,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL,LEE";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";
            CrozzleLines[4] = "VERTICAL,7,1,BILL";
            CrozzleLines[5] = "HORIZONTAL,10,1,LEE";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 10 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for invalid horizontal words validation.
        /// </summary>
        [TestMethod()]
        public void ValidateCrozzleHorizontalWordsInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,2,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL,PETE";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "HORIZONTAL,1,6,BILL";
            CrozzleLines[4] = "HORIZONTAL,2,6,PETE";
            CrozzleLines[5] = "VERTICAL,1,1,JAMES";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count != 0);
        }

        /// <summary>
        /// Test for successful vertical words validation.
        /// </summary>
        [TestMethod()]
        public void ValidateCrozzleVerticalWordsValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,4,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL,LEE";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";
            CrozzleLines[4] = "VERTICAL,7,1,BILL";
            CrozzleLines[5] = "HORIZONTAL,10,1,LEE";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 10 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for invalid vertical words validation.
        /// </summary>
        [TestMethod()]
        public void ValidateCrozzleVerticalWordsInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,2,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";
            CrozzleLines[4] = "VERTICAL,7,1,BILL";
            CrozzleLines[5] = "VERTICAL,3,3,PETE";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 10 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count != 0);
        }

        /// <summary>
        /// Test for successful no duplicate words validation.
        /// </summary>
        [TestMethod()]
        public void ValidateCrozzleNoDuplicateWordsValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,4,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL,LEE";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";
            CrozzleLines[4] = "VERTICAL,7,1,BILL";
            CrozzleLines[5] = "HORIZONTAL,10,1,LEE";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 10 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for invalid no duplicate words validation.
        /// </summary>
        [TestMethod()]
        public void ValidateCrozzleNoDuplicateWordsInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[5];
            CrozzleLines[0] = "EASY,2,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "HORIZONTAL,1,6,BILL";
            CrozzleLines[4] = "VERTICAL,1,1,JOHN";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 10 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count != 0);
        }

        /// <summary>
        /// Test for successful group limit validation.
        /// </summary>
        [TestMethod()]
        public void ValidateGroupLimitValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[7];
            CrozzleLines[0] = "EASY,5,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL,LEE,BART";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";
            CrozzleLines[4] = "VERTICAL,7,1,BILL";
            CrozzleLines[5] = "HORIZONTAL,10,1,LEE";
            CrozzleLines[6] = "HORIZONTAL,7,1,BART";
            
            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 2 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for invalid group limit validation.
        /// </summary>
        [TestMethod()]
        public void ValidateGroupLimitInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[5];
            CrozzleLines[0] = "EASY,2,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "HORIZONTAL,1,6,BILL";
            CrozzleLines[4] = "VERTICAL,1,1,JAMES";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count != 0);
        }

        /// <summary>
        /// Test for successful Easy vertical intersects validation.
        /// </summary>
        [TestMethod()]
        public void ValidateEasyVerticalIntersectsValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,4,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL,LEE";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";
            CrozzleLines[4] = "VERTICAL,7,1,BILL";
            CrozzleLines[5] = "HORIZONTAL,10,1,LEE";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for invalid Easy low range vertical intersects validation.
        /// </summary>
        [TestMethod()]
        public void ValidateEasyVerticalIntersectsLowerInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[5];
            CrozzleLines[0] = "EASY,2,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "HORIZONTAL,1,6,BILL";
            CrozzleLines[4] = "VERTICAL,3,1,JAMES";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 3);
        }

        /// <summary>
        /// Test for invalid Easy high range vertical intersects validation.
        /// </summary>
        [TestMethod()]
        public void ValidateEasyVerticalIntersectsUpperInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "EASY,4,10,10,1,3";
            CrozzleLines[1] = "JAMES,JOHN,MATT,SIMON";
            CrozzleLines[2] = "HORIZONTAL,1,1,JAMES";
            CrozzleLines[3] = "VERTICAL,1,1,JOHN";
            CrozzleLines[4] = "VERTICAL,1,3,MATT";
            CrozzleLines[5] = "VERTICAL,1,5,SIMON";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 1);
        }

        /// <summary>
        /// Test for invalid Hard low range vertical intersects validation.
        /// </summary>
        [TestMethod()]
        public void ValidateHardVerticalIntersectsLowerInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[5];
            CrozzleLines[0] = "HARD,2,10,10,1,1";
            CrozzleLines[1] = "JAMES,JOHN,BILL";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "HORIZONTAL,1,6,BILL";
            CrozzleLines[4] = "VERTICAL,3,1,JAMES";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 3);
        }

        /// <summary>
        /// Test for invalid Hard high range vertical intersects validation.
        /// </summary>
        [TestMethod()]
        public void ValidateHardVerticalIntersectsUpperInValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[6];
            CrozzleLines[0] = "HARD,4,10,10,1,3";
            CrozzleLines[1] = "JAMES,JOHN,MATT,SIMON";
            CrozzleLines[2] = "HORIZONTAL,1,1,JAMES";
            CrozzleLines[3] = "VERTICAL,1,1,JOHN";
            CrozzleLines[4] = "VERTICAL,1,3,MATT";
            CrozzleLines[5] = "VERTICAL,1,5,SIMON";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for successful word buffer validation.
        /// </summary>
        [TestMethod()]
        public void ValidateWordBufferValidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[5];
            CrozzleLines[0] = "EASY,4,10,10,1,3";
            CrozzleLines[1] = "JAMES,JOHN,MATT,SIMON";
            CrozzleLines[2] = "HORIZONTAL,1,1,JAMES";
            CrozzleLines[3] = "VERTICAL,1,1,JOHN";
            CrozzleLines[4] = "VERTICAL,1,3,MATT";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsTrue(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count == 0);
        }

        /// <summary>
        /// Test for invalid vertical word buffer validation.
        /// </summary>
        [TestMethod()]
        public void ValidateWordBufferVerticalInvalidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[7];
            CrozzleLines[0] = "EASY,4,10,10,1,3";
            CrozzleLines[1] = "JAMES,JOHN,MATT,SIMON,NED,ED,JO";
            CrozzleLines[2] = "HORIZONTAL,1,1,JAMES";
            CrozzleLines[3] = "VERTICAL,1,1,JOHN";
            CrozzleLines[4] = "HORIZONTAL,4,1,NED";
            CrozzleLines[5] = "VERTICAL,4,2,ED";
            CrozzleLines[6] = "HORIZONTAL,5,4,JO";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000 };

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count != 1);
        }

        /// <summary>
        /// Test for invalid horizontal word buffer validation.
        /// </summary>
        [TestMethod()]
        public void ValidateWordBufferHorizontalInvalidTest()
        {
            // Arrange.
            string[] CrozzleLines = new string[14];
            CrozzleLines[0] = "EASY,25,10,15,6,6";
            CrozzleLines[1] = "ALAN,ANGELA,BETTY,BILL,BRENDA,CHARLES,FRED,GARY,GEORGE,GRAHAM,HARRY,JACK,JESSICA,LARRY,MARK,MARY,MATTHEW,OSCAR,PAM,ROBERT,ROGER,RON,SUSAN,TOM,WENDY";
            CrozzleLines[2] = "HORIZONTAL,1,3,ROBERT";
            CrozzleLines[3] = "HORIZONTAL,3,1,ANGELA";
            CrozzleLines[4] = "HORIZONTAL,5,1,MARY";
            CrozzleLines[5] = "HORIZONTAL,5,11,LARRY";
            CrozzleLines[6] = "HORIZONTAL,6,5,SUSAN";
            CrozzleLines[7] = "HORIZONTAL,8,7,ALAN";
            CrozzleLines[8] = "VERTICAL,1,3,ROGER";
            CrozzleLines[9] = "VERTICAL,1,5,BILL";
            CrozzleLines[10] = "VERTICAL,5,7,OSCAR";
            CrozzleLines[11] = "VERTICAL,1,8,TOM";
            CrozzleLines[12] = "VERTICAL,5,14,RON";
            CrozzleLines[13] = "VERTICAL,1,15,WENDY";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = new ConfigModel { GroupsLimit = 1000};

            GridModel grid = new GridModel(crozzle);

            // Act.
            CrozzleValidationModel validator = new CrozzleValidationModel(grid);

            // Assert.
            Assert.IsFalse(validator.CrozzleIsValid);
            Assert.IsTrue(validator.ValidationErrors.Count >= 4);
        }
    }
}