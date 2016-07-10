using System;
using NUnit.Framework;
using PolinomCollection;
using System.Diagnostics;
using System.Linq;

namespace CollectionNTesst
{
    [TestFixture]
    public class PolinomNTest
    {
        [Test]
        [TestCase(new double[] { 1, 2 },
                  new double[] { 1, 2 },
            Result = new double[] { 1, 4, 4 })]
        [TestCase(new double[] { 0, 1 },
                  new double[] { 0, 1 },
            Result = new double[] { 0,0,1 })]
        [TestCase(new double[] { 0,0,1},
                  new double[] { 2 },
            Result = new double[] { 0,0,2})]

        public double[] Polinom_Mult_P1_P2_Result(double[] d1, double[] d2)
        {
            Polinom p1 = new Polinom(d1);
            Polinom p2 = new Polinom(d2);
            Polinom p3 = p1 * p2;
            return p3.Elements;
           // Assert.IsTrue(Enumerable.SequenceEqual(p3.elements, result));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Polinom_Empty_Mult_Polinom_Exeption()
        {
            double[] d1 = null;
            Polinom p1 = new Polinom(d1);
            p1.Calculate(10);
        }

        [Test]
        [TestCase(new double[] { 1, 2 },
                  new double[] { 1, 2 },
            Result = new double[] { 2,4 })]
        [TestCase(new double[] { 0, 1 },
                  new double[] { 0, 1 },
            Result = new double[] { 0, 2 })]
        [TestCase(new double[] { 0, 0, 1 },
                  new double[] { 2 },
            Result = new double[] { 2,0,1})]
        public double[] Polinom_Sum_P1_P2_Result(double[] d1, double[] d2)
        {
            Polinom p1 = new Polinom(d1);
            Polinom p2 = new Polinom(d2);
            Polinom p3 = p1 + p2;
            return p3.Elements;
        }

        [Test]
        [TestCase(new double[] { 1, 2 },
                  new double[] { 1, 2 },
            Result = new double[] {0, 0})]
        [TestCase(new double[] { 0, 1 },
                  new double[] { 0, 1 },
            Result = new double[] { 0, 0 })]
        [TestCase(new double[] { 0, 0, 1 },
                  new double[] { 2 },
            Result = new double[] { -2, 0, 1 })]
        public double[] Polinom_Sub_P1_P2_Result(double[] d1, double[] d2)
        {
            Polinom p1 = new Polinom(d1);
            Polinom p2 = new Polinom(d2);
            Polinom p3 = p1 - p2;
            return p3.Elements;
        }

        [Test]
        [TestCase(new double[] {},
                  new double[] { 1, 2 },
            Result = new double[] {1,2 })]
        [TestCase(new double[] {},
                  new double[] { 0, 1 },
            Result = new double[] { 0,1 })]
        [TestCase(new double[] {},
                  new double[] { 2 },
            Result = new double[] { 2 })]
        public double[] Polinom_Sum_Empty_P2_Result(double[] d1, double[] d2)
        {
            Polinom p2 = new Polinom(d2);
            Polinom p3 = p2 - d1;
            return p3.Elements;
        }
    }
}