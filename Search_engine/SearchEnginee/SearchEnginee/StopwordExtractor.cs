using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEnginee
{
    /// <summary>
    /// This class removes stopwords from a string array
    /// </summary>
    public class StopwordExtractor
    {

        public StopwordExtractor() { 

        }

        /// <summary>
        /// This method performs the main function of removing the stopwords from an array of string
        /// </summary>
        /// <param name="tokenizedString">array of string</param>
        /// <returns>List free from stopwords</returns>
        public static List<string> work(string[] tokenizedString) {

            List<string> wordsList = new List<string>(); 
            foreach (string element in tokenizedString)
            {
                if (!isStopWord(element))
                {
                    wordsList.Add(element);
                }
            }

            return wordsList;
        }

        /// <summary>
        /// Checks if a particular token or word is a stopword
        /// </summary>
        /// <param name="token">word to be checked if its a stopword</param>
        /// <returns>true if word is a stopword, false if its not</returns>
        static bool isStopWord(string token)
        {
            List<string> stopwords = new List<string>() { "to", "from", "in", "on", "a", "an", "the", "and","not",
                                                           "below" , "over","onto" , "off","into" , "unless" , "though",
                                                            "may" , "be" , "will" , "has" , "between", "itself" , "behind",
                                                            "where" , "wherever" , "front" , "other" , "while" , "both",
                                                            "either" , "neither" , "whether" , "which"};
            foreach (string stopword in stopwords)
            {
                if (token.ToLower() == stopword)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
