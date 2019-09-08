using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FishingSentencesFromPdfDotNetFramework.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult Convert(string text)
        {
            ViewBag.Message = "Try to convert PDF to string.";

            if (!string.IsNullOrEmpty(text))
                ViewBag.Content = text;

            return View();
        }


        public ActionResult OnPostConvert(HttpPostedFileBase postedFileBase)
        {
            var filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), postedFileBase.FileName);
            postedFileBase.SaveAs(filePath);

            StringBuilder text = new StringBuilder();

            if (postedFileBase.ContentLength > 0)
            {
                try
                {
                    using (PdfReader reader = new PdfReader(filePath))
                    {
                        for (int page = 1; page < reader.NumberOfPages; page++)
                        {
                            text.Append(PdfTextExtractor.GetTextFromPage(reader, page));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            ViewBag.Content = text.ToString();

            return View();
        }
    }
}