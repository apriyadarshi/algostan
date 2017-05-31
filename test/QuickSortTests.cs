using System;
using algostan;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace test
{
    
    [TestFixture]
    public class QuickSortTests
    {
        [Test]
        public void TestIfSortsProperly()
        {
            int[] testArray = { 9, 8, 7, 6, 5, 4, 3, 2, 1 },
                sortedArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            ISort sorter = new QuickSorter();
            sorter.Sort(testArray);

            bool isSame = true;
            for (int i = 0; i < testArray.Length; i++)
            {
                if (testArray[i] != sortedArray[i])
                    isSame = false;
            }

            Assert.AreEqual(isSame, true);
        }

        [Test]
        public void TestGetMedianAsPivot()
        {
            int[] testArray1 = { 8, 7, 6 },
                testArray2 = { 6, 7, 8 },
                testArray3 = { 7, 6, 8 },
                testArray4 = { 7, 8, 6 },
                testArray5 = { 8, 6, 7 };

            //int indexOfMidElem = (pEndI - pStartI) / 2;

            bool isCorrect = SortHelper.GetIndexOfMedian(testArray1, 0, 1, 2) == 1
                && SortHelper.GetIndexOfMedian(testArray2, 0, 1, 2) == 1
                && SortHelper.GetIndexOfMedian(testArray3, 0, 1, 2) == 0
                && SortHelper.GetIndexOfMedian(testArray4, 0, 1, 2) == 0
                && SortHelper.GetIndexOfMedian(testArray5, 0, 1, 2) == 2;

            Assert.AreEqual(isCorrect, true);

        }
    }
}
