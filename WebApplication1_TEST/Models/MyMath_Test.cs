using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1_TEST.Models
{
    [TestClass]
    public class MyMath_Test
    {
        [TestMethod]
        public void Podziel_Test1()
        {
            decimal a = 8, b = 9;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(a / b, wynik);
        }

        [TestMethod]
        public void Podziel_Test2()
        {
            decimal a = 8436, b = 0;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(0, wynik);
        }

        [TestMethod]
        public void Podziel_Test3()
        {
            decimal a = 234258, b = 9252425;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(a / b, wynik);
        }

        [TestMethod]
        public void Podziel_Test4()
        {
            decimal a = 8342, b = 3253349;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(a / b, wynik);
        }

        [TestMethod]
        public void Podziel_Test5()
        {
            decimal a = 438, b = 234235;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(a / b, wynik);
        }

        [TestMethod]
        public void Podziel_Test6()
        {
            decimal a = 438, b = -213;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(a / b, wynik);
        }

        [TestMethod]
        public void Podziel_Test7()
        {
            decimal a = 438, b = -0;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(0, wynik);
        }

        [TestMethod]
        public void Podziel_Test8()
        {
            decimal a = 1, b = -1;
            decimal wynik = MyMath.Podziel(a, b);
            Assert.AreEqual(a / b, wynik);
        }

        [TestMethod]
        public void Podziel_Test_11()
        {
            decimal a = 1, b = -1, c= 1;
            decimal wynik = MyMath.Podziel(a, b, c);
            Assert.AreEqual(0, wynik);
        }

        [TestMethod]
        public void Podziel_Test_12()
        {
            decimal a = 8, b = 2, c = 2;
            decimal wynik = MyMath.Podziel(a, b, c);
            Assert.AreEqual(2, wynik);
        }

        [TestMethod]
        public void Podziel_Test_13()
        {
            decimal a = 0, b = 2, c = 2;
            decimal wynik = MyMath.Podziel(a, b, c);
            Assert.AreEqual(0, wynik);
        }

        [TestMethod]
        public void Podziel_Test_14()
        {
            decimal a = -4, b = 1, c = 1;
            decimal wynik = MyMath.Podziel(a, b, c);
            Assert.AreEqual(-2, wynik);
        }


    }
}
