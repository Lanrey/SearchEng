using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace SearchEnginee
{

    /// <summary>
    /// This class models a document.
    /// It contains the document id, the path to the document, and its rank value
    /// </summary>

    [Serializable]
    public class Doocument
    {

        readonly int id;
        readonly string path;
        double rankValue;

        /// <summary>
        /// This set/initializes the documents id and path
        /// </summary>
        /// <param name="id">document id number</param>
        /// <param name="path">the path to the documents location</param>
        public Doocument(int id,string path) {
            this.id = id;
            this.path = path;
            rankValue = 0;
        }

        /// <summary>
        /// This returns the documents id number
        /// </summary>
        /// <returns>document id number</returns>
        public int getID() {
            return id;
        }

        /// <summary>
        /// This returns the documents path
        /// </summary>
        /// <returns>the path to the documents location</returns>
        public string getPath() {
            return path;
        }

        /// <summary>
        /// This returns the document rank value after it must have been matched or ranked with a query
        /// </summary>
        /// <returns>rank value</returns>
        public double getRankValue() {
            return rankValue;
        }

        /// <summary>
        /// This sets the document rank value after it must have been matched or ranked with a query
        /// </summary>
        /// <param name="rankValue">rank value</param>
        public void setRankValue(double rankValue) {
            Contract.Requires<ArgumentException>(rankValue >= 0);
            this.rankValue = rankValue;
        }

        /// <summary>
        /// This returns the file name of the document
        /// </summary>
        /// <returns>file name</returns>
        public string getFileName() {
            int lastIndexOfSlash = path.LastIndexOf('\\');
            return path.Substring(lastIndexOfSlash,path.Length);
        }

        /// <summary>
        /// This returns the contents or text of the document
        /// </summary>
        /// <returns>Content or text of the document</returns>
        public string getText() {
            FileReader fileReader = new FileReader();
            string text = fileReader.readFile(path);
            return text;
        }

    }
}
