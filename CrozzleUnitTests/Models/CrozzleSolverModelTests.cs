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
    /// This is a test class for the CrozzleSolverModel Class.
    /// </summary>
    [TestClass()]
    public class CrozzleSolverModelTests
    {
        /// <summary>
        /// Test for an easy crozzle solution that meets a low benchmark.
        /// </summary>
        [TestMethod()]
        public void SolveSmallEasyCrozzleTest()
        {
            // Arrange.
            ConfigParserModel configParser = new ConfigParserModel(BuildEasyConfiguration());                       
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "EASY,5,4,4,1,1";
            CrozzleLines[1] = "ABCD,AEFG,GHIJ,DKLJ";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(false);
            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;

            CrozzleSolverModel solver = new CrozzleSolverModel(crozzle);

            // Act.
            solver.SolveCrozzle();

            // Assert.
            Assert.IsTrue(solver.BestSolutionNode.NodeScore == 7);
        }

        /// <summary>
        /// Test for an easy crozzle solution that meets a high benchmark.
        /// </summary>
        [TestMethod()]
        public void SolveBigEasyCrozzleTest()
        {
            // Arrange.
            ConfigParserModel configParser = new ConfigParserModel(BuildEasyConfiguration());
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "EASY,200,10,10,0,0";
            CrozzleLines[1] = "AARON,AL,ALAN,ALEXANDRA,ALI,AMY,ANDREW,ANN,ANTHONY,AXL,AYDEN,BELINDA,BEN,BETTY,BEV,BEVERLEY,BILL,BLAZE,BOB,BOBBY,BORIS,BRENDA,BRUNO,BYNUM,CAM,CATHERINE,CHAZ,CLIVE,CON,CONNOR,CONWAY,CRUZ,DALE,DAN,DAVE,DAVID,DAVY,DAWN,DENNIS,DENZIL,DOM,DON,DREMA,DREW,ED,EDDY,EDITH,EDWARD,ELIZABET,ELVIS,EMMA,ENZO,ERICA,EVA,EVAN,EZZARD,FAWN,FRED,GARY,GIOVANNY,GLENDA,GRAHAM,GWEN,GWYN,HOPE,HUEY,HUXLEY,IAN,ISA,IVAN,IVON,IZA,JACK,JAKE,JAQUELINE,JILL,JIM,JOHN,KARL,KAY,KAYNE,KELVIN,KEVIN,LE,LEO,LEON,LES,LEWIS,LILA,LIZ,LOC,LOU,LU,LUCILE,LULU,MADALYNN,MAL,MARGARET,MARK,MARY,MATHEW,MATTHEW,MAVIS,MAY,MICHELLE,MITZI,MURRAY,NED,NEIL,NOE,NOEL,OLIVE,OMAR,ORAN,OSCAR,OWEN,OWENS,OZZIE,PAM,PENELOPE,PENNY,PETER,PHEBE,QUINCY,QUINTY,RACHELLE,RAS,RAY,REMI,RENTON,REX,RICHARD,ROBERT,ROD,ROGER,RON,ROWAN,ROY,ROZY,RYDER,SALLY,SAM,SARAH,SHELDON,SPENCER,STEPHEN,STEVE,STEVEN,SUE,SUZY,SYLVIA,TAMMY,TEDDY,TERRY,TIMOTHY,TOBY,TODD,TOM,TRACEY,TREVOR,TY,TYRIANNE,VELVET,VERA,VERN,VIC,VICK,VICKY,VICTOR,WADE,WALLY,WALT,WENDY,WES,WESLEY,WHITNEY,WILL,WILLY,WILMA,WYNNIE,XAVIER,XENA,XIA,XIANG,YEE,YOEL,YOKO,YOLANDA,YOSEF,ZACH,ZACK,ZARA,ZELDA,ZENA,ZETA,ZEUS,ZILI,ZOLA,ZORBA,ZUZANNY";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(false);
            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;
            
            CrozzleSolverModel solver = new CrozzleSolverModel(crozzle);

            // Act.
            solver.SolveCrozzle();

            // Assert.
            Assert.IsTrue(solver.BestSolutionNode.NodeScore == 51);
        }

        /// <summary>
        /// Test for a medium crozzle solution that meets a low benchmark.
        /// </summary>
        [TestMethod()]
        public void SolveSmallMediumCrozzleTest()
        {
            // Arrange.
            ConfigParserModel configParser = new ConfigParserModel(BuildMediumConfiguration());
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "MEDIUM,5,4,4,1,1";
            CrozzleLines[1] = "CAT,COW,WHAT,COPY,YOUR";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(false);
            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;
            
            CrozzleSolverModel solver = new CrozzleSolverModel(crozzle);
            
            // Act.
            solver.SolveCrozzle();

            // Assert.
            Assert.IsTrue(solver.BestSolutionNode.NodeScore > 112);
        }

        /// <summary>
        /// Test for a medium crozzle solution that meets a high benchmark.
        /// </summary>
        [TestMethod()]
        public void SolveBigMediumCrozzleTest()
        {
            // Arrange.
            ConfigParserModel configParser = new ConfigParserModel(BuildMediumConfiguration());
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "MEDIUM,300,10,10,0,0";
            CrozzleLines[1] = "AARON,AL,ALAN,ALEX,ALI,AMY,ANDREW,ANN,ARJUN,ARMANI,ARMIDA,ARTHUR,ARVIL,ARYANA,ASHELY,ASHLEA,ASHLEE,ASHLEY,ASHLIE,ASHLYN,ASHTON,ASHTYN,ASTRID,ATHENA,AUBREE,AUBREY,AUBRIE,AUDREY,AURORA,AURORE,AUSTIN,AXL,AYDEN,BELINDA,BEN,BETTY,BEV,BEVERLEY,BILL,BLAZE,BOB,BOBBY,BORIS,BRENDA,BRIGGS,BRODIE,BROGAN,BROOKS,BRUNO,BRYANT,BRYCEN,BRYSEN,BRYSON,BRYTON,BUDDIE,BUFORD,BURLEY,BURNEY,BURNIE,BURREL,BURTON,BUSTER,BUTLER,BYNUM,CAESAR,CAIDEN,CALLAN,CAM,CARA,CHAZ,CLIVE,CON,CONNOR,CONWAY,CRUZ,DALE,DAN,DAVE,DAVID,DAVY,DAWN,DAYANA,DEANNA,DEANNE,DEASIA,DEBBIE,DEBBRA,DEBERA,DEBORA,DEBRAH,DEEANN,DEEDEE,DEETTA,DEIDRA,DEIDRE,DELCIE,DELIAH,DELILA,DELINA,DELISA,DELLAR,DELLIA,DELLIE,DELOIS,DELORA,DELPHA,DELSIE,DENNIS,DENZIL,DOM,DON,DOT,DREW,ED,EDDY,EDITH,EDWARD,ELIZABETH,ELVIS,EMMA,ENZO,ERICA,ERLENE,ERLINE,ERMINA,ERMINE,ERNEST,ERYKAH,ESTELA,ESTELL,ESTHER,ETHYLE,EUDORA,EUGENE,EUNICE,EVA,EVALYN,EVAN,EVELIN,EVELYN,EVERLY,EVETTE,EVONNE,EZZARD,FALLON,FANNIE,FANNYE,FARRAH,FATIMA,FAWN,FELICE,FELIPA,FINLEY,FRED,GARY,GIOVANNY,GLENDA,GRAHAM,GWEN,GWYN,HARVY,HOWARD,HUXLEY,IAN,IVA,IVAN,IVON,IZA,JACK,JAKE,JAQUELINE,JILL,JO,JUDY,KARL,KAY,KAYLAH,KAYLAN,KAYLEE,KAYLEN,KAYNE,KELVIN,KEVIN,LE,LEO,LEON,LES,LEWIS,LILA,LIZ,LOC,LOU,LU,LUCILE,LULU,MADALYNN,MAL,MARGARET,MARK,MARY,MATHEW,MATTHEW,MAVIS,MAY,MICHELLE,MITZI,MURRAY,NAOMI,NED,NOE,NOEL,OLIVE,OMAR,ORAN,OSCAR,OWEN,OWENS,OZZIE,PAM,PENELOPE,PENNY,PETER,PHEBE,QUINCY,QUINTY,RACHELLE,RAE,RAY,REMY,RENTON,REX,RICHARD,ROBERT,ROD,ROGER,RON,ROWAN,ROY,ROZY,RYLEY,SALLY,SAM,SARAH,SHELDON,SOPHIE,STEPHEN,STEVE,STEVEN,SUE,SUZY,SYLVIA,TAMMY,TED,TERRY,TIMOTHY,TOBY,TODD,TOM,TRACEY,TREVOR,TY,TYRIANNE,VELVET,VERA,VELDA,VIC,VICK,VICKY,VICTOR,WADE,WALLY,WALT,WENDY,WES,WESLEY,WHITNEY,WILL,WILLY,WILMA,WYNNIE,XAVIER,XENA,XIA,XIANG,YEE,YOEL,YOKO,YOLANDA,YORK,ZACH,ZACK,ZARA,ZELIA,ZENA,ZETA,ZEUS,ZILI,ZOLA,ZORBA,ZUZANNY";
            
            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(false);
            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;
            
            CrozzleSolverModel solver = new CrozzleSolverModel(crozzle);

            // Act.
            solver.SolveCrozzle();

            // Assert.
            Assert.IsTrue(solver.BestSolutionNode.NodeScore == 665);
        }

        /// <summary>
        /// Test for a hard crozzle solution that meets a low benchmark.
        /// </summary>
        [TestMethod()]
        public void SolveSmallHardCrozzleTest()
        {
            // Arrange.
            ConfigParserModel configParser = new ConfigParserModel(BuildHardConfiguration());
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "HARD,4,6,6,2,2";
            CrozzleLines[1] = "AND,AM,SNATCH,SIGMA";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(false);
            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;

            CrozzleSolverModel solver = new CrozzleSolverModel(crozzle);

            // Act.
            solver.SolveCrozzle();

            // Assert.
            Assert.IsTrue(solver.BestSolutionNode.NodeScore > 1);
        }

        /// <summary>
        /// Test for a hard crozzle solution that meets a high benchmark.
        /// </summary>
        [TestMethod()]
        public void SolveBigHardCrozzleTest()
        {
            // Arrange.
            ConfigParserModel configParser = new ConfigParserModel(BuildHardConfiguration());
            configParser.TryParseConfiguration();

            string[] CrozzleLines = new string[4];
            CrozzleLines[0] = "HARD,400,10,10,0,0";
            CrozzleLines[1] = "AARON,AL,ALAN,ALEX,ALEXANDRA,ALI,AMY,ANDREW,ANN,ARLYNE,ARMANI,ARMIDA,ARTHUR,ARVIL,ARYANA,ASHELY,ASHLEA,ASHLEE,ASHLEY,ASHLIE,ASHLYN,ASHTON,ASHTYN,ASTRID,ATHENA,AUBREE,AUBREY,AUBRIE,AUDREY,AURORA,AURORE,AUSTIN,AXL,AYDEN,BELINDA,BEN,BETTY,BEV,BEVERLEY,BILL,BLAZE,BOB,BOBBY,BORIS,BRENDA,BRIGGS,BRODIE,BROGAN,BROOKS,BRUNO,BRYANT,BRYCEN,BRYSEN,BRYSON,BRYTON,BUDDIE,BUFORD,BURLEY,BURNEY,BURNIE,BURREL,BURTON,BUSTER,BUTLER,BYNUM,CAESAR,CAIDEN,CALLAN,CAM,CARA,CHAZ,CLIVE,CON,CONNOR,CONWAY,CRUZ,DALE,DAN,DAVE,DAVID,DAVY,DAX,DAYANA,DEANNA,DEANNE,DEASIA,DEBBIE,DEBBRA,DEBERA,DEBORA,DEBORRAH,DEBRAH,DEEANN,DEEDEE,DEETTA,DEIDRA,DEIDRE,DELCIE,DELIAH,DELILA,DELINA,DELISA,DELLAR,DELLIA,DELLIE,DELOIS,DELORA,DELPHA,DELPHINE,DELSIE,DEMETRIA,DENNIS,DENZIL,DESTINEE,DESTINEY,DEYANIRA,DOM,DOMENICA,DOMINQUE,DON,DORATHEA,DOROTHEA,DREMA,DREW,DRUCILLA,DRUSILLA,ED,EDDY,EDITH,EDWARD,ELEANORA,ELEANORE,ELEONORA,ELEONORE,ELFRIEDA,ELIZABET,ELIZABETH,ELIZBETH,ELVIS,EMMA,EMMALINE,EMMALYNN,EMMELINE,ENZO,ERICA,ERLENE,ERLINE,ERMINA,ERMINE,ERNEST,ERYKAH,ESTEFANI,ESTEFANY,ESTELA,ESTELL,ESTHER,ESTRELLA,ETHELENE,ETHYLE,EUDORA,EUGENE,EUNICE,EUPHEMIA,EVA,EVALYN,EVAN,EVE,EVELYN,EVERLY,EVETTE,EVONNE,EZZARD,FALLON,FANNIE,FANNYE,FARRAH,FATIMA,FAWN,FELICE,FELICITY,FELIPA,FERNANDA,FILOMENA,FINLEY,FRED,GARY,GIOVANNY,GLENDA,GRAHAM,GWEN,GWYN,HARRISON,HARTWELL,HARVY,HERSCHEL,HERSHELL,HEZEKIAH,HILLIARD,HOWARD,HUMBERTO,HUMPHREY,HUXLEY,IAN,IGNATIUS,IMMANUEL,ISA,IVAN,IVON,IZA,JACK,JAKE,JAMARCUS,JAMARION,JAQUELINE,JEDEDIAH,JEDIDIAH,JEFFEREY,JENNIFER,JENNINGS,JERAMIAH,JEREMIAH,JERIMIAH,JERMAINE,JILL,JIM,JOHATHAN,JOHN,JOHNPAUL,JONATHAN,JONATHON,JOSELUIS,JOSEPHUS,KARL,KATHLEEN,KAY,KAYLAH,KAYLAN,KAYLEE,KAYLEN,KAYNE,KELVIN,KENDRICK,KENYATTA,KEVIN,KEYSHAWN,LE,LEO,LEON,LES,LEW,LEWIS,LIZ,LOC,LOU,LU,LUCILE,LULU,MADALYNN,MAL,MARGARET,MARIANNA,MARIANNE,MARIBETH,MARICELA,MARIETTA,MARILYNN,MARISELA,MARJORIE,MARK,MARLEIGH,MARQUITA,MARY,MARYANNE,MARYBETH,MARYJANE,MATHEW,MATHILDA,MATHILDE,MATTHEW,MAVIS,MAY,MAYBELLE,MCKENZIE,MCKINLEY,MECHELLE,MELISSIA,MELLISSA,MERCEDES,MEREDITH,MERRILEE,MICHAELA,MICHAELE,MICHELLE,MIGDALIA,MILAGROS,MINERVIA,MISSOURI,MITZI,MURRAY,NANNETTE,NAOMI,NATHALIA,NATHALIE,NEIL,NICHELLE,NICHOLAS,NOE,NOEL,OLIVE,OMAR,ORAN,OSCAR,OWEN,OWENS,OZZIE,PAM,PATIENCE,PATRICIA,PAULETTA,PENELOPE,PENNY,PETER,PHEBE,QUINCY,QUINTY,RACHELLE,RAS,RAY,REMI,RENTON,REX,RICHARD,ROBERT,ROD,ROGER,RON,ROWAN,ROY,ROZY,RYDER,SALLY,SAM,SARAH,SHELDON,SOPHIE,STEPHEN,STEVE,STEVEN,SUE,SUZY,SYLVIA,TAMMY,TED,TERRY,TEX,TOBY,TODD,TOM,TRACEY,TREVOR,TY,TYRIANNE,VELVET,VERA,VERN,VIC,VICK,VICKY,VICTOR,WADE,WALLY,WALT,WENDY,WES,WESLEY,WHITNEY,WILL,WILLY,WILMA,WYNNIE,XAVIER,XENA,XIA,XIANG,YEE,YOEL,YOKO,YOLANDA,YORK,ZACH,ZACK,ZARA,ZED,ZENA,ZETA,ZITA,ZILI,ZOLA,ZORBA,ZUZANNY";

            CrozzleParserModel crozzleParser = new CrozzleParserModel(CrozzleLines);
            crozzleParser.TryParseCrozzle(false);
            CrozzleModel crozzle = crozzleParser.Crozzle;
            crozzle.Configuration = configParser.Configuration;
            
            CrozzleSolverModel solver = new CrozzleSolverModel(crozzle);

            // Act.
            solver.SolveCrozzle();

            // Assert.
            Assert.IsTrue(solver.BestSolutionNode.NodeScore == 499);
        }

        /// <summary>
        /// Build an Easy Configuration for test arrangement.
        /// </summary>
        /// <returns>An array of configuration file lines.</returns>
        private string[] BuildEasyConfiguration()
        {
            string[] testLines = new string[54];
            testLines[0] = "GROUPSPERCROZZLELIMIT=1000";
            testLines[1] = "POINTSPERWORD=0";
            testLines[2] = "INTERSECTING:A=1";
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

            return testLines;
        }

        /// <summary>
        /// Build a Medium Configuration for test arrangement.
        /// </summary>
        /// <returns>An array of configuration file lines.</returns>
        private string[] BuildMediumConfiguration()
        {
            string[] testLines = new string[54];

            testLines[0] = "GROUPSPERCROZZLELIMIT=1000";
            testLines[1] = "POINTSPERWORD=0";
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
            testLines[28] = "NONINTERSECTING:A=1";
            testLines[29] = "NONINTERSECTING:B=2";
            testLines[30] = "NONINTERSECTING:C=3";
            testLines[31] = "NONINTERSECTING:D=4";
            testLines[32] = "NONINTERSECTING:E=5";
            testLines[33] = "NONINTERSECTING:F=6";
            testLines[34] = "NONINTERSECTING:G=7";
            testLines[35] = "NONINTERSECTING:H=8";
            testLines[36] = "NONINTERSECTING:I=9";
            testLines[37] = "NONINTERSECTING:J=10";
            testLines[38] = "NONINTERSECTING:K=11";
            testLines[39] = "NONINTERSECTING:L=12";
            testLines[40] = "NONINTERSECTING:M=13";
            testLines[41] = "NONINTERSECTING:N=14";
            testLines[42] = "NONINTERSECTING:O=15";
            testLines[43] = "NONINTERSECTING:P=16";
            testLines[44] = "NONINTERSECTING:Q=17";
            testLines[45] = "NONINTERSECTING:R=18";
            testLines[46] = "NONINTERSECTING:S=19";
            testLines[47] = "NONINTERSECTING:T=20";
            testLines[48] = "NONINTERSECTING:U=21";
            testLines[49] = "NONINTERSECTING:V=22";
            testLines[50] = "NONINTERSECTING:W=23";
            testLines[51] = "NONINTERSECTING:X=24";
            testLines[52] = "NONINTERSECTING:Y=25";
            testLines[53] = "NONINTERSECTING:Z=26";

            return testLines;
        }

        /// <summary>
        /// Build a Hard Configuration for test arrangement.
        /// </summary>
        /// <returns>An array of configuration file lines.</returns>
        private string[] BuildHardConfiguration()
        {
            string[] testLines = new string[54];

            testLines[0] = "GROUPSPERCROZZLELIMIT=1";
            testLines[1] = "POINTSPERWORD=10";
            testLines[2] = "INTERSECTING:A=1";
            testLines[3] = "INTERSECTING:B=2";
            testLines[4] = "INTERSECTING:C=2";
            testLines[5] = "INTERSECTING:D=2";
            testLines[6] = "INTERSECTING:E=1";
            testLines[7] = "INTERSECTING:F=2";
            testLines[8] = "INTERSECTING:G=2";
            testLines[9] = "INTERSECTING:H=2";
            testLines[10] = "INTERSECTING:I=1";
            testLines[11] = "INTERSECTING:J=4";
            testLines[12] = "INTERSECTING:K=4";
            testLines[13] = "INTERSECTING:L=4";
            testLines[14] = "INTERSECTING:M=4";
            testLines[15] = "INTERSECTING:N=4";
            testLines[16] = "INTERSECTING:O=1";
            testLines[17] = "INTERSECTING:P=8";
            testLines[18] = "INTERSECTING:Q=8";
            testLines[19] = "INTERSECTING:R=8";
            testLines[20] = "INTERSECTING:S=8";
            testLines[21] = "INTERSECTING:T=8";
            testLines[22] = "INTERSECTING:U=1";
            testLines[23] = "INTERSECTING:V=16";
            testLines[24] = "INTERSECTING:W=16";
            testLines[25] = "INTERSECTING:X=32";
            testLines[26] = "INTERSECTING:Y=32";
            testLines[27] = "INTERSECTING:Z=64";
            testLines[28] = "NONINTERSECTING:A=0";
            testLines[29] = "NONINTERSECTING:B=0";
            testLines[30] = "NONINTERSECTING:C=0";
            testLines[31] = "NONINTERSECTING:D=0";
            testLines[32] = "NONINTERSECTING:E=0";
            testLines[33] = "NONINTERSECTING:F=0";
            testLines[34] = "NONINTERSECTING:G=0";
            testLines[35] = "NONINTERSECTING:H=0";
            testLines[36] = "NONINTERSECTING:I=0";
            testLines[37] = "NONINTERSECTING:J=0";
            testLines[38] = "NONINTERSECTING:K=0";
            testLines[39] = "NONINTERSECTING:L=0";
            testLines[40] = "NONINTERSECTING:M=0";
            testLines[41] = "NONINTERSECTING:N=0";
            testLines[42] = "NONINTERSECTING:O=0";
            testLines[43] = "NONINTERSECTING:P=0";
            testLines[44] = "NONINTERSECTING:Q=0";
            testLines[45] = "NONINTERSECTING:R=0";
            testLines[46] = "NONINTERSECTING:S=0";
            testLines[47] = "NONINTERSECTING:T=0";
            testLines[48] = "NONINTERSECTING:U=0";
            testLines[49] = "NONINTERSECTING:V=0";
            testLines[50] = "NONINTERSECTING:W=0";
            testLines[51] = "NONINTERSECTING:X=0";
            testLines[52] = "NONINTERSECTING:Y=0";
            testLines[53] = "NONINTERSECTING:Z=0";

            return testLines;
        }

    }
}