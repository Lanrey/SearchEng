using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace SearchEnginee
{

    /// <summary>
    /// This class sorts a list of documents using their rank values
    /// </summary>
    public class Sort
    {

        public Sort() { 

        }

        /// <summary>
        /// This method sorts portions and the entire passed array in ascending or descending order
        /// </summary>
        /// <param name="array">List of documents to be sorted in ascending or descending order</param>
        /// <param name="order">Enumerator that specifies if sorting is to be in ascending or descending</param>
        /// <param name="startIndex">An unsigned integer that indicates the first index to start sorting</param>
        /// <param name="endIndex">An unsigned integer that indicates the last index to be sorted</param>
        public static void mergeSort(List<Doocument> array, Order order, uint startIndex, uint endIndex)
        {
            //Contract.Requires<NullReferenceException>(array != null);

            if (array.Count != 0)//if array is empty
            {
                if (endIndex > array.Count - 1) { endIndex = (uint)(array.Count - 1); }
                if (startIndex > endIndex) { startIndex = endIndex; }

                if ((endIndex - startIndex) == 0)
                {
                    //return array;
                }
  
                else
                {
                    uint mid = (uint)((endIndex + startIndex - 1) / 2);
                    mergeSort(array, order, startIndex, mid);
                    mergeSort(array, order, mid + 1, endIndex);
                    merge(array, order, startIndex, mid, endIndex);
                }
            }
        }

        /// <summary>
        /// This method merges/arranges the passed array in the specified order
        /// </summary>
        /// <param name="array">The array to be merged</param>
        /// <param name="order">Enumerator that specifies if sorting is to be in ascending or descending</param>
        /// <param name="start">An unsigned integer that indicates the first index of the first part</param>
        /// <param name="mid">An unsigned integer that indicates the last index of the first part</param>
        /// <param name="end">An unsigned integer that indicates the last index of the second part</param>
        static void merge(List<Doocument> array, Order order, uint start, uint mid, uint end)
        {
            List<Doocument> arrayCopy = clone(array);
            int orderFactor;
            if (order == Order.ASCENDING)
            {
                orderFactor = 1;
            }
            else//if order == Order.DESCENDING
            {
                orderFactor = -1;
            }
            uint currentPosition = start;
            uint jStartIndex = mid + 1;
            uint i;
            for (i = start; i <= mid; i++)
            {
                for (uint j = jStartIndex; j <= end; j++)
                {
                    if (orderFactor * (arrayCopy[(int)i].getRankValue()).CompareTo(arrayCopy[(int)j].getRankValue()) < 0)
                    {
                        array[(int)currentPosition++] = arrayCopy[(int)i];
                        break;
                    }
                    else
                    {
                        array[(int)currentPosition++] = arrayCopy[(int)j];
                        jStartIndex = j + 1;
                    }
                }
                if (jStartIndex == end + 1)
                {
                    break;
                }
            }

            if (i == mid + 1)
            {
                for (uint k = jStartIndex; k <= end; k++)
                {
                    array[(int)currentPosition++] = arrayCopy[(int)k];
                }
            }
            else
            {
                for (uint k = i; k <= mid; k++)
                {
                    array[(int)currentPosition++] = arrayCopy[(int)k];
                }
            }
        }

        /// <summary>
        /// This method creates a copy of the passed list of documents
        /// </summary>
        /// <param name="array">List to be cloned/duplicated</param>
        /// <returns>Cloned list</returns>
        static List<Doocument> clone(List<Doocument> array)
        {
            List<Doocument> arrayCopy = new List<Doocument>();
            for (int i = 0; i < array.Count; i++)
            {
                arrayCopy.Add(array[i]);
            }
            return arrayCopy;
        }

    }

    /// <summary>
    /// An enumerator that has the values ascending and descending
    /// </summary>
    public enum Order
    {
        ASCENDING, DESCENDING
    }

}
