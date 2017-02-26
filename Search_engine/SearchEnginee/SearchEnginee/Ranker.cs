using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEnginee
{

    /// <summary>
    /// This class ranks a list of documents on the indexer according to a query
    /// It gives each document present a rank value which tells the order of relevance or closeness
    /// to the specified query
    /// </summary>
    class Ranker
    {

        Dictionary<string, Dictionary<int, uint>> dictionary;

        /// <summary>
        /// Initializes the indexer
        /// </summary>
        public Ranker() {
            dictionary = Storage.getStorageDetails().getIndexer().getDictionary();
        }

        /// <summary>
        /// This methods performs the main function of ranking the documents
        /// It collects the query and uses it get the rank value of each document
        /// </summary>
        /// <param name="query"></param>
        public void Rank(string query)
        {
            string[] tokenizedQuery = Tokenizer.tokenizeString(query);
            List<string> QW = StopwordExtractor.work(tokenizedQuery);

            List<Doocument> documentList = Storage.getStorageDetails().getDocumentList();
            for (int i = 0; i < documentList.Count; i++)
            {
                int id = documentList[i].getID();
                double a = totalFrequency(QW,id);
                double b = magnitudeRoot(QW,id);
                if (b != 0)
                {
                    documentList[i].setRankValue(a / b);
                }
                else
                {
                    documentList[i].setRankValue(0);
                }

            }
            
        }

        double totalFrequency(List<string> tokenizedQuery, int docID)
        {
            double total = 0;
            foreach (string element in tokenizedQuery)
            {
                if (dictionary.ContainsKey(element))
                {
                    if (dictionary[element].ContainsKey(docID))
                    {
                        total = total + dictionary[element][docID] * getInverseDF(element);
                    }
                }
            }
            return total;
        }

        double magnitudeRoot(List<string> tokenizedQuery, int docID)
        {
            double total = 0;
            foreach (string element in tokenizedQuery)
            {
                if (dictionary.ContainsKey(element))
                {
                    if (dictionary[element].ContainsKey(docID))
                    {
                        total = total + Math.Pow(dictionary[element][docID] * getInverseDF(element), 2);
                    }
                }
            }
            return Math.Sqrt(total);

        }

        double getInverseDF(string token)
        {
            double docFrequency = dictionary[token].Count;
            double totalNumberOfDocument = Storage.getStorageDetails().getDocumentList().Count;
            return Math.Log10(totalNumberOfDocument / docFrequency);
        }

    }
}
