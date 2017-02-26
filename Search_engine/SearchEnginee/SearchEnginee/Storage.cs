using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace SearchEnginee
{

    /// <summary>
    /// This class models the repository
    /// </summary>
    class Storage
    {

        static StorageDetails storageDetails;

        static string defaultFolderPath = @"C:\seeSharpSE\";

        public Storage() { 

        }

        /// <summary>
        /// Retrieves the repository details  saved to a file if available
        /// </summary>
        public static void initializeSavedInfo() {
            bool status;

            if (Directory.Exists(defaultFolderPath))
            {
                string serializeFileName = "seeSharpDB";
                if(File.Exists(serializeFileName)){
                    StorageDetailsSR storageDetailsSR = new StorageDetailsSR();
                    storageDetails = storageDetailsSR.Retrieve();
                    
                    status = false;
                }
                else{
                    storageDetails = new StorageDetails(defaultFolderPath);
                    status = false;
                }
            }
            else {
                Directory.CreateDirectory(defaultFolderPath);
                storageDetails = new StorageDetails(defaultFolderPath);
                status = true;
            }

            UpdateThread updateThread = new UpdateThread(status);
            Thread thread = new Thread(new ThreadStart(updateThread.run));
            thread.Start();

        }

        /// <summary>
        /// This method saves the storage details needed t be persistent to a file
        /// </summary>
        public void saveStorageDetails() {
            StorageDetailsSR storageDetailsSR = new StorageDetailsSR();
            storageDetailsSR.Save(storageDetails);
            storageDetailsSR.Exit();
        }

        /// <summary>
        /// THis method returns the details of this repository
        /// </summary>
        /// <returns></returns>
        public static StorageDetails getStorageDetails() {
            return storageDetails;
        }

        /// <summary>
        /// This method get all the paths currently in the repository
        /// </summary>
        /// <param name="directory">the default name oft he repository</param>
        /// <returns>the paths in the repository</returns>
        public static List<string> getPaths(string directory) {
            List<string> filePaths = new List<string>();
            string[] files = Directory.GetFiles(directory);
            if (files.Length != 0)
            {
                foreach (string filePath in files)
                {
                    filePaths.Add(filePath);
                }
            }
            string[] directories = Directory.GetDirectories(directory);
            if (directories.Length != 0)
            {
                foreach (string direc in directories)
                {
                    getPaths(direc);
                }
            }

            return filePaths;
        }

        /// <summary>
        /// This method gets a list of the saved paths
        /// </summary>
        /// <returns>List of saved paths</returns>
        public static List<string> getSavedPaths() {
            List<string> savedPaths = new List<string>();
            foreach(Doocument document in storageDetails.getDocumentList()){
                savedPaths.Add(document.getPath());
            }
            return savedPaths;
        }

        /// <summary>
        /// This method gets a list of added paths to the repository
        /// </summary>
        /// <returns>List of added paths</returns>
        public static List<string> getAddedPaths()
        {
            List<string> savedPaths = getSavedPaths();
            List<string> newPaths = getPaths(defaultFolderPath);
            List<string> addedPaths = new List<string>();

            foreach (string element in newPaths)
            {
                if (!savedPaths.Contains(element)) {
                    addedPaths.Add(element);
                }
            }

            return addedPaths;
        }

        /// <summary>
        /// This method gets the list of removed documents from the repository
        /// </summary>
        /// <returns>List of documents removed from the repository</returns>
        public static List<Doocument> getRemovedDocuments()
        {
            List<string> savedPaths = getSavedPaths();
            List<string> newPaths = getPaths(defaultFolderPath);
            List<Doocument> removedDocuments = new List<Doocument>();

            foreach (string element in savedPaths)
            {
                if (!newPaths.Contains(element))
                {
                    removedDocuments.Add(getDocumentByPath(element));
                }
            }

            return removedDocuments;
        }

        /// <summary>
        /// This method gets the Document object by specifying its path
        /// </summary>
        /// <param name="path">the path of the document to be gotten</param>
        /// <returns>the document associated to that path</returns>
        static Doocument getDocumentByPath(string path) {
            Doocument doc = null;
            foreach(Doocument document in storageDetails.getDocumentList()){
                if(document.getPath() == path){
                    doc = document;
                    break;
                }
            }

            if(doc != null){
                storageDetails.getDocumentList().Remove(doc);
                storageDetails.decrNumberOfDocument();
            }

            return doc;
        }

    }
}













//if (Directory.GetLastAccessTime(defaultFolderPath) == storageDetails.getLastAccessTime())
//{
//do not read default folder again
//status = true;
//}
//else
//{
//}