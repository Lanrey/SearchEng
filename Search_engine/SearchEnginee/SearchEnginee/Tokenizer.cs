using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace SearchEnginee
{

    /// <summary>
    /// This class splits a string with some delimiter characters
    /// </summary>
    public class Tokenizer
    {

        public Tokenizer()
        {
            
        }

        /// <summary>
        /// This method splits a given string with some delimiter characters
        /// and returns an array of the splitted string
        /// </summary>
        /// <param name="text">string to be splitted</param>
        /// <returns>an array of the splitted string</returns>
        public static string[] tokenizeString(string text)
        {
            Contract.Requires<NullReferenceException>(text != null);
            Contract.Requires<ArgumentException>(text != "");

            char[] splitChar = { ' ', ',', '.', '?', ':', ';', '/', '{', '}' };
            string[] tokenizedText = text.Split(splitChar);
            return tokenizedText;
        }

    }
}
