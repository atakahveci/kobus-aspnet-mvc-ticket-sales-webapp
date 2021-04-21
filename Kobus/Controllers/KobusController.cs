using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kobus.Entity;
using Kobus.Globals;
using Kobus.Substructure;

namespace Kobus.Controllers
{
    public class KobusController : Controller
    {
        // GET: Kobus

        Entity.Entity entity = new Entity.Entity();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginEmployee()
        {
            return View();
        }

        public ActionResult Sefer()
        {
            Globals.Globals.FileBus = Path.Combine(Server.MapPath("~/File/"));
            Globals.Globals.VoyageListLength();
            Globals.Globals.FileControl();
            return View();
        }
        public JsonResult VoyageAdd(Models.Voyage Voyage)
        {
            DoubleDirectionalList VoyageList = new DoubleDirectionalList();
            IFormatProvider culture = new CultureInfo("tr-TR", true);
            DateTime date = DateTime.ParseExact(Voyage.Date, "yyyy-MM-dd", culture);
            Voyage.Date = Globals.Globals.DATEFILE(date);
            VoyageList.Add(Convert.ToInt32(Globals.Globals.Length));
            VoyageList.Add(Voyage.Route);
            VoyageList.Add(Voyage.Date);
            VoyageList.Add(Voyage.Time);
            VoyageList.Add(Voyage.Capacity);
            VoyageList.Add(Voyage.TicketPrice);
            VoyageList.Add(Voyage.Plaqa);
            VoyageList.Add(Voyage.Captain);
            VoyageList.Add(Voyage.Bus);
            entity.VoyageAdd(VoyageList, Voyage.Capacity);
            entity.Days(Voyage.Date, VoyageList , Voyage.Capacity);
            return Json("");
        }

        public JsonResult VoyageEditData(int data)
        {
            Globals.Globals.Data = data;
            Models.Voyage voyage = new Models.Voyage();
            DoubleDirectionalList List = entity.VoyageList(data);
            IFormatProvider culture = new CultureInfo("tr-TR", true);
            DateTime date = DateTime.ParseExact(List.Data(2).ToString(),"dd.MM.yyyy",culture);
            string DateHTML = Globals.Globals.DATEHTML(date); 
            voyage.Route = List.Data(1).ToString();
            voyage.Date = DateHTML;
            voyage.Time = List.Data(3).ToString();
            voyage.Capacity = List.Data(4).ToString();
            voyage.TicketPrice = List.Data(5).ToString();
            voyage.Plaqa = List.Data(6).ToString();
            voyage.Captain = List.Data(7).ToString();
            voyage.Bus = List.Data(8).ToString();
            return Json(voyage);
        }
        public JsonResult VoyageEdit(Models.Voyage Voyage)
        {
            DoubleDirectionalList VoyageList = new DoubleDirectionalList();
            IFormatProvider culture = new CultureInfo("tr-TR", true);
            DateTime date = DateTime.ParseExact(Voyage.Date, "yyyy-MM-dd", culture);
            Voyage.Date = Globals.Globals.DATEFILE(date);
            VoyageList.Add(Globals.Globals.Data);
            VoyageList.Add(Voyage.Route);
            VoyageList.Add(Voyage.Date);
            VoyageList.Add(Voyage.Time);
            VoyageList.Add(Voyage.Capacity);
            VoyageList.Add(Voyage.TicketPrice);
            VoyageList.Add(Voyage.Plaqa);
            VoyageList.Add(Voyage.Captain);
            VoyageList.Add(Voyage.Bus);
            entity.VoyageEdit(VoyageList, Globals.Globals.Data);
            return Json("");

        }
        public JsonResult VoyageDelete(int data)
        {
            Globals.Globals.VoyageCapacity(data);
            foreach (var item in Globals.Globals.Capacity)
            {
                if (item != "Null")
                {
                    entity.Log(DateTime.Now + " - HATA!" + data + "numaralı sefer iptal edilemez");
                    return Json("Sefer İptal Edilemez");
                }
            }
            entity.VoyageDelete(data);
            return Json("Silindi");
        }
        public ActionResult DeletedVoyageList()
        {
            Globals.Globals.FileBus = Path.Combine(Server.MapPath("~/File/"));
            Globals.Globals.VoyageListLength();
            Globals.Globals.FileControl();
            return View();
        }
        public ActionResult HistoryVoyageList()
        {
            Globals.Globals.FileBus = Path.Combine(Server.MapPath("~/File/"));
            Globals.Globals.VoyageListLength();
            Globals.Globals.FileControl();
            return View();
        }
        public JsonResult BackToDeletedVoyage(int data)
        {
            entity.VoyageBackToDeleted(data);
            return Json("");
        }
        public ActionResult BiletEmployee()
        {
            return View();
        }
        public ActionResult BiletAdmin()
        {
            Globals.Globals.FileBus = Path.Combine(Server.MapPath("~/File/"));
            Globals.Globals.VoyageListLength();
            Globals.Globals.FileControl();
            return View();
        }
        public JsonResult TicketCapacity(int data)
        {
            Globals.Globals.Data = data;
            Globals.Globals.VoyageCapacity(data);
            return Json(Globals.Globals.Capacity);
        }
        public ActionResult BiletAl()
        {
            return View();
        }
        public JsonResult TicketBuy(string[] koltuknumarasi, string[] adsoyad, string[] cinsiyet, string durum)
        {
            for (int i = 0; i < koltuknumarasi.Length; i++)
            {
                entity.TicketBuy(koltuknumarasi[i], adsoyad[i], cinsiyet[i], durum);
            }
            Globals.Globals.VoyageCapacity(Globals.Globals.Data);
            return Json("");
        }
        public JsonResult TicketClose(int data)
        {
            Globals.Globals.Capacity[data] = "Null";
            entity.TicketClose(Globals.Globals.Capacity);
            return Json("");
        }
        public ActionResult AllVoyageList()
        {
            Globals.Globals.FileBus = Path.Combine(Server.MapPath("~/File/"));
            Globals.Globals.VoyageListLength();
            Globals.Globals.FileControl();
            return View();
        }

    }
}