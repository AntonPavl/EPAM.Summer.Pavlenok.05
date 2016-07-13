using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolinomCollection
{


    public class PolinomWithStruct
    {
        private readonly PolinomElement[] elements;

        public PolinomElement[] Elements { get { return elements; } }

        public PolinomWithStruct(PolinomElement[] polinom)
        {
            elements = polinom;
        }

        public PolinomWithStruct(double[] polinom)
        {
            elements = ToPolinomElement(polinom);
        }

        private static PolinomElement[] ToPolinomElement(double[] num)
        {
            if (num == null) throw new ArgumentNullException();
            if (num.Length == 0) return new PolinomElement[0];

            int buffer = 0;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] > 0) buffer++;
            }
            if (buffer == 0) return new PolinomElement[0];

            PolinomElement[] result = new PolinomElement[buffer];
            int polinomIndex = 0;
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] != 0)
                {
                    result[polinomIndex].coefficient = num[i];
                    result[polinomIndex].power = i;
                    polinomIndex++;
                }
            }
            return result;
        }
        /// <summary>
        /// Evaluate polinom
        /// </summary>
        /// <param name="num">Polinom's number</param>
        /// <returns></returns>
        public double        Calculate(double num)
        {
            if (elements == null) throw new ArgumentNullException();
            if (elements.Length == 0) return 0;
            double result = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                result +=  elements[i].coefficient* Math.Pow(num, elements[i].power);
            }
            return result;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns></returns>
        public override int  GetHashCode()
        {
            return base.GetHashCode();
        } //dopisat'

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is PolinomWithStruct))
                return false;
            else
                return Enumerable.SequenceEqual(elements, ((PolinomWithStruct)obj).elements);
        }

        /// <summary>
        /// Get Polinom in string's format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (elements == null) return "";
            var result = new StringBuilder();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i].coefficient != 0)
                {
                    result.Append($"{elements[i].coefficient}*X^{elements[i].power} ");
                }
            }
            return result.ToString();
        }

        #region Operator+
        public static PolinomWithStruct operator +(PolinomWithStruct lp, PolinomWithStruct rp)
        {
            return new PolinomWithStruct(Sum(lp.elements, rp.elements));
        }
        public static PolinomWithStruct operator +(double[] lp, PolinomWithStruct rp)
        {
            return new PolinomWithStruct(Sum(ToPolinomElement(lp), rp.elements));
        }
        public static PolinomWithStruct operator +(PolinomWithStruct lp, double[] rp)
        {
            return new PolinomWithStruct(Sum(lp.elements,ToPolinomElement(rp)));
        }
        #endregion Operator+
        #region Opeartor-
        public static PolinomWithStruct operator -(PolinomWithStruct lp, PolinomWithStruct rp)
        {
            return new PolinomWithStruct(Sub(lp.elements, rp.elements));
        }
        public static PolinomWithStruct operator -(double[] lp, PolinomWithStruct rp)
        {
            return new PolinomWithStruct(Sub(ToPolinomElement(lp), rp.elements));
        }
        public static PolinomWithStruct operator -(PolinomWithStruct lp, double[] rp)
        {
            return new PolinomWithStruct(Sub(lp.elements,ToPolinomElement(rp)));
        }
        #endregion Operator-
        #region Operator*
        public static PolinomWithStruct operator *(PolinomWithStruct lp, PolinomWithStruct rp)
        {
            return new PolinomWithStruct(Mult(lp.elements, rp.elements));
        }
        public static PolinomWithStruct operator *(double[] lp, PolinomWithStruct rp)
        {
            return new PolinomWithStruct(Mult(ToPolinomElement(lp), rp.elements));
        }
        public static PolinomWithStruct operator *(PolinomWithStruct lp, double[] rp)
        {
            return new PolinomWithStruct(Mult(lp.elements, ToPolinomElement(rp)));
        }
        #endregion Operator*

        private static int[] GetPowers(PolinomElement[] pe)
        {
            int[] buffer = new int[pe.Length];
            for (int i = 0; i < pe.Length; i++)
            {
                buffer[i] = pe[i].power;
            }
            buffer = buffer.Distinct().ToArray();
            return buffer;
        }

        private static PolinomElement[] Operate(PolinomElement[] p1, PolinomElement[] p2, Func<double, double, double> operation)
        {
            if (p1 == null || p2 == null || operation == null) throw new ArgumentNullException();
            if (p1.Length == 0) return p1;
            if (p2.Length == 0) return p2;
            int[] allPowers = GetPowers(p1).Union(GetPowers(p2)).ToArray();
            PolinomElement[] result = new PolinomElement[allPowers.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i].power = allPowers[i];
                for (int j = 0; j < p1.Length; j++)
                {
                    if (result[i].power == p1[j].power)
                        result[i].coefficient += p1[j].coefficient;
                }
                for (int j = 0; j < p2.Length; j++)
                {
                    if (result[i].power == p2[j].power)
                        result[i].coefficient = operation(result[i].coefficient, p2[j].coefficient);
                }
            }
            return result;
        }

        private static PolinomElement[] Sum(PolinomElement[] p1, PolinomElement[] p2) =>
            Operate(p1, p2, (a, b) => a + b);


        private static PolinomElement[] Sub(PolinomElement[] p1, PolinomElement[] p2) =>
            Operate(p1, p2, (a, b) => a - b);

        private static PolinomElement[] Mult(PolinomElement[] p1, PolinomElement[] p2)
        {
            if (p1 == null || p2 == null) throw new ArgumentNullException();
            if (p1.Length == 0) return p1;
            if (p2.Length == 0) return p2;
            PolinomElement[] result = new PolinomElement[p1.Length*p2.Length];
            int index = 0;
            for (int i = 0; i < p1.Length; i++)
            {
                for (int j = 0; j < p2.Length; j++)
                {
                    result[index].coefficient = p1[i].coefficient * p2[j].coefficient;
                    result[index].power = p1[i].power + p2[j].power;
                    index++;
                }
            }
            for (int i = 0; i < result.Length; i++)
            {
                for (int j = i+1; j < result.Length; j++)
                {
                    if (result[i].power == result[j].power && result[j].coefficient!=0)
                    {
                        result[i].coefficient += result[j].coefficient;
                        result[j].coefficient = 0;
                    }
                }
            }
            
            return DeleteEmptyElements(result);
        }

        private static PolinomElement[] DeleteEmptyElements(PolinomElement[] polinom)
        {
            int buffer = 0;
            for (int i = 0; i < polinom.Length; i++)
            {
                if (polinom[i].coefficient != 0) buffer++;
            }
            PolinomElement[] result = new PolinomElement[buffer];
            int index = 0;
            for (int i = 0; i < polinom.Length; i++)
            {
                if (polinom[i].coefficient != 0)
                {
                    result[index] = polinom[i];
                    index++;
                }
            }
            return result;
        }
    }
}
