using System;
using algostan;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace test
{
    [TestFixture]
    public class SortMethodsTest
    {
        [Test]
        public void ShouldCountInversions()
        {
            int[] testArray = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            Int64 inversionCount = new MergeSorter().CountInversions(testArray, 0, testArray.Length - 1);
            

            Assert.AreEqual(inversionCount, 45);
        }

        [Test]
        public void ShouldSortLargeNumberOfItems()
        {
            string fileName = "IntegerArray.txt";
            StreamReader reader = null;
            int[] testArray = null, testArrayCopy =null;


            try
            {
                if (File.Exists(fileName))
                {
                    var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(fileStream);
                    string fullTextCSV = reader.ReadToEnd().Replace(Environment.NewLine, ",");
                    string[] stringArray = fullTextCSV.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    testArray = stringArray.Select<string, int>(x => Convert.ToInt32(x)).ToArray<int>();
                    testArrayCopy = new Int32[testArray.Length];
                    testArray.CopyTo(testArrayCopy, 0);

                    new MergeSorter().MergeSort(testArray, 0, testArray.Length - 1);
                    Array.Sort<int>(testArrayCopy);

                    bool isSame = true;
                    for (int i = 0; i < testArray.Length; i++)
                    {
                        if (testArray[i] != testArrayCopy[i])
                            isSame = false;
                    }

                    Assert.AreEqual(isSame, true);                   
                }
                    
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }


        [Test]
        public void ShouldSortEvenNumberOfItems()
        {
            int[] testArray = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] testArrayCopy = { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

            new MergeSorter().MergeSort(testArray, 0, testArray.Length - 1);
            Array.Sort<int>(testArrayCopy);

            Assert.AreEqual(testArray, testArrayCopy);
        }

        [Test]
        public void ShouldSortOddNumberOfItems()
        {
            int[] testArray = { 9, 8, 7, 6, 5, 4, 3, 2, 1};
            int[] testArrayCopy = { 9, 8, 7, 6, 5, 4, 3, 2, 1};

            new MergeSorter().MergeSort(testArray, 0, testArray.Length - 1);
            Array.Sort<int>(testArrayCopy);

            Assert.AreEqual(testArray, testArrayCopy);
        }

        [Test]
        public void ShouldSortSingleItem()
        {
            int[] testArray = { 9 };
            Assert.AreEqual(testArray, testArray);
        }

        [Test]
        public void ShouldSortEmptyArray()
        {
            int[] testArray = {};
            Assert.AreEqual(testArray, testArray);
        }
    }
}