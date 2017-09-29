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
    /// This is a test class for the ScoringModel Class.
    /// </summary>
    [TestClass()]
    public class ScoringModelTests
    {
        /// <summary>
        /// Test for successful crozzle scoring.
        /// </summary>
        [TestMethod()]
        public void CalculateCrozzleScoreTest()
        {
            // Arrange.
            string[] configLines = BuildSampleArray();

            ConfigParserModel configParser = new ConfigParserModel(BuildSampleArray());
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "EASY,2,5,5,1,1";
            CrozzleLines[1] = "JAMES,JOHN";
            CrozzleLines[2] = "HORIZONTAL,1,1,JOHN";
            CrozzleLines[3] = "VERTICAL,1,1,JAMES";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(true);

            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;

            GridModel grid = new GridModel(crozzle);

            // Act.
            ScoreModel score = new ScoreModel(grid, true);

            // Assert.
            Assert.IsTrue(score.TotalScore == 287);
        }

        /// <summary>
        /// Build a sample configuration for test arrangement.
        /// </summary>
        /// <returns>An array of configuration file lines.</returns>
        private string[] BuildSampleArray()
        {
            string[] testLines = new string[54];

            testLines[0] = "GROUPSPERCROZZLELIMIT=1000";
            testLines[1] = "POINTSPERWORD=10";
            testLines[2] = "INTERSECTING:A=1";
            testLines[3] = "INTERSECTING:B=2";
            testLines[4] = "INTERSECTING:C=3";
            testLines[5] = "INTERSECTING:D=4";
            testLines[6] = "INTERSECTING:E=5";
            testLines[7] = "INTERSECTING:F=6";
            testLines[8] = "INTERSECTING:G=7";
            testLines[9] = "INTERSECTING:H=8";
            testLines[10] = "INTERSECTING:I=9";
            testLines[11] = "INTERSECTING:J=10";
            testLines[12] = "INTERSECTING:K=11";
            testLines[13] = "INTERSECTING:L=12";
            testLines[14] = "INTERSECTING:M=13";
            testLines[15] = "INTERSECTING:N=14";
            testLines[16] = "INTERSECTING:O=15";
            testLines[17] = "INTERSECTING:P=16";
            testLines[18] = "INTERSECTING:Q=17";
            testLines[19] = "INTERSECTING:R=18";
            testLines[20] = "INTERSECTING:S=19";
            testLines[21] = "INTERSECTING:T=20";
            testLines[22] = "INTERSECTING:U=21";
            testLines[23] = "INTERSECTING:V=22";
            testLines[24] = "INTERSECTING:W=23";
            testLines[25] = "INTERSECTING:X=24";
            testLines[26] = "INTERSECTING:Y=25";
            testLines[27] = "INTERSECTING:Z=26";
            testLines[28] = "NONINTERSECTING:A=27";
            testLines[29] = "NONINTERSECTING:B=28";
            testLines[30] = "NONINTERSECTING:C=29";
            testLines[31] = "NONINTERSECTING:D=30";
            testLines[32] = "NONINTERSECTING:E=31";
            testLines[33] = "NONINTERSECTING:F=32";
            testLines[34] = "NONINTERSECTING:G=33";
            testLines[35] = "NONINTERSECTING:H=34";
            testLines[36] = "NONINTERSECTING:I=35";
            testLines[37] = "NONINTERSECTING:J=36";
            testLines[38] = "NONINTERSECTING:K=37";
            testLines[39] = "NONINTERSECTING:L=38";
            testLines[40] = "NONINTERSECTING:M=39";
            testLines[41] = "NONINTERSECTING:N=40";
            testLines[42] = "NONINTERSECTING:O=41";
            testLines[43] = "NONINTERSECTING:P=42";
            testLines[44] = "NONINTERSECTING:Q=43";
            testLines[45] = "NONINTERSECTING:R=44";
            testLines[46] = "NONINTERSECTING:S=45";
            testLines[47] = "NONINTERSECTING:T=46";
            testLines[48] = "NONINTERSECTING:U=47";
            testLines[49] = "NONINTERSECTING:V=48";
            testLines[50] = "NONINTERSECTING:W=49";
            testLines[51] = "NONINTERSECTING:X=50";
            testLines[52] = "NONINTERSECTING:Y=51";
            testLines[53] = "NONINTERSECTING:Z=52";

            return testLines;
        }

    }
}