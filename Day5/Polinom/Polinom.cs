using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolinomCollection
{
    public class Polinom
    {
        private readonly double[] elements;
        /// <summary>
        /// Get an array of  PolinomElement structs
        /// </summary>
        public double[] Elements { get { return elements; } }
        /// <summary>
        /// Create a class from an array of doubles
        /// </summary>
        /// <param name="polinom">Array of doubles</param>
        public Polinom(double[] polinom)
        {
            elements = polinom;
        }
        /// <summary>
        /// Evaluate polinom
        /// </summary>
        /// <param name="num">Polinom's number</param>
        /// <returns></returns>
        public double Calculate(double num)
        {
            if (ReferenceEquals(elements, null)) throw new ArgumentNullException();
            if (elements.Length == 0) return 0;
            double result = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                result += elements[i] * Math.Pow(num, i);
            }
            return result;
        }
        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() 
        {
            return elements.GetHashCode();
        }
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns></returns>
        public override bool Equals(object obj) 
        {
            if (ReferenceEquals(obj, null)|| !(obj is Polinom))
                return false;
            else
                return Enumerable.SequenceEqual(elements,((Polinom)obj).elements);
        }
        /// <summary>
        /// Get Polinom in string's format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (ReferenceEquals(elements,null)) return "";
            var result = new StringBuilder();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != 0)
                {
                    result.Append($"{elements[i]}*X^{i} ");
                }
            }
            return result.ToString();
        }

        #region Operator+
        public static Polinom operator +(Polinom lp, Polinom rp)
        {
            return new Polinom(Sum(lp.elements, rp.elements));
        }
        public static Polinom operator +(double[] lp, Polinom rp)
        {
            return new Polinom(Sum(lp, rp.elements));
        }
        public static Polinom operator +(Polinom lp, double[] rp)
        {
            return new Polinom(Sum(lp.elements, rp));
        }
        #endregion Operator+
        #region Opeartor-
        public static Polinom operator -(Polinom lp, Polinom rp)
        {
            return new Polinom(Sub(lp.elements, rp.elements));
        }
        public static Polinom operator -(double[] lp, Polinom rp)
        {
            return new Polinom(Sub(lp, rp.elements));
        }
        public static Polinom operator -(Polinom lp, double[] rp)
        {
            return new Polinom(Sub(lp.elements, rp));
        }

        #endregion Operator-
        #region Operator*
        public static Polinom operator *(Polinom lp, Polinom rp)
        {
            return new Polinom(Mult(lp.elements, rp.elements));
        }
        public static Polinom operator *(double[] lp, Polinom rp)
        {
            return new Polinom(Mult(lp, rp.elements));
        }
        public static Polinom operator *(Polinom lp, double[] rp)
        {
            return new Polinom(Mult(lp.elements, rp));
        }
        #endregion Operator*

        private static double[] Operate(double[] p1, double[] p2, Func<double, double, double> operation)
        {
            if (p1 == null || p2 == null || operation == null) throw new ArgumentNullException();
            if (p1.Length == 0) return p2;
            if (p2.Length == 0) return p1;
            double[] result;
            if (p1.Length == p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = operation(p1[i], p2[i]);
                }
            }
            else if (p1.Length > p2.Length)
            {
                result = new double[p1.Length];
                Buffer.BlockCopy(p1, 0, result, 0, p1.Length * sizeof(double));

                for (int i = 0; i < p2.Length; i++)
                {
                    result[i] = operation(p1[i], p2[i]);
                }
            }
            else
            {
                result = new double[p2.Length];
                Buffer.BlockCopy(p2, 0, result, 0, p2.Length * sizeof(double));

                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = operation(p1[i], p2[i]);
                }
            }
            return result;
        }

        private static double[] Sum(double[] p1, double[] p2)
        {
            return Operate(p1, p2, (a, b) => a + b);
        }

        private static double[] Sub(double[] p1, double[] p2)
        {
            return Operate(p1, p2, (a, b) => a - b);
        }
        private static double[] Mult(double[] p1, double[] p2)
        {
            if (p1 == null || p2 == null) throw new ArgumentNullException();
            if (p1.Length == 0) return p2;
            if (p2.Length == 0) return p1;

            double[] result;
            if (p1.Length == 1 || p2.Length == 1) result = new double[Math.Max(p1.Length, p2.Length)];
            else result = new double[p1.Length + p2.Length - 1];     
                            
            for (int i = 0; i < p1.Length; i++)
            {
                for (int j = 0; j < p2.Length; j++)
                {
                    result[i + j] += p1[i] * p2[j];
                }
            }
            return result;
        }
    }
}
