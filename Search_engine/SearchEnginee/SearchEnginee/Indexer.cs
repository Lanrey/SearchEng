using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEnginee
{
    /// <summary>
    /// This class houses the indexer and the operations that can be performed on it.
    /// Operations like adding to the indexer and removing from the indexer
    /// </summary>

    [Serializable]
    class Indexer
    {

        Dictionary<string, Dictionary<int, uint>> dictionary;

        /// <summary>
        /// Initializes the indexer with an empty dictionary
        /// </summary>
        public Indexer() {
            dictionary = new Dictionary<string, Dictionary<int, uint>>();
        }

        /// <summary>
        /// Returns the indexer which is in form of a dictionary
        /// </summary>
        /// <returns>the indexer</returns>
        public Dictionary<string, Dictionary<int, uint>> getDictionary()
        {
            return dictionary;
        }

        /// <summary>
        /// Adds a document to the indexer
        /// </summary>
        /// <param name="document">Document to be indexed or added to the indexer</param>
        public void add(Doocument document) {
            int id = document.getID();
            string text = document.getText();
            add(getTokenizedKeywords(text),id);
        }

        List<string> getTokenizedKeywords(string text)
        {
            string[] tokenizedString = Tokenizer.tokenizeString(text);
            List<string> words = StopwordExtractor.work(tokenizedString);

            return words;
        }


        void add(List<string> words, int id)
        {
            Dictionary<int, uint> dict = new Dictionary<int, uint>();
            foreach (string element in words)
            {
                if (dictionary.ContainsKey(element))
                {
                    if (dictionary[element].ContainsKey(id))
                    {
                        dictionary[element][id]++;
                    }
                    else
                    {
                        dictionary[element].Add(id, 1);
                    }
                }
                else
                {
                    Dictionary<int, uint> dick = new Dictionary<int, uint>();
                    dick.Add(id, 1);
                    dictionary.Add(element, dick);
                }

            }

            foreach (KeyValuePair<string, Dictionary<int, uint>> pair in dictionary)
            {
                Console.WriteLine(pair.Key);
                foreach (KeyValuePair<int, uint> paired in pair.Value)
                {
                    Console.WriteLine("{0,-20} --> {1,-20}", paired.Key, paired.Value);
                }
            }

        }

        /// <summary>
        /// Removes a document from the indexer by removing all of the id of that document contained on the indexer
        /// </summary>
        /// <param name="id">the id of the document to be removed from the indexer</param>
        public void remove(int id) {
            Dictionary<string, Dictionary<int, uint>> copy = getDictionaryDeepCopy();
            foreach (string element in copy.Keys)
            {
                if(copy[element].ContainsKey(id)){
                    dictionary[element].Remove(id);
                }
            }
        }

        Dictionary<string, Dictionary<int, uint>> getDictionaryDeepCopy() {
            Dictionary<string, Dictionary<int, uint>> copy = new Dictionary<string, Dictionary<int, uint>>();
            foreach(string element in dictionary.Keys){
                copy.Add(element,dictionary[element]);
            }

            return copy;
        }

    }
}
