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
    /// This is a test class for the ConfigParseModel Class.
    /// </summary>
    [TestClass()]
    public class ConfigParserModelTests
    {
        /// <summary>
        /// Test for successful parsing of a configuration file with no errors.
        /// </summary>
        [TestMethod()]
        public void TryParseConfigurationTest()
        {
            // Arrange.
            string[] testLines = new string[54];
            testLines[0] = "GROUPSPERCROZZLELIMIT=1000";
            testLines[1] = "POINTSPERWORD=10";
            testLines[2] = "INTERSECTING:A=11";
            testLines[3] = "INTERSECTING:B=1";
            testLines[4] = "INTERSECTING:C=1";
            testLines[5] = "INTERSECTING:D=1";
            testLines[6] = "INTERSECTING:E=1";
            testLines[7] = "INTERSECTING:F=1";
            testLines[8] = "INTERSECTING:G=1";
            testLines[9] = "INTERSECTING:H=1";
            testLines[10] = "INTERSECTING:I=1";
            testLines[11] = "INTERSECTING:J=1";
            testLines[12] = "INTERSECTING:K=1";
            testLines[13] = "INTERSECTING:L=1";
            testLines[14] = "INTERSECTING:M=1";
            testLines[15] = "INTERSECTING:N=1";
            testLines[16] = "INTERSECTING:O=1";
            testLines[17] = "INTERSECTING:P=1";
            testLines[18] = "INTERSECTING:Q=1";
            testLines[19] = "INTERSECTING:R=1";
            testLines[20] = "INTERSECTING:S=1";
            testLines[21] = "INTERSECTING:T=1";
            testLines[22] = "INTERSECTING:U=1";
            testLines[23] = "INTERSECTING:V=1";
            testLines[24] = "INTERSECTING:W=1";
            testLines[25] = "INTERSECTING:X=1";
            testLines[26] = "INTERSECTING:Y=1";
            testLines[27] = "INTERSECTING:Z=1";
            testLines[28] = "NONINTERSECTING:A=12";
            testLines[29] = "NONINTERSECTING:B=1";
            testLines[30] = "NONINTERSECTING:C=1";
            testLines[31] = "NONINTERSECTING:D=1";
            testLines[32] = "NONINTERSECTING:E=1";
            testLines[33] = "NONINTERSECTING:F=1";
            testLines[34] = "NONINTERSECTING:G=1";
            testLines[35] = "NONINTERSECTING:H=1";
            testLines[36] = "NONINTERSECTING:I=1";
            testLines[37] = "NONINTERSECTING:J=1";
            testLines[38] = "NONINTERSECTING:K=1";
            testLines[39] = "NONINTERSECTING:L=1";
            testLines[40] = "NONINTERSECTING:M=1";
            testLines[41] = "NONINTERSECTING:N=1";
            testLines[42] = "NONINTERSECTING:O=1";
            testLines[43] = "NONINTERSECTING:P=1";
            testLines[44] = "NONINTERSECTING:Q=1";
            testLines[45] = "NONINTERSECTING:R=1";
            testLines[46] = "NONINTERSECTING:S=1";
            testLines[47] = "NONINTERSECTING:T=1";
            testLines[48] = "NONINTERSECTING:U=1";
            testLines[49] = "NONINTERSECTING:V=1";
            testLines[50] = "NONINTERSECTING:W=1";
            testLines[51] = "NONINTERSECTING:X=1";
            testLines[52] = "NONINTERSECTING:Y=1";
            testLines[53] = "NONINTERSECTING:Z=1";
            
            ConfigParserModel parser = new ConfigParserModel(testLines);

            // Act.
            bool parseOk = parser.TryParseConfiguration();

            // Assert.
            Assert.IsTrue(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 0);
            Assert.IsTrue(parser.Configuration.GroupsLimit == 1000);
            Assert.IsTrue(parser.Configuration.PointsPerWord == 10);
            Assert.IsTrue(parser.Configuration.IntersectingPoints.Count == 26);
            Assert.IsTrue(parser.Configuration.NonIntersectingPoints.Count == 26);
            Assert.IsTrue(parser.Configuration.IntersectingPoints["A"] == 11);
            Assert.IsTrue(parser.Configuration.NonIntersectingPoints["A"] == 12);
        }

        /// <summary>
        /// Test for validation errors whilst parsing a configuration file with format errors.
        /// </summary>
        [TestMethod()]
        public void TryParseConfigurationFormatInvalidTest()
        {
            // Arrange.
            string[] testLines = new string[54];
            testLines[0] = "CROZZLELIMIT=A";
            testLines[1] = "POINTSPER=B";
            testLines[2] = "INTERSECTING:A=*";
            testLines[3] = "INTERSECTING:B:-)1";
            testLines[4] = "IIIIII:C=1";
            testLines[5] = "INTERSECTINGD=1";
            testLines[6] = "INTERSECTING:=1";
            testLines[7] = "INTERSECTING:F1";
            testLines[8] = "G=1";
            testLines[9] = "INTERSECTING:H=XXXXXXX";
            testLines[10] = "INTERSECTING:I=1";
            testLines[11] = "INTERSECTING:J=1";
            testLines[12] = "INTERSECTING:K=1";
            testLines[13] = "INTERSECTING:L=1";
            testLines[14] = "INTERSECTING:M=1";
            testLines[15] = "INTERSECTING:N=1";
            testLines[16] = "INTERSECTING:O=1";
            testLines[17] = "INTERSECTING:P=1";
            testLines[18] = "INTERSECTING:Q=1";
            testLines[19] = "INTERSECTING:R=1";
            testLines[20] = "INTERSECTING:S=1";
            testLines[21] = "INTERSECTING:T=1";
            testLines[22] = "INTERSECTING:U=1";
            testLines[23] = "INTERSECTING:V=1";
            testLines[24] = "INTERSECTING:W=1";
            testLines[25] = "INTERSECTING:X=1";
            testLines[26] = "INTERSECTING:Y=1";
            testLines[27] = "INTERSECTING:*=1";
            testLines[28] = "NONINTERSECTING:A=1";
            testLines[29] = "NONINTERSECTING:B=1";
            testLines[30] = "NONINTERSECTING:C=1";
            testLines[31] = "NONINTERSECTING:D=1";
            testLines[32] = "NONINTERSECTING:E=1";
            testLines[33] = "NONINTERSECTING:F=1";
            testLines[34] = "NONINTERSECTING:G=1";
            testLines[35] = "NONINTERSECTING:H=1";
            testLines[36] = "NONINTERSECTING:I=1";
            testLines[37] = "NONINTERSECTING:J=1";
            testLines[38] = "NONINTERSECTING:K=1";
            testLines[39] = "NONINTERSECTING:L=1";
            testLines[40] = "NONINTERSECTING:M=1";
            testLines[41] = "NONINTERSECTING:N=1";
            testLines[42] = "NONINTERSECTING:O=1";
            testLines[43] = "NONINTERSECTING:P=1";
            testLines[44] = "NONINTERSECTING:Q=1";
            testLines[45] = "NONINTERSECTING:R=1";
            testLines[46] = "NONINTERSECTING:S=1";
            testLines[47] = "NONINTERSECTING:T=1";
            testLines[48] = "NONINTERSECTING:U=1";
            testLines[49] = "NONINTERSECTING:V=1";
            testLines[50] = "NONINTERSECTING:W=1";
            testLines[51] = "NONINTERSECTING:X=1";
            testLines[52] = "NONINTERSECTING:Y=1";
            testLines[53] = "NONINTERSECTING:Z=@";

            ConfigParserModel parser = new ConfigParserModel(testLines);

            // Act.
            bool parseOk = parser.TryParseConfiguration();

            // Assert.
            Assert.IsFalse(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 24);
        }

        /// <summary>
        /// Test for validation errors whilst parsing a configuration file with range errors.
        /// </summary>
        [TestMethod()]
        public void TryParseConfigurationRangeInvalidTest()
        {
            // Arrange.
            string[] testLines = new string[54];
            testLines[0] = "GROUPSPERCROZZLELIMIT=-1";
            testLines[1] = "POINTSPERWORD=-1";
            testLines[2] = "INTERSECTING:A=-1";
            testLines[3] = "INTERSECTING:B=1";
            testLines[4] = "INTERSECTING:C=1";
            testLines[5] = "INTERSECTING:D=1";
            testLines[6] = "INTERSECTING:E=1";
            testLines[7] = "INTERSECTING:F=1";
            testLines[8] = "INTERSECTING:G=1";
            testLines[9] = "INTERSECTING:H=1";
            testLines[10] = "INTERSECTING:I=1";
            testLines[11] = "INTERSECTING:J=1";
            testLines[12] = "INTERSECTING:K=1";
            testLines[13] = "INTERSECTING:L=1";
            testLines[14] = "INTERSECTING:M=1";
            testLines[15] = "INTERSECTING:N=1";
            testLines[16] = "INTERSECTING:O=1";
            testLines[17] = "INTERSECTING:P=1";
            testLines[18] = "INTERSECTING:Q=1";
            testLines[19] = "INTERSECTING:R=1";
            testLines[20] = "INTERSECTING:S=1";
            testLines[21] = "INTERSECTING:T=1";
            testLines[22] = "INTERSECTING:U=1";
            testLines[23] = "INTERSECTING:V=1";
            testLines[24] = "INTERSECTING:W=1";
            testLines[25] = "INTERSECTING:X=1";
            testLines[26] = "INTERSECTING:Y=1";
            testLines[27] = "INTERSECTING:Z=1";
            testLines[28] = "NONINTERSECTING:A=1";
            testLines[29] = "NONINTERSECTING:B=1";
            testLines[30] = "NONINTERSECTING:C=1";
            testLines[31] = "NONINTERSECTING:D=1";
            testLines[32] = "NONINTERSECTING:E=1";
            testLines[33] = "NONINTERSECTING:F=1";
            testLines[34] = "NONINTERSECTING:G=1";
            testLines[35] = "NONINTERSECTING:H=1";
            testLines[36] = "NONINTERSECTING:I=1";
            testLines[37] = "NONINTERSECTING:J=1";
            testLines[38] = "NONINTERSECTING:K=1";
            testLines[39] = "NONINTERSECTING:L=1";
            testLines[40] = "NONINTERSECTING:M=1";
            testLines[41] = "NONINTERSECTING:N=1";
            testLines[42] = "NONINTERSECTING:O=1";
            testLines[43] = "NONINTERSECTING:P=1";
            testLines[44] = "NONINTERSECTING:Q=1";
            testLines[45] = "NONINTERSECTING:R=1";
            testLines[46] = "NONINTERSECTING:S=1";
            testLines[47] = "NONINTERSECTING:T=1";
            testLines[48] = "NONINTERSECTING:U=1";
            testLines[49] = "NONINTERSECTING:V=1";
            testLines[50] = "NONINTERSECTING:W=1";
            testLines[51] = "NONINTERSECTING:X=1";
            testLines[52] = "NONINTERSECTING:Y=1";
            testLines[53] = "NONINTERSECTING:Z=1";

            ConfigParserModel parser = new ConfigParserModel(testLines);

            // Act.
            bool parseOk = parser.TryParseConfiguration();

            // Assert.
            Assert.IsFalse(parseOk);
            Assert.IsTrue(parser.ValidationErrors.Count == 4);
        }
    }
}