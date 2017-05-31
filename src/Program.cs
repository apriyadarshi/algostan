﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace algostan
{
    class Program
    {
        static void Main(string[] args)
        {
            KargerMinCut();
            TestAssignment3();
            int[] testArray = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            int comparisonCount = new QuickSorter().QuickSortAndCountComparisons(testArray, 0, testArray.Length -1);

            int realCount = 45;

            //Assert.AreEqual(inversionCount, 8);

            ShouldSortLargeNumberOfItems();            
            
        }

        private static void TestAssignment2()
        {
            CountInversions();
        }

        private static void TestAssignment3()
        {
            CountComparisons();
        }

        private static void CountComparisons()
        {
            string fileName = "QuickSort.txt";
            StreamReader reader = null;
            int[] testArray = null, testArrayCopy = null;


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

                    int invs = new QuickSorter().QuickSortAndCountComparisons(testArray, 0, testArray.Length - 1);
                    Array.Sort<int>(testArrayCopy);

                    bool isSame = true;
                    for (int i = 0; i < testArray.Length; i++)
                    {
                        if (testArray[i] != testArrayCopy[i])
                            isSame = false;
                    }

                    if (isSame)
                        Console.WriteLine("succeeded");
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

        public static void CountInversions()
        {
            string fileName = "IntegerArray.txt";
            StreamReader reader = null;
            int[] testArray = null, testArrayCopy = null;


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

                    Int64 invs = new MergeSorter().CountInversions(testArray, 0, testArray.Length - 1);
                    Array.Sort<int>(testArrayCopy);

                    bool isSame = true;
                    for (int i = 0; i < testArray.Length; i++)
                    {
                        if (testArray[i] != testArrayCopy[i])
                            isSame = false;
                    }

                    if (isSame)
                        Console.WriteLine("succeeded");
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

        public static void KargerMinCut()
        {
            string fileName = "kargerMinCut.txt";
            StreamReader reader = null;
            int[] testArray = null, testArrayCopy = null;


            try
            {
                if (File.Exists(fileName))
                {
                    var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    reader = new StreamReader(fileStream);

                    List<Vertex> vertices = new List<Vertex>();
                    while(!reader.EndOfStream)
                    {
                        string[] lineElems = reader.ReadLine().Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        List<int> adjacentVert = new List<int>();
                        for (int i = 1; i < lineElems.Length; i++)
                            adjacentVert.Add(Convert.ToInt32(lineElems[i]));
                        vertices.Add(new Vertex(Convert.ToInt32(lineElems[0]), Convert.ToInt32(lineElems[0]), adjacentVert));
                    }

                    UGraph graph = new UGraph(vertices);
                    LinkedList<Edge> crossingEdges = graph.GetCrossingEdgesForMinCut();
                    int minCutCount = crossingEdges.Count;                    
                }

            }
            catch (Exception ex)
            {
                throw ex;                     
            }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }

        public static void ShouldSortLargeNumberOfItems()
        {
            string fileName = "IntegerArray.txt";
            StreamReader reader = null;
            int[] testArray = null, testArrayCopy = null;


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
                    for (int i=0; i<testArray.Length; i++)
                    {
                        if (testArray[i] != testArrayCopy[i])
                            isSame = false;
                    }

                    if (isSame)
                        Console.WriteLine("succeeded");
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

        private static void TestAssignment1()
        {
            TestAddition();
            TestSubtraction();
            TestComparison();
            TestMultiplication();
        }

        private static void TestSpecialCases()
        {
            BigInt big1 = null, big2 = null;
            int testSmallInt1 = 9687, testSmallInt2 = 4999;

            //Test simple cases
            Console.WriteLine("Testing BigInt Multiplication 9687*0");
            testSmallInt1 = 9687; testSmallInt2 = 0;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            BigInt product = big1 * big2;
            if (product.ToString() == (testSmallInt1 * testSmallInt2).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            Console.ReadLine();

            //Test simple cases
            Console.WriteLine("Testing BigInt Multiplication 9687*1");
            testSmallInt1 = 9687; testSmallInt2 = 1;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            product = big1 * big2;
            if (product.ToString() == (testSmallInt1 * testSmallInt2).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            Console.ReadLine();

            //Test simple cases
            Console.WriteLine("Testing BigInt Multiplication 9687*-1");
            testSmallInt1 = 9687; testSmallInt2 = -1;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            product = big1 * big2;
            if (product.ToString() == (testSmallInt1 * testSmallInt2).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            Console.ReadLine();

            //Multiplication by powers of 10
            Console.WriteLine("Testing BigInt Multiplication 9687*1000000000000");
            testSmallInt1 = 9687; testSmallInt2 = -1;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt("1000000000000");

            product = big1 * big2;
            if (product.ToString() == "9687000000000000")
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            Console.ReadLine();
        }

        private static void TestSingleDigit()
        {
            BigInt big1 = null, big2 = null;
            int testSmallInt1 = 8, testSmallInt2 = 4;

            //Test simple cases
            Console.WriteLine("Testing BigInt Multiplication 8*4");
            testSmallInt1 = 8; testSmallInt2 = 4;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            BigInt product = big1 * big2;
            if (product.ToString() == (testSmallInt1 * testSmallInt2).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            Console.ReadLine();

            //Test simple cases
            Console.WriteLine("Testing BigInt Multiplication 845678*4");
            testSmallInt1 = 845678; testSmallInt2 = 4;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            product = big1 * big2;
            if (product.ToString() == (testSmallInt1 * testSmallInt2).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            Console.ReadLine();
        }

        private static void TestKaratsuba()
        {
            BigInt big1 = null, big2 = null;
            int testSmallInt1 = 9000, testSmallInt2 = 4999;

            
            Console.WriteLine("Testing BigInt Karatsuba Multiplication 9000000000000*4000000000000");
            testSmallInt1 = 9687; testSmallInt2 = 4999;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            BigInt product = big1 * big2;
            if (product.ToString() == (testSmallInt1*testSmallInt2).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful. Expected: " + (testSmallInt1 * testSmallInt2).ToString() + " Received: " + product.ToString());
            //Console.ReadLine();


            Console.WriteLine("Testing BigInt Karatsuba Multiplication 9000000000000*4000000000000");
            big1 = new BigInt(new StringBuilder("32").Append('0', 62).ToString());
            big2 = new BigInt(new StringBuilder("28").Append('0', 62).ToString());

            product = big1 * big2;
            if (product.ToString() == new StringBuilder("36").Append('0', 128).ToString())
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful");
            //Console.ReadLine();

            Console.WriteLine("Testing BigInt Karatsuba Multiplication sample case");
            big1 = new BigInt("3141592653589793238462643383279502884197169399375105820974944592");
            big2 = new BigInt("2718281828459045235360287471352662497757247093699959574966967627");

            product = big1 * big2;
            if (product.ToString() == "")
                Console.WriteLine("Multiplication == successful");
            else
                Console.WriteLine("Multiplication == unsuccessful");
            Console.ReadLine();
        }

        private static void TestMultiplication()
        {
            //TestSpecialCases(); //0,1,-1, 10^x
            //TestSingleDigit();
            TestKaratsuba();
        }

        private static void TestComparison()
        {
            BigInt big1 = null, big2 = null;
            int testSmallInt1 = 9687, testSmallInt2 = 4999;

            Console.WriteLine("Testing BigInt Comaprison 9687 == 4999");
            testSmallInt1 = 9687; testSmallInt2 = 4999;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            bool isEqual = big1 == big2;
            if (isEqual == false)
                Console.WriteLine("Comparison == successful");
            else
                Console.WriteLine("Comparison == unsuccessful");
            Console.ReadLine();

            Console.WriteLine("Testing BigInt Comaprison 4999 == -4999");
            testSmallInt1 = -4999; testSmallInt2 = 4999;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            isEqual = big1 == big2;
            if (isEqual == (testSmallInt1==testSmallInt2))
                Console.WriteLine("Comparison == successful");
            else
                Console.WriteLine("Comparison == unsuccessful");
            Console.ReadLine();

            Console.WriteLine("Testing BigInt Comaprison 4999 == 4999");
            testSmallInt1 = 4999; testSmallInt2 = 4999;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            isEqual = big1 == big2;
            if (isEqual == (testSmallInt1 == testSmallInt2))
                Console.WriteLine("Comparison == successful");
            else
                Console.WriteLine("Comparison == unsuccessful");
            Console.ReadLine();
        }

        private static void TestSubtraction()
        {
            Console.WriteLine("Testing BigInt subtraction: 9687 - "
                                                      + "4999");

            int testSmallInt1 = 9687, testSmallInt2 = 4999;


            BigInt big1 = new BigInt(testSmallInt1.ToString());
            BigInt big2 = new BigInt(testSmallInt2.ToString());

            BigInt diff = big1 - big2;
            if (Convert.ToInt32(diff.AbsValue) == testSmallInt1 - testSmallInt2)
                Console.WriteLine("Subtraction successful. Diff was: " + diff.AbsValue);
            else
                Console.WriteLine("Subtraction unsuccessful. Diff was: " + diff.AbsValue + Environment.NewLine + "Expected Diff was:" + diff);

            Console.ReadLine();

            Console.WriteLine("Testing BigInt subtraction: 4999 - "
                                                      + "9687");

            


            diff = big2 - big1;
            if (Convert.ToInt32(diff.ToString()) == testSmallInt2 - testSmallInt1)
                Console.WriteLine("Subtraction successful. Diff was: " + diff.ToString());
            else
                Console.WriteLine("Subtraction unsuccessful. Diff was: " + diff.ToString() + Environment.NewLine + "Expected Diff was:" + (testSmallInt2 - testSmallInt1).ToString());


            Console.WriteLine("Testing BigInt subtraction: -9687 - 4999");



            testSmallInt1 = -9687; testSmallInt2 = 4999;
            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());
            diff = big1 - big2;
            if (Convert.ToInt32(diff.ToString()) == testSmallInt1 - testSmallInt2)
                Console.WriteLine("Subtraction successful. Diff was: " + diff.ToString());
            else
                Console.WriteLine("Subtraction unsuccessful. Diff was: " + diff.ToString() + Environment.NewLine + "Expected Diff was:" + (testSmallInt1 - testSmallInt2).ToString());


            Console.ReadLine();

            Console.WriteLine("Testing BigInt Subtraction: 1234567789798797897998798797987987"
                                                      + "1111110000000000000000000000000000");

            big1 = new BigInt("1234567789798797897998798797987987");
            big2 = new BigInt("1111110000000000000000000000000000");

            diff = big1 - big2;
            if (diff.AbsValue == "123457789798797897998798797987987")
                Console.WriteLine("Subtraction successful. Sum was: " + diff.AbsValue);
            else
                Console.WriteLine("Subtraction unsuccessful. Sum was: " + diff.AbsValue);


            Console.WriteLine("Testing BigInt Subtraction: 10000000"
                                                      + "9");

            big1 = new BigInt("10000000");
            big2 = new BigInt("9");
            testSmallInt1 = 10000000; testSmallInt2 = 9;
            diff = big1 - big2;
            if (Convert.ToInt32(diff.ToString()) == testSmallInt1 - testSmallInt2)
                Console.WriteLine("Subtraction successful. Diff was: " + diff.ToString());
            else
                Console.WriteLine("Subtraction unsuccessful. Diff was: " + diff.ToString() + Environment.NewLine + "Expected Diff was:" + (testSmallInt2 - testSmallInt1).ToString());



            Console.ReadLine();
        }

        private static void TestAddition()
        { 
            Console.WriteLine("Testing BigInt addition: 1234567789798797897998798797987987"
                                                      + "1000000000000000000000000000000000");
            BigInt big1, big2;
            int testSmallInt1 = 0, testSmallInt2 = 0;
            testSmallInt1 = 1234567; testSmallInt2 = -92738923;


            big1 = new BigInt(testSmallInt1.ToString());
            big2 = new BigInt(testSmallInt2.ToString());

            BigInt sum = big1 + big2;
            if (Convert.ToInt32(sum.ToString()) == testSmallInt1 + testSmallInt2)
                Console.WriteLine("Addition successful. Sum was: " + sum.AbsValue);
            else
                Console.WriteLine("Addition unsuccessful. Sum was: " + sum.AbsValue);

            Console.ReadLine();

            Console.WriteLine("Testing BigInt addition: 1234567789798797897998798797987987"
                                                      + "1111110000000000000000000000000000");

            big1 = new BigInt("1234567789798797897998798797987987");
            big2 = new BigInt("1111110000000000000000000000000000");

            sum = big1 + big2;
            if (sum.AbsValue == "2345677789798797897998798797987987")
                Console.WriteLine("Addition successful. Sum was: " + sum.AbsValue);
            else
                Console.WriteLine("Addition unsuccessful. Sum was: " + sum.AbsValue);

            Console.ReadLine();
        }
    }
}
