using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SearchEnginee
{
    /// <summary>
    /// This class contains details of the repository that needs to be saved or stored
    /// </summary>
    [Serializable]
    class StorageDetails
    {
        int numberOFDocument;
        int lastDocumentID;
        DateTime lastAccessTime;
        Indexer indexer;
        List<Doocument> documentList;

        /// <summary>
        /// Initializes the details of this class
        /// </summary>
        /// <param name="defaultFolderPath">Name of the repository</param>
        public StorageDetails(string defaultFolderPath) {
            numberOFDocument = 0;
            lastDocumentID = 0;
            lastAccessTime = Directory.GetLastAccessTime(defaultFolderPath);//can also use the current time,that is, new DateTime()
            indexer = new Indexer();
            documentList = new List<Doocument>();
        }

        /// <summary>
        /// Returns the number of documents contained in the repository
        /// </summary>
        /// <returns>Number of documents in the repository</returns>
        public int getNumberOfDocument() {
            return numberOFDocument;
        }

        /// <summary>
        /// Increments the number of documents contained in the repository
        /// </summary>
        public void incrNumberOfDocument() {
            numberOFDocument++;
        }

        /// <summary>
        /// Decrements the number of documents contained in the repository
        /// </summary>
        public void decrNumberOfDocument()
        {
            numberOFDocument--;
        }

        /// <summary>
        /// Increments the last document ID and returns it
        /// </summary>
        /// <returns></returns>
        public int getAndIncrLastDocumentID(){//get and increment last document id
            lastDocumentID++;
            return lastDocumentID;
        }

        /// <summary>
        /// Returns the last time the repository was accessed
        /// </summary>
        /// <returns>Last access time of the repository</returns>
        public DateTime getLastAccessTime() {
            return lastAccessTime;
        }

        /// <summary>
        /// Returns the current indexer
        /// </summary>
        /// <returns>Indexer</returns>
        public Indexer getIndexer() {
            return indexer;
        }

        /// <summary>
        /// Returns the list of documents contained in the repository
        /// </summary>
        /// <returns></returns>
        public List<Doocument> getDocumentList() {
            return documentList;
        }
    }
}
