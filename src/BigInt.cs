using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algostan
{
    //An ideal BigInt will switch between regular int and string int
    public class BigInt
    {
        string m_AbsValue = "0";
        Sign m_Sign = Sign.Positive;
        int m_Size = 0;

        public string AbsValue
        {
            get
            {
                return m_AbsValue;
            }
        }
        public Sign Sign
        {
            get
            {
                return m_Sign;
            }
        }
        public int Size
        {
            get
            {
                return m_Size;
            }
        }

        public BigInt(string pSignedValue)
        {
            //Validate and sanitize data - Use regex

            //Set Values
            if (pSignedValue[0] == '-')
                m_Sign = Sign.Negative;

            //Trim sign chars and leading zeroes. Handle case when all zeroes
            pSignedValue = pSignedValue.TrimStart('-', '+', '0');
            if (pSignedValue.Length == 0)
                pSignedValue = "0";

            m_AbsValue = pSignedValue;
            m_Size = m_AbsValue.Length;
        }

        public override string ToString()
        {
            return m_Sign == Sign.Negative ? "-" + m_AbsValue : m_AbsValue;
        }

        //Operator overloads for simple formats
        public static BigInt operator +(BigInt pNum1, BigInt pNum2)
        {
            //Optimize for zero. Return copies as original maybe needed
            if (pNum2.AbsValue == "0")
                return new BigInt(pNum1.AbsValue);
            if (pNum1.AbsValue == "0")
                return new BigInt(pNum1.AbsValue);

            BigInt result = null;
            StringBuilder resultBuilder = null;

            //Check Sign and decide if substraction needs to be done
            bool areBothPositive = pNum1.Sign == Sign.Positive && pNum2.Sign == Sign.Positive;
            bool areBothNegative = pNum1.Sign == Sign.Negative && pNum2.Sign == Sign.Negative;
            if (pNum1.Sign == pNum2.Sign)
            {
                bool isResultNegative = areBothNegative;

                //Carry out normal addition with carry over
                resultBuilder = new StringBuilder();

                //Find integer of larger length and loop over it.
                BigInt largerBigInt, smallerBigInt;
                if (pNum2.Size >= pNum1.Size)
                {
                    largerBigInt = pNum2;
                    smallerBigInt = pNum1;
                }
                else
                {
                    largerBigInt = pNum1;
                    smallerBigInt = pNum2;
                }


                int carryOver = 0;
                int smallerIntIterator = smallerBigInt.Size - 1;

                int loopSum = 0;
                string loopSumString = "0";
                for (int largeIntIterator = largerBigInt.Size - 1; largeIntIterator >= 0; largeIntIterator--)
                {
                    bool isSmallerIntExhausted = smallerIntIterator == -1;
                    if (isSmallerIntExhausted)
                    {
                        loopSum = Convert.ToInt32(largerBigInt.AbsValue[largeIntIterator].ToString()) + carryOver;
                        loopSumString = loopSum.ToString();
                        resultBuilder.Insert(0, loopSumString[loopSumString.Length - 1]);
                        carryOver = (loopSum > 9) ? 1 : 0; //Check and reset on each loop. Max carry over can be one in two way addition
                    }
                    else
                    {
                        loopSum = Convert.ToInt32(largerBigInt.AbsValue[largeIntIterator].ToString()) + Convert.ToInt32(smallerBigInt.AbsValue[smallerIntIterator].ToString()) + carryOver;
                        loopSumString = loopSum.ToString();
                        resultBuilder.Insert(0, loopSumString[loopSumString.Length - 1]);
                        carryOver = (loopSum > 9) ? 1 : 0;
                        smallerIntIterator--;
                    }
                }
                //Add last carryOver if left. This won't be covered in loop
                if (carryOver > 0)
                    resultBuilder.Insert(0, carryOver);

                //Add sign
                if (isResultNegative)
                    resultBuilder.Insert(0, "-");

                //return result
                result = new BigInt(resultBuilder.ToString());

            }
            else
            {
                //If one is negative, subtract. 
                result = pNum1 - -pNum2;
            }
            return result;
        }

        public static bool operator >(BigInt pNum1, BigInt pNum2)
        {
            bool result = false;

            //Compare by sign
            if (pNum1.Sign == pNum2.Sign)
            {
                if (pNum1.Size == pNum2.Size)
                {
                    int currNum1Int = 0;
                    int currNum2Int = 0;

                    //Loop and return whenever one found greater
                    for (int i = 0; i < pNum1.Size; i++)
                    {
                        currNum1Int = Convert.ToInt32(pNum1.AbsValue[i].ToString());
                        currNum2Int = Convert.ToInt32(pNum2.AbsValue[i].ToString());

                        if (currNum1Int != currNum2Int)
                        {
                            bool iscurrNum1Greater = currNum1Int > currNum2Int;
                            result = (pNum1.Sign == Sign.Positive) ? iscurrNum1Greater : !iscurrNum1Greater;
                            return result;
                        }
                    }

                    //Execution reaching here means that both are equal
                    return false;
                }
                else
                {
                    if (pNum1.Size > pNum2.Size)
                    {
                        result = (pNum1.Sign == Sign.Positive) ? true : false;
                        return result;
                    }
                    else
                    {
                        result = (pNum2.Sign == Sign.Positive) ? false : true;
                        return result;
                    }
                }
            }
            else
            {
                result = pNum1.Sign == Sign.Positive;
                return result;
            }
        }

        public static bool operator <(BigInt pNum1, BigInt pNum2)
        {
            return pNum2 > pNum1;
        }

        public static bool operator ==(BigInt pNum1, BigInt pNum2)
        {
            if ((pNum1.Sign == pNum2.Sign) && (pNum1.Size == pNum2.Size))
            {
                int currNum1Int = 0;
                int currNum2Int = 0;

                //Loop and return whenever one found greater
                for (int i = 0; i < pNum1.Size; i++)
                {
                    currNum1Int = Convert.ToInt32(pNum1.AbsValue[i]);
                    currNum2Int = Convert.ToInt32(pNum2.AbsValue[i]);

                    if (currNum1Int != currNum2Int)
                    {
                        return false;
                    }

                }

                //Execution reaching here means that both are equal
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool operator !=(BigInt pNum1, BigInt pNum2)
        {
            return !(pNum1 == pNum2);
        }

        public static BigInt operator -(BigInt pNum1, BigInt pNum2)
        {
            //Optimize for zero. Return copies as original maybe needed
            if (pNum2.AbsValue == "0")
                return new BigInt(pNum1.AbsValue);
            if (pNum1.AbsValue == "0")
                return new BigInt("-" + pNum1.AbsValue);

            //If Equal, return 0
            if (pNum1 == pNum2)
                return new BigInt("0");

            BigInt result = null;
            StringBuilder resultBuilder = null;

            //Check Sign and decide if addition needs to be done
            bool areBothPositive = pNum1.Sign == Sign.Positive && pNum2.Sign == Sign.Positive;
            bool areBothNegative = pNum1.Sign == Sign.Negative && pNum2.Sign == Sign.Negative;

            if (pNum1.Sign == pNum2.Sign)
            {
                //Subtraction needs to be done
                resultBuilder = new StringBuilder();

                //Set majorterm and minorterm i.e. to do majorterm-minorterm
                //Alt algo - Define Compare unsigned
                BigInt majorTerm = null;
                BigInt minorTerm = null;
                bool isResultPositiveSigned = pNum1 > pNum2;
                if ((areBothPositive && isResultPositiveSigned) || (areBothNegative && !isResultPositiveSigned))
                {
                    majorTerm = pNum1; minorTerm = pNum2;
                }
                else
                {
                    majorTerm = pNum2; minorTerm = pNum1;
                }


                //Loop and do diff
                int borrow = 0, loopDiff = 0, currMajor = 0, currMinor = 0; ;
                int minorTermIterator = minorTerm.Size - 1;

                for (int majorTermIterator = majorTerm.Size - 1; majorTermIterator > -1; majorTermIterator--)
                {
                    bool isMinorTermExhausted = minorTermIterator == -1;
                    if (isMinorTermExhausted)
                    {
                        currMajor = Convert.ToInt32(majorTerm.AbsValue[majorTermIterator].ToString());
                        currMinor = 0;

                        //Handle cases like 1000008 - 9, when each zero borrows one till the last digit 1
                        if (currMajor == 0 && borrow > 0)
                        {
                            loopDiff = 10 - borrow + currMajor - currMinor;
                            borrow = 1;
                        }
                        else
                        {
                            loopDiff = currMajor - borrow - currMinor;
                            borrow = 0;
                        }
                    }
                    else
                    {
                        currMajor = Convert.ToInt32(majorTerm.AbsValue[majorTermIterator].ToString());
                        currMinor = Convert.ToInt32(minorTerm.AbsValue[minorTermIterator].ToString());
                        bool isNewBorrowRequired = (currMajor - borrow) < currMinor;
                        if (isNewBorrowRequired)
                        {
                            loopDiff = 10 + currMajor - borrow - currMinor;
                            borrow = 1;
                        }
                        else
                        {
                            loopDiff = currMajor - borrow - currMinor;
                            borrow = 0;
                        }
                        minorTermIterator--;
                    }
                    resultBuilder.Insert(0, loopDiff); //loopdiff will always be less than 10
                }

                //Borrow is already accounted for in last loop
                //Add sign
                if (!isResultPositiveSigned)
                    resultBuilder.Insert(0, "-");
                //return result
                result = new BigInt(resultBuilder.ToString());
            }
            else
            {
                //Opposite signs => Addition needs to be done
                result = pNum1 + -pNum2; //This is an equivalent expression of -
            }
            return result;
        }

        //Unary operators
        public static BigInt operator -(BigInt pNum)
        {
            if (pNum.Sign == Sign.Positive)
            {
                return new BigInt("-" + pNum.AbsValue);
            }
            else
            {
                return new BigInt(pNum.AbsValue);
            }

        }

        public static BigInt operator +(BigInt pNum)
        {
            return new BigInt(pNum.AbsValue);
        }

        //Operator overloads for simple formats
        public static BigInt operator *(BigInt pNum1, BigInt pNum2)
        {
            //Special Cases
            //With 0
            if (pNum1.AbsValue == "0" || pNum2.AbsValue == "0")
                return new BigInt("0");

            string resultSign = pNum1.Sign == pNum2.Sign ? String.Empty : "-";
            StringBuilder sb = new StringBuilder();

            //With 1 and -1
            if (pNum1.AbsValue == "1")
            {
                return new BigInt(resultSign + pNum2.AbsValue);
            }
            if (pNum2.AbsValue == "1")
            {
                return new BigInt(resultSign + pNum1.AbsValue);
            }

            //With multiples of 10
            if (pNum1.AbsValue[0] == '1' && pNum1.AbsValue.TrimEnd('0') == "1")
            {
                return new BigInt(sb.Append(resultSign).Append(pNum2.AbsValue).Append('0', pNum1.AbsValue.Length - 1).ToString());
            }
            if (pNum2.AbsValue[0] == '1' && pNum2.AbsValue.TrimEnd('0') == "1")
            {
                return new BigInt(sb.Append(resultSign).Append(pNum1.AbsValue).Append('0', pNum2.AbsValue.Length - 1).ToString());
            }

            //Between small numbers

            //When difference between the number of digits of the two numbers is very large

            //Between large numbers, when difference between the no of digits of the two numbers is not very large - Karatsuba best
            return SignedKaratsubaMult(pNum1, pNum2);
        }

        public static string UnSignedMultByDigit(BigInt pNum, char pDigit)
        {
            int singleDigit = Convert.ToInt32(pDigit.ToString());

            //Handle 0,1
            if (singleDigit == 0)
                return "0";
            if (singleDigit == 1)
                return pNum.AbsValue.ToString();

            //Loop and build result
            StringBuilder sbProduct = new StringBuilder();
            int carryOver = 0;
            for (int i = pNum.Size - 1; i >= 0; i--)
            {
                int currProduct = (Convert.ToInt32(pNum.AbsValue[i].ToString()) * singleDigit) + carryOver;
                string currProductString = currProduct.ToString();
                if (currProduct > 9)
                    carryOver = Convert.ToInt32(currProductString[0].ToString());
                else
                    carryOver = 0;
                sbProduct.Insert(0, currProductString[currProductString.Length - 1].ToString());
            }
            if (carryOver != 0)
                sbProduct.Insert(0, carryOver.ToString());

            return sbProduct.ToString();
        }

        //Assumption none of the input is 0 or 1
        public static BigInt SignedKaratsubaMult(BigInt pNum1, BigInt pNum2)
        {
            string resultData = null;

            //Base Case of recursive call: Check if any 1 number is single digit
            int intNum1 = 0, intNum2 = 0;

            if (pNum1.Size == 1)
            {
                if (pNum2.Size == 1)
                    resultData = (Convert.ToInt32(pNum1.AbsValue) * Convert.ToInt32(pNum2.AbsValue)).ToString();
                else
                {
                    resultData = UnSignedMultByDigit(pNum2, pNum1.AbsValue[0]);
                }
            }
            else if (pNum2.Size == 1)
            {
                resultData = UnSignedMultByDigit(pNum1, pNum2.AbsValue[0]);
            }
            else
            {
                //Case when both have more than one digit

                //Multiply using local copy
                string num1Data = pNum1.AbsValue;
                string num2Data = pNum2.AbsValue;

                //Pad with leading zeroes and make length same to make eligible for Karatsuba
                if (pNum1.Size != pNum2.Size)
                {
                    int diff = pNum1.Size - pNum2.Size;
                    StringBuilder leadingZeroes = new StringBuilder();
                    leadingZeroes.Append('0', Math.Abs(diff));
                    if (diff > 0)
                        num2Data = leadingZeroes.Append(num2Data).ToString();
                    else
                        num1Data = leadingZeroes.Append(num1Data).ToString();
                }


                //Partition
                int len = num1Data.Length;
                int rPartLen = len / 2;
                int lPartLen = len - rPartLen;

                BigInt num1lPart = new BigInt(num1Data.Substring(0, lPartLen));
                BigInt num1rPart = new BigInt(num1Data.Substring(lPartLen, rPartLen));

                BigInt num2lPart = new BigInt(num2Data.Substring(0, lPartLen));
                BigInt num2rPart = new BigInt(num2Data.Substring(lPartLen, rPartLen));

                //Call recursive
                BigInt productLeft = SignedKaratsubaMult(num1lPart, num2lPart),
                    productRight = SignedKaratsubaMult(num1rPart, num2rPart),
                    productMid = SignedKaratsubaMult((num1lPart + num1rPart), (num2lPart + num2rPart));

                //Math.pow will fail at very high value
                BigInt result = (new BigInt(new StringBuilder("1").Append('0', 2 * rPartLen).ToString()) * productLeft)
                                + (new BigInt(new StringBuilder("1").Append('0', rPartLen).ToString())) * (productMid - productLeft - productRight)
                                + productRight;
                //Populate resultData. Set Sign Later
                resultData = result.AbsValue;
            }

            if (resultData != null)
                resultData = pNum1.Sign == pNum2.Sign ? resultData : "-" + resultData;

            return new BigInt(resultData);
        }
    }

    public enum Sign
    {
        Negative,
        Positive //Zero asssumed +ve
    }
}
