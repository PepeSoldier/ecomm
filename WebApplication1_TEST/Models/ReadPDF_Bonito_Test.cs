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
    public class ReadPDF_Bonito_Test
    {
        [TestMethod]
        public void GetSymbol_Test()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symbol: 9788394018535, PKWiU: 58.11.1";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9788394018535");
        }

        [TestMethod]
        public void GetSymbol_Test2()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symbol:0788324018835, PKWiU: 58.11.1";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "0788324018835");
        }

        [TestMethod]
        public void GetSymbol_Test3()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symbol: 9588384028535 Symbol";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9588384028535");
        }

        [TestMethod]
        public void GetSymbol_Test4()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symbol: 1788394018835 Artur Kamil Piotr";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "1788394018835");
        }

        [TestMethod]
        public void GetSymbol_Test5()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symb. 9788398018535 Artur Kamil Piotr";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9788398018535");
        }

        [TestMethod]
        public void GetSymbol_Test6()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symb. 9438352311235 4321 Kamil Piotr";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9438352311235");
        }

        [TestMethod]
        public void GetSymbol_Test7()
        {
            string text = "Samoleczenie metodą B.S.M. + CD Z Symb. 9788352318535 32523 Kamil Piotr";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9788352318535");
        }

        [TestMethod]
        public void GetSymbol_Test8()
        {
            string text = "Samoleczenie metodą B.S.M. + CD 10005000 Z Symb. 9788352318535 32523 Kamil 19880507 Piotr";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9788352318535");
        }
        [TestMethod]
        public void GetSymbol_Test9()
        {
            string text = "Samoleczenie metodą B.S.M. + CD (9788352318535)";
            string[] subsrings = text.Split(' ');
            string symbol = ReadPDF_Bonito.GetSymbol(subsrings);

            Assert.AreEqual(symbol, "9788352318535");
        }

    }
}
