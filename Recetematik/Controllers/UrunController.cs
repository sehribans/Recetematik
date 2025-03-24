
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
        #region urun
        public IActionResult Index()
        {
            var urunlerTemp = _c.TblUruns.ToList();

            var urunler = urunlerTemp.Select(x => new urunModel { 
                Id = x.Id,
                Fiyat = x.Fiyat,
                Urunadi = x.Urunadi,
                Adet = 0,
            }).ToList();

           foreach(var item in urunler)
            {
                var hammadeler = _c.TblUrunbilgis.Where(m=>m.UrunId == item.Id).ToList();
                int urunAdedi = 0;
                var referansUrun = _c.TblUrunbilgis.FirstOrDefault(m=>m.UrunId==item.Id) ?? new();
                var referansHammade = _c.TblHammaddes.FirstOrDefault(x => x.Id == referansUrun.HammaddeId) ?? new();
                urunAdedi = (referansHammade.Adet / referansUrun.Miktar) ?? 0;

                foreach(var it in hammadeler)
                {
                    var hammade = _c.TblHammaddes.FirstOrDefault(x => x.Id == it.Id) ?? new();

                    var tempAdet = hammade.Adet / it.Miktar;

                    if(tempAdet < urunAdedi)
                    {
                        urunAdedi = tempAdet ?? 0;
                    }
                }    

           item.Adet = urunAdedi;

            } 
           

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
        #endregion
        #region hammadde
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
#endregion
        #region birim
        public JsonResult BirimListele()
        {
            var birim = _c.TblBirims.ToList();
            return Json(birim);
        }
        public IActionResult UrunBirim()
        {
            return View();
        }
        [HttpPost]
        public void BirimForm(TblBirim model)
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

        }
        public void BirimSil(int id)
        {
            _c.TblBirims.Remove(_c.TblBirims.Find(id));
            _c.SaveChanges();

        }
        #endregion

        #region sil
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
            var urunbilgi = _c.TblUrunbilgis.Find(id);
          var urunId=  _c.TblUruns.FirstOrDefault(x => x.Id == urunbilgi.UrunId)?.Id;
            _c.TblUrunbilgis.Remove(urunbilgi);
            _c.SaveChanges();
           
            return Redirect("/Urun/UrunBilgi/"+urunId);
        }
        #endregion
        #region UrunBilgi
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
            return RedirectToAction("UrunBilgi", new { id = model.UrunId});
        }

        public IActionResult UrunBilgi(int id)
        {
            ViewBag.UrunList = _c.TblUruns.ToList();   
            var hammadde = _c.TblHammaddes.ToList();
            var urun = _c.TblUrunbilgis.Where(m => m.UrunId == id).ToList();
            ViewBag.Hammadde = hammadde;
            ViewBag.Urun = urun;
            ViewBag.Birim = _c.TblBirims.ToList();
            ViewBag.Id= id; 

            return View();
        }
        #endregion
    }
}