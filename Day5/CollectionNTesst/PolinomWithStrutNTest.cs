using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using PolinomCollection;
using System.Linq;

namespace CollectionNTesst
{
    /// <summary>
    /// Summary description for PolinomWithStrutNTest
    /// </summary>
    [TestFixture]
    public class PolinomWithStrutNTest
    {
        [Test]
        public void ToPolinom()
        {
           var arr = new double[4] { 7,7,0,7};
           var temp = new PolinomWithStruct(arr);
           var s = temp.Elements;
            Assert.IsTrue(
                s[0].coefficient == 7 && s[0].power == 0 &&
                s[2].coefficient == 7 && s[2].power == 3
                );
        }

        [Test]
        public void Sum_101_101_202()
        {
            var arr = new double[3]  { 1, 0, 1 };
            var arr2 = new double[3] { 2, 0, 2 };
            var temp1 = new PolinomWithStruct(arr);
            var temp2 = new PolinomWithStruct(arr);
            var temp3 = temp1 + temp2;
            temp1 = new PolinomWithStruct(arr2);
            Assert.IsTrue(Enumerable.SequenceEqual(temp3.Elements,temp1.Elements));
        }

        [Test]
        public void Sub_101_101_000()
        {
            var arr = new double[3] { 1, 0, 1 };
            var arr2 = new PolinomElement[2];
            arr2[0].power = 0;
            arr2[0].coefficient = 0;
            arr2[1].power = 2;
            arr2[1].coefficient = 0;
            var temp1 = new PolinomWithStruct(arr);
            var temp2 = new PolinomWithStruct(arr);
            var temp3 = temp1 - temp2;
            Assert.IsTrue(Enumerable.SequenceEqual(temp3.Elements, arr2));
        }

        [Test]
        public void Mult_11_11_121()
        {
            var arr = new double[2] { 1, 1 };
            var arr2 = new PolinomElement[3];
            arr2[0].power = 0;
            arr2[0].coefficient = 1;
            arr2[1].power = 1;
            arr2[1].coefficient = 2;
            arr2[2].power = 2;
            arr2[2].coefficient = 1;
            var temp1 = new PolinomWithStruct(arr);
            var temp2 = new PolinomWithStruct(arr);
            var temp3 = temp1 * temp2;
            Assert.IsTrue(Enumerable.SequenceEqual(temp3.Elements, arr2));
        }
    }
}
