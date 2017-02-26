using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Diagnostics.Contracts;

namespace SearchEnginee
{

    /// <summary>
    /// This class saves,retrieves and closes an object of StorageDetails to a file
    /// </summary>
    class StorageDetailsSR
    {

        readonly string FILE_NAME;
        BinaryFormatter formatter;
        FileStream output = null;

        /// <summary>
        /// Initializes the file name
        /// </summary>
        public StorageDetailsSR()
        {
            FILE_NAME = "seeSharpDB";
            formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Saves an object of StorageDetails to a file
        /// </summary>
        /// <param name="storageDetails">Object of StorageDetails</param>
        public void Save(StorageDetails storageDetails)
        {
            Contract.Requires(storageDetails != null);
            try
            {
                output = new FileStream(FILE_NAME, FileMode.OpenOrCreate, FileAccess.Write);
                formatter.Serialize(output, storageDetails);
            }
            catch (IOException)
            {
                //do nothing
            }
            catch (SerializationException)
            {
                //do nothing
            }

        }

        /// <summary>
        /// Closes the file being used
        /// </summary>
        public void Exit()
        {
            if (output != null)
            {
                try
                {
                    output.Close();
                }
                catch (IOException)
                {
                    //do nothing
                }
            }
        }

        /// <summary>
        /// Retrieves the saved object in the file
        /// </summary>
        /// <returns>Saved information</returns>
        public StorageDetails Retrieve()
        {
            StorageDetails storageDetails = null;
            FileStream input = null;
            try
            {
                input = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
                storageDetails = (StorageDetails)formatter.Deserialize(input);
            }
            catch (IOException)
            {
                //do nothing
            }
            catch (SerializationException)
            {
                //do nothing
            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
                else
                {
                    string defaultFolderPath = @"C:\seeSharpSE\";
                    storageDetails = new StorageDetails(defaultFolderPath);
                }
            }
            return storageDetails;
        }

    }
}
