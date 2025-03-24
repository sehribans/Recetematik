using Microsoft.AspNetCore.Mvc;
using Recetematik.Models;
using Recetematik.Models.Vm;
using System.Diagnostics;

namespace Recetematik.Controllers
{
    public class HomeController : Controller
    {
        private readonly RmContext _c;

        public HomeController(RmContext c)
        {
            _c = c;
        }

        public IActionResult Index()

        {
          

            var satis = _c.TblSatis.Where(x => x.Tarih.Value.Month == DateTime.Now.Month) ;
            var oncekiay = _c.TblSatis.Where(x => x.Tarih.Value.Month == DateTime.Now.AddMonths(-1).Month);
            var oncekisatis = oncekiay.Sum(x => x.Fiyat);
          
            var ToplamSatis= satis.Sum(x => x.Fiyat);
            var fark = ToplamSatis - oncekisatis;
            if (oncekisatis == 0) { oncekisatis = 1; }
            var yuzde= (int?)((100*fark)/oncekisatis);
            List<TblUrun> urunliste = new List<TblUrun>();
            var tümmadde = _c.TblUrunbilgis.Where(x => _c.TblHammaddes.Select(y => y.Id).Contains(x.HammaddeId ?? 0)).ToList();
            // urunbilgide ki miktarı bölü adeti < miktardan ile hammadde yeterli değil .
            foreach(var item in tümmadde)
            {
                var madde = _c.TblHammaddes.FirstOrDefault(x=> x.Id == item.HammaddeId);
                var uretilensayi = madde.Adet / item.Miktar;
                if (uretilensayi==0)
                {
                   var urun= _c.TblUruns.FirstOrDefault(x=> x.Id== item.UrunId) ?? new();
                    urunliste.Add(urun);
                   
                }
             
            }
         
            ViewBag.Depodaki= _c.TblUruns.ToList();
            ViewBag.SatisListe= _c.TblSatis.ToList();
            ViewBag.Uruns = urunliste;
           ViewBag.ToplamSatis = ToplamSatis;
            ViewBag.Fark= yuzde;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}
