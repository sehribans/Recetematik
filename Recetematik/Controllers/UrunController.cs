﻿
using Microsoft.AspNetCore.Mvc;
using Recetematik.Models;

namespace Recetematik.Controllers
{
    public class UrunController : Controller
    {
        private readonly RmContext _c;

        public UrunController(RmContext c)
        {
            _c = c;
        }

        public IActionResult Index()
        {
            var urunler = _c.TblUruns.ToList();
            ViewBag.Sayi = TempData["Sayi"];  

            return View(urunler);
        }
        public IActionResult UrunForm(int id)
        {

            var urun = new TblUrun();
            if(id> 0)
            {
                urun = _c.TblUruns.Find(id);

            }
                
           
            return View(urun);
        }
        [HttpPost]
        public IActionResult UrunForm(TblUrun model) {
            if (model.Id > 0)
            {
                _c.TblUruns.Update(model);
            }
            else
            {
                _c.TblUruns.Add(model);
            }

            _c.SaveChanges();
            return RedirectToAction("Index"); }

        public IActionResult Hammadde()
        {
            ViewBag.Birim = _c.TblBirims.ToList();
            return View(_c.TblHammaddes.ToList());
        }
        public IActionResult HammaddeForm(int id)
        {
            ViewBag.Birim = _c.TblBirims.ToList();
            var hammadde = new TblHammadde();
            if (id > 0)
            {
                hammadde = _c.TblHammaddes.Find(id);

            }
            return View(hammadde);
        }

        [HttpPost]
        public IActionResult HammaddeForm(TblHammadde model)
        {
        if(model.Id> 0)
            {
                _c.TblHammaddes.Update(model);
            }
        else
            {
                _c.TblHammaddes.Add(model);
            }
          
            _c.SaveChanges();
            return RedirectToAction("Hammadde");
        }

        public IActionResult UrunBirim()
        {

            ViewBag.Birim = _c.TblBirims.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult BirimForm(TblBirim model)
        {
            if (model.Id > 0)
            {
                _c.TblBirims.Update(model);
            }
            else
            {
                _c.TblBirims.Add(model);
            }

            _c.SaveChanges();
            return RedirectToAction("UrunBirim");
        }
        public IActionResult BirimSil(int id)
        {
            _c.TblBirims.Remove(_c.TblBirims.Find(id));
            _c.SaveChanges();

            return RedirectToAction("UrunBirim");
        }
        public IActionResult UrunSil(int id)
        {
            _c.TblUruns.Remove(_c.TblUruns.Find(id));
            _c.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult HammaddeSil(int id)
        {
            _c.TblHammaddes.Remove(_c.TblHammaddes.Find(id));
            _c.SaveChanges();

            return RedirectToAction("Hammadde");
        }
        public IActionResult UrunBilgiSil(int id)
        {
            _c.TblUrunbilgis.Remove(_c.TblUrunbilgis.Find(id));
            _c.SaveChanges();
           
            return RedirectToAction("UrunBilgi");
        }
        [HttpPost]
        public IActionResult UrunBilgiForm(TblUrunbilgi model)
        {
            if (model.Id > 0)
            {
                _c.TblUrunbilgis.Update(model);
            }
            else
            {
                _c.TblUrunbilgis.Add(model);
            }

            _c.SaveChanges();
            var hammadde= _c.TblHammaddes.FirstOrDefault(m=> m.Id == model.HammaddeId);
            hammadde.Adet = hammadde.Adet - model.Miktar;
            return RedirectToAction("UrunBilgi");
        }





        public IActionResult UrunBilgi(int id)
        {
            ViewBag.UrunList = _c.TblUruns.ToList();   
            var hammadde = _c.TblHammaddes.ToList();
            var urun = _c.TblUrunbilgis.Where(m => m.UrunId == id).ToList();
            var madde = hammadde.Where(m => urun.Select(x => x.HammaddeId).Contains(m.Id));
            var urunmadde = urun.Where(m => hammadde.Select(x => x.Id).Contains(m.HammaddeId ?? 0));

            var n = madde.FirstOrDefault(m => urun.Select(x => x.HammaddeId).Contains(m.Id));
            var urunbilgi= urunmadde.FirstOrDefault(x=> x.HammaddeId == n.Id);
            var UretilebilirAdet = n.Adet / urunbilgi.Miktar;
           
            foreach (var x in urunmadde)
            {
                var urunsayi = 0;
                var y = madde.FirstOrDefault(m => m.Id == x.HammaddeId);
                var temp = y.Adet / x.Miktar;

                if (temp < UretilebilirAdet)
                {
                    UretilebilirAdet = temp;
                }
            
            }
            ViewBag.adet = UretilebilirAdet;
              TempData["Sayi"] = UretilebilirAdet;

            ViewBag.Hammadde = hammadde;
            ViewBag.Urun = urun;
            ViewBag.Birim = _c.TblBirims.ToList();


            return View();
        }

    }
}