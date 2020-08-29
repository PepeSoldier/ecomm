using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1.Models
{
    public class ReadPDF_Bonito : ReadPDF
    {

        public override Faktura PutDataToInvoice(string theText)
        {

            Faktura faktura = new Faktura();
            Regex nrFakturyBonito = new Regex(@"FB\/[0-9]{0,2}\/[0-9]{0,2}\/[0-9]{0,6}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            bool write = false;
            string temp = "";
            string[] theLines = theText.Split('\n');

            foreach (string theLine in theLines)
            {

                Match m = nrFakturyBonito.Match(theLine);
                if (m.Success)
                {
                    faktura.Numer = m.ToString();
                }


                if (theLine.StartsWith("Razem:"))
                {
                    write = false;
                }

                if (write == true)
                {
                    if (Regex.IsMatch(temp, @"^\d+. "))
                    {
                        temp += " " + theLine;
                        string[] substrings = temp.Split(' ');
                        string[] cenyZFakury = pobierzCenyZFaktury(substrings);
                        faktura.Pozycje.Add(new FakturaPozycja()
                        {
                            Lp = int.Parse(substrings[0].Replace(".", "")),
                            Nazwa = string.Join(" ", substrings.Slice(1, -14)),
                            Ilosc = int.Parse(substrings[substrings.Length - 14]),
                            Jm = substrings[substrings.Length - 13],
                            CenaJednostkowa = Convert.ToDecimal(cenyZFakury[5]),
                            WartoscBrutto = Convert.ToDecimal(cenyZFakury[1]),
                            VAT = int.Parse(substrings[substrings.Length - 10]),
                            WartoscVAT = Convert.ToDecimal(cenyZFakury[2]),
                            WartoscNetto = Convert.ToDecimal(cenyZFakury[3]),
                            Symbol = GetSymbol(substrings),
                            PKWiU = substrings[substrings.Length - 1]
                        });
                    }
                }

                if (theLine == "netto")
                {
                    write = true;
                }
                temp = theLine;
            };
            return faktura;

        }

        public static string GetSymbol(string[] substrings)
        {
            Regex symbolRegex = new Regex("[0-9]{6,}");
            string wiersz = string.Join(" ", substrings);
            MatchCollection matches = symbolRegex.Matches(wiersz);
            if (matches.Count == 1)
            {
                return matches[0].Value;
            }
            else if (matches.Count > 1)
            {
                string match = Regex.Match(wiersz, "[sS][Yy][Mm].{0,5} [0-9]{5,}").Value;
                string[] symbol = match.Split(' ');
                return symbol[symbol.Length - 1];
            }
            return "";
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