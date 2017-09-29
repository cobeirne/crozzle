/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 1
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       28/08/16
/// </summary>

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CrozzleGame.Models
{
    /// <summary>
    /// This class models a time stamped log book containing both information and error entries.
    /// The log buffer can be saved to file or returned as a HMTL document.
    /// </summary>
    public class LogModel
    {
        #region Class Fields

        /// <summary>
        /// This mutex provides exclusive access to the the log buffer i.e. to add or read events.
        /// </summary>
        Mutex LogEditMutex;

        #endregion

        #region Class Properties

        /// <summary>
        /// The collection of time stamped log entries currently buffered.
        /// </summary>
        public List<KeyValuePair<DateTime, string>> LogBuffer { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Log Model constructor, is called on object creation and initialises object properties.
        /// </summary>
        public LogModel()
        {
           this.LogEditMutex = new Mutex();
           this.LogBuffer = new List<KeyValuePair<DateTime, string>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function adds a time stamped log into the log log buffer.
        /// </summary>
        /// <param name="logText">The log event description.</param>
        public void AddLog(string logText)
        {
            // Wait for exclusive ownership of the Log Buffer.
            LogEditMutex.WaitOne();

            this.LogBuffer.Add(new KeyValuePair<DateTime, string> (DateTime.Now, logText));

            // Release Log Buffer ownership. 
            LogEditMutex.ReleaseMutex();
        }

        /// <summary>
        /// This function builds the current log buffer into a html table.
        /// </summary>
        /// <returns>All buffered logs in a html formated string.</returns>
        public string GetLogBufferHtml()
        {
            StringBuilder logsHtmlBuilder = new StringBuilder();

            // Create and style a HTML table.
            logsHtmlBuilder.Append("<html><body><table style='font-size:x-small;font-family=sans-serif;'>");

            // Wait for exclusive ownership of the Log Buffer.
            LogEditMutex.WaitOne();

            // Add each log as a new HTML row in the table.
            foreach (var log in this.LogBuffer)
            {
                logsHtmlBuilder.AppendFormat("<tr><td width='22%' style='vertical-align:text-top;'>{0}</td><td style='vertical-align:text-top;padding-left:5px;'>{1}</td></tr>", 
                    log.Key, log.Value);
            }

            // Release Log Buffer ownership. 
            LogEditMutex.ReleaseMutex();

            // Close the HTML table.
            logsHtmlBuilder.Append("</table></body></html>");

            // Return the HTML formated text as a string.
            return logsHtmlBuilder.ToString();
        }

        #endregion
    }
}
