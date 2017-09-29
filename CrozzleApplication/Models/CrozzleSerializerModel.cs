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
    /// This class models the process for serializing the properties of a crozzle to file.
    /// </summary>
    public class CrozzleSerializerModel
    {
        #region Class Properties

        /// <summary>
        /// The crozzle to be serialized.
        /// </summary>
        public CrozzleModel Crozzle { get; set; }

        #endregion

        #region Class Constructor

        /// <summary>
        /// Crozzle Serializer constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="crozzle">The crozzle to be serialized.</param>
        public CrozzleSerializerModel(CrozzleModel crozzle)
        {
            this.Crozzle = crozzle;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function attempts to serialize the crozzle properties into text based Crozzle File
        /// format.
        /// </summary>
        /// <returns>Formated crozzle file content.</returns>
        public string Serialize()
        {
            StringBuilder crozzleText = new StringBuilder();

            // Serialise header line.
            crozzleText.AppendFormat("{0},{1},{2},{3},{4},{5}",
                this.Crozzle.Difficulty,
                this.Crozzle.WordPoolSize,
                this.Crozzle.Rows,
                this.Crozzle.Columns,
                this.Crozzle.HorizontalWords,
                this.Crozzle.VerticalWords);

            // Append the header to the file content.
            crozzleText.AppendLine();

            // Serialize word pool
            foreach (string word in this.Crozzle.WordPool)
            {
                if (word == this.Crozzle.WordPool.First())
                {
                    crozzleText.AppendFormat("{0}", word);
                }
                else
                {
                    crozzleText.AppendFormat(",{0}", word);
                }
            }

            // Order the word list by orientation.
            List<WordModel> wordList =  this.Crozzle.WordList.OrderBy(w => w.Orientation).ToList();

            // For each in word list, serialize word
            foreach(WordModel word in wordList)
            {
                crozzleText.AppendLine();

                crozzleText.AppendFormat("{0},{1},{2},{3}",
                    word.Orientation,
                    word.StartRow,
                    word.StartColumn,
                    word.Word);
            }

            return crozzleText.ToString();
        }

        #endregion
    }
}
