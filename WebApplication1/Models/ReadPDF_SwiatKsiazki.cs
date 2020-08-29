using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Models
{
    public class ReadPDF_SwiatKsiazki : ReadPDF
    {

        public ReadPDF_SwiatKsiazki() : base()
        {

        }

        public override Faktura PutDataToInvoice(string theText)
        {
            Faktura faktura = new Faktura();
            Regex nrFakturySK = new Regex(@"[0-9]{4}\/[0-9]{2}\/.{2,}\/.{2}\/[0-9]{6,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool write = false;
            string temp = "";
            string[] theLines = theText.Split('\n');

            foreach (string theLine in theLines)
            {
                Match m = nrFakturySK.Match(theLine);
                if (m.Success && theLine.IndexOf("Faktura") >= 0)
                {
                    faktura.Numer = m.ToString();
                }

                if (theLine.StartsWith("Razem"))
                {
                    write = false;
                }

                if (write == true)
                {
                    string[] substrings = theLine.Split(' ');
                    string[] cenyZFaktury = pobierzCenyZFaktury(substrings);

                    if (Regex.IsMatch(theLine, @"^ \d+ ")){
                        faktura.Pozycje.Add(new FakturaPozycja()
                        {
                            Lp = int.Parse(substrings[1]),
                            Symbol = substrings[2],
                            Nazwa = string.Join(" ", substrings.Slice(3, -8)),
                            PKWiU = substrings[substrings.Length - 8],
                            Jm = substrings[substrings.Length - 7],
                            Ilosc = Convert.ToInt32(Convert.ToDouble( substrings[substrings.Length - 6])),
                            CenaJednostkowa = Convert.ToDecimal(cenyZFaktury[1]),
                            WartoscNetto = Convert.ToDecimal(cenyZFaktury[2]),
                            VAT = int.Parse(substrings[substrings.Length - 3].Replace("%","")),
                            WartoscVAT = Convert.ToDecimal(cenyZFaktury[3]),
                            WartoscBrutto = Convert.ToDecimal(cenyZFaktury[4])
                        });
                    };
                }

                if (theLine == "%" && temp.IndexOf("Lp") >= 0 )
                {
                    write = true;
                }

                temp = theLine;
            };
            return faktura;
        }

        private string[] pobierzCenyZFaktury(string[] substrings)
        {
            string stringFaktura = string.Join(" ", substrings);
            MatchCollection matches = Regex.Matches(stringFaktura, @"\d+,\d{2}");
            string[] cenyZFaktury = matches.Cast<Match>().Select(m => m.Value).ToArray();
            return cenyZFaktury;
        }
    }
}