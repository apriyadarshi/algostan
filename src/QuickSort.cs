using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace algostan
{
    public class QuickSorter : ISort
    {
        public void Sort(int[] pArray)
        {
            QuickSort(pArray, 0, pArray.Length - 1);
        }

        private void QuickSort(int[] pArr, int pStartI, int pEndI)
        {
            if (pEndI - pStartI + 1 <= 1)
                return;
            else
            {
                int pivotInitialIndex = GetPivot(pArr, pStartI, pEndI);
                int pivotFinalIndex = PartitionAndGetFinalPivotIndex(pArr, pStartI, pEndI, pivotInitialIndex);

                QuickSort(pArr, pStartI, pivotFinalIndex - 1);
                QuickSort(pArr, pivotFinalIndex + 1, pEndI);
            }
        }

        public int QuickSortAndCountComparisons(int[] pArr, int pStartI, int pEndI)
        {
            int comparisonCount = 0;
            if (pEndI - pStartI + 1 <= 1)
                return 0;
            else
            {
                int pivotInitialIndex = GetPivot(pArr, pStartI, pEndI);
                int pivotFinalIndex = PartitionAndGetFinalPivotIndex(pArr, pStartI, pEndI, pivotInitialIndex);
                comparisonCount = comparisonCount + pEndI - pStartI;

                comparisonCount = comparisonCount + QuickSortAndCountComparisons(pArr, pStartI, pivotFinalIndex - 1);
                comparisonCount = comparisonCount + QuickSortAndCountComparisons(pArr, pivotFinalIndex + 1, pEndI);
            }
            return comparisonCount;
        }

        private int PartitionAndGetFinalPivotIndex(int[] pArr, int pStartI, int pEndI, int pIntitialPivotIndex)
        {
            //Move pivot at start
            Swap(pArr, pStartI, pIntitialPivotIndex);

            int rPartStartIn = pStartI + 1,
                pivot = pArr[pStartI];


            for (int unPartIn = pStartI + 1; unPartIn < pEndI + 1; unPartIn++)
            {
                if (pArr[unPartIn] <= pivot)
                {
                    Swap(pArr, unPartIn, rPartStartIn);
                    rPartStartIn = rPartStartIn + 1;
                }
            }

            //Swap pivot to correct position
            Swap(pArr, pStartI, rPartStartIn - 1);
            return rPartStartIn - 1;
        }

        private void Swap(int[] pArr, int pIndex1, int pIndex2)
        {
            if (pIndex1 == pIndex2)
                return;
            int temp = pArr[pIndex1];
            pArr[pIndex1] = pArr[pIndex2];
            pArr[pIndex2] = temp;
        }

        private int GetPivot(int[] pArr, int pStartI, int pEndI)
        {
            //return pStartI;
            //return pEndI;
            int indexOfMidElem = pStartI + ((pEndI - pStartI) / 2);
            return SortHelper.GetIndexOfMedian(pArr, pStartI, pEndI, indexOfMidElem);
        }        
    }

    public class SortHelper
    {
        public static int GetIndexOfMedian(int[] pArr, int pIndex1, int pIndex2, int pIndex3)
        {
            if ((pArr[pIndex1] >= pArr[pIndex2] && pArr[pIndex2] >= pArr[pIndex3])
                || (pArr[pIndex3] >= pArr[pIndex2] && pArr[pIndex2] >= pArr[pIndex1]))
            {
                return pIndex2;
            }
            if ((pArr[pIndex3] >= pArr[pIndex1] && pArr[pIndex1] >= pArr[pIndex2])
                || (pArr[pIndex2] >= pArr[pIndex1] && pArr[pIndex1] >= pArr[pIndex3]))
            {
                return pIndex1;
            }
            return pIndex3;
        } 
    }
}
