using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace algostan
{
    public interface ISort
    {
        void Sort(int[] pArray);
    }

    public enum SortAlgo
    {
        MergeSort = 0,
        BubbleSort = 1,
        QuickSort = 2
    }

    public class MergeSorter : ISort
    {
        public void Sort(int[] pArray)
        {
            if(pArray.Length != 0)
                MergeSort(pArray, 0, pArray.Length - 1);
        }

        public void MergeSort(int[] pArray, int pStartIndex, int pEndIndex)
        {
            if (pStartIndex == pEndIndex)
                return;
            else
            {
                int midpoint = (pStartIndex + pEndIndex) / 2;
                MergeSort(pArray, pStartIndex, midpoint);
                MergeSort(pArray, midpoint + 1, pEndIndex);
                Merge(pArray, pStartIndex, midpoint, pEndIndex);
            }
        }

        private void Merge(int[] pArray, int pStartIndex, int pMidpoint, int pEndIndex)
        {
            //Partition and copy data in subarrays
            int lArrLen = pMidpoint - pStartIndex + 1;
            int rArrLen = pEndIndex - pMidpoint;
            int len = lArrLen + rArrLen;
            
            int[] lSubArr = new int[lArrLen];
            int[] rSubArr = new int[rArrLen];

            for (int k=0; k<lArrLen; k++)
            {
                lSubArr[k] = pArray[pStartIndex + k];
            }

            for (int k = 0; k < rArrLen; k++)
            {
                rSubArr[k] = pArray[pMidpoint + 1 + k];
            }


            //Merge
            int li = 0, ri = 0;
            for (int k=0; k<len; k++)
            {
                //If leftArray is already merged, just copy right array for remaining
                if (li==lArrLen)
                {
                    if (ri == rArrLen)
                        break;
                    else
                    {
                        pArray[pStartIndex + k] = rSubArr[ri];
                        ri = ri + 1;
                    }
                }
                //If rightArray is already merged, just copy left array for remaining
                else if (ri==rArrLen)
                {
                    pArray[pStartIndex + k] = lSubArr[li];
                    li = li + 1;
                }
                //Merge after comparing
                else
                {
                    if (lSubArr[li] <= rSubArr[ri])
                    {
                        pArray[pStartIndex + k] = lSubArr[li];
                        li = li + 1;
                    }
                    else
                    {
                        pArray[pStartIndex + k] = rSubArr[ri];
                        ri = ri + 1;
                    }
                }
            }
        }

        public Int64 CountInversions(int[] pArray, int pStartIndex, int pEndIndex)
        {
            Int64 inversionCount = 0;
            if (pStartIndex == pEndIndex)
                return 0;
            else
            {
                int midpoint = (pStartIndex + pEndIndex) / 2;
                Int64 leftInversions = CountInversions(pArray, pStartIndex, midpoint);
                Int64 rightInversions = CountInversions(pArray, midpoint + 1, pEndIndex);
                Int64 currInversions = MergeAndCountInv(pArray, pStartIndex, midpoint, pEndIndex);

                inversionCount = leftInversions + rightInversions + currInversions;
            }
            return inversionCount;
        }

        private Int64 MergeAndCountInv(int[] pArray, int pStartIndex, int pMidpoint, int pEndIndex)
        {
            Int64 inversionCount = 0;
            
            
            //Partition and copy data in subarrays
            int lArrLen = pMidpoint - pStartIndex + 1;
            int rArrLen = pEndIndex - pMidpoint;
            int len = lArrLen + rArrLen;

            int[] lSubArr = new int[lArrLen];
            int[] rSubArr = new int[rArrLen];

            for (int k = 0; k < lArrLen; k++)
            {
                lSubArr[k] = pArray[pStartIndex + k];
            }

            for (int k = 0; k < rArrLen; k++)
            {
                rSubArr[k] = pArray[pMidpoint + 1 + k];
            }


            //Merge
            int li = 0, ri = 0;
            for (int k = 0; k < len; k++)
            {
                //If leftArray is already merged, just copy right array for remaining
                if (li == lArrLen)
                {
                    if (ri == rArrLen)
                        break;
                    else
                    {
                        pArray[pStartIndex + k] = rSubArr[ri];
                        ri = ri + 1;
                    }
                }
                //If rightArray is already merged, just copy left array for remaining
                else if (ri == rArrLen)
                {
                    pArray[pStartIndex + k] = lSubArr[li];
                    li = li + 1;
                }
                //Merge after comparing
                else
                {
                    if (lSubArr[li] <= rSubArr[ri])
                    {
                        pArray[pStartIndex + k] = lSubArr[li];
                        li = li + 1;
                    }
                    else
                    {
                        pArray[pStartIndex + k] = rSubArr[ri];
                        ri = ri + 1;
                        inversionCount = inversionCount + lArrLen - li;
                    }
                }
            }
            return inversionCount;
        }
    }

}

