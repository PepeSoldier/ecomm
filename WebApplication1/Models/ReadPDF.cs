using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WebApplication1.Models
{
    public class ReadPDF
    {
        Faktura faktura;
        public string imie;
        protected string pesel;
        private string wiek;

        public ReadPDF()
        {
        }

        public ReadPDF(string imie, string pesel, string wiek)
        {
            this.faktura = new Faktura();
            this.imie = imie;
            this.pesel = pesel;
            this.wiek = wiek;
        }

        /// <summary>
        /// Zwraca: Empik, SwiatKsiązki, KFI
        /// </summary>
        /// <returns></returns>
        public static string GetInvoiceType(string path)
        {
            Regex nrFakturyEmpik = new Regex(@"FS\/[0-9]{0,}\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex nrFakturyKFI = new Regex(@"[0-9]{0,}\/KFI\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex nrFakturySK = new Regex(@"[0-9]{4}\/[0-9]{2}\/.{2,}\/.{2}\/[0-9]{6,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex nrFakturyBonito = new Regex(@"FB\/[0-9]{0,2}\/[0-9]{0,2}\/[0-9]{0,6}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            string invoiceLines = ReadDataFromPdf(path);

            if (nrFakturyEmpik.Match(invoiceLines).Success)
            {
                return "Empik";
            }
            else if(nrFakturyKFI.Match(invoiceLines).Success)
            {
                return "KFI";
            }
            else if (nrFakturySK.Match(invoiceLines).Success)
            {
                return "SwiatKsiazki";
            }
            else if (nrFakturyBonito.Match(invoiceLines).Success)
            {
                return "Bonito";
            }
            else
            {
                return "";
            }
            
        }

        /*public static Faktura ExtractTextFromPdf(string path)
        {
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();

            using (PdfReader reader = new PdfReader(path))
            {
                
                bool write = false;
                string sklep = "";

                Faktura faktura = new Faktura();
                faktura.Pozycje = new List<FakturaPozycja>();

                Regex nrFakturyEmpik = new Regex(@"FS\/[0-9]{0,}\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex nrFaktury = new Regex(@"FS\/[0-9]{0,}\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, i, its);

                    if (nrFakturyEmpik.Match(thePage).Success)
                    {
                        sklep = "Empik";
                    };

                    string[] theLines = thePage.Split('\n');

                    switch (sklep)
                    {
                        case "Empik":
                            foreach (var theLine in theLines)
                            {

                                Match m = nrFakturyEmpik.Match(theLine);
                                if (m.Success)
                                {
                                    faktura.Numer = m.ToString();
                                }


                                if (theLine.IndexOf("Razem:") >= 0)
                                {
                                    write = false;
                                }

                                if (write == true)
                                {
                                    String[] substrings = theLine.Split(' ');

                                    if (substrings[1] != "%")
                                    {

                                        faktura.Pozycje.Add(new FakturaPozycja()
                                        {
                                            Lp = int.Parse(substrings[0]),
                                            Symbol = substrings[1],
                                            Nazwa = string.Join(" ", substrings.Slice(2, -8)),
                                            Jm = substrings[substrings.Length - 8],
                                            Ilosc = int.Parse(substrings[substrings.Length - 7]),
                                            CenaJednostkowa = Convert.ToDecimal(substrings[substrings.Length - 6]),
                                            VAT = int.Parse(substrings[substrings.Length - 5]),
                                            WartoscNetto = Convert.ToDecimal(substrings[substrings.Length - 3]),
                                            WartoscVAT = Convert.ToDecimal(substrings[substrings.Length - 2]),
                                            WartoscBrutto = Convert.ToDecimal(substrings[substrings.Length - 1])
                                        });
                                    }
                                }

                                if (theLine == "netto VAT netto VAT towaru / usługi")
                                {
                                    write = true;
                                }

                            };
                            break;
                        case "x":

                            break;
                        default:
                            Debug.WriteLine("Brak");
                            break;
                    };

                }

                return faktura;
            }
        }
        */
        public static string ReadDataFromPdf(string path)
        {
            ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
            string PDFText = "";

            if (System.IO.File.Exists(path))
            {
                using (PdfReader reader = new PdfReader(path))
                for (int j = 1; j <= reader.NumberOfPages; j++)
                {
                    string thePage = PdfTextExtractor.GetTextFromPage(reader, j, its);
                    //string[] theLines = thePage.Split('\n');
                    PDFText += thePage;             
                }
            }
            return PDFText;
        }

        public virtual Faktura PutDataToInvoice(string theText)
        {
            return new Faktura();
        }
    }
}
