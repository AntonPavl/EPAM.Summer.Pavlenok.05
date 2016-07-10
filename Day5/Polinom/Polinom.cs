using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolinomCollection
{
    public class Polinom
    {
        public readonly double[] elements;
        public Polinom(double[] polinom)
        {
            elements = polinom;
        }
        public double Calculate(double num)
        {
            if (elements == null) throw new ArgumentNullException();
            double result = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                result += elements[i] * Math.Pow(num, i);
            }
            return result;
        }

        public override bool Equals(object obj) 
        {
            if (obj == null || !(obj is Polinom))
                return false;
            else
                return Enumerable.SequenceEqual(elements,((Polinom)obj).elements);
        }

        public override string ToString()
        {
            if (elements == null) return "";
            string result = String.Empty;
            for (int i = 0; i < elements.Length; i++)
            {
               if (elements[i]!=0)
               {
                    result += $"{elements[i]}*X^{i} ";
               }    
            }
            return result;
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
            return new Polinom(Dif(lp.elements, rp.elements));
        }
        public static Polinom operator -(double[] lp, Polinom rp)
        {
            return new Polinom(Dif(lp, rp.elements));
        }
        public static Polinom operator -(Polinom lp, double[] rp)
        {
            return new Polinom(Dif(lp.elements, rp));
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




        private static double[] Sum(double[] p1,double[] p2)
        {
            double[] result;
            if (p1.Length == p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = p1[i] + p2[i];
                }
            }
            else if (p1.Length>p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p2.Length; i++)
                {
                    result[i] = p1[i] + p2[i];
                }
                for (int i = p2.Length; i < p1.Length; i++)
                {
                    result[i] = p1[i];
                }
            }
            else
            {
                result = new double[p2.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = p1[i] + p2[i];
                }
                for (int i = p1.Length; i < p2.Length; i++)
                {
                    result[i] = p2[i];
                }
            }
            return result;
        }

        private static double[] Dif(double[] p1, double[] p2)
        {
            double[] result;
            if (p1.Length == p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = p1[i] - p2[i];
                }
            }
            else if (p1.Length > p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p2.Length; i++)
                {
                    result[i] = p1[i] - p2[i];
                }
                for (int i = p2.Length; i < p1.Length; i++)
                {
                    result[i] = p1[i];
                }
            }
            else
            {
                result = new double[p2.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = p1[i] - p2[i];
                }
                for (int i = p1.Length; i < p2.Length; i++)
                {
                    result[i] = p2[i];
                }
            }
            return result;
        }

        private static double[] Mult(double[] p1, double[] p2)
        {
            double[] result;
            if (p1.Length == p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = p1[i] + p2[i];
                }
            }
            else if (p1.Length > p2.Length)
            {
                result = new double[p1.Length];
                for (int i = 0; i < p2.Length; i++)
                {
                    result[i] = p1[i] + p2[i];
                }
                for (int i = p2.Length; i < p1.Length; i++)
                {
                    result[i] = p1[i];
                }
            }
            else
            {
                result = new double[p2.Length];
                for (int i = 0; i < p1.Length; i++)
                {
                    result[i] = p1[i] + p2[i];
                }
                for (int i = p1.Length; i < p2.Length; i++)
                {
                    result[i] = p2[i];
                }
            }
            return result;
        }
    }
}
