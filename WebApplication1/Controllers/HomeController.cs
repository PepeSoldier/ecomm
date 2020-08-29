using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Migrations;
using System.Data;
using System.Xml;
using System.Reflection;
using System.Xml.Serialization;
using WebApplication1.ViewModels;
using WebApplication1.Mappers;

public static class Extensions
{
    /// <summary>
    /// Get the array slice between the two indexes.
    /// ... Inclusive for start index, exclusive for end index.
    /// </summary>
    public static T[] Slice<T>(this T[] source, int start, int end)
    {
        // Handles negative ends.
        if (end < 0)
        {
            end = source.Length + end;
        } 
        // Handles negative starts.
        if (start < 0)
        {
            start = source.Length + start;
        }
        int len = end - start;

        // Return new array.
        return new List<T>(source)
                    .GetRange(start, len)
                    .ToArray();
    }

}

namespace WebApplication1.Controllers
{

    public class HomeController : Controller
    {

        MyDBContext Db = new MyDBContext();

        public ActionResult Index()
        {
            ViewBag.imie = "Kuba";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult WZ()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tabelka()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult readPDF()
        {
            Faktury fakturyZPlikow = new Faktury();
            Faktury fakturyZBazy = new Faktury();

            for (int i = 1; i <= 12; i++)
            {
                string path = @"C:\Users\IMPLEA\Desktop\Artur\test\" + i + ".pdf";
                //string path = @"C:\Users\IMPLEA\Desktop\Artur\test\2.pdf"; // <= testowe
                czytajFaktureZPliku(fakturyZPlikow, path);
            }

            ZapiszNoweFakturyDoBazy(fakturyZPlikow);
            PobierzFakturyZBazy(fakturyZBazy);

            ZapisywanieFakturDoXML(fakturyZBazy.ListaFaktur);
            ViewBag.faktury = fakturyZBazy;
            return View();
        }

        private void czytajFaktureZPliku(Faktury fakturyZPlikow, string path)
        {
            ReadPDF pdfReader = null;
            string invoiceType = ReadPDF.GetInvoiceType(path);

            switch (invoiceType)
            {
                case "Empik": pdfReader = new ReadPDF_Empik(); break;
                case "KFI": pdfReader = new ReadPDF_KFI(); break;
                case "SwiatKsiazki": pdfReader = new ReadPDF_SwiatKsiazki(); break;
                case "Bonito": pdfReader = new ReadPDF_Bonito(); break;
            }

            fakturyZPlikow.ListaFaktur.Add(pdfReader.PutDataToInvoice(ReadPDF.ReadDataFromPdf(path)));
        }

        private void ZapisywanieFakturDoXML(List<Faktura> faktury)
        {
            foreach(Faktura fktr in faktury)
            {
                foreach (FakturaPozycja fp in fktr.Pozycje)
                {
                    fp.Faktura = null;
                }
            }

            List<FakturaXMLViewModel> FakturaXMLViewModel = faktury.ToList<FakturaXMLViewModel>();
            XmlSerializer x = new XmlSerializer(typeof(List<FakturaXMLViewModel>), new XmlRootAttribute("Faktury"));
            TextWriter writer = new StreamWriter(@"C:\Users\IMPLEA\Desktop\Artur\test\x.xml");
            x.Serialize(writer, FakturaXMLViewModel);  
        }

        private void ZapisywanieFakturyDoXML(Faktura faktura)
        {
            if (!System.IO.File.Exists(@"C:\Users\IMPLEA\Desktop\Artur\test\" + faktura.Numer.Replace("/", "-") + ".xml"))
            {
                foreach (FakturaPozycja fp in faktura.Pozycje)
                {
                    fp.Faktura = null;
                }

                List<FakturaPozycjaXMLViewModel> fakturaPozycjaXMLViewModelList = faktura.Pozycje.ToList<FakturaPozycjaXMLViewModel>();
                XmlSerializer x = new XmlSerializer(typeof(List<FakturaPozycjaXMLViewModel>), new XmlRootAttribute("Faktura"));
                TextWriter writer = new StreamWriter(@"C:\Users\IMPLEA\Desktop\Artur\test\" + faktura.Numer.Replace("/", "-") + ".xml");
                x.Serialize(writer, fakturaPozycjaXMLViewModelList);
            }

        }

        private void PobierzFakturyZBazy(Faktury fakturyZBazy)
        {
            Regex nrFakturyEmpik = new Regex(@"FS\/[0-9]{0,}\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex nrFakturyKFI = new Regex(@"[0-9]{0,}\/KFI\/[0-9]{0,2}\/[0-9]{0,4}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex nrFakturySK = new Regex(@"[0-9]{4}\/[0-9]{2}\/.{2,}\/.{2}\/[0-9]{6,}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            List<Faktura> ListaFaktur = Db.Faktury.ToList();

            ListaFaktur.ForEach(fktr =>
            {
                Match empik = nrFakturyEmpik.Match(fktr.Numer);
                Match KFI = nrFakturyKFI.Match(fktr.Numer);
                Match SK = nrFakturySK.Match(fktr.Numer);
                if (empik.Success || KFI.Success || SK.Success)
                {
                    fakturyZBazy.ListaFaktur.Add(Db.Faktury.Include(y => y.Pozycje).Where(x => x.Id == fktr.Id).FirstOrDefault());
                }
            });
        }

        private void ZapiszNoweFakturyDoBazy(Faktury faktury)
        {
            foreach (Faktura f in faktury.ListaFaktur)
            {
                var numers = Db.Faktury.Select(x => x.Numer).ToList();
                var exists = numers.Contains(f.Numer);
                if (!exists)
                {
                    Db.Faktury.Add(f);
                    Db.FakturaPozycja.AddRange(f.Pozycje);
                }
            }
            Db.SaveChanges();
        }
    }
}