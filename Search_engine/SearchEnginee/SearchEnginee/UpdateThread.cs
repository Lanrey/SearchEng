using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SearchEnginee
{
    /// <summary>
    /// This class updates the indexer when a document is added to the repository
    /// and when a document is removed from it
    /// </summary>
    class UpdateThread
    {

        int sleepTime;
        bool rest = true;

        /// <summary>
        /// Initializes the sleep time of the thread to be run and the status
        /// When status is true, the first time the thread runs, it sleeps for a duration of the sleeptime
        /// When status is false, the first time the thread runs, it does not sleep
        /// </summary>
        /// <param name="status"></param>
        public UpdateThread(bool status) {
            sleepTime = 10;
            rest = status;
        }

        /// <summary>
        /// Performs the main function of getting the newly added documents and add them to the indexer,
        /// and also get the removed documents and remove them from the indexer
        /// </summary>
        public void run() { 

            if(rest){
                Thread.Sleep(sleepTime);
            }

            while(true){
                Indexer indexer = Storage.getStorageDetails().getIndexer();

                foreach(string path in Storage.getAddedPaths()){
                    int id = Storage.getStorageDetails().getAndIncrLastDocumentID();
                    Doocument document = new Doocument(id,path);
                    indexer.add(document);
                    Storage.getStorageDetails().getDocumentList().Add(document);
                    Storage.getStorageDetails().incrNumberOfDocument();
                }

                foreach (Doocument document in Storage.getRemovedDocuments())
                {
                    int id = document.getID();
                    indexer.remove(id);
                }

                Thread.Sleep(20000);//time in milliseconds
            }

        }

        

    }
}
