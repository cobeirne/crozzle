using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrozzleGame.Controllers;

namespace CrozzleGame
{
    /// <summary>
    /// This class is initialised on application startup. It includes the Main() function which is
    /// executed on startup.  It is also used to intialise global objects.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// This Crozzle Controller is initialised during program startup and lives until program 
        /// close. It handles all crozzle business functions.
        /// </summary>
        public static CrozzleController CrozzleControl = new CrozzleController();

        /// <summary>
        /// This function is executed on application startup to setup the application and call the
        /// main form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
