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
        public void TestMethod1()
        {
            var test = new Polinom(new double[3] {0,10,1});
            Debug.WriteLine($"Elements = {test.Calculate(2)}");
            var a = new double[2] { 1, 1 };
            var b = new double[2] { 1, 1 };
            Assert.AreEqual(Enumerable.SequenceEqual(a, b), true);
        }

        [Test]
        public void TestMethod2()
        {
            var test = new Polinom(new double[6] { 0, 10, 1,0,-3,-5 });
            var test2 = new Polinom(new double[6] { 0, -10, -1, 0, 3, 6 });
            test += test2;
            Debug.WriteLine($"Elements = {test.ToString()}");
            Assert.AreEqual(1, 1);
        }
    }
}